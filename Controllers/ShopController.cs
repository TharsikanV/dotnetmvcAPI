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
            shop.CreatedAt = DateTime.UtcNow;
            shop.UpdatedAt = DateTime.UtcNow;

            _context.Shops.Add(shop);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        return View(shop);
    }

    // EDIT (GET)
    public IActionResult Edit(int id)
    {
        var shop = _context.Shops.FirstOrDefault(s => s.Id == id);
        if (shop == null)
        {
            return NotFound();
        }

        return View(shop);
    }

    // EDIT (POST)
    [HttpPost]
    public IActionResult Edit(int id, Shop shop)
    {
        if (id != shop.Id)
        {
            return BadRequest();
        }

        if (ModelState.IsValid)
        {
            var existingShop = _context.Shops.FirstOrDefault(s => s.Id == id);
            if (existingShop == null)
            {
                return NotFound();
            }

            existingShop.Name = shop.Name;
            existingShop.Category = shop.Category;
            existingShop.Location = shop.Location;
            existingShop.Manager_Name = shop.Manager_Name;
            existingShop.Description = shop.Description;
            existingShop.UpdatedAt = DateTime.UtcNow;

            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        return View(shop);
    }

    // Get shop details
    public IActionResult Details(int id)
    {
        var shop = _context.Shops.FirstOrDefault(s => s.Id == id);
        if (shop == null)
        {
            return NotFound();
        }

        ViewBag.Items = _context.Items.Where(item => item.ShopId == id).ToList();
        ViewBag.OpenItemModal = false;
        return View(shop);
    }

    [HttpPost]
    public IActionResult AddItem(int shopId, Item item)
    {
        var shop = _context.Shops.FirstOrDefault(s => s.Id == shopId);
        if (shop == null)
        {
            return NotFound();
        }

        if (!ModelState.IsValid)
        {
            ViewBag.Items = _context.Items.Where(existingItem => existingItem.ShopId == shopId).ToList();
            ViewBag.OpenItemModal = true;
            return View("Details", shop);
        }

        item.ShopId = shopId;
        _context.Items.Add(item);
        _context.SaveChanges();

        return RedirectToAction(nameof(Details), new { id = shopId });
    }
}
