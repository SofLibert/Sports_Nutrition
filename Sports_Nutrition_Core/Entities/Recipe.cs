namespace Sports_Nutrition_Core.Entities
{
    public class Recipe : IEntity
    {
        public Id Id { get; set; }

        public virtual ICollection<Food> Foods { get; set; }
        public virtual NutritionPlan NutritionPlan { get; set; }
    }
}
