using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PruebaTecnicaPlatec.Domain.DTOs;
using PruebaTecnicaPlatec.Domain.Entidades;
using PruebaTecnicaPlatec.Infrastructure;
using PruebaTecnicaPlatec.Infrastructure.Interface;

namespace PruebaTecnicaPlatec.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly ICreateService _createService;

        public ProductsController(AppDbContext context, ICreateService createService)
        {
            _context = context;
            _createService = createService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetAll()
        {
            return Ok(await _context.Products.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetById(int id)
        {
            var product = await _context.Products.FindAsync(id);
            return product == null ? NotFound() : Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult<Product>> Create(ProductDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name) || dto.Price <= 0 || dto.Quantity <= 0)
                return BadRequest("Datos inválidos");

            bool success = await _createService.Create(dto);

            if (!success)
                return BadRequest("No se pudo guardar el producto. Verifica los datos ingresados.");

            return Ok("Producto creado exitosamente.");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, ProductDto dto)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) return NotFound();

            if (string.IsNullOrWhiteSpace(dto.Name) || dto.Price <= 0 || dto.Quantity <= 0)
                return BadRequest("Datos inválidos");

            product.Name = dto.Name;
            product.Price = dto.Price;
            product.Quantity = dto.Quantity;

            await _context.SaveChangesAsync();
            return Ok(product);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) return NotFound();

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
