
// вспомогательная функция для доступа к элементам DOM
$ = id => document.getElementById(id);

window.addEventListener("load", () => {
    // Объект для реализации AJAX-запроса
    let xhr = new XMLHttpRequest();

    //Таблица инструкторов, для обработки события клика на вложенном элементе
    let table = $("routesTable");

    console.log(table);

    table.addEventListener("click",
        (e) => {
            console.log(e.target.dataset);
            let value = e.target.dataset.routeid;

            // открыть AJAX-запрос

            xhr.open("GET", `/Routes/GetRoute?id=${value}`);

            // отправить запрос
            xhr.send();

            //Прием данных
            xhr.onload = () => {
                let route = JSON.parse(xhr.response);

                console.log(route);

                $("StartingPoint").innerHTML = `<b>Начальный пункт:</b> ${route.startingPoint}`;
                $("MiddlePoint").innerHTML = `<b>Промежуточный пункт:</b> ${route.middlePoint}`;
                $("EndPoint").innerHTML = `<b>Конечный пункт:</b> ${route.endPoint}`;
                $("Length").innerHTML = `<b>Протяженность (км):</b> ${route.length}`;
                $("Complexity").innerHTML = `<b>Сложность:</b> ${route.complexity}`;

                //Инструктор

                let date = new Date(Date.parse(`${route.instructorData.birthDate}`));
                let day = date.getDate();
                let month = date.getMonth() + 1;
                let year = date.getFullYear();

                /*$("InstructorData").innerHTML = `<b>Инструктор:</b> ${route.instructorData.surname} ${route.instructorData.name} ${route.instructorData.patronymic} <br/> 
                 &nbsp&nbsp<b>Категория:</b> ${route.instructorData.category} <br/> 
                 &nbsp&nbsp<b>Дата рождения:</b> ${(day < 10 ? `0${day}` : day)}.${(month < 10 ? `0${month}` : month)}.${year}`;*/

                $("InstructorData").innerHTML = `<b>Инструктор:</b> ${route.instructorData.surname} ${route.instructorData.name} ${route.instructorData.patronymic}
                                                <ul>
                                                    <li><b>Категория:</b> ${route.instructorData.category} <br/> </li>
                                                    <li><b>Дата рождения:</b> ${(day < 10 ? `0${day}` : day)}.${(month < 10 ? `0${month}` : month)}.${year}</li>
                                                </ul>`;
            };
        });
})