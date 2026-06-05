// Константы игры
const ROW_COUNT = 4;
const COLUMN_COUNT = 4;
const EMPTY_CELL_VALUE = 0;
const WIN_TILE_VALUE = 2048;

// Класс игры 2048
class Game2048 {
    constructor() {
        this.board = Array(ROW_COUNT).fill().map(() => Array(COLUMN_COUNT).fill(EMPTY_CELL_VALUE));
        this.score = 0;
        this.hasWon = false;

        this.gameBoard = document.getElementById('gameBoard');
        this.scoreDisplay = document.getElementById('scoreDisplay');

        this.initGameBoard();
        this.attachEventListeners();
        this.startNewGame();
    }

    initGameBoard() {
        this.gameBoard.innerHTML = '';
        this.cells = [];
        for (let i = 0; i < ROW_COUNT * COLUMN_COUNT; i++) {
            const cell = document.createElement('div');
            cell.className = 'cell';
            this.gameBoard.appendChild(cell);
            this.cells.push(cell);
        }
    }

    attachEventListeners() {
        document.getElementById('newGameBtn').addEventListener('click', () => this.startNewGame());

        document.querySelectorAll('.control-btn').forEach(btn => {
            btn.addEventListener('click', (e) => {
                const direction = btn.getAttribute('data-direction');
                this.makeMove(direction);
            });
        });
    }

    startNewGame() {
        for (let row = 0; row < ROW_COUNT; row++) {
            for (let col = 0; col < COLUMN_COUNT; col++) {
                this.board[row][col] = EMPTY_CELL_VALUE;
            }
        }
        this.score = 0;
        this.hasWon = false;

        this.addRandomTile();
        this.addRandomTile();

        this.updateUI();
    }

    makeMove(direction) {
        const before = this.cloneBoard();
        let scoreGain = 0;

        switch (direction) {
            case 'left': scoreGain = this.moveLeft(); break;
            case 'right': scoreGain = this.moveRight(); break;
            case 'up': scoreGain = this.moveUp(); break;
            case 'down': scoreGain = this.moveDown(); break;
        }

        if (this.areEqual(before, this.board)) {
            if (this.isGameOver()) {
                this.showGameOver();
            }
            return;
        }

        this.score += scoreGain;
        this.addRandomTile();

        if (!this.hasWon && this.checkWin()) {
            this.hasWon = true;
            this.showWin();
        }

        if (this.isGameOver()) {
            this.showGameOver();
        }

        this.updateUI();
    }

    moveLeft() {
        let scoreGain = 0;
        for (let row = 0; row < ROW_COUNT; row++) {
            let line = [];
            for (let col = 0; col < COLUMN_COUNT; col++) {
                if (this.board[row][col] !== EMPTY_CELL_VALUE) {
                    line.push(this.board[row][col]);
                }
            }

            for (let i = 0; i < line.length - 1; i++) {
                if (line[i] === line[i + 1]) {
                    line[i] *= 2;
                    scoreGain += line[i];
                    line.splice(i + 1, 1);
                }
            }

            while (line.length < COLUMN_COUNT) {
                line.push(EMPTY_CELL_VALUE);
            }

            for (let col = 0; col < COLUMN_COUNT; col++) {
                this.board[row][col] = line[col];
            }
        }
        return scoreGain;
    }

    moveRight() {
        let scoreGain = 0;
        for (let row = 0; row < ROW_COUNT; row++) {
            let line = [];
            for (let col = COLUMN_COUNT - 1; col >= 0; col--) {
                if (this.board[row][col] !== EMPTY_CELL_VALUE) {
                    line.push(this.board[row][col]);
                }
            }

            for (let i = 0; i < line.length - 1; i++) {
                if (line[i] === line[i + 1]) {
                    line[i] *= 2;
                    scoreGain += line[i];
                    line.splice(i + 1, 1);
                }
            }

            while (line.length < COLUMN_COUNT) {
                line.push(EMPTY_CELL_VALUE);
            }

            for (let col = 0; col < COLUMN_COUNT; col++) {
                this.board[row][col] = line[COLUMN_COUNT - 1 - col];
            }
        }
        return scoreGain;
    }

    moveUp() {
        let scoreGain = 0;
        for (let col = 0; col < COLUMN_COUNT; col++) {
            let line = [];
            for (let row = 0; row < ROW_COUNT; row++) {
                if (this.board[row][col] !== EMPTY_CELL_VALUE) {
                    line.push(this.board[row][col]);
                }
            }

            for (let i = 0; i < line.length - 1; i++) {
                if (line[i] === line[i + 1]) {
                    line[i] *= 2;
                    scoreGain += line[i];
                    line.splice(i + 1, 1);
                }
            }

            while (line.length < ROW_COUNT) {
                line.push(EMPTY_CELL_VALUE);
            }

            for (let row = 0; row < ROW_COUNT; row++) {
                this.board[row][col] = line[row];
            }
        }
        return scoreGain;
    }

