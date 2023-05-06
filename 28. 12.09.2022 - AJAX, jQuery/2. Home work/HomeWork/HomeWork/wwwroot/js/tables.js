// запросы

var tablesModule = {};

// получение данных о запросе из сесcионного хранилища
tablesModule.getDataAboutTable = function () {
    return sessionStorage.getItem("tableInfo");
}

// заполнение данных в выпадающие списки формы
tablesModule.initSelectListsForm = function () {
    // выпадающие списки
    let routeList = $("#routeIdSelectId");
    let clientsList = $("#clientIdSelectId");

    routeList.empty();
    clientsList.empty();

    // загрузка данных в выпадающие списки
    dataModule.getClients((c) => clientsList.append(`<option value="${c.id}">${c.fullDataClient}</option>`));
    dataModule.getRouts((r) => routeList.append(`<option value="${r.id}">${r.fullDataRoute}</option>`));
}


// получить данные о поездке по id
tablesModule.getVisitById = function (id = 0, callback = (item) => {}) {
    dataModule.getUrlData(`https://localhost:9900/api/visits/get/${id}`, callback);
}


// заполнить форму для добавления поездки
tablesModule.formAdd = function () {
    // установка заголовка
    $("#visitFormLabelId").html("Добавление поездки");

    // инициализация выпадающих списков
    tablesModule.initSelectListsForm();

    // кнопка submit
    let btnOk = $("#visitFormModalBtnOk");

    // установка id
    $("#idVisit").val(0);

    // установка надписи для кнопки submit
    btnOk.html("Добавить");

    // установка типа формы
    btnOk.data("type", "add");
}


// заполнить форму для редактирования
tablesModule.formEdit = function (id) {

    // установка заголовка
    $("#visitFormLabelId").html("Редактирование поездки");

    // инициализация выпадающих списков
    tablesModule.initSelectListsForm();

    // заполнение данными
    tablesModule.getVisitById(id, (i) => {
        let dateStart = new Date(i.dateStart);
        dateStart.setDate(dateStart.getDate() + 1);
        let dateString = dateStart.toISOString();
        console.log(dateString.substring(0, dateString.indexOf("T")));

        $("#idVisit").val(i.id);
        $("#dateStartId").val(dateString.substring(0, dateString.indexOf("T")));
        $("#clientIdSelectId").val(i.clientId);
        $("#routeIdSelectId").val(i.routeId);
        $("#durationId").val(i.duration);

        console.log("getVisitById");
    });

    // кнопка submit
    let btnOk = $("#visitFormModalBtnOk");

    // установка надписи для кнопки submit
    btnOk.html("Сохранить");

    // установка типа формы
    btnOk.data("type", "edit");

    console.log(btnOk.data("type"));
}


// удаление поездки
tablesModule.removeVisit = function (id = 0) {
    console.log(id);
    $.ajax({
        method: "delete",
        url: `https://localhost:9900/api/visits/delete/${id}`,
    }).done(() => tablesModule.getDataTable(tablesModule.getDataAboutTable()));
}


// обработчик событий таблицы
tablesModule.clickTableHandler = function (e) {
    let type = $(e.target).data("type");
    let id = +$(e.target).data("id");

    console.log("clickTableHandler; ID:" + id);

    switch (type) {
        case "edit":
            tablesModule.formEdit(id);
            break;
        case "remove":
            tablesModule.removeVisit(id);
            break;
    }
}

// обработчик кнопки sumbit на форме
tablesModule.clickBtnOkHandler = function (e) {
    e.preventDefault();

    let type = $(e.target).data("type");

    // данные формы
    let formData = $("#formVisitId").serializeArray();

    switch (type) {
        case "add":
            $.ajax({
                method: "post",
                url: "https://localhost:9900/api/visits/post",
                data: formData
            }).done(() => tablesModule.getDataTable(tablesModule.getDataAboutTable()));
            break;
        case "edit":
            $.ajax({
                method: "put",
                url: `https://localhost:9900/api/visits/put`,
                data: formData
            }).done(() => tablesModule.getDataTable(tablesModule.getDataAboutTable()));
            break;
    }
}


