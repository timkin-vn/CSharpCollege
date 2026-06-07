class Game2048 {
    constructor() {
        this.gameBoard = document.getElementById('gameBoard');
        this.scoreDisplay = document.getElementById('scoreDisplay');

        this.initBoard();
        this.addEventListeners();
        this.startNewGame();
    }

    initBoard() {
        this.gameBoard.innerHTML = '';
        this.cells = [];
        for (let i = 0; i < 16; i++) {
            const cell = document.createElement('div');
            cell.className = 'cell';
            this.gameBoard.appendChild(cell);
            this.cells.push(cell);
        }
    }

    addEventListeners() {
        document.getElementById('newGameBtn').onclick = () => this.startNewGame();

        document.querySelectorAll('.control-btn').forEach(btn => {
            btn.onclick = () => this.makeMove(btn.dataset.direction);
        });
    }

    async startNewGame() {
        const res = await fetch('/api/game/start', { method: 'POST' });
        const data = await res.json();
        this.updateUI(data);
    }

    async makeMove(direction) {
        const res = await fetch('/api/game/move', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ direction })
        });

        const data = await res.json();
        this.updateUI(data.gameState);

        if (data.isWin) {
            setTimeout(() => {
                if (confirm('Победа! Продолжить?')) this.continueGame();
            }, 50);
        } else if (data.isGameOver) {
            setTimeout(() => {
                if (confirm('Игра окончена! Начать заново?')) this.startNewGame();
            }, 50);
        }
    }

    async continueGame() {
        const res = await fetch('/api/game/continue', { method: 'POST' });
        const data = await res.json();
        this.updateUI(data);
    }

    updateUI(state) {
        this.scoreDisplay.textContent = state.score;

        for (let row = 0; row < 4; row++) {
            for (let col = 0; col < 4; col++) {
                const val = state.cells[row][col];
                const idx = row * 4 + col;
                this.cells[idx].textContent = val === 0 ? '' : val;
                this.cells[idx].className = `cell cell-${val}`;
            }
        }
    }
}

new Game2048();