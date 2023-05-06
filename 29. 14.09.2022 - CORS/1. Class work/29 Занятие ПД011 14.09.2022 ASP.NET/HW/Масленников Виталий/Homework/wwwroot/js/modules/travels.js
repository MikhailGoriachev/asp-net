import * as utils  from './utils.js';

const $travelForm = $('#travelForm'),
      $selRoutes = $("#selRoutes"),
      $selClients = $("#selClients");

let $output;

export function init(output) {
    $output = output;
    
    // Загрузить и отрендерить данные
    loadTravels();

    // Заполнить список маршрутов для выпадающего списка формы
    $.getJSON("api/routes", function (data) {
        $selRoutes.empty();
        data.forEach(d => $selRoutes.append($('<option>').val(d.id).text(`${d.country} - ${d.purpose}`)));
    });

    // Заполнить список клиентов для выпадающего списка формы
    $.getJSON("api/clients", function (data) {
        $selClients.empty();
        data.forEach(d => $selClients
            .append($('<option>').val(d.id).text(`${d.surname} ${d.name[0]}.${d.patronymic[0]}. ${d.passport}`)));
    });
}

// Ajax-загрузка данных в таблицу
function loadTravels() {
    utils.loadPaginated({
        $output,
        url: "api/travels/page/",
        page: 1,
        controls: true,
        nowrapClmns: ["client"],
        callback: () => {
            // Назначить обработчик нажатия на кнопку добавления
            $("#btnCreate").click(displayCreateTravelForm);
            // Назначить бработчик ЛКМ по таблице
            $("tbody").click(onTableClick);
        }
    });
}

// Обработчик ЛКМ по таблице
function onTableClick(e) {
    if (e.target.dataset.edit) {
        $.getJSON(`api/travel/${e.target.dataset.edit}`, function (data) {
            displayUpdateTravelForm(data);
        });
    }
    if (e.target.dataset.del) {
        $.ajax({
            url: `api/travel/delete/${e.target.dataset.del}`,
            type: 'DELETE',
            success: loadTravels
        });
    }
}

// Отображение формы добавления данных 
function displayCreateTravelForm() {
    $travelForm.trigger("reset").unbind("submit").on("submit", onSubmitCreate);
    
    displayTravelForm("Добавить данные о поездке", "Добавить");
}

// Отображение формы редактирования данных 
function displayUpdateTravelForm(sale) {
    utils.populateForm($travelForm, sale);
    
    $travelForm.unbind("submit").on("submit", onSubmitUpdate);
    
    displayTravelForm("Изменить данные о поездке", "Изменить");
}

// Обработчик submit формы для добавления данных
function onSubmitCreate(e) {
    e.preventDefault();
    
    let data = $("#travelForm").serializeArray();
    data.filter(v => v.name === 'id')[0].value = 0;
    
    $.post("api/travel/create", data, function () {
        $("#modalFormTravel").modal("hide");
        $("#msgOutput").text(`Данные о поездке добавлены`);
        $("#modalMsgBox").modal('show');
        loadTravels();
    });
}

// Обработчик submit формы для изменения данных
function onSubmitUpdate(e) {
    e.preventDefault();

    $.ajax({
        url: 'api/travel/update',
        type: 'PUT',
        data: $("#travelForm").serializeArray(),
        success: function () {
            $("#modalFormTravel").modal("hide");
            $("#msgOutput").text("Данные о поездке  изменены");
            $("#modalMsgBox").modal("show");
            loadTravels();
        }
    });
}

function displayTravelForm(wndTitle, btnTitle) {
    $('#modalTravelLabel').text(wndTitle);
    $('#btnSubmitTravel').val(btnTitle);
    $('#modalFormTravel').modal('show');
}