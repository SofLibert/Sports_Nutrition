namespace Sports_Nutrition_Core.Exceptions;

public class FoodNotFoundException(Guid wrongId) : Exception($"Food {wrongId} is not found.") { }
