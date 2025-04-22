using PruebaTecnicaPlatec.Domain.DTOs;
using PruebaTecnicaPlatec.Domain.Entidades;
using PruebaTecnicaPlatec.Infrastructure.Interface;

namespace PruebaTecnicaPlatec.Infrastructure
{
    public class CreateService : ICreateService
    {
        private readonly AppDbContext _context;
        public CreateService(AppDbContext context)
        {

            _context = context;

        }

        public async Task<bool> Create(ProductDto dto)
        {
            var product = new Product
            {
                Name = dto.Name?.Trim() ?? string.Empty,
                Price = dto.Price,
                Quantity = dto.Quantity
            };

            _context.Products.Add(product);
            int result = await _context.SaveChangesAsync();

            return result > 0;
        }
    }
}
