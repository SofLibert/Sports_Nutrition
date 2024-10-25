namespace Sports_Nutrition_Core.DTOs;

public class FoodDTO
{
    public Id Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public double Proteins { get; set; }
    public double Fats { get; set; }
    public double Carbohydrates { get; set; }

    public static implicit operator FoodDTO(Food other) =>
        new()
        {
            Id = other.Id,
            Name = other.Name,
            Description = other.Description,
            Proteins = other.Proteins,
            Fats = other.Fats,
            Carbohydrates = other.Carbohydrates
        };
}
