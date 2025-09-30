using MHKCafe.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

public class mhkCartController : Controller
{
    private const string CartSessionKey = "ShoppingCart";

    public IActionResult Index()
    {
        var cart = GetCart();
        return View(cart);
    }

    [HttpPost]
    public IActionResult AddToCart(int productId, string productName, decimal price, string imageUrl, int quantity = 1)
    {
        var cart = GetCart();
        var existingItem = cart.Items.FirstOrDefault(x => x.ProductId == productId);

        if (existingItem != null)
        {
            existingItem.Quantity += quantity;
        }
        else
        {
            cart.Items.Add(new CartItem
            {
                ProductId = productId,
                ProductName = productName,
                Price = price,
                Quantity = quantity,
                ImageUrl = imageUrl
            });
        }

        SaveCart(cart);
        return Json(new { success = true, totalItems = cart.TotalItems });
    }

    [HttpPost]
    public IActionResult UpdateQuantity(int productId, int quantity)
    {
        var cart = GetCart();
        var item = cart.Items.FirstOrDefault(x => x.ProductId == productId);

        if (item != null)
        {
            if (quantity <= 0)
            {
                cart.Items.Remove(item);
            }
            else
            {
                item.Quantity = quantity;
            }
            SaveCart(cart);
        }

        return Json(new
        {
            success = true,
            totalItems = cart.TotalItems,
            grandTotal = cart.GrandTotal
        });
    }

    [HttpPost]
    public IActionResult RemoveItem(int productId)
    {
        var cart = GetCart();
        var item = cart.Items.FirstOrDefault(x => x.ProductId == productId);

        if (item != null)
        {
            cart.Items.Remove(item);
            SaveCart(cart);
        }

        return Json(new
        {
            success = true,
            totalItems = cart.TotalItems,
            grandTotal = cart.GrandTotal
        });
    }

    [HttpPost]
    public IActionResult ClearCart()
    {
        HttpContext.Session.Remove(CartSessionKey);
        return RedirectToAction(nameof(Index));
    }

    private ShoppingCart GetCart()
    {
        var cartJson = HttpContext.Session.GetString(CartSessionKey);
        return cartJson == null ? new ShoppingCart() :
               JsonSerializer.Deserialize<ShoppingCart>(cartJson);
    }

    private void SaveCart(ShoppingCart cart)
    {
        HttpContext.Session.SetString(CartSessionKey, JsonSerializer.Serialize(cart));
    }
}