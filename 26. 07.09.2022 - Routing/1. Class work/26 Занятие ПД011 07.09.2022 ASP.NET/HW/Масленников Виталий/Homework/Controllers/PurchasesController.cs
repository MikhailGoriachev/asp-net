using Homework.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Homework.Models;
using Homework.Models.ViewModels;

namespace Homework.Controllers
{
    public class PurchasesController : Controller
    {
        private readonly WholeSaleContext _context;
        
        private const int PerPage = 8;
        
        public PurchasesController(WholeSaleContext context) => _context = context;

        // GET: Purchases
        public async Task<IActionResult> Index(int page = 1, string sort = "Id", FilterViewModel? filterModel = null)
        {
            var items = await _context.Purchases
                .AsNoTracking()
                .Include(s => s.Unit)
                .Include(p => p.Goods)
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
            filterModelTransit.GoodsList = new SelectList(items.Select(i => i.Goods!.Name)
                .Distinct()
                .ToList()
                .Prepend(""));

            // Фильтрация по выбранной номенклатуре товара
            if (!string.IsNullOrEmpty(filterModelTransit.GoodsSelected))
                items = items.Where(i => i.Goods!.Name
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
                items = items.Where(purchase => purchase.Price >= filterModelTransit.PriceFrom).ToList();
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
                    items = items.Where(purchase => purchase.Price >= filterModelTransit.PriceTo).ToList();
                }
            }

            // Количество отобранных элементов
            var itemsCount = items.Count;
            
            // Выборка для пагинации
            items = items.Skip((page - 1) * PerPage)
                .Take(PerPage).ToList();
            
            return View(new IndexViewModel<Purchase>
            {
                CurrentPage = page,
                PagesCount = (int)Math.Ceiling((double) itemsCount / PerPage),
                Items = items,
                FilterModel = filterModelTransit
            });
        }

        // GET: Purchases/Create
        public IActionResult Create()
        {
            ViewData["GoodsId"] = new SelectList(_context.Goods, "Id", "Name");
            ViewData["UnitId"] = new SelectList(_context.Units, "Id", "Long");
            return View();
        }

        // POST: Purchases/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,GoodsId,UnitId,PurchaseDate,Price,Amount")] Purchase purchase)
        {
            if (ModelState.IsValid)
            {
                _context.Add(purchase);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GoodsId"] = new SelectList(_context.Goods, "Id", "Name", purchase.GoodsId);
            ViewData["UnitId"] = new SelectList(_context.Units, "Id", "Long", purchase.UnitId);
            return View(purchase);
        }

        // GET: Purchases/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null)
                return NotFound();

            var purchase = await _context.Purchases.FindAsync(id);

            if (purchase == null)
                return NotFound();

            ViewData["GoodsId"] = new SelectList(_context.Goods, "Id", "Name", purchase.GoodsId);
            ViewData["UnitId"] = new SelectList(_context.Units, "Id", "Long", purchase.UnitId);
            
            return View(purchase);
        }

        // POST: Purchases/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,GoodsId,UnitId,PurchaseDate,Price,Amount")] Purchase purchase)
        {
            if (id != purchase.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                _context.Update(purchase);
                await _context.SaveChangesAsync();
                    
                return RedirectToAction(nameof(Index));
            }
            
            ViewData["GoodsId"] = new SelectList(_context.Goods, "Id", "Name", purchase.GoodsId);
            ViewData["UnitId"] = new SelectList(_context.Units, "Id", "Long", purchase.UnitId);

            return View(purchase);
        }
    }
}
