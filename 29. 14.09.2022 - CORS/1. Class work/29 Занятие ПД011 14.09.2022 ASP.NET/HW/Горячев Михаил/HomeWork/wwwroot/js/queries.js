﻿// запросы

var queriesModule = {};

// получение данных о запросе из сесcионного хранилища
queriesModule.getDataAboutQuery = function () {
    return sessionStorage.getItem("queryInfo");
}


// отправка запроса и получение данных
queriesModule.getDataQuery = function (queryInfo = "query01", isFormData = false) {

    let formData = new FormData();

    let collback = (content) => {
        $("#mainId").html(content)

        // форма
        let form = $("#formId");

        console.log(form);

        // кнопка сброса
        $("#formCancelBtn").click(() => queriesModule.getDataQuery(queriesModule.getDataAboutQuery()));

        switch (queryInfo) {
            case "query01":
                form.submit(function (e) {
                    e.preventDefault();

                    formData.set("Value", 0);

                    $.ajax({
                        url: "https://localhost:9900/api/Queries/Query01",
                        method: "put",
                        data: form.serializeArray()
                    }).done(collback);
                });
                break;

            case "query02":
                form.submit(function (e) {
                    e.preventDefault();

                    formData.set("value", 0);

                    $.ajax({
                        url: "https://localhost:9900/api/Queries/Query02",
                        method: "put",
                        data: form.serializeArray()
                    }).done(collback);
                });
                break;

            case "query03":
                form.submit(function (e) {
                    e.preventDefault();

                    formData.set("objectiveId", 0);
                    formData.set("maxPrice", 0);

                    $.ajax({
                        url: "https://localhost:9900/api/Queries/Query03",
                        method: "put",
                        data: form.serializeArray()
                    }).done(collback);
                });
                break;

            case "query04":
                form.submit(function (e) {
                    e.preventDefault();

                    formData.set("value", 0);

                    $.ajax({
                        url: "https://localhost:9900/api/Queries/Query04",
                        method: "put",
                        data: form.serializeArray()
                    }).done(collback);
                });
                break;

            case "query05":
                form.submit(function (e) {
                    e.preventDefault();

                    formData.set("Model.Min", "0");
                    formData.set("Model.Max", "0");

                    $.ajax({
                        url: "https://localhost:9900/api/Queries/Query05",
                        method: "put",
                        data: form.serializeArray()
                    }).done(collback);
                });
                break
        }
    }

    console.log(queryInfo);
    console.log(formData);

    $.ajax({
        url: `https://localhost:9900/api/Queries/${!queryInfo ? "query01" : queryInfo}`,
        method: "put",
        data: $(formData).serialize()
    }).done(collback);
}

// обработка перехода по запросам
queriesModule.runQuery = function (e) {

    // установка данных о запросе в сессионное хранилище
    sessionStorage.setItem("queryInfo", $(e.target).data("query-info"));

    // получение данных по запросу
    queriesModule.getDataQuery(queriesModule.getDataAboutQuery());
}


// метод инициализации страницы
queriesModule.init = function () {

    // получение данных по запросу
    queriesModule.getDataQuery(queriesModule.getDataAboutQuery());

    // устанвока обработчика перехода по запросам
    $("[data-query-info]").click(queriesModule.runQuery);
}


// инициализация страницы
$(queriesModule.init);
