-- Create database
CREATE DATABASE Game2048DB;

-- Connect to the database
\c Game2048DB;

-- Create Users table
CREATE TABLE "Users" (
    "Id" SERIAL PRIMARY KEY,
    "Username" VARCHAR(50) NOT NULL UNIQUE,
    "CreatedAt" TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

-- Create Games table
CREATE TABLE "Games" (
    "Id" SERIAL PRIMARY KEY,
    "UserId" INTEGER NOT NULL REFERENCES "Users"("Id"),
    "Score" INTEGER NOT NULL DEFAULT 0,
    "IsGameOver" BOOLEAN NOT NULL DEFAULT FALSE,
    "IsWon" BOOLEAN NOT NULL DEFAULT FALSE,
    "BoardState" TEXT NOT NULL,
    "CreatedAt" TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    "UpdatedAt" TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

-- Create index for better performance
CREATE INDEX idx_games_user_id ON "Games"("UserId");
CREATE INDEX idx_games_created_at ON "Games"("CreatedAt");

-- Insert default user
INSERT INTO "Users" ("Username") VALUES ('Player');
