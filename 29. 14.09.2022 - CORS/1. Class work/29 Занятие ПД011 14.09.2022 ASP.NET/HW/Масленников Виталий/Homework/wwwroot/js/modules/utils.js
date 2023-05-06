
// Настройка текущей страницы
export function customizePage(header, link) {
    $("a[id^='page']").removeClass("active");
    $(link).addClass("active");
    $("#title").html(`${header} - Домашняя работа`);
    $("#header").html(header);
}

// Сформировать таблицу с секцией пагинации
export function loadPaginated(options) {
    const linksCount = 5;
    options.page ??= 1;
    
    $.getJSON(`${options.url}${options.page}`, function (data) {
        options.$output.html(generateTableFromObjects(data.results, options.controls, options.nowrapClmns))
            .append(constructPaginationLinks(
                data.currentPage,
                data.pagesCount,
                (page) => loadPaginated({
                    $output: options.$output,
                    url: options.url, 
                    page: page, 
                    controls: options.controls, 
                    nowrapClmns: options.nowrapClmns,
                    callback: options.callback} ),
                Math.min(linksCount, data.pagesCount)));
        
        options.callback();
    })
}

// Генерация таблицы по по коллекции объектов
export function generateTableFromObjects(data, controlsAdd = false, nowrapClmns = []) {
    // паттерн проверки на дату
    const dateRegex = new RegExp(/^\d{4}-\d{2}-\d{2}/);
    
    // паттерн проверки на свойство хранящее id другого объекта
    const idPropRegex = new RegExp(/^.+Id/);
    
    let table = $("<table/>").addClass("table");
    let tHead = $("<thead/>").addClass("border-dark align-top");
    let tBody = $("<tbody/>").addClass("color-2");
    let header = $("<tr/>");
    
    // Формирование заголовков
    $.each(data[0], function (key, val) {
        if(!idPropRegex.test(key))
            header.append($("<th/>").text(headersDict[key] || key))
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
                let td = $(`<td/>`).text(val ?? "н/д");

                if(nowrapClmns.includes(key))
                    td.addClass("text-nowrap");
                
                row.append(td);
            }
        })
        
        // Если нужны управляющие кнопки
        if(controlsAdd){
            row.append(`<td class="text-nowrap">
                        <button class="btn btn-outline-primary" data-edit="${val.id}">
                        <i class="bi bi-pencil-square" data-edit="${val.id}"></i>
                        </button>
                        <button class="btn btn-outline-danger" data-del="${val.id}">
                        <i class="bi bi-trash3" data-del="${val.id}"></i>
                        </button></td>`)
        }
        
        $(tBody).append(row);
    })
    
    return table.append(tHead).append(tBody);
}

// Сформировать элемент со ссылками постраничной навигации
export function constructPaginationLinks(currentPage, pagesCount, linkCallback, linksCount) {
    let list = $("<ul/>").addClass("pagination justify-content-center");

    // Первая страница
    list.append($("<li/>").addClass(`page-item ${currentPage === 1 ? "disabled" : ""}`)
        .append($("<a/>").addClass("page-link").attr("href", "#")
            .click(() => linkCallback(1))
            .append($("<i/>").addClass("bi bi-skip-start-fill"))));

    // Предыдущая страница
    list.append($("<li/>").addClass(`page-item ${currentPage === 1 ? "disabled" : ""}`)
        .append($("<a/>").addClass("page-link").attr("href", "#")
            .click(() => linkCallback(currentPage - 1))
            .append($("<i/>").addClass("bi bi-caret-left-fill"))));
    
    let start = Math.max(currentPage - Math.floor(linksCount / 2), 1);
    
    // Если от номера стартовой страницы до последней меньше количества отображаемых ссылок - 
    // по возможности откатиться до нужного
    if((pagesCount - start + 1) < linksCount)
        start = Math.max(pagesCount - linksCount + 1, 1);
    
    // Ряд кнопок ближайших страниц
    for(let i = start, j = 0; j < linksCount && i <= pagesCount; i++, j++) {
        list.append($("<li/>").addClass(`page-item ${currentPage === i ? "active" : ""}`)
            .append($("<a/>").addClass("page-link").attr("href", "#")
                .click(() => linkCallback(i)).text(i)));
    }

    // Следующая страница
    list.append($("<li/>").addClass(`page-item ${currentPage === pagesCount ? "disabled" : ""}`)
        .append($("<a/>").addClass("page-link").attr("href", "#")
            .click(() => linkCallback(currentPage + 1))
            .append($("<i/>").addClass("bi bi-caret-right-fill"))));

    // Последняя страница
    list.append($("<li/>").addClass(`page-item ${currentPage === pagesCount ? "disabled" : ""}`)
        .append($("<a/>").addClass("page-link").attr("href", "#")
            .click(() => linkCallback(pagesCount))
            .append($("<i/>").addClass("bi bi-skip-end-fill"))));

    return $("<nav/>").append(list);
}

