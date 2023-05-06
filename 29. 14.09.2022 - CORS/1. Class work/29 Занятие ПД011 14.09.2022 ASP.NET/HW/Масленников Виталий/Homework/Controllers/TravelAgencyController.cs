using Homework.Models;
using Homework.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Homework.Controllers;

[ApiController]
[Route("api/")]
public class TravelAgencyController : ControllerBase
{
    private readonly TravelAgencyContext _context;
    private const int PerPage = 10;
    public TravelAgencyController(TravelAgencyContext context) => _context = context;

    #region Clients
        
    // GET: api/clients
    [HttpGet("clients")]
    public async Task<ActionResult<IEnumerable<ClientDTO>>> GetClientsAsync() =>
        (await _context.Clients.ToListAsync()).Select(v => new ClientDTO(v)).ToList();
    
    // GET: api/clients/page/2
    [HttpGet("clients/page/{page:int}")]
    public async Task<IActionResult> GetClientsPaginatedAsync(int page)
    {
        var items = (await _context.Clients.AsNoTracking()
            .Skip((page - 1) * PerPage)
            .Take(PerPage)
            .ToListAsync()).Select(v => new ClientDTO(v)).ToList();

        return new JsonResult(new
        {
            CurrentPage = page,
            PagesCount = (int)Math.Ceiling((double)await _context.Clients.CountAsync() / PerPage),
            Results = items
        });
    }

    // GET: api/client/5
    [HttpGet("clients/{id}")]
    public async Task<ActionResult<ClientDTO>> GetUnitAsync(int id)
    {
        var client = await _context.Clients
            .FirstOrDefaultAsync(s => s.Id == id);

        if (client is null)
            return NotFound();

        return new ClientDTO(client);
    }

    #endregion
    
    #region Purposes
        
    // GET: api/purposes
    [HttpGet("purposes")]
    public async Task<ActionResult<IEnumerable<PurposeDTO>>> GetPurposesAsync() =>
        (await _context.Purposes.ToListAsync()).Select(v => new PurposeDTO(v)).ToList();
    
    // GET: api/purposes/page/2
    [HttpGet("purposes/page/{page:int}")]
    public async Task<IActionResult> GetPurposesPaginatedAsync(int page)
    {
        var items = (await _context.Purposes.AsNoTracking()
            .Skip((page - 1) * PerPage)
            .Take(PerPage)
            .ToListAsync()).Select(v => new PurposeDTO(v)).ToList();

        return new JsonResult(new
        {
            CurrentPage = page,
            PagesCount = (int)Math.Ceiling((double)await _context.Purposes.CountAsync() / PerPage),
            Results = items
        });
    }

    // GET: api/purpose/5
    [HttpGet("purpose/{id}")]
    public async Task<ActionResult<PurposeDTO>> GetPurposeAsync(int id)
    {
        var purpose = await _context.Purposes
            .FirstOrDefaultAsync(s => s.Id == id);

        if (purpose is null)
            return NotFound();

        return new PurposeDTO(purpose);
    }

    #endregion
    
    #region Countries
        
    // GET: api/countries
    [HttpGet("countries")]
    public async Task<ActionResult<IEnumerable<CountryDTO>>> GetCountriesAsync() =>
        (await _context.Countries.ToListAsync()).Select(v => new CountryDTO(v)).ToList();

    // GET: api/countries/page/2
    [HttpGet("countries/page/{page:int}")]
    public async Task<IActionResult> GetCountriesPaginatedAsync(int page)
    {
        var items = (await _context.Countries.AsNoTracking()
            .Skip((page - 1) * PerPage)
            .Take(PerPage)
            .ToListAsync()).Select(v => new CountryDTO(v)).ToList();
        
        return new JsonResult(new
        {
            CurrentPage = page,
            PagesCount = (int)Math.Ceiling((double) await _context.Countries.CountAsync() / PerPage),
            Results = items
        });
    }
    
    // GET: api/country/5
    [HttpGet("country/{id}")]
    public async Task<ActionResult<CountryDTO>> GetCountryAsync(int id)
    {
        var country = await _context.Countries
            .FirstOrDefaultAsync(s => s.Id == id);

        if (country is null)
            return NotFound();

        return new CountryDTO(country);
    }

