// обработка страницы sales.html

// модуль страницы
var salesModule = {};


// обновление данных в таблице
salesModule.updateData = function () {
    $("#tbodySalesId").empty();
    dataModule.getSales(salesModule.saleToTableRow);
}


// метод для вывода товара в строку таблицы
salesModule.saleToTableRow = function (item) {
    $("#tbodySalesId").append(`
        <tr>
            <th>${item.id}</th>
            <td>${item.seller.surname} ${item.seller.name[0]}. ${item.seller.patronymic[0]}.</td>
            <td>${item.purchase.goods.name}</td>
            <td>${item.unit.shortName}</td>
            <td>${item.price}</td>
            <td>${item.amount}</td>
            <td>${new Date(item.dateSell).toLocaleDateString()}</td>
            <td class="text-end">
                <button class="btn btn-outline-primary" data-type="edit" data-bs-toggle="modal" data-bs-target="#saleFormModalId"
                        data-id="${item.id}">
                    <i class="bi bi-pencil-square" data-type="edit" data-id="${item.id}"></i>
                </button>
                <button class="btn btn-outline-danger me-4" data-type="remove" data-id="${item.id}">
                    <i class="bi bi-trash3" data-type="remove" data-id="${item.id}"></i>
                </button>
            </td>
        </tr>
    `);
}


// заполнение данных в выпадающие списки формы
salesModule.initSelectListsForm = function () {
    // выпадающие списки
    let sellerList = $("#sellerIdSelectId");
    let purchaseList = $("#purchaseIdSelectId");
    let unitList = $("#unitIdSelectId");

    // загрузка данных в выпадающие списки
    dataModule.getSellers((s) => sellerList.append(`<option value="${s.id}">${s.idAndFullName}</option>`))
    dataModule.getPurchases((p) => purchaseList.append(`<option value="${p.id}">${p.stringInfoPurchase}</option>`))
    dataModule.getUnits((u) => unitList.append(`<option value="${u.id}">${u.longName}</option>`))
}


// получить данные о продаже по id
salesModule.getSaleById = function (id = 0, callback = (item) => {}) {
    dataModule.getUrlData(`https://localhost:9900/Sales/Get/${id}`, callback);
}

// заполнить форму для добавления
salesModule.formAdd = function () {
    // установка заголовка
    $("#saleFormLabelId").html("Добавление продажи");

    // инициализация выпадающих списков
    salesModule.initSelectListsForm();

    // кнопка submit
    let btnOk = $("#saleFormModalBtnOk");

    // установка id
    $("#idSale").val(0);

    // установка надписи для кнопки submit
    btnOk.html("Добавить");

    // установка типа формы
    btnOk.data("type", "add");
}


// заполнить форму для редактирования
salesModule.formEdit = function (id) {

    // установка заголовка
    $("#saleFormLabelId").html("Редактирование продажи");

    // инициализация выпадающих списков
    salesModule.initSelectListsForm();

    // заполнение данными
    salesModule.getSaleById(id, (i) => {
        let dateSell = new Date(i.dateSell);
        dateSell.setDate(dateSell.getDate() + 1);
        let dateString = dateSell.toISOString();
        console.log(dateString.substring(0, dateString.indexOf("T")));

        $("#idSale").val(i.id);
        $("#dateSellId").val(dateString.substring(0, dateString.indexOf("T")));
        $("#sellerIdSelectId").val(i.sellerId);
        $("#purchaseIdSelectId").val(i.purchaseId);
        $("#unitIdSelectId").val(i.unitId);
        $("#amountId").val(i.amount);
        $("#priceId").val(i.price);
    });

    // кнопка submit
    let btnOk = $("#saleFormModalBtnOk");

    // установка надписи для кнопки submit
    btnOk.html("Сохранить");

    // установка типа формы
    btnOk.data("type", "edit");

    console.log(btnOk.data("type"));
}

salesModule.remove = function (id = 0) {
    console.log(id);
    $.ajax({
        method: "delete",
        url: `https://localhost:9900/Sales/Delete/${id}`,
    }).done(salesModule.updateData);
}


// обработчик событий таблицы
salesModule.clickTableHandler = function (e) {
    let type = $(e.target).data("type");
    let id = +$(e.target).data("id");

    switch (type) {
        case "edit":
            salesModule.formEdit(id);
            break;
        case "remove":
            salesModule.remove(id);
            break;
    }
}

// обработчик кнопки sumbit на форме
salesModule.clickBtnOkHandler = function (e) {
    e.preventDefault();

    let type = $(e.target).data("type");

    // данные формы
    let formData = $("#formSaleId").serializeArray();

    switch (type) {
        case "add":
            $.ajax({
                method: "post",
                url: "https://localhost:9900/Sales/Post",
                data: formData
            }).done(salesModule.updateData);
            break;
        case "edit":
            $.ajax({
                method: "put",
                url: `https://localhost:9900/Sales/Put`,
                data: formData
            }).done(salesModule.updateData);
            break;
    }
}

// метод инициализации страницы
salesModule.init = function () {

    // обработчик кнопки "Обновить данные"
    $("#updateBtnId").click(salesModule.updateData);

    // получить данные
    salesModule.updateData();

    // обработчик событий для таблицы
    $("#salesTableId").click(salesModule.clickTableHandler);

    // обработчик кнопки submit формы
    $("#saleFormModalBtnOk").click(salesModule.clickBtnOkHandler);

    // обработчик кнопки "добавить"
    $("#addBtnId").click(salesModule.formAdd);
}


// инициализация страницы
$(salesModule.init);
