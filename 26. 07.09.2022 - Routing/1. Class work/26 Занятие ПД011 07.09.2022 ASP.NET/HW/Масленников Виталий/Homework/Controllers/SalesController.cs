using Homework.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Homework.Models;
using Homework.Models.ViewModels;

namespace Homework.Controllers
{
    public class SalesController : Controller
    {
        private readonly WholeSaleContext _context;
        
        private const int PerPage = 8;

        public SalesController(WholeSaleContext context) => _context = context;

        // GET: Sales
        public async Task<IActionResult> Index(int page = 1, string sort = "Id", FilterViewModel? filterModel = null)
        {
            var items = await _context.Sales
                .AsNoTracking()
                .Include(s => s.Unit)
                .Include(s => s.Seller)
                .Include(s => s.Purchase)
                .ThenInclude(p => p!.Goods)
                .OrderBy(sort)
                .ToListAsync();

            // Модель фильтрации для транзита в представление
            var filterModelTransit = filterModel ?? new FilterViewModel();
            
            filterModelTransit.Sort = sort;
            
            // Список значений единиц товаров для выбора в фильтре
            filterModelTransit.UnitsList = new SelectList(items.Select(i => i.Unit!.Short)
                .Distinct()
                .ToList()
                .Prepend(""));
            
            // Список значений номенклатур товаров для выбора в фильтре
            filterModelTransit.GoodsList = new SelectList(items.Select(i => i.Purchase!.Goods!.Name)
                .Distinct()
                .ToList()
                .Prepend(""));

            // Фильтрация по выбранной номенклатуре товара
            if (!string.IsNullOrEmpty(filterModelTransit.GoodsSelected))
                items = items.Where(i => i.Purchase!.Goods!.Name
                        .Equals(filterModelTransit.GoodsSelected, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            
            // Фильтрация по выбранной единице измерения товара
            if (!string.IsNullOrEmpty(filterModelTransit.UnitsSelected))
                items = items.Where(i => i.Unit!.Short
                        .Equals(filterModelTransit.UnitsSelected, StringComparison.OrdinalIgnoreCase))
                    .ToList();

            // Фильтрация по выбранной цене закупки от
            if (filterModelTransit.PriceFrom is not null)
            {
                items = items.Where(sale => sale.Purchase!.Price >= filterModelTransit.PriceFrom).ToList();
            }
            
            // Фильтрация по выбранной цене закупки до
            if (filterModelTransit.PriceTo is not null)
            {
                // Проверка на валидность логического соответствия границ в случае полного диапазона
                if(filterModelTransit.PriceFrom is not null &&
                   filterModelTransit.PriceTo < filterModelTransit.PriceFrom)
                {
                    filterModelTransit.PriceTo = null;
                    ModelState.AddModelError(nameof(filterModelTransit.PriceTo), "Верхний порог цены не может быть меньше нижнего");
                }else
                {
                    items = items.Where(sale => sale.Purchase!.Price >= filterModelTransit.PriceTo).ToList();
                }
            }

            // Количество отобранных элементов
            var itemsCount = items.Count;
            
            // Выборка для пагинации
            items = items.Skip((page - 1) * PerPage)
                .Take(PerPage).ToList();
            
            return View(new IndexViewModel<Sale>
            {
                CurrentPage = page,
                PagesCount = (int)Math.Ceiling((double) itemsCount / PerPage),
                Items = items,
                FilterModel = filterModelTransit
            });
        }
        
        // GET: Sales/Create
        public IActionResult Create()
        {
            ViewData["Units"] = new SelectList(_context.Units, "Id", "Long");
            ViewData["Purchases"] = new SelectList(_context.Units, "Id", "Id");
            ViewData["Sellers"] = new SelectList(_context.Sellers, "Id", "ShortName");

            return View();
        }

        // POST: Sales/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IdPurchase,IdUnit,IdSeller,SaleDate,Price,Amount")] Sale sale)
        {
            
            if (ModelState.IsValid)
            {
                _context.Add(sale);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(sale);
        }

        // GET: Sales/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null)
                return NotFound();
            
            ViewData["Units"] = new SelectList(_context.Units, "Id", "Long");
            ViewData["Purchases"] = new SelectList(_context.Units, "Id", "Id");
            ViewData["Sellers"] = new SelectList(_context.Sellers, "Id", "ShortName");
            
            var sale = await _context.Sales.FindAsync(id);
            
            if (sale is not null)
                return View(sale);
            
            return NotFound();
        }

        // POST: Sales/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IdPurchase,IdUnit,IdSeller,SaleDate,Price,Amount")] Sale sale)
        {
            if (id != sale.Id)
                return NotFound();

            if (!ModelState.IsValid)
                return View(sale);
     
            _context.Update(sale);
            await _context.SaveChangesAsync();
          
            return RedirectToAction(nameof(Index));
        }

        // GET: Sales/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if(id is null)
                return NotFound();

            var sale = new Sale { Id = id.Value };

            _context.Entry(sale).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
