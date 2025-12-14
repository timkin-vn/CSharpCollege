

let canvas;
let ctx;
let tileSize = 35;

window.onload = () => {
    canvas = document.getElementById("board");
    ctx = canvas.getContext("2d");

    if (!canvas || !ctx) {
        console.error("КРИТИЧЕСКАЯ ОШИБКА: Canvas или контекст отрисовки не инициализированы.");
        return;
    }

    console.log(`[INIT] Canvas initialized. Dimensions: ${canvas.width}x${canvas.height}. TileSize: ${tileSize}`);
    gameLoop();
}

function gameLoop() {
    $.post('/Game/Move', { direction: getDirection() }, function (data) {
        if (!data) return;

        window.gameData = data;

        
        if (data.board && data.board.length > 0) {
            console.log(`[DATA] Board received: ${data.board.length} rows. First cell value (Expected 0): ${data.board[0][0]}`);
        } else {
            console.error("[DATA ERROR] GameBoard пуст или отсутствует. Проверьте GameState.cs!");
        }
        

        render(data);

        if (data.gameOver) {
            showGameOver(data);
            return;
        }

    }).always(() => {
        setTimeout(gameLoop, 180);
    });
}

function render(data) {
    if (!ctx) return;

   
    ctx.clearRect(0, 0, canvas.width, canvas.height);

    
    ctx.fillStyle = "black";
    ctx.fillRect(0, 0, canvas.width, canvas.height);

    
    drawMap(data.board);

    
    drawDots(data.board);

    
    drawPacman(data.playerX, data.playerY);
    drawGhosts(data.ghosts);

    $("#score").text(data.score);
    $("#lives").text(data.lives);
}


function drawMap(gameBoard) {
    if (!gameBoard || gameBoard.length === 0) return;

    let wallCount = 0;

    for (let y = 0; y < gameBoard.length; y++) {
        for (let x = 0; x < gameBoard[y].length; x++) {
            if (gameBoard[y][x] === 0) {
                
                ctx.fillStyle = "white";
                ctx.fillRect(x * tileSize, y * tileSize, tileSize, tileSize);
                wallCount++;
            }
        }
    }

    
    if (wallCount === 0) {
        console.error("[DRAW ERROR] drawMap завершилась, но НЕ НАШЛА ни одного '0' в массиве!");
    } else {
        console.log(`[DRAW INFO] drawMap нашла и нарисовала ${wallCount} стен (должны быть белыми).`);
    }
    
}


function drawDots(gameBoard) {
    if (!gameBoard) return;
    for (let y = 0; y < gameBoard.length; y++) {
        for (let x = 0; x < gameBoard[y].length; x++) {
            const centerX = x * tileSize + tileSize / 2;
            const centerY = y * tileSize + tileSize / 2;
            const cellValue = Math.abs(gameBoard[y][x]);
            if (cellValue === 2) {
                ctx.fillStyle = "white";
                ctx.beginPath();
                ctx.arc(centerX, centerY, 3, 0, Math.PI * 2);
                ctx.fill();
            }
            else if (cellValue === 3) {
                ctx.fillStyle = "yellow";
                ctx.beginPath();
                ctx.arc(centerX, centerY, 8, 0, Math.PI * 2);
                ctx.fill();
            }
        }
    }
}

function drawPacman(x, y) {
    if (x === undefined || y === undefined) return;
    const centerX = x * tileSize + tileSize / 2;
    const centerY = y * tileSize + tileSize / 2;
    const radius = tileSize / 2 - 2;
    ctx.fillStyle = "yellow";
    ctx.beginPath();
    ctx.arc(centerX, centerY, radius, 0.2 * Math.PI, 1.8 * Math.PI);
    ctx.lineTo(centerX, centerY);
    ctx.fill();
}

function drawGhosts(ghosts) {
    if (!ghosts) return;
    ghosts.forEach(g => {
        ctx.fillStyle = g.Color.toLowerCase() || "red";
        ctx.fillRect(g.X * tileSize + 4, g.Y * tileSize + 4, tileSize - 8, tileSize - 8);
        ctx.fillStyle = "white";
        ctx.beginPath();
        ctx.arc(g.X * tileSize + tileSize / 2 - 4, g.Y * tileSize + tileSize / 2, 4, 0, Math.PI * 2);
        ctx.arc(g.X * tileSize + tileSize / 2 + 4, g.Y * tileSize + tileSize / 2, 4, 0, Math.PI * 2);
        ctx.fill();
    });
}

function showGameOver(data) {
    $("#finalscore").text(data.score);
    $("#result").text(data.won ? "YOU WIN!" : "GAME OVER");
    $("#gameover").fadeIn();
}