// получение данных таблиц

// модуль доступа к данным
var dataModule = {};

// клиенты
dataModule.getClients = function (callback = function (item) {}) {
    dataModule.getUrlDataCollection("https://localhost:9900/api/Clients/Get", callback);
}

// цели поездок
dataModule.getObjectives = function (callback = function (item) {}) {
    dataModule.getUrlDataCollection("https://localhost:9900/api/Objectives/Get", callback);
}

// страны
dataModule.getCountries = function (callback = function (item) {}) {
    dataModule.getUrlDataCollection("https://localhost:9900/api/Countries/Get", callback);
}

// маршруты
dataModule.getRouts = function (callback = function (item) {}) {
    dataModule.getUrlDataCollection("https://localhost:9900/api/Routs/Get", callback);
}

// поездки
dataModule.getVisits = function (callback = function (item) {}) {
    dataModule.getUrlDataCollection("https://localhost:9900/api/Visits/Get", callback);
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
