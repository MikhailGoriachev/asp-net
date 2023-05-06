var builder = WebApplication.CreateBuilder(args);

// добавление функционала MVC
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
// HTML + CSS
app.UseStaticFiles();

// разрешение работы маршрутизации
app.UseRouting();

app.UseEndpoints(endpoints => {
    endpoints.MapControllerRoute(
        name: "Default",
        pattern: "{controller=home}/{action=index}/{id:int?}"
    );

    // Ограничения позволяют на уровне маршрутизации определить правила проверки значений
    // попадающих в сегмент если значение не соответствует ограничению, приложение
    // возвращает 404 статус код.
    // {id:int?} - :int ограничение на использование только целочисленных значений
    //             параметр id является не обязательным, но если значение есть оно
    //             должно быть целочисленным.
    // возможные ограничения - https://docs.microsoft.com/en-us/aspnet/core/fundamentals/routing?view=aspnetcore-2.1#route-constraint-reference

    endpoints.MapControllerRoute(
        name: "Documents",
        pattern: "documents/{controller=invoices}/{docNumber:regex([a-z]{{2}}\\d{{5}})}/{action=view}"
    );

    // Использование регулярного выражения для ограничения
    // {number:regex([a-z]{{2}}\\d{{5}})}
    // имя параметра number
    // ограничение параметра :regex([a-z]{{2}}\\d{{5}}) символ { необходимо экранировать дополнительным символом {
    // в противном случае { будет расцениваться как имя параметра.

    // documents/invoices/ad12345/view
    // documents/invoices/ad12345/edit

});

app.Run();
