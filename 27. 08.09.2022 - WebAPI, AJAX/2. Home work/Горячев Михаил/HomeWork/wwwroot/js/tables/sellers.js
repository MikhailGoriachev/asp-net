// обработка страницы sellers.html

// модуль страницы
var sellersModule = {};

// метод для вывода товара в строку таблицы
sellersModule.sellerToTableRow = function (item) {
    $("#tbodySellersId").append(`
        <tr>
            <th>${item.id}</th>
            <td>${item.surname}</td>
            <td>${item.name}</td>
            <td>${item.patronymic}</td>
            <td>${item.interest}</td>
            <td></td>
        </tr>
    `);
}

// метод инициализации страницы
sellersModule.init = function () {

    // обработчик кнопки "Обновить данные"
    $("#updateBtnId").click(function () {
        $("#tbodySellersId").empty();
        dataModule.getSellers(sellersModule.sellerToTableRow);
    });

    // получить данные
    dataModule.getSellers(sellersModule.sellerToTableRow);
}

$(sellersModule.init);