// отправка запроса и получение данных
tablesModule.getDataTable = function (tableInfo = "clients") {

    let collback = () => {
    };

    // заголовок
    let label = $("#tableLabelId");

    // заголовки столбцов
    let thead = $("#theadId");

    // тело таблицы
    let tbody = $("#tbodyId");

    switch (tableInfo) {
        case "clients":
            label.html("Клиенты");
            thead.html(`
                <tr>
                    <th>Id</th>
                    <th>Фамилия</th>
                    <th>Имя</th>
                    <th>Отчество</th>
                    <th>Паспорт</th>
                    <th class="text-end">
                        <a class="btn btn-primary me-4" id="updateBtnId">
                            <i class="bi bi-arrow-clockwise"></i>
                            Обновить данные
                        </a>
                    </th>
                </tr>`);

            collback = (i) => {
                tbody.append(`
                <tr>
                    <th>${i.id}</th>
                    <td>${i.surname}</td>
                    <td>${i.name}</td>
                    <td>${i.patronymic}</td>
                    <td colspan="2">${i.passport}</td>
                </tr>`);
            };

            break;

        case "countries":
            label.html("Страны");
            thead.html(`
                <tr>
                <th>Id</th>
                <th>Название</th>
                <th>Транспортные услуги (&#8381;)</th>
                <th>Оформление визы (&#8381;)</th>
                <th class="text-end">
                    <a class="btn btn-primary me-4" id="updateBtnId">
                        <i class="bi bi-arrow-clockwise"></i>
                        Обновить данные
                    </a>
                </th>
            </tr>`);

            collback = (i) => {
                tbody.append(`
                <tr>
                    <th>${i.id}</th>
                    <td>${i.name}</td>
                    <td>${i.costService}</td>
                    <td colspan="2">${i.costVisa}</td>
                </tr>`);
            };

            break;

        case "objectives":
            label.html("Цели поездки");
            thead.html(`
                <tr>
                    <th>Id</th>
                    <th>Название</th>
                    <th class="text-end">
                        <a class="btn btn-primary me-4" id="updateBtnId">
                            <i class="bi bi-arrow-clockwise"></i>
                            Обновить данные
                        </a>
                    </th>
                </tr>`);

            collback = (i) => {
                tbody.append(`
                <tr>
                    <th>${i.id}</th>
                    <td colspan="2">${i.name}</td>
                </tr>`);
            };

            break;

        case "routs":
            label.html("Маршруты");
            thead.html(`
            <tr>
                <th>Id</th>
                <th>Страна</th>
                <th>Цель поездки</th>
                <th>Стоимость 1 дня пребывания (&#8381;)</th>
                <th>Транспортные услуги (&#8381;)</th>
                <th>Оформление визы (&#8381;)</th>
                    <th class="text-end">
                        <a class="btn btn-primary me-4" id="updateBtnId">
                            <i class="bi bi-arrow-clockwise"></i>
                            Обновить данные
                        </a>
                    </th>
            </tr>`);

            collback = (i) => {
                tbody.append(`
                <tr>
                    <th>${i.id}</th>
                    <td>${i.country.name}</td>
                    <td>${i.objective.name}</td>
                    <td>${i.dailyCost}</td>
                    <td>${i.country.costService}</td>
                    <td colspan="2">${i.country.costVisa}</td>
                </tr>`);
            };

            break;

        case "visits":
            label.html("Поездки");
            thead.html(`
            <tr>
                 
                <th class="align-middle">Id</th>
                <th class="align-middle">Клиент</th>
                <th class="align-middle">Страна</th>
                <th class="align-middle">Цель поездки</th>
                <th class="align-middle">Стоим. 1 дня (&#8381;)</th>
                <th class="align-middle">Транс. услуги (&#8381;)</th>
                <th class="align-middle">Виза (&#8381;)</th>
                <th class="align-middle">Дата поездки</th>
                <th class="align-middle">Длительность (день)</th>
                <th class="text-end">
                    <a class="btn btn-primary me-4" id="updateBtnId">
                        <i class="bi bi-arrow-clockwise"></i>
                        Обновить
                    </a>

                    <a class="btn btn-success" id="addBtnId" data-bs-toggle="modal" data-bs-target="#visitFormModalId">
                        <i class="bi bi-plus-lg"></i> Добавить
                    </a>
                </th>
            </tr>`);

            collback = (i) => {
                tbody.append(`
                <tr>
                    <th>${i.id}</th>
                    <td>${i.client.surname} ${i.client.name[0]}. ${i.client.patronymic[0]}.</td>
                    <td>${i.route.country.name}</td>
                    <td>${i.route.objective.name}</td>
                    <td>${i.route.dailyCost}</td>
                    <td>${i.route.country.costService}</td>
                    <td>${i.route.country.costVisa}</td>
                    <td>${new Date(i.dateStart).toLocaleDateString()}</td>
                    <td>${i.duration}</td>
                    <td class="text-end">
                        <button class="btn btn-outline-primary" data-type="edit" data-bs-toggle="modal" data-bs-target="#visitFormModalId"
                                data-id="${i.id}">
                            <i class="bi bi-pencil-square" data-type="edit" data-id="${i.id}"></i>
                        </button>
                        <button class="btn btn-outline-danger me-4" data-type="remove" data-id="${i.id}">
                            <i class="bi bi-trash3" data-type="remove" data-id="${i.id}"></i>
                        </button>
                    </td>

                </tr>`);
            };

            break;
    }

    let func = () => {

        tbody.empty();

        dataModule.getUrlDataCollection(
            `https://localhost:9900/api/${!tableInfo ? "clients" : tableInfo}/get`,
            collback);
    }

    // кнопка обновления данных
    $("#updateBtnId").click(func);

    // получение данных
    func();
}

// обработка перехода по запросам
tablesModule.runQuery = function (e) {

    // установка данных о запросе в сессионное хранилище
    sessionStorage.setItem("tableInfo", $(e.target).data("table-info"));

    // получение данных по запросу
    tablesModule.getDataTable(tablesModule.getDataAboutTable());
}


// метод инициализации страницы
tablesModule.init = function () {

    // получение данных по запросу
    tablesModule.getDataTable(tablesModule.getDataAboutTable());

    // устанвока обработчика перехода по запросам
    $("[data-table-info]").click(tablesModule.runQuery);

    // обработчик событий для таблицы
    $("#tableId").click(tablesModule.clickTableHandler);

    // обработчик кнопки submit формы
    $("#visitFormModalBtnOk").click(tablesModule.clickBtnOkHandler);

    // обработчик кнопки "добавить"
    $("#addBtnId").click(tablesModule.formAdd);
}


// инициализация страницы
$(tablesModule.init);
