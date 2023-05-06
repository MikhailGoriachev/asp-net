using ComponentContext.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace ComponentContext.Components;

// Компонент получает параметры, возвращает строго типизированное 
// представление 
public class ProductTable : ViewComponent
{
	// ссылка на репозиторий, получаем через внедрение зависимости 
	private readonly ProductRepository _repository;

	// конструктор с внедрением зависимости
	public ProductTable(ProductRepository repository) {
		this._repository = repository;
	} // ProductTable

	// параметры принимаем из InvokeAsync вызова рендеринга компонента
	public IViewComponentResult Invoke(bool showId, bool showTotal) {
		ProductTableViewModel viewModel = new ProductTableViewModel {
			Products = _repository.Products,
			ShowId = showId,
			ShowTotal = showTotal,
			Total = showTotal ? _repository.Products.Sum(x => x.Price) : 0
		};

		return View(viewModel);
	} // Invoke
} // class ProductTable

