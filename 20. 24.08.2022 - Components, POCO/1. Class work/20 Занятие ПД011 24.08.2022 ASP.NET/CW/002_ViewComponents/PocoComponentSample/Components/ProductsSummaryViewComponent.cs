using PocoComponentSample.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Html;

namespace PocoComponentSample.Components;

// пример POCO (Plain Old CLR Object) - открытый класс, имя заканчиватся на ViewComponent
public class ProductsSummaryViewComponent
{
    private readonly ProductRepository _productRepositroy;


    // конструктор 
    public ProductsSummaryViewComponent(ProductRepository productRepositroy) {
        this._productRepositroy = productRepositroy;
    } // ProductsSummaryViewComponent


    // логика, подготавливающая данные для представления (как бы дочернее действие)
    public HtmlString Invoke() => new HtmlString(
        $"<ul><li>товаров: {_productRepositroy.Products.Count} шт.</li>" +
        $"<li>на сумму: {_productRepositroy.Products.Sum(x => x.Price)} $</li>" +
        $"</ul>");
    // Invoke
}

