
// вспомогательная функция для доступа к элементам DOM
$ = id => document.getElementById(id);

window.addEventListener("load", () => {

    // Объект для реализации AJAX-запроса
    let xhr = new XMLHttpRequest();

    //Таблица инструкторов, для обработки события клика на вложенном элементе
    let table = $("instructorsTable");

    console.log(table);

    table.addEventListener("click",
        (e) => {
            let value = e.target.dataset.instructorid;

            console.log(`Id: ${value}`);

            // открыть AJAX-запрос

            xhr.open("GET", `/Instructors/GetInstructor?id=${value}`);

            // отправить запрос
            xhr.send();

            //Прием данных
            xhr.onload = () => {
                let instructor = JSON.parse(xhr.response);

                console.log(instructor);

                $("InsSurame").innerHTML = `<b>Фамилия:</b> ${instructor.surname}`;
                $("InsName").innerHTML = `<b>Имя:</b> ${instructor.name}`;
                $("InsPatronymic").innerHTML = `<b>Отчество:</b> ${instructor.patronymic}`;
                $("InsCategory").innerHTML = `<b>Категория:</b> ${instructor.category}`;


                let date = new Date(Date.parse(`${instructor.birthDate}`));
                let day = date.getDate();
                let month = date.getMonth() + 1;
                let year = date.getFullYear();
                //$("InsBirthDate").innerHTML = `<b>дата рождения:</b> ${(date.getDate()<10 ? `0${date.getDate()}`: date.getDate())}.${(date.getMonth()+1<10 ? `0${date.getMonth()+1}`: date.getMonth()+1)}.${date.getFullYear()}`;
                $("InsBirthDate").innerHTML = `<b>Дата рождения:</b> ${(day < 10 ? `0${day}` : day)}.${(month < 10 ? `0${month}` : month)}.${year}`;
            };
        });
})