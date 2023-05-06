window.addEventListener("load", function () {
    // �������� ������� �� id
    $ = id => document.getElementById(id);

    // ������� � ����������
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

    // ������ ��� ������� AJAX
    let xhr = new XMLHttpRequest();

    // �������� ������ ��������
    function getDataAndShow(id = -1) {
        // �������� �������
        xhr.open("GET", `/Routes/GetRouteData/${id}`);

        // �������� �������
        xhr.send();

        // callback ��� ��������� � ������ ������
        xhr.onload = () => showFormInfo(JSON.parse(xhr.response));
    }

    // ��������� ����������� ����� �� �������
    table.addEventListener("click", function (e) {
        let target = e.target;

        // ���� � �������� ������������ ������� � ���������������
        if (target.hasAttribute("data-route-id"))
            getDataAndShow(target.getAttribute("data-route-id"));
    });

});
