
startGame(8, 8, 10);


function startGame(width, height, minesCount) {
    let isWinner = true;
    let mine = 'X';
    let flagsCount = 0;

    let gameField = document.querySelector('.gameField');
    gameField.style.setProperty('grid-template-columns', 'repeat(' + width + ', 40px)');

    let cellsCount = width * height;
    gameField.innerHTML = "<button class='btn'></button>".repeat(cellsCount);

    let minesArray = [...new Array(cellsCount).keys()]
        .sort(() => Math.random() - 0.5).slice(0, minesCount);
    let cellsBtns = [...gameField.children];
    let fieldCells = new Array(cellsCount);
    fillFieldCells(fieldCells);

    let minesLeftTable = document.querySelector('.minesCount');
    minesLeftTable.textContent = minesCount;

    let newGameBtn = document.querySelector('.newGameBtn');
    newGameBtn.addEventListener('click', (event) => {
        location.reload();
    })

    // левый клик на ячейку поля
    let restart = false;
    gameField.addEventListener('click', (event) => {
        if (event.target.tagName != 'BUTTON') {
            return;
        }

        let index = cellsBtns.indexOf(event.target);
        let rowIndex = Math.floor(index / width);
        let columnIndex = index % width;

        if (index >= 0) {
            if (OpenCells(rowIndex, columnIndex)) {
                gameEndDeclaration();
            }
        }
    })

    // right mouse click
    gameField.addEventListener('contextmenu', (event) => {
        if (event.target.tagName != 'BUTTON' || event.target.disabled == true || cellsBtns.indexOf(event.target) < 0) {
            return;
        }

        window.oncontextmenu = function () {
            return false;
        }

        if (event.target.style.backgroundImage != '') {
            event.target.style.backgroundImage = '';
            --flagsCount;
            minesLeftTable.textContent = flagsCount < minesCount ?
                minesCount - flagsCount : 0;
            return;
        }

        ++flagsCount;
        minesLeftTable.textContent = flagsCount < minesCount ? minesCount - flagsCount : 0;
        event.target.style.backgroundImage = "url('../Images/bombFlag.png')";
    })

    // инициализация ячеек поля
    function fillFieldCells(fieldCells) {
        for (let i = 0; i < fieldCells.length; i++) {
            if (minesArray.includes(i)) {
                fieldCells[i] = mine;
                cellsBtns[i].style.color = 'rgb(189, 13, 13)';
                continue;
            }

            cellsBtns[i].style.backgroundImage = '';

            let tmp = getMinesCountAround(i);
            fieldCells[i] = tmp == 0 ? ' ' : tmp;
        }
    }

    // обработка нажатия на ячейку
    function OpenCells(rowIndex, columnIndex) {
        let index = columnIndex + rowIndex * width;

        if (cellsBtns[index].disabled == false) {
            cellsBtns[index].disabled = true;


            if (cellsBtns[index].style.backgroundImage != '') {
                --flagsCount;
                minesLeftTable.textContent = flagsCount < minesCount ?
                    minesCount - flagsCount : 0;
            }
            cellsBtns[index].style.backgroundImage = '';

            if (isMine(rowIndex, columnIndex)) {
                isWinner = false;
                return true;
            }

            let minesCountAround = fieldCells[index];
            if (minesCountAround == 0) {
                for (let x = -1; x < 2; x++) {
                    for (let y = -1; y < 2; y++) {
                        if (isValid(rowIndex + y, columnIndex + x)) {
                            OpenCells(rowIndex + y, columnIndex + x);
                        }
                    }
                }
            }
            else {
                cellsBtns[index].textContent = fieldCells[index];
            }

            return rUWinningSon();
        }
    }

    function rUWinningSon() {
        isWinner = cellsBtns.filter(x => x.disabled == false).length == minesCount;
        return isWinner;
    }

    // раскрытие всего игровго поля
    function gameEndDeclaration() {
        newGameBtn.style.backgroundImage = !isWinner ? "url('../Images/sadPonyFace.png')" : "url('../Images/funnyFace.png')";
        for (let i = 0; i < cellsBtns.length; ++i) {
            cellsBtns[i].disabled = true;
            cellsBtns[i].textContent = fieldCells[i];
            cellsBtns[i].style.backgroundImage = '';
        }

        console.log(isWinner);

        $.ajax({
            type: "POST",
            url: 'Home/SaveResult',
            data: { result: isWinner },
        });
    }

    function getMinesCountAround(index) {
        let rowIndex = Math.floor(index / width);
        let columnIndex = index % width;
        let count = 0;
        for (let x = -1; x < 2; x++) {
            for (let y = -1; y < 2; y++) {
                if (isValid(rowIndex + y, columnIndex + x)
                    && isMine(rowIndex + y, columnIndex + x)) {
                    ++count;
                }
            }
        }

        return count;
    }

    function isValid(rowIndex, columnIndex) {
        return rowIndex >= 0 && rowIndex < height
            && columnIndex >= 0 && columnIndex < width;
    }

    function isMine(rowIndex, columnIndex) {
        let index = columnIndex + rowIndex * width;
        return minesArray.includes(index);
    }
}