using ComponentViewModel.Model;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace ComponentViewModel.Components;

public class ProductsSummary : ViewComponent
{
    private readonly ProductRepository _repository;

    public ProductsSummary(ProductRepository repository) {
        this._repository = repository;
    }

    // использование ViewModel - для получения данных... 
    public IViewComponentResult Invoke() {
        ProductsSummaryViewModel viewModel = new ProductsSummaryViewModel() {
            Sum = _repository.Products.Sum(x => x.Price),
            ProductCount = _repository.Products.Count()
        };

        return View(viewModel);
    }
}

