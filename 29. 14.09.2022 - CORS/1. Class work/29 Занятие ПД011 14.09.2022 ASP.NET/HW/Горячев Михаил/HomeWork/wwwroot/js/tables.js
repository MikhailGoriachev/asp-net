// запросы

var tablesModule = {};

// получение данных о запросе из сесcионного хранилища
tablesModule.getDataAboutTable = function () {
    return sessionStorage.getItem("tableInfo");
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
                        Обновить данные
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
                    <td>
                        <div class="d-flex justify-content-center">
                            <a class="btn btn-outline-primary me-3" asp-controller="TouristAgency" asp-action="EditVisit" asp-route-id="@visit.Id">
                                <i class="bi bi-pencil-square"></i>
                            </a>
                            <a class="btn btn-outline-danger" asp-controller="TouristAgency" asp-action="RemoveVisit" asp-route-id="@visit.Id">
                                <i class="bi bi-trash3"></i>
                            </a>
                        </div>
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
}


// инициализация страницы
$(tablesModule.init);
