-- Этот SQL-скрипт добавляет значения по умолчанию для числовых полей в таблице Users
-- и убирает ограничение NOT NULL, если оно есть

-- 1. Установка значений по умолчанию для полей
ALTER TABLE "Users" 
  ALTER COLUMN "GamesPlayed" SET DEFAULT 0,
  ALTER COLUMN "Wins" SET DEFAULT 0,
  ALTER COLUMN "Losses" SET DEFAULT 0,
  ALTER COLUMN "Draws" SET DEFAULT 0;

-- 2. Убираем ограничение NOT NULL для полей Password и числовых полей
ALTER TABLE "Users" 
  ALTER COLUMN "Password" DROP NOT NULL,
  ALTER COLUMN "GamesPlayed" DROP NOT NULL,
  ALTER COLUMN "Wins" DROP NOT NULL,
  ALTER COLUMN "Losses" DROP NOT NULL,
  ALTER COLUMN "Draws" DROP NOT NULL;

-- 3. Обновляем существующие NULL значения на 0 (каждый столбец отдельно)
UPDATE "Users" SET "GamesPlayed" = 0 WHERE "GamesPlayed" IS NULL;
UPDATE "Users" SET "Wins" = 0 WHERE "Wins" IS NULL;
UPDATE "Users" SET "Losses" = 0 WHERE "Losses" IS NULL;
UPDATE "Users" SET "Draws" = 0 WHERE "Draws" IS NULL;

-- 4. Проверка структуры таблицы после изменений
-- SELECT * FROM information_schema.columns WHERE table_name = 'Users'; 