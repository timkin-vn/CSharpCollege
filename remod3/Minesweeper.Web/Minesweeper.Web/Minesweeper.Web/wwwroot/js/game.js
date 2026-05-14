function submitCell(action, row, col) {
    document.getElementById('f-action').value = action;
    document.getElementById('f-row').value = row;
    document.getElementById('f-col').value = col;
    document.getElementById('game-form').submit();
}

(function () {
    const el = document.getElementById('status-time');
    if (el && el.dataset.started === 'true' && el.dataset.gameOver !== 'true') {
        const base = parseInt(el.dataset.seconds, 10) || 0;
        const startedAt = Date.now();
        setInterval(function () {
            el.textContent = (base + Math.floor((Date.now() - startedAt) / 1000)).toString();
        }, 1000);
    }

    document.getElementById('difficulty')?.addEventListener('change', function () {
        document.getElementById('custom-settings')?.classList.toggle('active', this.value === 'custom');
    });
}());
