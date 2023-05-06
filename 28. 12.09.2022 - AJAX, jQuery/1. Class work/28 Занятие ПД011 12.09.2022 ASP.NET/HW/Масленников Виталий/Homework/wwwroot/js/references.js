// Скрипт отображения данных справочников

$(async function(){
    let output = $("#output");
    
    // При начальной загрузке страницы - отображение данных о номенклатурах
    $.getJSON("api/goods", function (data) {
        $("#output").append(generateTableFromObjects(data));
    })
    
    // Загрузить и отобразить данные о номенклатурах
    $("#btnGoods").click(() => {
        $.getJSON("api/goods", function (data) {
            output.empty().append(generateTableFromObjects(data));
        })  
    })

    // Загрузить и отобразить данные о единицах измерения
    $("#btnUnits").click(() => {
        $.getJSON("api/units", function (data) {
            output.empty().append(generateTableFromObjects(data));
        })  
    })

    // Загрузить и отобразить данные о продавцах
    $("#btnSellers").click(() => {
        $.getJSON("api/sellers", function (data) {
            output.empty().append(generateTableFromObjects(data));
        })  
    })
})