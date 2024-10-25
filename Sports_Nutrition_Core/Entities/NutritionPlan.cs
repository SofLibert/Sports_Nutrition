namespace Sports_Nutrition_Core.Entities
{
    public class NutritionPlan : IEntity
    {
        public Id Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Target Target { get; set; }

        public virtual ICollection<Recipe> Recipes { get; set; }
        public virtual Client Client { get; set; }
    }
}
