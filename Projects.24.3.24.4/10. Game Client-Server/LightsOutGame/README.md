# LightsOutGame (клиент-сервер)

Игра «Lights Out» («Огни»):
два решения, клиент на WPF (MVVM) и сервер на ASP.NET Web API, общий слой `Common`,
бизнес-логика, доступ к данным через Entity Framework 6 + PostgreSQL, DI через Ninject.

## Правила игры

Поле 5×5 из лампочек (горит / не горит). Нажатие на ячейку **переключает её саму и четырёх
ортогональных соседей** (вверх/вниз/влево/вправо). Цель — погасить все лампочки.
Счётчик ходов = количество нажатий. Новое поле собирается из решённого состояния случайными
нажатиями, поэтому **всегда решаемо**.

## Состав решений

**LightsOutGame.Client.sln** (клиент):
- `LightsOutGame.Wpf` — WPF-клиент (MVVM, окно входа + игровое поле)
- `LightsOutGame.Common` — общий слой (модели, DTO, контракты, Ninject)
- `LightsOutGame.BusinessProxy` — HTTP-прокси к серверу (реализует те же контракты сервисов)
- `LightsOutGame.ClientUnitTests` — тесты клиента

**LightsOutGame.Server.sln** (сервер):
- `LightsOutGame.WebApi` — ASP.NET Web API (контроллеры, Ninject-модуль, Swagger)
- `LightsOutGame.Common` — тот же общий слой
- `LightsOutGame.Business` — бизнес-логика (вся механика Lights Out здесь)
- `LightsOutGame.DataAccess.EF` — доступ к данным (EF6 + Npgsql)
- `LightsOutGame.UnitTests` — тесты сервера

## Технологии

.NET Framework 4.7.2, WPF, ASP.NET Web API 5.2.9, Entity Framework 6.5.2,
EntityFramework6.Npgsql 6.4.3 + Npgsql 6.0.13 (PostgreSQL), Ninject 3.3.6,
System.Text.Json, Swashbuckle. **Требуется Windows + Visual Studio** (.NET Framework).

## Как запустить

1. **Восстановить NuGet-пакеты.** Открыть каждое решение в Visual Studio и выполнить
   восстановление пакетов (или `nuget restore` / сборка восстановит автоматически).
   В контейнере под Linux проекты собрать нельзя — это .NET Framework, только Windows.

2. **Настроить базу PostgreSQL.** Строки подключения лежат в `LightsOutGame.WebApi/Web.config`:
   - `Main` — обычное подключение,
   - `EFMain` — то же с `providerName="Npgsql"` (его использует `DbContext`, см.
     `base("name=EFMain")`).
   Поправьте `Server/Port/Database/User Id/Password` под свою БД. EF создаст схему при первом
   обращении (Code First). Те же значения продублированы в
   `LightsOutGame.DataAccess.EF/App.config` и `LightsOutGame.UnitTests/app.config`.

3. **Запустить сервер.** Сделать `LightsOutGame.WebApi` стартовым в `Server.sln` и запустить.
   Запомнить адрес/порт из браузера (например `https://localhost:44387/`). Swagger доступен
   по `/swagger`.

4. **Указать клиенту адрес сервера.** В `LightsOutGame.Wpf/App.config`:
   ```xml
   <add key="ServerConnection" value="https://localhost:44387/" />
   ```
   Значение должно **совпадать с реальным портом запущенного WebApi**.

5. **Запустить клиент.** Открыть `Client.sln`, стартовый проект `LightsOutGame.Wpf`, запустить.
   Ввести имя игрока → откроется поле 5×5. Сервер должен быть уже запущен.

## Важное замечание про слой данных

`LightsOutGame.DataAccess.EF` и `LightsOutGame.Business`/`LightsOutGame.Common` — это слой
лабораторной №9, на который шаблон ссылается извне. В архиве преподавателя их не было, поэтому
они **воссозданы здесь по контрактам** (по интерфейсам репозиториев и тестам). Слой EF —
рабочий и эталонный, но если у вас **уже есть своя лаба 9** с этими проектами — можно подставить
свои `Common/Business/DataAccess.EF`, добавив в них только новую механику Lights Out
(см. `MAPPING.md`), а транспортные DTO и контракты сервисов держать как здесь.

Поле в БД хранится строкой из 25 чисел через запятую (`GameEntity.CellsData`), упаковка/распаковка
в `int[,]` — в `GameRepositoryEF`.
