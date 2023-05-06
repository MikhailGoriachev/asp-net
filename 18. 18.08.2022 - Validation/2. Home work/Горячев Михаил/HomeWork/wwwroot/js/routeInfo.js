window.addEventListener("load", function () {
    // получить элемент по id
    $ = id => document.getElementById(id);

    // таблица с элементами
    let table = $("routesTableId");

    function showFormInfo(route = {}) {
        $("routeId").value = route.id;
        $("routeStartPoint").value = route.startPoint;
        $("routeMiddlePoint").value = route.middlePoint;
        $("routeEndPoint").value = route.endPoint;
        $("routeLength").value = route["length"];
        $("routeDifficulty").value = route.difficulty;
        $("routeInstructor").value = `${route.instructor.id}. ${route.instructor.surname} ${route.instructor.name} ${route.instructor.patronymic}`;
    }

    // объект для запроса AJAX
    let xhr = new XMLHttpRequest();

    // получить данные элемента
    function getDataAndShow(id = -1) {
        // создание запроса
        xhr.open("GET", `/Routes/GetRouteData/${id}`);

        // отправка запроса
        xhr.send();

        // callback для получения и вывода данных
        xhr.onload = () => showFormInfo(JSON.parse(xhr.response));
    }

    // установка обработчика клика по таблице
    table.addEventListener("click", function (e) {
        let target = e.target;

        // если у элемента присутствует атрибут с идентификатором
        if (target.hasAttribute("data-route-id"))
            getDataAndShow(target.getAttribute("data-route-id"));
    });

});
