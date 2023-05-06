
// вспомогательная функция для доступа к элементам DOM
$ = id => document.getElementById(id);

window.addEventListener("load", () => {

    // Объект для реализации AJAX-запроса
    let xhr = new XMLHttpRequest();

    //Таблица инструкторов, для обработки события клика на вложенном элементе
    let table = $("ClientsTable");

    console.log(table);

    table.addEventListener("click",
        (e) => {
            let value = e.target.dataset.clientid;

            console.log(`Id: ${value}`);

            if (value === null)
                return;

            // открыть AJAX-запрос

            xhr.open("GET", `/Clients/GetClient?id=${value}`);

            // отправить запрос
            xhr.send();

            //Прием данных
            xhr.onload = () => {
                let client = JSON.parse(xhr.response);

                console.log(client);

                $("ClientPhoto").innerHTML = `<img src="/images/clients/${client.photoFile}" alt="фотография клиента, id: ${client.id}" height="160"/>`;
                $("ClientSurame").innerHTML = `<b>Фамилия:</b> ${client.surname}`;
                $("ClientName").innerHTML = `<b>Имя:</b> ${client.name}`;
                $("ClientPatronymic").innerHTML = `<b>Отчество:</b> ${client.patronymic}`
                $("ClientAge").innerHTML = `<b>Возраст:</b> ${client.age}`;
                $("ClientPhoneNumber").innerHTML = `<b>Номер телефона:</b> ${client.phoneNumber}`;
                $("ClientEmail").innerHTML = `<b>Электронный адрес:</b> ${client.email}`;
                $("ClientPassword").innerHTML = `<b>Пароль:</b> ${client.password}`;
                $("IsConstantClient").innerHTML = `<b>Признак постоянного клиента:</b> ${(client.isConstant ? "Постоянный"
                    : "Не постоянный")}`;
            };
        });
})