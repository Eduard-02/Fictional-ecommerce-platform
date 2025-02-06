using Gandalf.Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Gandalf.Backend.Services;

public class ProductService(IDbContextFactory<GandalfDbContext> dbContextFactory)
{
    public List<Product> GetProducts()
    {
        using var context = dbContextFactory.CreateDbContext();

        return context.Products.Include(p => p.Category).ToList();
    }

    public void CreateProduct(string name, string image, decimal price, string description, int? categoryId)
    {
        using var context = dbContextFactory.CreateDbContext();

        context.Products.Add(new Product 
        { 
            Name = name,
            Price = price,
            Image = image,
            Description = description,
            CategoryId = categoryId
        });

        context.SaveChanges();
    }

    public void Update(Product product)
    {
        using var context = dbContextFactory.CreateDbContext();

        context.Products.Update(product);

        context.SaveChanges();
    }

    public Product? GetProduct(int? id)
    {
        using var context = dbContextFactory.CreateDbContext();

        return context.Products.Include(p => p.Category).FirstOrDefault(m => m.ProductId == id);
    }

    public Product? DeleteProduct(int? id)
    {
        using var context = dbContextFactory.CreateDbContext();

        var product = context.Products.FirstOrDefault(m => m.ProductId == id);

        if (product is null)
            return default;

        context.Products.Remove(product);
        context.SaveChanges();

        return product;
    }
}
