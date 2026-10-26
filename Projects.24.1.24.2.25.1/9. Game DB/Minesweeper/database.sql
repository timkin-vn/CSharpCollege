-- Выполните этот скрипт в Query Tool базы minesweeper_db в pgAdmin 4.
CREATE TABLE IF NOT EXISTS game_results (
    id BIGSERIAL PRIMARY KEY,
    is_won BOOLEAN NOT NULL,
    moves INTEGER NOT NULL CHECK (moves >= 0),
    finished_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP
);

-- Проверка: SELECT * FROM game_results ORDER BY finished_at DESC;
