// game.js - исправленная версия
$(document).ready(function () {
    console.log("Game script loaded - jQuery version:", $.fn.jquery);

    // Используем делегирование событий для кликов
    $(document).on('click', '.game-cell', function () {
        var row = $(this).data('row');
        var col = $(this).data('column');

        console.log("Cell clicked - row:", row, "col:", col, "class:", $(this).attr('class'));

        // Проверяем, не закончилась ли игра
        var mistakes = parseInt($('#mistakesCount').text()) || 0;
        if (mistakes >= 5) {
            console.log("Game over - no more moves allowed");
            alert('Игра окончена! Начните новую игру.');
            return false;
        }

        // Проверяем, не заполнена ли уже клетка
        if ($(this).hasClass('filled') || $(this).hasClass('crossed')) {
            console.log("Cell already filled or crossed");
            return false;
        }

        makeMove(row, col);
        return false;
    });

    // Визуальная обратная связь при наведении
    $(document).on('mouseenter', '.game-cell:not(.filled):not(.crossed)', function () {
        $(this).css('background-color', '#e8f4ff');
    });

    $(document).on('mouseleave', '.game-cell:not(.filled):not(.crossed)', function () {
        $(this).css('background-color', 'white');
    });

    // Отключаем выделение текста на клетках
    $('.game-cell').css({
        'user-select': 'none',
        '-webkit-user-select': 'none',
        '-moz-user-select': 'none',
        '-ms-user-select': 'none'
    });

    // Проверяем, загружен ли Bootstrap
    if (typeof $.fn.modal === 'undefined') {
        console.error("Bootstrap not loaded properly!");
    }
});

// Функция для выполнения хода
function makeMove(row, column) {
    console.log("Making move - row:", row, "column:", column);

    // Получаем CSRF токен
    var token = $('input[name="__RequestVerificationToken"]').val();
    if (!token) {
        console.error("CSRF token not found!");
        alert('Ошибка безопасности. Перезагрузите страницу.');
        return;
    }

    // Отправляем AJAX запрос
    $.ajax({
        url: '/Game/MakeMove',
        type: 'POST',
        dataType: 'json',
        data: {
            row: row,
            column: column,
            __RequestVerificationToken: token
        },
        beforeSend: function () {
            console.log("Sending AJAX request...");
        },
        success: function (data) {
            console.log("Move response:", data);

            if (data.Success) {
                // Обновляем счетчик ошибок
                $('#mistakesCount').text(data.MistakesCount);

                // Находим клетку
                var cellSelector = '.game-cell[data-row="' + row + '"][data-column="' + column + '"]';
                var cell = $(cellSelector);

                console.log("Updating cell:", cellSelector, "new state:", data.CellState);

                if (cell.length === 0) {
                    console.error("Cell not found:", cellSelector);
                    return;
                }

                // Обновляем классы клетки
                cell.removeClass('empty filled crossed');

                if (data.CellState === 'Filled') {
                    cell.addClass('filled');
                    cell.css('background-color', '#000');
                } else if (data.CellState === 'Crossed') {
                    cell.addClass('crossed');
                    cell.css('background-color', '#fff');
                    // Добавляем крестик
                    if (cell.find('.cross').length === 0) {
                        cell.append('<div class="cross"></div>');
                    }
                } else {
                    cell.addClass('empty');
                    cell.css('background-color', '#fff');
                    cell.find('.cross').remove();
                }

                // Обновляем подсказки строк
                if (data.CompletedRows && data.CompletedRows.length > 0) {
                    console.log("Completed rows:", data.CompletedRows);
                    data.CompletedRows.forEach(function (rowIndex) {
                        $('.row-clue[data-row="' + rowIndex + '"]').addClass('completed');
                        $('.game-cell[data-row="' + rowIndex + '"].filled').addClass('completed-cell');
                    });
                }

                // Обновляем подсказки столбцов
                if (data.CompletedColumns && data.CompletedColumns.length > 0) {
                    console.log("Completed columns:", data.CompletedColumns);
                    data.CompletedColumns.forEach(function (colIndex) {
                        $('.column-clue[data-column="' + colIndex + '"]').addClass('completed');
                        $('.game-cell[data-column="' + colIndex + '"].filled').addClass('completed-cell');
                    });
                }

                // Проверяем условия окончания игры
                if (data.IsGameOver) {
                    console.log("Game over!");
                    showMessage('Игра окончена!', 'Вы сделали 5 ошибок. Попробуйте ещё раз!');
                } else if (data.IsGameWon) {
                    console.log("Game won!");
                    showMessage('Поздравляем!', 'Вы решили кроссворд!');
                }
            } else {
                console.error("Server returned error:", data.ErrorMessage);
                if (data.ErrorMessage) {
                    alert('Ошибка: ' + data.ErrorMessage);
                } else {
                    alert('Произошла неизвестная ошибка.');
                }
            }
        },
        error: function (xhr, status, error) {
            console.error("AJAX error:", status, error, xhr.responseText);
            alert('Произошла ошибка при выполнении хода. Проверьте консоль для подробностей.');
        }
    });
}

// Функция показа сообщения
function showMessage(title, message) {
    $('#messageModalTitle').text(title);
    $('#messageModalBody').text(message);
    $('#messageModal').modal('show');
}

// CSRF токен для AJAX запросов
$(document).ajaxSend(function (event, xhr, options) {
    if (options.type.toUpperCase() === "POST") {
        var token = $('input[name="__RequestVerificationToken"]').val();
        if (token) {
            xhr.setRequestHeader("RequestVerificationToken", token);
        }
    }
});