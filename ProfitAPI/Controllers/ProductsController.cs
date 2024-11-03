using Microsoft.AspNetCore.Mvc;
using ProfitAPI.Models;
using ProfitAPI.Services;
using System.Collections.Generic;
using OfficeOpenXml;

namespace ProfitAPI.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ProductsController(IProductService productService) : ControllerBase
{
    [HttpGet]
    public ActionResult<IEnumerable<LocalProduct>> GetProducts()
    {
        return Ok(productService.GetAllProducts());
    }

    [HttpGet("{id}")]
    public ActionResult<LocalProduct> GetProduct(int id)
    {
        var product = productService.GetProductById(id);
        if (product == null)
            return NotFound();

        return Ok(product);
    }

    [HttpPost]
    public ActionResult AddProduct(LocalProduct product)
    {
        productService.AddProduct(product);
        return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
    }

    [HttpPut("{id}")]
    public ActionResult UpdateProduct(int id, LocalProduct product)
    {
        if (id != product.Id)
            return BadRequest();

        var existingProduct = productService.GetProductById(id);
        if (existingProduct == null)
            return NotFound();

        productService.UpdateProduct(product);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public ActionResult DeleteProduct(int id)
    {
        var product = productService.GetProductById(id);
        if (product == null)
            return NotFound();

        productService.DeleteProduct(id);
        return NoContent();
    }
    [HttpGet("export")]
    public IActionResult ExportToExcel()
    {
        var products = productService.GetAllProducts().ToList();

        using (var package = new ExcelPackage())
        {
            var worksheet = package.Workbook.Worksheets.Add("Products");
            worksheet.Cells[1, 1].Value = "ID";
            worksheet.Cells[1, 2].Value = "Name";
            worksheet.Cells[1, 3].Value = "Description";
            worksheet.Cells[1, 4].Value = "Price";
            worksheet.Cells[1, 5].Value = "Stock";

            for (int i = 0; i < products.Count; i++)
            {
                var product = products[i];
                worksheet.Cells[i + 2, 1].Value = product.Id;
                worksheet.Cells[i + 2, 2].Value = product.Name;
                worksheet.Cells[i + 2, 3].Value = product.Description;
                worksheet.Cells[i + 2, 4].Value = product.Price;
                worksheet.Cells[i + 2, 5].Value = product.Stock;
            }

            var stream = new MemoryStream();
            package.SaveAs(stream);
            stream.Position = 0;

            var fileName = $"Products_{System.DateTime.Now:yyyyMMddHHmmss}.xlsx";
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
        }
    }
}