    #endregion
    
    #region Routes
        
    // GET: api/routes
    [HttpGet("routes")]
    public async Task<ActionResult<IEnumerable<RouteDTO>>> GetRoutesAsync() =>
        (await _context.Routes
            .Include(r => r.Purpose)
            .Include(r => r.Country)
            .ToListAsync()).Select(v => new RouteDTO(v)).ToList();
    
    // GET: api/routes/page/2
    [HttpGet("routes/page/{page:int}")]
    public async Task<IActionResult> GetRoutesPaginatedAsync(int page)
    {
        var items = (await _context.Routes.AsNoTracking()
            .Skip((page - 1) * PerPage)
            .Take(PerPage)
            .Include(r => r.Purpose)
            .Include(r => r.Country)
            .ToListAsync()).Select(v => new RouteDTO(v)).ToList();
        
        return new JsonResult(new
        {
            CurrentPage = page,
            PagesCount = (int)Math.Ceiling((double) await _context.Routes.CountAsync() / PerPage),
            Results = items
        });
    }

    // GET: api/route/5
    [HttpGet("route/{id}")]
    public async Task<ActionResult<RouteDTO>> GetRouteAsync(int id)
    {
        var route = await _context.Routes
            .Include(r => r.Purpose)
            .Include(r => r.Country)
            .FirstOrDefaultAsync(s => s.Id == id);

        if (route is null)
            return NotFound();

        return new RouteDTO(route);
    }

    #endregion

    #region Travels
        
    // GET: api/travels
    [HttpGet("travels/")]
    public async Task<ActionResult<IEnumerable<TravelDTO>>> GetTravelsAsync() =>
        (await _context.Travels
            .Include(tr => tr.Client)
            .Include(tr => tr.Route)
            .ThenInclude(r => r!.Purpose)
            .Include(tr => tr.Route)
            .ThenInclude(r => r!.Country).ToListAsync()).Select(v => new TravelDTO(v)).ToList();

    // GET: api/travels/page/2
    [HttpGet("travels/page/{page:int}")]
    public async Task<IActionResult> GetTravelsPaginatedAsync(int page)
    {
        var items = (await _context.Travels.AsNoTracking()
            .Skip((page - 1) * PerPage)
            .Take(PerPage)
            .Include(tr => tr.Client)
            .Include(tr => tr.Route)
            .ThenInclude(r => r!.Purpose)
            .Include(tr => tr.Route)
            .ThenInclude(r => r!.Country).ToListAsync()).Select(v => new TravelDTO(v)).ToList();
        
        return new JsonResult(new
        {
            CurrentPage = page,
            PagesCount = (int)Math.Ceiling((double) await _context.Travels.CountAsync() / PerPage),
            Results = items
        });
    }

    // GET: api/travel/5
    [HttpGet("travel/{id}")]
    public async Task<ActionResult<TravelDTO>> GetTravelAsync(int id)
    {
        var travel = await _context.Travels
            .Include(tr => tr.Client)
            .Include(tr => tr.Route)
            .ThenInclude(r => r!.Purpose)
            .Include(tr => tr.Route)
            .ThenInclude(r => r!.Country)
            .FirstOrDefaultAsync(s => s.Id == id);

        if (travel is null)
            return NotFound();

        return new TravelDTO(travel);
    }
    
    // PUT: api/travel/update/
    [HttpPut("travel/update/")]
    public async Task<IActionResult> PutTravelAsync([FromForm]Travel travel)
    {
        if (!(_context?.Travels.Any(e => e.Id == travel.Id)).GetValueOrDefault())
            return NotFound();
            
        _context!.Entry(travel).State = EntityState.Modified;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    // POST: api/travel/create
    [HttpPost("travel/create")]
    public async Task<IActionResult> PostTravelAsync([FromForm]Travel travel)
    {
        _context.Travels.Add(travel);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    // DELETE: api/travel/delete
    [HttpDelete("travel/delete/{id}")]
    public async Task<IActionResult> DeleteTravelAsync(int id)
    {
        var sale = await _context.Travels.FindAsync(id);
            
        if (sale == null)
            return NotFound();

        _context.Travels.Remove(sale);
            
        await _context.SaveChangesAsync();

        return new JsonResult(sale);
    }

    #endregion
}