namespace Sports_Nutrition_Application.Foods;

public class FoodService(IRepository<Food> repository) : IService
{
    private IRepository<Food> Repository { get; init; } = repository;
    public async Task CreateOrUpdateFoodAsync(FoodDTO food, CancellationToken cancellationToken = default)
    {
        Food localFood;
        if (food.Id is not null)
        {
            localFood = (await Repository.Get(x => x.Id.Value == food.Id.Value, cancellationToken)).FirstOrDefault() ??
                throw new FoodNotFoundException(food.Id.Value);
            localFood.Name = food.Name;
            localFood.Description = food.Description;
            localFood.Proteins = food.Proteins;
            localFood.Fats = food.Fats;
            localFood.Carbohydrates = food.Carbohydrates;
        }
        else
            localFood = new()
            {
                Name = food.Name,
                Description = food.Description,
                Proteins = food.Proteins,
                Fats = food.Fats,
                Carbohydrates= food.Carbohydrates
            };

        if (localFood.Id is null)
            await Repository.Add(localFood, cancellationToken);
        else
            await Repository.Update(localFood, cancellationToken);
    }

    public async Task GetFoodById(Guid foodId, CancellationToken cancellationToken)
    {
        var food = (await Repository.Get(x => x.Id.Value == foodId, cancellationToken)).FirstOrDefault() ??
            throw new FoodNotFoundException(foodId);
    }
}
