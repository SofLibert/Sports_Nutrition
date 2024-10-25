namespace Sports_Nutrition_Core.Exceptions;

public class ClientNotFoundException(Guid wrongId) : Exception($"Client {wrongId} is not found.") { }
