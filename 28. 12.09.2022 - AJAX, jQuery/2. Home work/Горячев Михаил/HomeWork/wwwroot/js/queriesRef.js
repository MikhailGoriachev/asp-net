// обработка ссылок на запросы

// модуль обработки страницы
var queriesRefModule = {};


// обработка перехода по запросам
queriesRefModule.gotoQuery = function (e) {

    // установка данных о запросе в сессионное хранилище
    sessionStorage.setItem("queryInfo", $(e.target).data("query-info"));

    // переход на страницу запросов
    location.href = "/htdocs/queries.html";
}


// метод инициализации страницы
queriesRefModule.init = function () {

    // устанвока обработчика перехода по запросам
    $("[data-query-info]").click(queriesRefModule.gotoQuery);
}


// инициализация страницы
$(queriesRefModule.init);
