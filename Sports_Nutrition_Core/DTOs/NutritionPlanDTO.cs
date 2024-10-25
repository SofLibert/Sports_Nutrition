namespace Sports_Nutrition_Core.DTOs;

public class NutritionPlanDTO
{
    public Id Id { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public Target Target { get; set; }

    public static implicit operator NutritionPlanDTO(NutritionPlan other) =>
        new()
        {
            Id = other.Id,
            StartDate = other.StartDate,
            EndDate = other.EndDate,
            Target = other.Target,
        };
}