// Заполнение формы из объекта
export function populateForm($form, data)
{
    $form.find('input:text, input:password, input:file, select, textarea').val('');
    $form.find('input:radio, input:checkbox').removeAttr('checked').removeAttr('selected');

    $.each(data, function(key, value) {
        let $control = $form.find('[name='+key+']');
        if ($control.is('select')){
            $('option', $control).each(function() {
                if (this.value.toString() === value.toString())
                    this.selected = true;
            });
        } else if ($control.is('textarea')) {
            $control.val(value);
        } else {
            switch($control.attr("type")) {
                case "text":
                case "number":
                case "hidden":
                    $control.val(value);
                    break;
                case "date":
                    $control.val(toDateInputFormat(value));
                    break;
                case "checkbox":
                    $control.prop('checked', value === '1');
                    break;
            }
        }
    });
}

// Приведение объекта даты или строки, представляющей дату к подходящему для элемента input формату
function toDateInputFormat(date) {
    if (Object.prototype.toString.call(date) !== '[object Date]') {
        date = new Date(date);
        // Фикс отката на один день из-за разницы временных поясов
        date = new Date(date.getTime() + Math.abs(date.getTimezoneOffset() * 60000));
    }
    return date.toISOString().split('T')[0];
}

// Словарь заголовков колонок по имени свойства
const headersDict = {
    id: "Id",
    surname: "Фамилия",
    name: "Имя",
    patronymic: "Отчество",
    passport: "Паспорт",
    dailyCost: "Стоимость 1 дня пребывания, ₽",
    transferCost: "Транспортные услуги, ₽",
    visaCost: "Стоимость визы, ₽",
    purpose: "Цель поездки",
    client: "Клиент",
    country: "Страна",
    startDate: "Дата начала пребывания",
    duration: "Количество дней",
    fullCost: "Стоимость (полная), ₽"
}


