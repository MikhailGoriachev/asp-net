
// Генерация таблицы по по коллекции объектов
function generateTableFromObjects(data, controlsAdd = false) {
    // паттерн проверки на дату
    const dateRegex = new RegExp(/^\d{4}-\d{2}-\d{2}/);
    
    // паттерн проверки на свойство хранящее id другого объекта
    const idPropRegex = new RegExp(/^.+Id/);
    
    let table = $("<table/>").addClass("table");
    let tHead = $("<thead/>").addClass("border-dark");
    let tBody = $("<tbody/>").addClass("color-2");
    let header = $("<tr/>");
    
    // Формирование заголовков
    $.each(data[0], function (key, val) {
        if(!idPropRegex.test(key))
            header.append($("<th/>").text(headersDict[key]))
    });
    
    if(controlsAdd) {
        header.append(`<th><button id="btnCreate" class="btn btn-primary btn-sm">Добавить</button></th>`)
    }
    
    tHead.append(header);
    
    // Формирование строк данных
    data.forEach(val => {
        let row = $("<tr/>");
        
        $.each(val, function (key, val) {
            if(!idPropRegex.test(key)) { 
                if(dateRegex.test(val))
                    val = new Date(val).toLocaleDateString()
                row.append($(`<td/>`).text(val ?? "н/д"));
            }
        })
        
        // Если нужны управляющие кнопки
        if(controlsAdd){
            row.append(`<td>
                     <button class="btn btn-outline-primary" data-edit="${val.id}">
                     <i class="bi bi-pencil-square" data-edit="${val.id}"></i>
                     </button>
                     <button class="btn btn-outline-danger" data-del="${val.id}">
                     <i class="bi bi-trash3" data-del="${val.id}"></i>
                     </button>
                     </td>`)
        }
        
        $(tBody).append(row);
    })

    table.append(tHead).append(tBody);
    
    return table;
}

// Словарь заголовков колонок по имени свойства
const headersDict = {
    id: "Id",
    goods: "Наименование",
    long: "Единицы",
    short: "Единицы (сокращенно)",
    surname: "Фамилия",
    name: "Имя",
    patronymic: "Отчество",
    passport: "Паспорт",
    interest: "Комиссионные, %",
    purchaseDate: "Дата закупки",
    sellDate: "Дата продажи",
    saleDate: "Дата продажи",
    amount: "Количество",
    unit: "Единицы",
    units: "Единицы",
    price: "Цена закупки",
    purchasePrice: "Цена закупки",
    sellPrice: "Цена продажи",
    profit: "Прибыль",
    avgPrice: "Средняя цена",
    amountSales: "Количество продаж",
    seller: "Продавец",
    soldSum: "Продано на сумму",
    soldAmount: "Количество товара продано",
    salesAmount: "Количество продаж",
    minSale: "Мин. цена продажи",
    maxSale: "Макс. цена продажи",
    sumSales: "Сумма продаж",
}
