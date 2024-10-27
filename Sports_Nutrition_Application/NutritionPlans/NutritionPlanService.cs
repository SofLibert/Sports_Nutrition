namespace Sports_Nutrition_Application.NutritionPlans;

public class NutritionPlanService(IRepository<NutritionPlan> repository) : IService
{
    private IRepository<NutritionPlan> Repository { get; init; } = repository;
    public IRepository<Client> ClientRepository { get; init; }
    public async Task CalculateNutritionAsync(NutritionPlanDTO plan, ClientDTO client, CancellationToken cancellationToken = default)
    {
        Client localClient;
        if (client.Id is not null)
        {
            localClient = (await ClientRepository.Get(x => x.Id.Value == client.Id.Value, cancellationToken)).FirstOrDefault() ??
                throw new ClientNotFoundException(client.Id.Value);
        }

        double bmr = CalculateBMR(client.Gender, client.Weight, client.Height, client.Age);
        double tdee = CalculateTDEE(bmr, client.ActivityLevel.ToString());
        double targetCalories = CalculateTargetCalories(tdee, plan.Target.ToString());

        var macros = CalculateMacros(targetCalories);
    }

    public double CalculateBMR(Gender gender, double weight, double height, int age)
    {
        return gender == Gender.Male
        ? 10 * weight + 6.25 * height - 5 * age + 5 // Формула для мужчин
        : 10 * weight + 6.25 * height - 5 * age - 161; // Формула для женщин
    }

    public double CalculateTDEE(double bmr, string activityLevel)
    {
        return activityLevel switch
        {
            "Sedentary" => bmr * 1.2,
            "Light" => bmr * 1.375,
            "Moderate" => bmr * 1.55,
            "Active" => bmr * 1.725,
            _ => bmr,
        };
    }

    public double CalculateTargetCalories(double tdee, string target)
    {
        return target switch
        {
            "Lose" => tdee - 500, // Уменьшаем на 500 ккал для потери веса
            "Gain" => tdee + 500, // Увеличиваем на 500 ккал для набора веса
            _ => tdee,
        };
    }
     
    public (double Protein, double Carbs, double Fats) CalculateMacros(double calories)
    {
        double protein = calories * 0.3 / 4; // 30% калорий из белков
        double carbs = calories * 0.5 / 4; // 50% калорий из углеводов
        double fats = calories * 0.2 / 9; // 20% калорий из жиров

        return (protein, carbs, fats);
    }
}