    moveDown() {
        let scoreGain = 0;
        for (let col = 0; col < COLUMN_COUNT; col++) {
            let line = [];
            for (let row = ROW_COUNT - 1; row >= 0; row--) {
                if (this.board[row][col] !== EMPTY_CELL_VALUE) {
                    line.push(this.board[row][col]);
                }
            }

            for (let i = 0; i < line.length - 1; i++) {
                if (line[i] === line[i + 1]) {
                    line[i] *= 2;
                    scoreGain += line[i];
                    line.splice(i + 1, 1);
                }
            }

            while (line.length < ROW_COUNT) {
                line.push(EMPTY_CELL_VALUE);
            }

            for (let row = 0; row < ROW_COUNT; row++) {
                this.board[row][col] = line[ROW_COUNT - 1 - row];
            }
        }
        return scoreGain;
    }

    addRandomTile() {
        const emptyCells = [];
        for (let row = 0; row < ROW_COUNT; row++) {
            for (let col = 0; col < COLUMN_COUNT; col++) {
                if (this.board[row][col] === EMPTY_CELL_VALUE) {
                    emptyCells.push({ row, col });
                }
            }
        }

        if (emptyCells.length === 0) return;

        const cell = emptyCells[Math.floor(Math.random() * emptyCells.length)];
        const value = Math.random() < 0.9 ? 2 : 4;
        this.board[cell.row][cell.col] = value;

        const cellIndex = cell.row * COLUMN_COUNT + cell.col;
        const cellElement = this.cells[cellIndex];
        cellElement.style.animation = 'none';
        setTimeout(() => {
            cellElement.style.animation = 'appear 0.2s ease-out';
        }, 10);
    }

    checkWin() {
        for (let row = 0; row < ROW_COUNT; row++) {
            for (let col = 0; col < COLUMN_COUNT; col++) {
                if (this.board[row][col] >= WIN_TILE_VALUE) {
                    return true;
                }
            }
        }
        return false;
    }

    isGameOver() {
        for (let row = 0; row < ROW_COUNT; row++) {
            for (let col = 0; col < COLUMN_COUNT; col++) {
                if (this.board[row][col] === EMPTY_CELL_VALUE) {
                    return false;
                }
            }
        }

        for (let row = 0; row < ROW_COUNT; row++) {
            for (let col = 0; col < COLUMN_COUNT; col++) {
                const value = this.board[row][col];
                if (col + 1 < COLUMN_COUNT && this.board[row][col + 1] === value) return false;
                if (row + 1 < ROW_COUNT && this.board[row + 1][col] === value) return false;
            }
        }
        return true;
    }

    cloneBoard() {
        const copy = Array(ROW_COUNT).fill().map(() => Array(COLUMN_COUNT));
        for (let row = 0; row < ROW_COUNT; row++) {
            for (let col = 0; col < COLUMN_COUNT; col++) {
                copy[row][col] = this.board[row][col];
            }
        }
        return copy;
    }

    areEqual(board1, board2) {
        for (let row = 0; row < ROW_COUNT; row++) {
            for (let col = 0; col < COLUMN_COUNT; col++) {
                if (board1[row][col] !== board2[row][col]) {
                    return false;
                }
            }
        }
        return true;
    }

    updateUI() {
        this.scoreDisplay.textContent = this.score;

        for (let row = 0; row < ROW_COUNT; row++) {
            for (let col = 0; col < COLUMN_COUNT; col++) {
                const value = this.board[row][col];
                const cellIndex = row * COLUMN_COUNT + col;
                const cell = this.cells[cellIndex];

                cell.textContent = value === 0 ? '' : value;
                cell.className = `cell cell-${value}`;
            }
        }
    }

    showWin() {
        setTimeout(() => {
            if (confirm('🎉 Победа! Вы собрали 2048! Продолжить игру?')) {
                this.hasWon = false;
            } else {
                this.startNewGame();
            }
        }, 100);
    }

    showGameOver() {
        setTimeout(() => {
            if (confirm(`Игра окончена! Ваш счёт: ${this.score}. Начать заново?`)) {
                this.startNewGame();
            }
        }, 100);
    }
}

// Запуск игры
document.addEventListener('DOMContentLoaded', () => {
    new Game2048();
});