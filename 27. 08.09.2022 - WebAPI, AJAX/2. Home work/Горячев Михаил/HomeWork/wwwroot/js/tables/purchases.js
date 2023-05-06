// обработка страницы purchases.html

// модуль страницы
var purchasesModule = {};

// метод для вывода товара в строку таблицы
purchasesModule.purchasesToTableRow = function (item) {
    $("#tbodyPurchasesId").append(`
        <tr>
            <th>${item.id}</th>
            <td>${item.goods.name}</td>
            <td>${item.unit.longName}</td>
            <td>${item.price}</td>
            <td>${item.amount}</td>
            <td>${new Date(item.datePurchase).toLocaleDateString()}</td>
            <td></td>
        </tr>`);
}

// метод инициализации страницы
purchasesModule.init = function () {

    // обработчик кнопки "Обновить данные"
    $("#updateBtnId").click(function () {
        $("#tbodyPurchasesId").empty();
        dataModule.getPurchases(purchasesModule.purchasesToTableRow);
    });

    // получить данные
    dataModule.getPurchases(purchasesModule.purchasesToTableRow);
}

$(purchasesModule.init);
