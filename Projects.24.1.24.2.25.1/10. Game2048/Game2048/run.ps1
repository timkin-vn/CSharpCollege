#!/usr/bin/env pwsh
<#
.SYNOPSIS
    Скрипт запуска игры 2048 (Server + Client)
.DESCRIPTION
    Запускает сервер Game2048.WebApi в фоне, затем запускает консольный клиент.
    При выходе из клиента сервер останавливается автоматически.
#>

param(
    [switch]$ServerOnly,   # Запустить только сервер
    [switch]$ClientOnly,   # Запустить только клиент (сервер уже запущен)
    [switch]$Tests,        # Запустить unit-тесты
    [string]$ServerUrl = "http://localhost:5000"
)

$ROOT = $PSScriptRoot
if (-not $ROOT) { $ROOT = Get-Location }

function Write-Header($text) {
    Write-Host ""
    Write-Host "══════════════════════════════════════════" -ForegroundColor Cyan
    Write-Host "  $text" -ForegroundColor Cyan
    Write-Host "══════════════════════════════════════════" -ForegroundColor Cyan
}

# ── Проверка .NET SDK ──────────────────────────────────────────────────────
if (-not (Get-Command dotnet -ErrorAction SilentlyContinue)) {
    Write-Host "[ОШИБКА] .NET SDK не найден. Установите .NET 8 SDK:" -ForegroundColor Red
    Write-Host "  https://dotnet.microsoft.com/download/dotnet/8.0" -ForegroundColor Yellow
    exit 1
}

$dotnetVersion = (dotnet --version 2>&1)
Write-Host "[INFO] .NET версия: $dotnetVersion" -ForegroundColor Green

# ── Запуск тестов ─────────────────────────────────────────────────────────
if ($Tests) {
    Write-Header "Запуск Unit-тестов"
    dotnet test "$ROOT\Game2048.UnitTests\Game2048.UnitTests.csproj" --logger "console;verbosity=normal"
    exit $LASTEXITCODE
}

# ── Сборка ────────────────────────────────────────────────────────────────
if (-not $ClientOnly) {
    Write-Header "Сборка серверного решения"
    dotnet build "$ROOT\Game2048.Server.sln" -c Debug
    if ($LASTEXITCODE -ne 0) {
        Write-Host "[ОШИБКА] Сборка сервера не удалась!" -ForegroundColor Red
        exit 1
    }
}

if (-not $ServerOnly) {
    Write-Header "Сборка клиентского решения"
    dotnet build "$ROOT\Game2048.Client.sln" -c Debug
    if ($LASTEXITCODE -ne 0) {
        Write-Host "[ОШИБКА] Сборка клиента не удалась!" -ForegroundColor Red
        exit 1
    }
}

# ── Запуск сервера ────────────────────────────────────────────────────────
$serverProcess = $null

if (-not $ClientOnly) {
    Write-Header "Запуск сервера (Game2048.WebApi)"
    Write-Host "[INFO] Сервер будет доступен по адресу: $ServerUrl" -ForegroundColor Green
    Write-Host "[INFO] Swagger UI:                       $ServerUrl/swagger" -ForegroundColor Green

    $serverProcess = Start-Process -PassThru -NoNewWindow `
        -FilePath "dotnet" `
        -ArgumentList "run", "--project", "$ROOT\Game2048.WebApi\Game2048.WebApi.csproj", "--urls", $ServerUrl `
        -WorkingDirectory "$ROOT\Game2048.WebApi"

    Write-Host "[INFO] Ожидаем запуска сервера (PID=$($serverProcess.Id))..." -ForegroundColor Yellow

    # Ждём, пока сервер не начнёт отвечать (до 30 сек)
    $maxWait = 30
    $waited = 0
    $serverReady = $false
    while ($waited -lt $maxWait) {
        Start-Sleep -Seconds 1
        $waited++
        try {
            $response = Invoke-WebRequest -Uri "$ServerUrl/api/users/get-all" -TimeoutSec 2 -ErrorAction Stop
            $serverReady = $true
            Write-Host "[OK] Сервер запущен через $waited сек." -ForegroundColor Green
            break
        }
        catch {
            Write-Host "  Ожидание... ($waited/$maxWait сек)" -ForegroundColor DarkGray
        }
    }

    if (-not $serverReady) {
        Write-Host "[ОШИБКА] Сервер не ответил за $maxWait секунд." -ForegroundColor Red
        if ($serverProcess -and -not $serverProcess.HasExited) {
            $serverProcess.Kill()
        }
        exit 1
    }
}

if ($ServerOnly) {
    Write-Host ""
    Write-Host "Сервер запущен. Нажмите Ctrl+C для остановки." -ForegroundColor Green
    Wait-Process -Id $serverProcess.Id
    exit 0
}

# ── Запуск клиента ────────────────────────────────────────────────────────
Write-Header "Запуск клиента (Game2048.Console)"

$env:SERVER_URL = $ServerUrl + "/"

try {
    dotnet run --project "$ROOT\Game2048.Console\Game2048.Console.csproj"
}
finally {
    # Останавливаем сервер при выходе из клиента
    if ($serverProcess -and -not $serverProcess.HasExited) {
        Write-Host ""
        Write-Host "[INFO] Останавливаем сервер (PID=$($serverProcess.Id))..." -ForegroundColor Yellow
        $serverProcess.Kill()
        Write-Host "[OK] Сервер остановлен." -ForegroundColor Green
    }
}
