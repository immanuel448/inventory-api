using InventoryApi.Data;
using InventoryApi.DTOs;
using InventoryApi.Models;
using Microsoft.EntityFrameworkCore;

namespace InventoryApi.Services;

public class ProductService : IProductService
{
    // guarda nuestro acceso al EF Core.
    private readonly AppDbContext _context;

    // es inyección de dependencias.
    public ProductService(AppDbContext context)
    {
        _context = context;
    }

    //obtener todos los productos.
    public async Task<List<ProductDto>> GetAllAsync()
    {
        //convierte cada Product de la base de datos en un ProductDto.
        return await _context.Products
            .Where(p => p.IsActive)
            .Select(p => new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                Stock = p.Stock,
                IsActive = p.IsActive,
                CreatedAt = p.CreatedAt,
                UpdatedAt = p.UpdatedAt
            })
            .ToListAsync();
    }

    //obtener un producto específico por su ID.
    public async Task<ProductDto?> GetByIdAsync(int id)
    {
        return await _context.Products
            .Where(p => p.Id == id && p.IsActive)
            .Select(p => new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                Stock = p.Stock,
                IsActive = p.IsActive,
                CreatedAt = p.CreatedAt,
                UpdatedAt = p.UpdatedAt
            })
            .FirstOrDefaultAsync();
    }

    public Task<ProductDto> CreateAsync(CreateProductDto dto)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateAsync(int id, UpdateProductDto dto)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }
}