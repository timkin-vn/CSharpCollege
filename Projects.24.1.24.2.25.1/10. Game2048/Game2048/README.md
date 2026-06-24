# Game 2048 — Клиент-Серверная Реализация

Проект реализует игру **2048** по той же архитектуре, что и FifteenGame:
**клиент** общается с **сервером** через REST API, сервер хранит состояние в SQLite.

---

## Структура решения

```
Game2048/
├── Game2048.Common/          ← Общие интерфейсы, модели, DTO, константы
│   ├── BusinessModels/       ← GameModel, UserModel
│   ├── BusinessDtos/         ← Request/Reply объекты для API
│   ├── Contracts/            ← IGameService, IUserService, IGameRepository, IUserRepository
│   └── Definitions/          ← Constants (GridSize=4, WinValue=2048), MoveDirection enum
│
├── Game2048.Business/        ← Бизнес-логика (аналог FifteenGame.Business)
│   └── Services/             ← GameService (алгоритм 2048), UserService
│
├── Game2048.DataAccess/      ← Entity Framework + SQLite (аналог FifteenGame.DataAccess.EF)
│   ├── Game2048DbContext.cs  ← DbContext
│   ├── Models/               ← UserEntity, GameEntity
│   └── Repositories/         ← UserRepositoryEF, GameRepositoryEF
│
├── Game2048.WebApi/          ← ASP.NET Core Web API (аналог FifteenGame.WebApi)
│   ├── Controllers/          ← GamesController, UsersController
│   ├── Program.cs            ← DI-регистрация (вместо NinjectModule)
│   └── appsettings.json      ← Строка подключения к SQLite
│
├── Game2048.BusinessProxy/   ← HTTP-клиент к серверу (аналог FifteenGame.BusinessProxy)
│   ├── Infrastructure/       ← HttpConnection (читает SERVER_URL из env)
│   └── Services/             ← GameServiceProxy, UserServiceProxy
│
├── Game2048.Console/         ← Консольный клиент (аналог FifteenGame.Wpf)
│   └── Program.cs            ← UI в консоли: отрисовка поля, ввод хода
│
├── Game2048.UnitTests/       ← MSTest + Moq тесты (аналог FifteenGame.UnitTest)
│   ├── ServiceTests/         ← GameServiceTests, UserServiceTests
│   └── RepositoryTests/      ← GameRepositoryEFTests (SQLite in-memory)
│
├── Game2048.Server.sln       ← Серверное решение (WebApi + Business + DataAccess + Common + Tests)
├── Game2048.Client.sln       ← Клиентское решение (Console + BusinessProxy + Common)
├── run.ps1                   ← Скрипт запуска для Windows (PowerShell)
└── run.sh                    ← Скрипт запуска для Linux/macOS (bash)
```

---

## Требования

- **.NET 8 SDK** — https://dotnet.microsoft.com/download/dotnet/8.0
- **Windows / Linux / macOS**

Проверьте версию:
```
dotnet --version   # должно быть 8.x.x
```

---

## Быстрый старт

### Windows (PowerShell)

```powershell
# Перейдите в папку проекта
cd Game2048

# Запустить сервер + клиент одной командой:
.\run.ps1

# Только запустить unit-тесты:
.\run.ps1 -Tests

# Только сервер (без клиента):
.\run.ps1 -ServerOnly

# Только клиент (если сервер уже запущен в другом окне):
.\run.ps1 -ClientOnly
```

### Linux / macOS (bash)

```bash
cd Game2048
chmod +x run.sh   # только первый раз

# Запустить сервер + клиент:
./run.sh

# Только тесты:
./run.sh --tests

# Только сервер:
./run.sh --server

# Только клиент:
./run.sh --client
```

---

## Запуск вручную (без скрипта)

### Шаг 1 — Запустить сервер

```bash
# Терминал 1
cd Game2048/Game2048.WebApi
dotnet run
```

Сервер запустится на `http://localhost:5000`.  
Swagger UI будет доступен по адресу `http://localhost:5000/swagger`.

> Первый запуск автоматически создаёт файл `game2048.db` (SQLite).

### Шаг 2 — Запустить клиент

```bash
# Терминал 2
cd Game2048/Game2048.Console
dotnet run
```

### Шаг 3 — Запустить тесты

```bash
cd Game2048
dotnet test Game2048.UnitTests/Game2048.UnitTests.csproj
```

---

## Управление в игре

| Клавиша       | Действие  |
|---------------|-----------|
| W / ↑         | Ход вверх |
| S / ↓         | Ход вниз  |
| A / ←         | Ход влево |
| D / →         | Ход вправо|
| Q             | Выход     |

---

## REST API (эндпоинты)

| Метод  | URL                              | Описание                              |
|--------|----------------------------------|---------------------------------------|
| GET    | /api/users/get-all               | Список всех пользователей             |
| POST   | /api/users/get-or-create         | Найти или создать пользователя        |
| GET    | /api/users/get-by-name/{name}    | Найти пользователя по имени           |
| GET    | /api/games/get-by-user-id/{id}   | Получить/создать игру пользователя    |
| GET    | /api/games/get-by-game-id/{id}   | Получить игру по ID                   |
| POST   | /api/games/make-move             | Сделать ход (`{ gameId, direction }`) |
| GET    | /api/games/is-over/{id}          | Проверить, окончена ли игра           |
| GET    | /api/games/is-won/{id}           | Проверить, выиграна ли игра           |
| DELETE | /api/games/remove/{id}           | Удалить игру                          |

---

## Как работает алгоритм 2048

1. **Сдвиг** — все ненулевые плитки сдвигаются в сторону хода (нули убираются).
2. **Слияние** — соседние одинаковые плитки объединяются (2+2=4, 4+4=8 и т.д.).  
   Одна плитка может слиться не более одного раза за ход.
3. **Новая плитка** — после хода в случайную пустую клетку добавляется 2 (90%) или 4 (10%).
4. **Победа** — когда на поле появляется плитка 2048.
5. **Поражение** — когда нет ни одного возможного хода (поле заполнено, нет соседних равных).
