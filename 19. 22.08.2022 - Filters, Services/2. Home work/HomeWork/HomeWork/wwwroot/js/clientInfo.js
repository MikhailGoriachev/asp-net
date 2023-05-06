window.addEventListener("load", function () {
    // получить элемент по id
    $ = id => document.getElementById(id);

    // таблица с элементами
    let table = $("clientsTableId");

    function showFormInfo(client = {}) {
        $("clientId").value = client.id;
        $("clientSurname").value = client.surname;
        $("clientName").value = client.name;
        $("clientPatronymic").value = client.patronymic;
        $("clientYearsOld").value = client.yearsOld;
        $("clientTelNumber").value = client.telNumber;
        $("clientMail").value = client.mail;
        $("clientIsActive").value = client.isActive ? "да" : "нет";
        $("clientPassword").value = client.password;
    }

    // объект для запроса AJAX
    let xhr = new XMLHttpRequest();

    // получить данные элемента
    function getDataAndShow(id = -1) {
        // создание запроса
        xhr.open("GET", `/Clients/GetClientData/${id}`);

        // отправка запроса
        xhr.send();

        // callback для получения и вывода данных
        xhr.onload = () => showFormInfo(JSON.parse(xhr.response));
    }

    // установка обработчика клика по таблице
    table.addEventListener("click", function (e) {
        let target = e.target;

        // если у элемента присутствует атрибут с идентификатором
        if (target.hasAttribute("data-client-id"))
            getDataAndShow(target.getAttribute("data-client-id"));
    });

});
