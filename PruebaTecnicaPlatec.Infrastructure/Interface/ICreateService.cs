using PruebaTecnicaPlatec.Domain.DTOs;

namespace PruebaTecnicaPlatec.Infrastructure.Interface
{
    public interface ICreateService
    {
        Task<bool> Create(ProductDto dto);
    }
}
