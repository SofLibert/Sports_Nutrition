namespace Sports_Nutrition_Core.Entities
{
    public class Food : IEntity
    {
        public Id Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Proteins { get; set; }
        public double Fats { get; set; }
        public double Carbohydrates { get; set; }

        public virtual ICollection<Recipe> Recipes { get; set; }
    }
}
