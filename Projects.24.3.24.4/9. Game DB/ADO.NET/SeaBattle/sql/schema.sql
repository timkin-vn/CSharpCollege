CREATE TABLE IF NOT EXISTS public."Users" (
    "Id"   serial PRIMARY KEY,
    "Name" varchar(200) NOT NULL
);

CREATE TABLE IF NOT EXISTS public."Games" (
    "Id"        serial PRIMARY KEY,
    "UserId"    integer NOT NULL,
    "MoveCount" integer NOT NULL DEFAULT 0,
    "Won"       boolean NOT NULL DEFAULT false,
    CONSTRAINT "FK_Games_Users" FOREIGN KEY ("UserId")
        REFERENCES public."Users" ("Id") ON DELETE CASCADE
);

CREATE INDEX IF NOT EXISTS "IX_Games_UserId" ON public."Games" ("UserId");
