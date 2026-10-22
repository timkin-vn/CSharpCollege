-- Пользователи
CREATE TABLE "Users" (
    "Id" SERIAL PRIMARY KEY,
    "Name" TEXT NOT NULL UNIQUE
);

-- Партии пятнашек
CREATE TABLE "Games" (
    "Id" SERIAL PRIMARY KEY,
    "UserId" INTEGER NOT NULL REFERENCES "Users"("Id"),
    "MoveCount" INTEGER NOT NULL DEFAULT 0
);

-- Ячейки пятнашек
CREATE TABLE "Cells" (
    "Id" SERIAL PRIMARY KEY,
    "GameId" INTEGER NOT NULL REFERENCES "Games"("Id"),
    "Row" SMALLINT NOT NULL,
    "Column" SMALLINT NOT NULL,
    "Value" INTEGER NOT NULL
);

-- Индексы для старой игры
CREATE INDEX IX_Games_UserId ON "Games"("UserId");
CREATE INDEX IX_Cells_GameId ON "Cells"("GameId");

-- Партии
CREATE TABLE "CheckersGames" (
    "Id" SERIAL PRIMARY KEY,
    "UserId" INTEGER NOT NULL REFERENCES "Users"("Id"),
    "StartDate" TIMESTAMPTZ NOT NULL DEFAULT NOW(),
    "CurrentPlayer" SMALLINT NOT NULL DEFAULT 0,   -- 0 = белые, 1 = чёрные
    "IsFinished" BOOLEAN NOT NULL DEFAULT FALSE,
    "Winner" SMALLINT NULL,                        -- 0, 1 или NULL
    "GameStateJson" TEXT NULL
);

-- Ходы
CREATE TABLE "CheckersMoves" (
    "Id" SERIAL PRIMARY KEY,
    "GameId" INTEGER NOT NULL REFERENCES "CheckersGames"("Id"),
    "MoveNumber" INTEGER NOT NULL,
    "FromRow" SMALLINT NOT NULL,
    "FromCol" SMALLINT NOT NULL,
    "ToRow" SMALLINT NOT NULL,
    "ToCol" SMALLINT NOT NULL,
    "IsCapture" BOOLEAN NOT NULL DEFAULT FALSE,
    "CapturedRow" SMALLINT NULL,
    "CapturedCol" SMALLINT NULL,
    "PromotedToKing" BOOLEAN NOT NULL DEFAULT FALSE,
    "MoveTime" TIMESTAMPTZ NOT NULL DEFAULT NOW()
);

-- Индексы для новой игры
CREATE INDEX IX_CheckersGames_UserId ON "CheckersGames"("UserId");
CREATE INDEX IX_CheckersMoves_GameId ON "CheckersMoves"("GameId");