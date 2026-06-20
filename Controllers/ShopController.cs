// using System.Diagnostics;
// using Microsoft.AspNetCore.Mvc;
// using MyMvcApp.Models;

// namespace MyMvcApp.Controllers;

// public class ShopController : Controller
// {
//      public IActionResult Shop()
//     {
//         return View();
//     }

//     [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
//     public IActionResult Error()
//     {
//         return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
//     }
// }

using Microsoft.AspNetCore.Mvc;
using MyMvcApp.Data;
using MyMvcApp.Models;

namespace MyMvcApp.Controllers;

public class ShopController : Controller
{
    private readonly ShopDbContext _context;

    public ShopController(ShopDbContext context)
    {
        _context = context;
    }

    // LIST
    public IActionResult Index()
    {
        var shops = _context.Shops.ToList();
        return View(shops);
    }

    // CREATE (GET)
    public IActionResult Create()
    {
        return View();
    }

    // CREATE (POST)
    [HttpPost]
    public IActionResult Create(Shop shop)
    {
        if (ModelState.IsValid)
        {
            _context.Shops.Add(shop);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        return View(shop);
    }
}