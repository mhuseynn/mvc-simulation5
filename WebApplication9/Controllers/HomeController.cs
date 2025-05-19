using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using WebApplication9.Contexts;
using WebApplication9.Models;

namespace WebApplication9.Controllers;

[Authorize(Roles = "Admin,Member")]
public class HomeController : Controller
{
    AppDbContext _context;

    public HomeController(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        List<Villa> villaList = await _context.Villas.ToListAsync();

        return View(villaList);

    }
}
