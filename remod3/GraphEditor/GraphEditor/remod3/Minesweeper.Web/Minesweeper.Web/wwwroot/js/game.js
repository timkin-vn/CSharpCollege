(() => {
    const initialStateElement = document.getElementById('initial-state');
    if (!initialStateElement) {
        return;
    }

    const boardElement = document.getElementById('board');
    const difficultySelect = document.getElementById('difficulty');
    const customSettings = document.getElementById('custom-settings');
    const newGameButton = document.getElementById('new-game');
    const rowsInput = document.getElementById('rows');
    const columnsInput = document.getElementById('columns');
    const minesInput = document.getElementById('mines');
    const statusDifficulty = document.getElementById('status-difficulty');
    const statusMines = document.getElementById('status-mines');
    const statusTime = document.getElementById('status-time');
    const statusText = document.getElementById('status-text');

    const CellState = {
        Hidden: 0,
        Revealed: 1,
        Flagged: 2,
        Questioned: 3,
        Mine: 4,
        Exploded: 5
    };

    let view = JSON.parse(initialStateElement.textContent || '{}');
    let timerId = null;
    let timerBase = view.seconds || 0;
    let timerStartedAt = null;

    function showCustomSettings(selectValue) {
        const isCustom = selectValue === 'custom';
        customSettings?.classList.toggle('active', isCustom);
    }

    function updateTimer() {
        if (!statusTime) {
            return;
        }

        if (timerId) {
            clearInterval(timerId);
            timerId = null;
        }

        const updateDisplay = () => {
            statusTime.textContent = timerBase.toString();
        };

        timerStartedAt = null;
        updateDisplay();

        if (!view.gameOver && view.hasStarted) {
            timerStartedAt = Date.now();
            timerId = window.setInterval(() => {
                const elapsed = Math.floor((Date.now() - timerStartedAt) / 1000);
                statusTime.textContent = (timerBase + elapsed).toString();
            }, 1000);
        }
    }

    function renderStatus() {
        if (statusDifficulty) {
            statusDifficulty.textContent = view.difficulty;
        }
        if (statusMines) {
            statusMines.textContent = view.flagsLeft;
        }
        if (statusText) {
            statusText.textContent = view.statusText;
        }
        timerBase = view.seconds || 0;
        updateTimer();
    }

    function getCellDisplay(cell) {
        switch (cell.state) {
            case CellState.Flagged:
                return { text: '🚩', classes: ['flagged'] };
            case CellState.Questioned:
                return { text: '?', classes: ['questioned'] };
            case CellState.Mine:
                return { text: '💣', classes: ['mine', 'revealed'] };
            case CellState.Exploded:
                return { text: '💥', classes: ['exploded'] };
            case CellState.Revealed:
                if (cell.adjacentMines > 0) {
                    return {
                        text: cell.adjacentMines.toString(),
                        classes: ['revealed', `number-${cell.adjacentMines}`]
                    };
                }
                return { text: '', classes: ['revealed'] };
            default:
                return { text: '', classes: [] };
        }
    }

    function renderBoard() {
        if (!boardElement) {
            return;
        }

        boardElement.innerHTML = '';
        boardElement.style.gridTemplateColumns = `repeat(${view.columns}, 36px)`;

        view.cells.forEach((row, r) => {
            row.forEach((cell, c) => {
                const button = document.createElement('button');
                button.type = 'button';
                button.className = 'cell';
                button.dataset.row = r.toString();
                button.dataset.col = c.toString();

                const { text, classes } = getCellDisplay(cell);
                if (classes.length > 0) {
                    button.classList.add(...classes);
                }
                button.textContent = text;
                if (cell.state === CellState.Revealed) {
                    button.setAttribute('aria-label', `Открытое поле (${cell.adjacentMines} мин рядом)`);
                }

                button.addEventListener('click', () => {
                    if (view.gameOver) return;
                    handleAction('reveal', r, c);
                });

                button.addEventListener('contextmenu', (event) => {
                    event.preventDefault();
                    if (view.gameOver) return;
                    handleAction('flag', r, c);
                });

                button.addEventListener('dblclick', (event) => {
                    event.preventDefault();
                    if (view.gameOver) return;
                    handleAction('chord', r, c);
                });

                boardElement.appendChild(button);
            });
        });
    }

    async function postJson(url, data) {
        const response = await fetch(url, {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(data)
        });

        if (!response.ok) {
            throw new Error(`Запрос не выполнен: ${response.status}`);
        }

        return response.json();
    }

    async function handleAction(action, row, column) {
        try {
            const updated = await postJson('/api/game/action', { action, row, column });
            view = updated;
            renderStatus();
            renderBoard();
        } catch (err) {
            console.error('Не удалось выполнить действие', err);
        }
    }

    async function startNewGame() {
        const preset = difficultySelect?.value ?? 'intermediate';
        const payload = { preset, rows: null, columns: null, mines: null };

        if (preset === 'custom') {
            payload.rows = parseInt(rowsInput?.value ?? '0', 10) || 0;
            payload.columns = parseInt(columnsInput?.value ?? '0', 10) || 0;
            payload.mines = parseInt(minesInput?.value ?? '0', 10) || 0;
        }

        try {
            const updated = await postJson('/api/game/new', payload);
            view = updated;
            renderStatus();
            renderBoard();
        } catch (err) {
            console.error('Не удалось начать новую игру', err);
        }
    }

    difficultySelect?.addEventListener('change', (event) => {
        const value = event.target.value;
        showCustomSettings(value);
    });

    newGameButton?.addEventListener('click', () => startNewGame());

    showCustomSettings(difficultySelect?.value ?? 'intermediate');
    renderStatus();
    renderBoard();
})();
