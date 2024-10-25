namespace Sports_Nutrition_Core.Entities
{
    public class Client : IEntity
    {
        public Id Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string MiddleName { get; set; }
        public int Age { get; set; }
        public Gender Gender { get; set; }
        public double Height { get; set; }
        public double Weight { get; set; }
        public ActivityLevel ActivityLevel { get; set; }

        public virtual NutritionPlan NutritionPlan { get; set; }
    }
}