// Текст задания
export const task = $(`
    <h2>Теоретическая часть</h2>
        <ul>
            <li>Синхронная и асинхронная работа с объектом XMLHttpRequest</li>
            <li>Событие “load” в AJAX для получения данных от сервера</li>
            <li>Использование jQuery для работы с AJAX-запросами</li>
            <li>Передача и прием данных в jQuery при помощи метода load()</li>
            <li>Варианты отправки данных на сервер в AJAX</li>
        </ul>

        <p>Создать приложение ASP.NET Core WebAPI и набор html-страниц для работы с базой данных 
            при помощи Entity Framework Core 6. Стилизацию разметки выполняйте при помощи Bootstrap.
            Реализуйте вывод задания, вывод всех записей таблиц базы данных. </p>
        
        <p>Все таблицы должны быть проинициированы не менее чем 10 записями. </p>

        <p>Требуется также выполнение запросов по заданию, запросы отправлять на сервер операцией <b>PUT</b>, 
            параметры вводить в формы (рекомендую формы в модальных окнах). AJAX-запросы пишите на чистом 
            JS (vanilla JS) или с использованием jQuery – на Ваш выбор.</p>
        
        <p>Методы <b>API</b>-контроллера для реализации запросов должны возвращать разметку, выводить результаты
            всех запросов в одну и ту же <b>html</b>-страницу.</p>

        <div class="col-8">
            <table class="table-bordered ps-2 pe-2 w-100">
                <tbody>
                <tr>
                    <td class="ps-1 pe-2">
                        <i>База данных <b>«Туристическое агентство»</b></i>
                    </td>
                </tr>
                <tr>
                    <td class="ps-1 pe-2">
                        <div class="fw-bold">Описание предметной области</div>
                        <div>
                            Фирма предоставляет клиентам услуги по организации зарубежных поездок. При этом цели поездок могут
                            быть различными (отдых, туризм, лечение и т.д.). При оформлении услуги устанавливается фиксированная
                            стоимость 1 дня пребывания в той или иной стране.
                        </div>
                        <div>
                            Стоимость поездки может быть вычислена как
                            <b>Стоимость 1 дня пребывания * Количество дней пребывания +Стоимость транспортных услуг +
                                Стоимость оформления визы</b>. Кроме того, клиент платит налог надобавленную стоимость
                            (НДС) в размере 3% от стоимости поездки.
                        </div>
                    </td>
                </tr>
                <tr>
                    <td class="ps-1 pe-2 fw-bold fst-italic">
                        База данных должна включать как минимум таблицы КЛИЕНТЫ, МАРШРУТЫ, ПОЕЗДКИ, содержащие следующую
                        информацию:
                    </td>
                </tr>
                <tr>
                    <td class="ps-1 pe-2">Фамилия клиента</td>
                </tr>
                <tr>
                    <td class="ps-1 pe-2">Имя клиента</td>
                </tr>
                <tr>
                    <td class="ps-1 pe-2">Отчество клиента</td>
                </tr>
                <tr>
                    <td class="ps-1 pe-2">Серия – номер паспорта клиента</td>
                </tr>
                <tr>
                    <td class="ps-1 pe-2">Страна назначения</td>
                </tr>
                <tr>
                    <td class="ps-1 pe-2">Цель поездки</td>
                </tr>
                <tr>
                    <td class="ps-1 pe-2">Стоимость 1 дня пребывания в стране назначения</td>
                </tr>
                <tr>
                    <td class="ps-1 pe-2">Стоимость транспортных услуг (определяется выбором страны)</td>
                </tr>
                <tr>
                    <td class="ps-1 pe-2">Стоимость оформления визы (определяется выбором страны)</td>
                </tr>
                <tr>
                    <td class="ps-1 pe-2">Дата начала пребывания в стране назначения</td>
                </tr>
                <tr>
                    <td class="ps-1 pe-2">Количество дней пребывания в стране назначения</td>
                </tr>
                <tr>
                    <td class="ps-1 pe-2">Страна назначения</td>
                </tr>
                </tbody>
            </table>

            <table class="table-bordered mt-4 w-100">
                <thead class="fw-bold text-center">
                <tr>
                    <td>Номер запроса</td>
                    <td>Тип запроса</td>
                    <td>Какую задачу решает запрос</td>
                </tr>
                </thead>
                <tbody>
                <tr>
                    <td class="ps-2 pe-2 text-center">1</td>
                    <td class="ps-2 pe-2">Запрос на выборку</td>
                    <td class="ps-2 pe-2">Выбирает информацию о маршрутах с заданной целью поездки </td>
                </tr>
                <tr>
                    <td class="ps-2 pe-2 text-center">2</td>
                    <td class="ps-2 pe-2">Запрос на выборку</td>
                    <td class="ps-2 pe-2">
                        Выбирает информацию о маршрутах с заданной целью поездки и стоимостью транспортных услуг менее заданного значения
                    </td>
                </tr>
                <tr>
                    <td class="ps-2 pe-2 text-center">3</td>
                    <td class="ps-2 pe-2">Запрос на выборку</td>
                    <td class="ps-2 pe-2">
                        Выбирает информацию о клиентах, совершивших поездки с заданным количеством дней пребывания в стране
                    </td>
                </tr>
                <tr>
                    <td class="ps-2 pe-2 text-center">4</td>
                    <td class="ps-2 pe-2">Запрос на выборку</td>
                    <td class="ps-2 pe-2">
                        Выбирает информацию о маршрутах в заданную страну
                    </td>
                </tr>
                <tr>
                    <td class="ps-2 pe-2 text-center">5</td>
                    <td class="ps-2 pe-2">Запрос на выборку</td>
                    <td class="ps-2 pe-2">
                        Выбирает информацию о странах, для которых стоимость оформления визы есть значение из некоторого диапазона
                    </td>
                </tr>
                <tr>
                    <td class="ps-2 pe-2 text-center">6</td>
                    <td class="ps-2 pe-2">Запрос на выборку</td>
                    <td class="ps-2 pe-2">
                        Вычисляет для каждой поездки ее полную стоимость с НДС. Включает поля
                        <b>Страна назначения, Цель
                            поездки, Дата начала поездки, Количество дней пребывания</b>, Полная стоимость поездки. Сортировка по
                        полю <b>Страна назначения</b>
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;</td><td></td><td></td>
                </tr>
                <tr>
                    <td class="ps-2 pe-2 text-center">7</td>
                    <td class="ps-2 pe-2">Итоговый запрос – агрегатные функции</td>
                    <td class="ps-2 pe-2">
                        Выполняет группировку по полю <b>Цель поездки</b>.  Определяет минимальную, среднюю и максимальную стоимость
                        1 дня пребывания
                    </td>
                </tr>
                <tr>
                    <td class="ps-2 pe-2 text-center">8</td>
                    <td class="ps-2 pe-2">Итоговый запрос – агрегатные функции</td>
                    <td class="ps-2 pe-2">
                        Выполняет группировку по полю <b>Страна назначения</b>. Для каждой страны вычисляет среднее значение
                        по полю <b>Стоимость транспортных услуг</b>
                    </td>
                </tr>
                </tbody>

            </table>
        </div>
        
        <p>Для таблицы <b>ПОЕЗДКИ</b> требуется реализовать добавление (метод <b>PUT</b>), редактирование (метод <b>POST</b>) и удаление записей (метод
            <b>DELETE</b>).  </p>

        <h2>Дополнительно</h2>
        <p>
            Запись занятия можно скачать по
            <a href="https://cloud.mail.ru/public/H78p/T8DcFAcXy"> <b>этой ссылке</b> </a>.
            Материалы занятия – в этом же архиве.
        </p>    
    `);
