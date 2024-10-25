using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Reflection.Emit;
using Sports_Nutrition_Core;

namespace Sports_Nutrition_Infrastructure.Db;

internal class NutritionContext : DbContext
{
    class IdConverter() : ValueConverter<Guid, Id>(x => new Id(x), x => x.Value);
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var entityTypes = typeof(IEntity).Assembly
                                        .GetTypes()
                                        .Where(x => !x.IsInterface &&
                                                    !x.IsAbstract &&
                                                    typeof(IEntity).IsAssignableFrom(x));
        foreach (var entityType in entityTypes)
        {
            modelBuilder.Entity(entityType)
                .Property(nameof(IEntity.Id))
                .HasConversion<IdConverter>()
                .HasDefaultValueSql("NEWSEQUENTIALID()");
        }
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(NutritionContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseLazyLoadingProxies()
                      .UseSqlServer("Server=db;Database=nutritiondb;User Id=qwe;Password=postavtezachetplz");
        base.OnConfiguring(optionsBuilder);
    }
}
