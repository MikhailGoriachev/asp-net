window.addEventListener("load", function () {
    // �������� ������� �� id
    $ = id => document.getElementById(id);

    // ������� � ����������
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

    // ������ ��� ������� AJAX
    let xhr = new XMLHttpRequest();

    // �������� ������ ��������
    function getDataAndShow(id = -1) {
        // �������� �������
        xhr.open("GET", `/Instructors/GetDataInstructor/${id}`);

        // �������� �������
        xhr.send();

        // callback ��� ��������� � ������ ������
        xhr.onload = () => showFormInfo(JSON.parse(xhr.response));
    }

    // ��������� ����������� ����� �� �������
    table.addEventListener("click", function (e) {
        let target = e.target;

        // ���� � �������� ������������ ������� � ���������������
        if (target.hasAttribute("data-instructor-id"))
            getDataAndShow(target.getAttribute("data-instructor-id"));
    });

});
