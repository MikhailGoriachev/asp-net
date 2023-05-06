// Скрипт отображения данных о закупках
$(function () {
    $.getJSON("api/purchases", function (data) {
        $("#output").append(generateTableFromObjects(data));
    })
})