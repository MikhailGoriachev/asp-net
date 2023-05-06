// получение данных таблиц

// !!! Работа с AJAX с помощью jQuery: https://api.jquery.com/jquery.getjson/ !!!

// модуль доступа к данным
var dataModule = {};

// единицы измерения
dataModule.getUnits = function (callback = function (item) {}) {
    dataModule.getUrlDataCollection("https://localhost:9900/Units/Get", callback);
}

// товары
dataModule.getGoodsList = function (callback = function (item) {}) {
    dataModule.getUrlDataCollection("https://localhost:9900/GoodsList/Get", callback);
}

// продавцы
dataModule.getSellers = function (callback = function (item) {}) {
    dataModule.getUrlDataCollection("https://localhost:9900/Sellers/Get", callback);
}

// закупки
dataModule.getPurchases = function (callback = function (item) {}) {
    dataModule.getUrlDataCollection("https://localhost:9900/Purchases/Get", callback);
}

// продажи
dataModule.getSales = function (callback = function (item) {}) {
    dataModule.getUrlDataCollection("https://localhost:9900/Sales/Get", callback);
}

// получить коллекцию данных по url ссылке
dataModule.getUrlDataCollection = function (url = "", callback = function (item) {}) {
    $.getJSON(url, function (data) {
        $.each(data, function (k, item) {
            callback(item);
        })
    });
}

// получить данные по url ссылке
dataModule.getUrlData = function (url = "", callback = function (data) {}) {
    $.getJSON(url, function (data) {
        callback(data);
    });
}
