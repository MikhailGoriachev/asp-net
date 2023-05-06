// обработка страницы goodsList.html

// модуль страницы
var goodsListModule = {};

// метод для вывода товара в строку таблицы
goodsListModule.goodsToTableRow = function (item) {
    $("#tbodyGoodsListId").append(`
    <tr>
        <th>${item.id}</th>
        <td>${item.name}</td>
        <td></td>
    </tr>`);
}

// метод инициализации страницы
goodsListModule.init = function () {

    // обработчик кнопки "Обновить данные"
    $("#updateBtnId").click(function () {
        $("#tbodyGoodsListId").empty();
        dataModule.getGoodsList(goodsListModule.goodsToTableRow);
    });

    // получить данные
    dataModule.getGoodsList(goodsListModule.goodsToTableRow);
}

$(goodsListModule.init);
