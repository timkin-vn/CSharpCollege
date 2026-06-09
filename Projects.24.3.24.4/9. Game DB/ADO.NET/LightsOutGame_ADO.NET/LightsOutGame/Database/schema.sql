-- PostgreSQL схема для ADO.NET-варианта (имена в кавычках = регистрозависимые, как в репозитории)
create table "Users" (
    "Id"   serial primary key,
    "Name" text not null
);

create table "Games" (
    "Id"        serial primary key,
    "UserId"    integer not null references "Users" ("Id"),
    "MoveCount" integer not null default 0
);

create table "Cells" (
    "Id"     serial primary key,
    "GameId" integer not null references "Games" ("Id"),
    "Row"    integer not null,
    "Column" integer not null,
    "IsOn"   boolean not null
);
