// обработка страницы units.html

// модуль страницы
var unitsModule = {};

// метод для вывода товара в строку таблицы
unitsModule.unitToTableRow = function (item) {
    $("#tbodyUnitsId").append(`
        <tr>
            <th>${item.id}</th>
            <td>${item.shortName}</td>
            <td>${item.longName}</td>
            <td></td>
        </tr>
    `);
}

// метод инициализации страницы
unitsModule.init = function () {

    // обработчик кнопки "Обновить данные"
    $("#updateBtnId").click(function () {
        $("#tbodyUnitsId").empty();
        dataModule.getUnits(unitsModule.unitToTableRow);
    });

    // получить данные
    dataModule.getUnits(unitsModule.unitToTableRow);
}

$(unitsModule.init);
