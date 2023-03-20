using Microsoft.EntityFrameworkCore;
using liquorApi.Exceptions;
using liquorApi.Context.Entities;
using liquorApi.Context;
using liquorApi.Models;

namespace liquorApi.Services;

    public interface IProductsService
    {
        Task<IEnumerable<Product>> GetAll();
        Task<Product> GetById(int id);
        Task<Product> Create(ProductDtoIn productDtoIn);
        Task Update(ProductDto product);
        Task Delete(int id);
    }

    public class ProductsService : IProductsService
    {
        private readonly LicoresDbContext _context;

        public ProductsService(LicoresDbContext context)
        {
            this._context = context;
        }

    public async Task<IEnumerable<Product>> GetAll()
    {
        return await this._context.Products.ToListAsync();
    }

    public async Task<Product> GetById(int id)
    {
        var existProduct = await this._context.Products.FindAsync(id);

        if (existProduct is null)
            throw new ProductNotFoundException("Product not found by id: " + id);

        return existProduct;
    }

    public async Task<Product> Create(ProductDtoIn productDtoIn)
    {
        Product product = new Product();

        product.Name = productDtoIn.Name;
        product.Description = productDtoIn.Description;
        product.Price = productDtoIn.Price;
        product.Stock = productDtoIn.Stock;

        this._context.Products.Add(product);
        await this._context.SaveChangesAsync();

        return product;
    }

    public async Task Update(ProductDto product)
    {
        var existProduct = await this._context.Products.FindAsync(product.Id);

        if (existProduct is null)
            throw new ProductNotFoundException("Product not found by id: " + product.Id);

        existProduct.Name = product.Name;
        existProduct.Description = product.Description;
        existProduct.Price = product.Price;
        existProduct.Stock = product.Stock;

        await this._context.SaveChangesAsync();
    }

    public async Task Delete(int id)
    {
        var existProduct = await this._context.Products.FindAsync(id);

        if (existProduct is null)
            throw new ProductNotFoundException("Product not found by id: " + id);
        
        this._context.Products.Remove(existProduct);
        await this._context.SaveChangesAsync();
    }
}
