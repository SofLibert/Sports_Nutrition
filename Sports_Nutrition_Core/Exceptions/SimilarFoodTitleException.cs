namespace Sports_Nutrition_Core.Entities;

public class SimilarFoodTitleException(string newName) : Exception
{
    public override string Message => $"There is already food title with {newName}";
}
