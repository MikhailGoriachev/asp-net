using Microsoft.AspNetCore.Mvc;

namespace ComponentViewResults.Components;

public class Component3 : ViewComponent
{
    public IViewComponentResult Invoke() {
        //  Возврат файла представления по пути
        // Views/Home/Components/Component3/Default.cshtml
        // или по пути 
        // Views/Shared/Components/Component3/Default.cshtml
        return View();
    }
} // class Component3

