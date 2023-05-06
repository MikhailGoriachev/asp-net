using BaseViewComponent.Model;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.AspNetCore.Html;

namespace BaseViewComponent.Components;

// При наследовании от класса ViewComponent указывать суффикс ViewComponent
// в имени класса не обязательно
public class ProductsSummary : ViewComponent
{
    private readonly ProductRepository _productRepositroy;

    public ProductsSummary(ProductRepository productRepositroy) {
        _productRepositroy = productRepositroy;
    }

    // логика, подготавливающая данные для представления (как бы дочернее действие)
    public HtmlString Invoke() => new HtmlString(
        $"<ul><li>товаров: {_productRepositroy.Products.Count} шт.</li>" +
        $"<li>на сумму: {_productRepositroy.Products.Sum(x => x.Price)} $</li>" +
        $"</ul>");
}

