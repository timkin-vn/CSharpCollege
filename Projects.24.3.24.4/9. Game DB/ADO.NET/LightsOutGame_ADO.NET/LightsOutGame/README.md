# LightsOutGame — вариант ADO.NET (полностью собираемый)

Структура и стиль повторяют шаблон преподавателя (FifteenGame): слоистая архитектура,
паттерны Repository + DTO + Service, DI через Ninject, UI на WPF/MVVM.
Решение открывается файлом **LightsOutGame.sln** (Visual Studio, .NET Framework 4.7.2).

## Проекты
- LightsOutGame.Common      — контракты, модели, DTO, константы, NinjectKernel
- LightsOutGame.Business    — сервисы (вся игровая логика в GameService)
- LightsOutGame.DataAccess  — репозитории на Npgsql / ADO.NET (GameRepository, UserRepository)
- LightsOutGame.Wpf         — MVVM (ViewModels, Views, Infrastructure, App), стартовый проект

## Запуск
1. Создать БД PostgreSQL.
2. Выполнить Database/schema.sql (таблицы Users / Games / Cells).
3. Проверить строку подключения "Main" в LightsOutGame.Wpf/App.config.
4. Открыть LightsOutGame.sln, восстановить NuGet-пакеты (Restore), F5.
   Пакеты: Ninject 3.3.6, Npgsql 8.0.9 (+ транзитивные) — как в шаблоне.

## Игровая механика (сохранена 1-в-1 из исходного проекта)
- поле 5x5, клик по клетке инвертирует её и 4 соседей (крест);
- победа — когда все лампы выключены;
- перемешивание — 8..15 случайных нажатий + страховка от уже решённого поля.

## Заглушки (по требованию)
- UnitTests-проект из шаблона не воспроизводился.
