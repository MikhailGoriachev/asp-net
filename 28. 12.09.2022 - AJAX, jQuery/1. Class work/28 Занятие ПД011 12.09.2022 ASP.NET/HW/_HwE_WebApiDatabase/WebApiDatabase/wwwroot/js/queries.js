// Выполнение запросов AJAX для получения данных от сервера через WebAPI

function ajaxGetHtml(url) {
    // XNLHttpRequest это и есть реализация AJAX в JavaScript
    let request = new XMLHttpRequest(url);

    // формально требуется тело запроса, т.к. перелдача через параметры, тело пустое
    let body = "";

    // настройка и отправка AJAX-запроса на сервер
    request.open("GET", url);
    request.setRequestHeader("Content-Type", "application/x-www-form-urlencoded");

    // callBack, работающий по окончании запроса
    request.onreadystatechange = function () {
        // если запрос завершен и завершен корректно вывести полученные от сервера данные
        if (request.readyState === 4 && request.status === 200) {
            document.getElementById("outputMarkup").innerHTML = request.responseText;
        } // if
    } // callBack

    // собственно отправка запроса
    request.send(body);

    document.getElementById("outputMarkup").innerHTML = "<h4>GET-запрос отправлен</h4>"
    // console.log("GET-запрос отправлен");
} // ajaxGet