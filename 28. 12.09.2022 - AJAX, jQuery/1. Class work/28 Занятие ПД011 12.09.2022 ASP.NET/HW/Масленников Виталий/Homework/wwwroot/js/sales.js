
$(function(){    
    let $output = $("#output"),
        $saleForm = $('#saleForm');

    //region Init
    
    // Загрузить и отрендерить данные
    loadSales();
    
    // Заполнить список закупок для выпадающего списка формы
    $.getJSON("api/purchases", function (data) {
        data.forEach(d => $("#selPurchaseId")
            .append($('<option>', {value: d.id})
                .text(`${d.id}: ${d.goods} ${new Date(d.purchaseDate).toLocaleDateString()}`)));
    });

    // Заполнить список единиц измерения для выпадающего списка формы
    $.getJSON("api/units", function (data) {
        data.forEach(d => $("#selUnitId").append($('<option>', {value: d.id}).text(d.long)));
    });

    // Заполнить список продавцов для мвыпадающего списка формы
    $.getJSON("api/sellers", function (data) {
        data.forEach(d => $("#selSellerId")
            .append($('<option>', {value: d.id}).text(`${d.surname} ${d.name[0]}.${d.patronymic[0]}. ${d.passport}`)));
    });
    
    //endregion

    // Ajax-загрузка данных в таблицу
    function loadSales() {
        $.getJSON("api/sales", function (data) {
            $output.empty().append(generateTableFromObjects(data, true));

            // Назначить обработчик нажатия на кнопку добавления
            $("#btnCreate").click(displayCreateSaleForm);
            // Назначить бработчик ЛКМ по таблице
            $output.click(onTableClick);
        })
    }

    // Обработчик ЛКМ по таблице
    function onTableClick(e) {
        if(e.target.dataset.edit){
            $.getJSON(`api/sale/${e.target.dataset.edit}`, function (data) {
                displayUpdateSaleForm(data);
            });
        }
        if(e.target.dataset.del) {
            $.ajax({
                url: `api/sale/delete/${e.target.dataset.del}`,
                type: 'DELETE',
                success: loadSales
            });
        }
    }
    
    // Отображение формы добавления данных 
    function displayCreateSaleForm() {
        $('#modalLabel').text("Добавить данные о продаже");
        $('#btnSubmitCreate').val("Добавить");
        
        fillForm(null);
        
        $('#modalForm').modal('show');
        
        $saleForm.unbind("submit");
        $saleForm.on("submit", onSubmitCreate);        
    }

    // Отображение формы редактирования данных 
    function displayUpdateSaleForm(sale) {
        $('#modalLabel').text("Изменить данные о продаже");
        $('#btnSubmitCreate').val("Изменить");

        fillForm(sale);

        $('#modalForm').modal('show');

        $saleForm.unbind("submit");
        $saleForm.on("submit", onSubmitUpdate);
    }

    // Обработчик submit формы для добавления данных
    function onSubmitCreate(e) {
        e.preventDefault();
        
        const sale = assembleSaleFromFields();
        
        $.post("api/sale/create", sale, function () {
            $("#modalForm").modal("hide");
            $("#msgOutput").text(`Данные о продаже добавлены`);
            $("#modalMsgBox").modal('show');
            loadSales();
        });
    }
    
    // Обработчик submit формы для изменения данных
    function onSubmitUpdate(e) {
        e.preventDefault();
        
        const sale = assembleSaleFromFields();
        sale.id = $("#saleId").val();
        
        $.ajax({
            url: 'api/sale/update',
            type: 'PUT',
            data: sale,
            success: function () {
                $("#modalForm").modal("hide");
                $("#msgOutput").text(`Данные о продаже  изменены`);
                $("#modalMsgBox").modal("show");
                loadSales();
            } 
        });        
    }
    
    // Собрать объект продажи из введенных данных
    function assembleSaleFromFields() {
        return {
            id: $("#saleId").val(),
            purchaseId: $("#selPurchaseId").val(),
            unitId: $("#selUnitId").val(),
            sellerId: $("#selSellerId").val(),
            saleDate: $("#inpSaleDate").val(),
            price: $("#inpPrice").val(),
            amount: $("#inpAmount").val()
        }
    }

    // Заполнение формы данными объекта либо по умолчанию
    function fillForm(sale) {
        $("#saleId").val(sale?.id ?? 0);
        $("#selPurchaseId").val(sale?.purchaseId ?? $("#selPurchaseId option:first").val());
        $("#selUnitId").val(sale?.unitId ?? $("#selUnitId option:first").val());
        $("#selSellerId").val(sale?.sellerId ?? $("#selSellerId option:first").val());
        $("#inpSaleDate").val(toInputFormat(sale?.saleDate ?? new Date()));
        $("#inpPrice").val(sale?.sellPrice ?? "");
        $("#inpAmount").val(sale?.amount ?? "");
    }
    
    // Приведение объекта даты или строки, представляющей дату к подходящему для элемента input формату
    function toInputFormat(date) {
        if(Object.prototype.toString.call(date) !== '[object Date]') {
            date = new Date(date);
            // Фикс отката на один день из-за разницы временных поясов
            date = new Date(date.getTime() + Math.abs(date.getTimezoneOffset() * 60000));
        }
        return date.toISOString().split('T')[0];
    } 
})