-- Схема БД для Game2048 (PostgreSQL).
-- Имена в кавычках с CamelCase — чтобы совпадали с тем, что генерирует EF Code First.
-- Выполнить в созданной заранее базе (например game2048) под нужным пользователем.

CREATE TABLE IF NOT EXISTS public."Users" (
    "Id"   serial PRIMARY KEY,
    "Name" varchar(200) NOT NULL
);

CREATE TABLE IF NOT EXISTS public."Games" (
    "Id"        serial PRIMARY KEY,
    "UserId"    integer NOT NULL,
    "Score"     integer NOT NULL DEFAULT 0,
    "MoveCount" integer NOT NULL DEFAULT 0
);

CREATE TABLE IF NOT EXISTS public."Cells" (
    "Id"     serial PRIMARY KEY,
    "GameId" integer NOT NULL,
    "Row"    integer NOT NULL,
    "Column" integer NOT NULL,
    "Value"  integer NOT NULL DEFAULT 0,
    CONSTRAINT "FK_Cells_Games" FOREIGN KEY ("GameId")
        REFERENCES public."Games" ("Id") ON DELETE CASCADE
);

CREATE INDEX IF NOT EXISTS "IX_Games_UserId" ON public."Games" ("UserId");
CREATE INDEX IF NOT EXISTS "IX_Cells_GameId" ON public."Cells" ("GameId");
