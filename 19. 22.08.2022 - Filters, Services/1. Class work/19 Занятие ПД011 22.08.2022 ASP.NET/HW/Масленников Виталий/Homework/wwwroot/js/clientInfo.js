$ = id => document.getElementById(id);
$("clientsTable").addEventListener("click",
    (e) => {

        if(!e.target.dataset.id)
            return;

        let xhr = new XMLHttpRequest();
        xhr.open("GET", `/Clients/ClientInfo/${e.target.dataset.id}`);
        xhr.send();

        xhr.onload = () => {
            let client = JSON.parse(xhr.response);
            $("id").innerHTML =  `${client.id}`;
            $("shortName").innerHTML =  `${client.shortName}`;
            $("surname").innerHTML =  `${client.surname}`;
            $("name").innerHTML =  `${client.name}`;
            $("patronymic").innerHTML =  `${client.patronymic}`;
            $("age").innerHTML =  `${client.age}`;
            $("email").innerHTML =  `${client.email}`;
            $("phone").innerHTML =  `${client.phoneNumber}`;
            $("regular").innerHTML =  `${client.phoneNumber ? "Да" : "Нет"}`;
            $("password").innerHTML =  `${client.password}`;
        };

    });