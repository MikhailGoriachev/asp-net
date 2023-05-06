window.addEventListener("load", function () {
    // получить элемент по id
    $ = id => document.getElementById(id);

    // таблица с элементами
    let table = $("instructorsTableId");

    function showFormInfo(instructor = {}) {
        let date = new Date(Date.parse(instructor.dateOfBirth));

        $("instId").value = instructor.id;
        $("instSurname").value = instructor.surname;
        $("instName").value = instructor.name;
        $("instPatronymic").value = instructor.patronymic;
        $("instDateOfBirth").value = `${date.toLocaleDateString()}`;
        $("instCategory").value = instructor.category;
    }

    // объект для запроса AJAX
    let xhr = new XMLHttpRequest();

    // получить данные элемента
    function getDataAndShow(id = -1) {
        // создание запроса
        xhr.open("GET", `/Instructors/GetDataInstructor/${id}`);

        // отправка запроса
        xhr.send();

        // callback для получения и вывода данных
        xhr.onload = () => showFormInfo(JSON.parse(xhr.response));
    }

    // установка обработчика клика по таблице
    table.addEventListener("click", function (e) {
        let target = e.target;

        // если у элемента присутствует атрибут с идентификатором
        if (target.hasAttribute("data-instructor-id"))
            getDataAndShow(target.getAttribute("data-instructor-id"));
    });

});
