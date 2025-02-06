using Gandalf.Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Gandalf.Backend.Services;

public class CategoryService(IDbContextFactory<GandalfDbContext> dbContextFactory)
{
    public List<Category> GetCategories()
    {
        using var context = dbContextFactory.CreateDbContext();

        return context.Categories.ToList();
    }

    public void CreateCategory(string name) 
    {
        using var context = dbContextFactory.CreateDbContext();

        context.Categories.Add(new Category { Name = name });

        context.SaveChanges();
    }
   
    public void Update(Category category)
    {
        using var context = dbContextFactory.CreateDbContext();

        context.Categories.Update(category);

        context.SaveChanges();
    }

    public Category? GetCategory(int? id)
    {
        using var context = dbContextFactory.CreateDbContext();

        return context.Categories.FirstOrDefault(m => m.CategoryId == id);
    }

    public Category? DeleteCategory(int? id)
    {
        using var context = dbContextFactory.CreateDbContext();

        var category = context.Categories.FirstOrDefault(m => m.CategoryId == id);

        if (category is null)
            return default;

        context.Categories.Remove(category);
        context.SaveChanges();

        return category;
    }
}
