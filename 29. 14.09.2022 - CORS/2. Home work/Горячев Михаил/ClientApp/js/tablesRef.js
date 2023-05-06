// обработка ссылок на таблицы

// модуль обработки страницы
var tablesRefModule = {};


// обработка перехода по запросам
tablesRefModule.gotoTable = function (e) {
    // установка данных о запросе в сессионное хранилище
    sessionStorage.setItem("tableInfo", $(e.target).data("table-info"));
}


// метод инициализации страницы
tablesRefModule.init = function () {

    // устанвока обработчика перехода по запросам
    $("[data-table-info]").click(tablesRefModule.gotoTable);
}


// инициализация страницы
$(tablesRefModule.init);
