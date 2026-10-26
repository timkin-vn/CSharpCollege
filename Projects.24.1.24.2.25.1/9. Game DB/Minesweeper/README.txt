Сапёр + PostgreSQL (адаптация под структуру исходного Minesweeper)

1. Откройте pgAdmin 4 и подключитесь к серверу PostgreSQL.
2. Нажмите правой кнопкой Databases -> Create -> Database... и создайте minesweeper_db.
3. Выберите minesweeper_db -> Tools -> Query Tool. Откройте database.sql, выполните скрипт (F5).
4. В Minesweeper.Wpf/App.config поменяйте Username и Password на свои данные PostgreSQL. По умолчанию Host=localhost, Port=5432, Database=minesweeper_db.
5. Откройте Minesweeper.sln в Visual Studio. Восстановите NuGet-пакеты: правой кнопкой по решению -> Restore NuGet Packages (или Tools -> NuGet Package Manager -> Package Manager Console: nuget restore). Если пакет Npgsql не восстановился, установите Npgsql версии 4.1.14 в проект Minesweeper.Wpf через NuGet.
6. Пересоберите решение и запустите приложение.

Что сохраняется:
После завершения каждой партии в game_results записываются результат (is_won), число ходов (moves) и время окончания (finished_at). Если PostgreSQL недоступен, игра продолжит работу, а приложение покажет предупреждение.

Проверка сохранения:
В pgAdmin 4 откройте Query Tool для minesweeper_db и выполните:
SELECT * FROM game_results ORDER BY finished_at DESC;

Важно: pgAdmin 4 — это клиент для управления PostgreSQL, а не сама база данных. Должен быть установлен и запущен сервер PostgreSQL.
