// Скрипт работы выполнения запросов
$(function () {
    let $output = $("#output");
    
    //region Заполнение выпадающих списков в формах
    
    $.getJSON("api/sellers", function (data) {
        data.forEach(d => $("#selSellerQ5")
            .append($('<option>', {value: d.id})
                .text(`${d.surname} ${d.name[0]}.${d.patronymic[0]}. ${d.passport}`)));
    });

    $.getJSON("api/purchases", function (data) {
        data.map(v => v.goods)
            .filter((v, i, a) => a.indexOf(v) === i)
            .forEach(v => $("#selGoodsQ3").append($('<option>', {value: v}).text(v)));
    });

    $.getJSON("api/units", function (data) {
        data.map(v => v.long)
            .filter((v, i, a) => a.indexOf(v) === i)
            .forEach(v => $("#selUnitsQ1").append($('<option>', {value: v}).text(v)));
    });
    //endregion

    // Назначение обработчиков на кнопки запросов 1-6
    Array(6).fill().forEach((v, i) => {
        $(`#btnQuery${i + 1}`).click(function () {
            $(`#modalQuery${i + 1}`).modal("show");
        })
    })

    // Назначение обработчиков на кнопки запросов 7-11
    Array(5).fill().forEach((v, i) => {
        $(`#btnQuery${i + 7}`).click(function () {
            $.get(`api/query${i + 7}`, function (data) {
                $output.empty().append(generateTableFromObjects(data));
            })
        })
    })
    
    //region Назначение обработчиков submit для форм ввода параметров

    $("#query6Form").submit(function (e) {
        e.preventDefault();
        $.get(`api/query6/${$("#fromPrice").val()}/${$("#toPrice").val()}`, function (data) {
            $output.empty().append(generateTableFromObjects(data));
            $("#modalQuery6").modal("hide");
        })
    })
    
    $("#query5Form").submit(function (e) {
        e.preventDefault();
        $.get(`api/query5/${$("#selSellerQ5").val()}`, function (data) {
            $output.empty().append(generateTableFromObjects(data));
            $("#modalQuery5").modal("hide");
        })
    })
    
    $("#query4Form").submit(function (e) {
        e.preventDefault();
        $.get(`api/query4/${$("#inpInterestQ4").val()}`, function (data) {
            $output.empty().append(generateTableFromObjects(data));
            $("#modalQuery4").modal("hide");
        })
    })
    
    $("#query3Form").submit(function (e) {
        e.preventDefault();
        $.get(`api/query3/${$("#selGoodsQ3").val()}/${$("#inpMaxPriceQ3").val()}`, function (data) {
            $output.empty().append(generateTableFromObjects(data));
            $("#modalQuery3").modal("hide");
        })
    })
    
    $("#query2Form").submit(function (e) {
        e.preventDefault();
        $.get(`api/query2/${$("#inpMaxPriceQ2").val()}`, function (data) {
            $output.empty().append(generateTableFromObjects(data));
            $("#modalQuery2").modal("hide");
        })
    })

    $("#query1Form").submit(function (e) {
        e.preventDefault();
        $.get(`api/query1/${$("#selUnitsQ1").val()}/${$("#inpMaxPriceQ1").val()}`, function (data) {
            $output.empty().append(generateTableFromObjects(data));
            $("#modalQuery1").modal("hide");
        })
    })
    //endregion
})