using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Homework.Models;
using Homework.Models.DTO;

namespace Homework.Controllers
{
    [ApiController]
    [Route("api/")]
    public class WholesaleController : ControllerBase
    {
        private readonly WholeSaleContext _context;

        public WholesaleController(WholeSaleContext context) => _context = context;


        #region Units
        
        // GET: api/units
        [HttpGet("units")]
        public async Task<ActionResult<IEnumerable<UnitDTO>>> GetUnitsAsync() =>
            (await _context.Units.ToListAsync()).Select(v => new UnitDTO(v)).ToList();

        // GET: api/unit/5
        [HttpGet("unit/{id}")]
        public async Task<ActionResult<UnitDTO>> GetUnitAsync(int id)
        {
            var unit = await _context.Units
                .FirstOrDefaultAsync(s => s.Id == id);

            if (unit is null)
                return NotFound();

            return new UnitDTO(unit);
        }

        #endregion
        
        #region Goods

        // GET: api/goods
        [HttpGet("goods")]
        public async Task<ActionResult<IEnumerable<GoodsDTO>>> GetGoodsAsync() =>
            (await _context.Goods.ToListAsync()).Select(v => new GoodsDTO(v)).ToList();

        // GET: api/goods/5
        [HttpGet("goods/{id}")]
        public async Task<ActionResult<GoodsDTO>> GetGoodsAsync(int id)
        {
            var goods = await _context.Goods
                .FirstOrDefaultAsync(s => s.Id == id);

            if (goods is null)
                return NotFound();

            return new GoodsDTO(goods);
        }

        #endregion
        
        #region Sellers

        // GET: api/sellers
        [HttpGet("sellers")]
        public async Task<ActionResult<IEnumerable<SellerDTO>>> GetSellersAsync() =>
            (await _context.Sellers.ToListAsync()).Select(v => new SellerDTO(v)).ToList();

        // GET: api/seller/5
        [HttpGet("seller/{id}")]
        public async Task<ActionResult<SellerDTO>> GetSellerAsync(int id)
        {
            var seller = await _context.Sellers
                .FirstOrDefaultAsync(s => s.Id == id);

            if (seller is null)
                return NotFound();

            return new SellerDTO(seller);
        }

        #endregion

        #region Purchases
        
        // GET: api/purchases
        [HttpGet("purchases")]
        public async Task<ActionResult<IEnumerable<PurchaseDTO>>> GetPurchasesAsync() =>
            await _context.Purchases
                    .AsNoTracking()
                    .Include(s => s.Unit)
                    .Include(p => p.Goods)
                .Select(purchase => new PurchaseDTO(purchase)).ToListAsync();

        // GET: api/purchase/5
        [HttpGet("purchase/{id}")]
        public async Task<ActionResult<PurchaseDTO>> GetPurchaseAsync(int id)
        {
            var purchase = await _context.Purchases
                .AsNoTracking()
                .Include(s => s.Unit)
                .Include(p => p.Goods)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (purchase is null)
                return NotFound();

            return new PurchaseDTO(purchase);
        }
        
        #endregion
        
        #region Sales
        
        // GET: api/sales
        [HttpGet("sales")]
        public async Task<ActionResult<IEnumerable<SaleDTO>>> GetSalesAsync() =>
            await _context.Sales.AsNoTracking()
                .Include(s => s.Unit)
                .Include(s => s.Seller)
                .Include(s => s.Purchase)
                .ThenInclude(p => p!.Goods)
                .Select(sale => new SaleDTO(sale)).ToListAsync();

        // GET: api/sale/5
        [HttpGet("sale/{id}")]
        public async Task<ActionResult<SaleDTO>> GetSaleAsync(int id)
        {
            var sale = await _context.Sales.AsNoTracking()
                .Include(s => s.Unit)
                .Include(s => s.Seller)
                .Include(s => s.Purchase)
                .ThenInclude(p => p!.Goods)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (sale is null)
                return NotFound();

            return new SaleDTO(sale);
        }

        // PUT: api/sale/update/
        [HttpPut("sale/update/")]
        public async Task<IActionResult> PutSaleAsync([FromForm]Sale sale)
        {
            if (!(_context?.Sales.Any(e => e.Id == sale.Id)).GetValueOrDefault())
                return NotFound();
            
            _context!.Entry(sale).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/sale/create
        [HttpPost("sale/create")]
        public async Task<ActionResult<Sale>> PostSaleAsync([FromForm]Sale sale)
        {
            _context.Sales.Add(sale);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/sale/delete
        [HttpDelete("sale/delete/{id}")]
        public async Task<IActionResult> DeleteSaleAsync(int id)
        {
            var sale = await _context.Sales.FindAsync(id);
            
            if (sale == null)
                return NotFound();

            _context.Sales.Remove(sale);
            
            await _context.SaveChangesAsync();

            return new JsonResult(sale);
        }
        #endregion
    }
    
}
