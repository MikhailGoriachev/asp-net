import * as utils  from './modules/utils.js'; 
import * as travels  from './modules/travels.js'; 

$(function() {   
    const $output = $("#main"),
          $pageHome = $("#pageHome");

    // Обработка нажатия на ссылку главной страницы
    $pageHome.click(() => {
        utils.customizePage("Главная", "#pageHome");
        $output.html(utils.task);
    })

    // Обработка нажатия на ссылку клиенты
    $("#pageClients").click(() => {
        utils.customizePage("Клиенты", "#pageClients");
        debugger
        utils.loadPaginated({ $output, url: "api/clients/page/" });
    });

    // Обработка нажатия на ссылку страны
    $("#pageCountries").click(() => {
        utils.customizePage("Страны", "#pageCountries");
        utils.loadPaginated({ $output, url: "api/countries/page/" });
    });

    // Обработка нажатия на ссылку маршруты
    $("#pageRoutes").click(() => {
        utils.customizePage("Маршруты", "#pageRoutes");
        utils.loadPaginated({ $output, url: "api/routes/page/" });
    })

    // Обработка нажатия на ссылку поездки
    $("#pageTravels").click(() => {
        utils.customizePage("Поездки", "#pageTravels");
        travels.init($output);
    })

    // Назначение обработчиков на кнопки и формы запросов 1-5
    Array(6).fill().forEach((v, i) => {
        // обработчик кнопки
        $(`#btnQuery${i + 1}`).click(() => $(`#modalQuery${i + 1}`).modal("show"));
        
        // обработчик формы
        $(`#query${i + 1}Form`).submit(function (e) {
            e.preventDefault();
            $.ajax({
                url: `api/query${i + 1}`,
                type: "PUT",
                data: $(`#query${i + 1}Form`).serializeArray(),
                success: function (data) {
                    utils.customizePage(`Запрос ${i + 1}`, "#pageQueries");
                    $output.html(data);
                    $(`#modalQuery${i + 1}`).modal("hide");
                }
            })
        })        
    })

    // Назначение обработчиков на кнопки запросов 6-8
    Array(3).fill().forEach((v, i) => {
        $(`#btnQuery${i + 6}`).click(function () {
            $.ajax({
                url: `api/query${i + 6}`,
                type: "PUT",
                success: function (data) {
                    utils.customizePage(`Запрос ${i + 6}`, "#pageQueries");
                    $output.html(data);
                }});
        })
    })
    
    //region Заполнение выпадающих списков в формах для запросов

    $.getJSON("api/purposes", function (data) {
        data.forEach(d => {
            $("#selPurposeQ1").append($('<option>', {value: d.id}).text(d.name));
            $("#selPurposeQ2").append($('<option>', {value: d.id}).text(d.name));
        });
    });

    $.getJSON("api/countries", function (data) {
        data.forEach(d => {
            $("#selCountriesQ3").append($('<option>', {value: d.id}).text(d.name));
            $("#selCountriesQ4").append($('<option>', {value: d.id}).text(d.name));
        });
    });

    //endregion

    // При загрузке отобразить информацию главной страницы
    $pageHome.trigger("click");  
})