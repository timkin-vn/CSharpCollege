#!/usr/bin/env bash
# ============================================================
#  Скрипт запуска игры 2048 (Server + Client)
#  Использование:
#    ./run.sh            — запустить сервер + клиент
#    ./run.sh --server   — только сервер
#    ./run.sh --client   — только клиент (сервер уже запущен)
#    ./run.sh --tests    — запустить unit-тесты
# ============================================================

set -e

ROOT="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
SERVER_URL="${SERVER_URL:-http://localhost:5000}"
SERVER_PID=""

SERVER_ONLY=false
CLIENT_ONLY=false
RUN_TESTS=false

for arg in "$@"; do
  case $arg in
    --server) SERVER_ONLY=true ;;
    --client) CLIENT_ONLY=true ;;
    --tests)  RUN_TESTS=true ;;
  esac
done

# ── Цвета ──────────────────────────────────────────────────
RED='\033[0;31m'; GREEN='\033[0;32m'; CYAN='\033[0;36m'
YELLOW='\033[1;33m'; GRAY='\033[0;37m'; RESET='\033[0m'

header() { echo -e "\n${CYAN}══════════════════════════════════════════\n  $1\n══════════════════════════════════════════${RESET}"; }
info()   { echo -e "${GREEN}[INFO]${RESET} $1"; }
warn()   { echo -e "${YELLOW}[WARN]${RESET} $1"; }
error()  { echo -e "${RED}[ОШИБКА]${RESET} $1"; }
ok()     { echo -e "${GREEN}[OK]${RESET} $1"; }

# ── Проверка .NET SDK ─────────────────────────────────────
if ! command -v dotnet &>/dev/null; then
    error ".NET SDK не найден. Установите .NET 8 SDK:"
    echo "  https://dotnet.microsoft.com/download/dotnet/8.0"
    exit 1
fi

DOTNET_VER=$(dotnet --version 2>&1)
info ".NET версия: $DOTNET_VER"

# ── Тесты ─────────────────────────────────────────────────
if $RUN_TESTS; then
    header "Запуск Unit-тестов"
    dotnet test "$ROOT/Game2048.UnitTests/Game2048.UnitTests.csproj" \
        --logger "console;verbosity=normal"
    exit $?
fi

# ── Сборка ────────────────────────────────────────────────
if ! $CLIENT_ONLY; then
    header "Сборка серверного решения"
    dotnet build "$ROOT/Game2048.Server.sln" -c Debug
fi

if ! $SERVER_ONLY; then
    header "Сборка клиентского решения"
    dotnet build "$ROOT/Game2048.Client.sln" -c Debug
fi

# ── Cleanup при выходе ────────────────────────────────────
cleanup() {
    if [ -n "$SERVER_PID" ] && kill -0 "$SERVER_PID" 2>/dev/null; then
        warn "Останавливаем сервер (PID=$SERVER_PID)..."
        kill "$SERVER_PID" 2>/dev/null || true
        wait "$SERVER_PID" 2>/dev/null || true
        ok "Сервер остановлен."
    fi
}
trap cleanup EXIT INT TERM

# ── Запуск сервера ────────────────────────────────────────
if ! $CLIENT_ONLY; then
    header "Запуск сервера (Game2048.WebApi)"
    info "Сервер: $SERVER_URL"
    info "Swagger: $SERVER_URL/swagger"

    dotnet run \
        --project "$ROOT/Game2048.WebApi/Game2048.WebApi.csproj" \
        --urls "$SERVER_URL" \
        --no-build &
    SERVER_PID=$!

    # Ждём готовности сервера
    MAX_WAIT=30
    waited=0
    while [ $waited -lt $MAX_WAIT ]; do
        sleep 1
        waited=$((waited + 1))
        if curl -sf "$SERVER_URL/api/users/get-all" >/dev/null 2>&1; then
            ok "Сервер запущен через ${waited} сек. (PID=$SERVER_PID)"
            break
        fi
        echo -e "${GRAY}  Ожидание... (${waited}/${MAX_WAIT} сек)${RESET}"
    done

    if ! curl -sf "$SERVER_URL/api/users/get-all" >/dev/null 2>&1; then
        error "Сервер не ответил за $MAX_WAIT секунд."
        exit 1
    fi
fi

if $SERVER_ONLY; then
    info "Сервер запущен. Нажмите Ctrl+C для остановки."
    wait "$SERVER_PID"
    exit 0
fi

# ── Запуск клиента ────────────────────────────────────────
header "Запуск клиента (Game2048.Console)"
export SERVER_URL="$SERVER_URL/"

dotnet run --project "$ROOT/Game2048.Console/Game2048.Console.csproj" --no-build
