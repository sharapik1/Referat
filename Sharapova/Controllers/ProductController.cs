using System.Collections.ObjectModel;
using Microsoft.AspNetCore.Mvc;
using Sharapova.data;
using Sharapova.data.mysql;
using Sharapova.model;

namespace Sharapova.Controllers;

[Route("product")]
[ApiController]
public class ProductController : ControllerBase
{
    private DataProvider _provider =
        new MySqlDataProvider("localhost",
            "sharapova",
            "sharapova",
            "1234");

    [HttpGet]
    [Route("all")]
    public Collection<Product> All()
    {
        return _provider.LoadAllProducts();
    }

    [HttpGet]
    public IActionResult ById(int id)
    {
        Product? product = _provider.LoadProductById(id);
        if (product == null) return NotFound();
        return Ok(product);
    }

    [HttpPost]
    [Route("save")]
    public IActionResult Save(Product product)
    {
        _provider.SaveProduct(product);
        return Ok(_provider.LoadProductById(product.Id));
    }
}