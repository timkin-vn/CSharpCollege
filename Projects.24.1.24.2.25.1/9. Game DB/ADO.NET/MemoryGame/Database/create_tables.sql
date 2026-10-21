-- Выполнять в БД "MemoryGame.1.Dev.24.1.24.2.25.1"
-- (создание БД: create database "MemoryGame.1.Dev.24.1.24.2.25.1";)

create table if not exists "Users" (
    "Id" serial primary key,
    "Name" text not null
);

create table if not exists "Games" (
    "Id" serial primary key,
    "UserId" integer not null references "Users" ("Id") on delete cascade,
    "MoveCount" integer not null
);

create table if not exists "Cells" (
    "Id" serial primary key,
    "GameId" integer not null references "Games" ("Id") on delete cascade,
    "Row" integer not null,
    "Column" integer not null,
    "Value" integer not null
);
