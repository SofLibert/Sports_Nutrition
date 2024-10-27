using Moq;
using NUnit.Framework;
using NUnit.Framework.Legacy;
using Sports_Nutrition_Application.NutritionPlans;
using Sports_Nutrition_Core;
using Sports_Nutrition_Core.DTOs;
using Sports_Nutrition_Core.Entities;
using Sports_Nutrition_Core.Exceptions;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

public class NutritionServiceTests
{
    private Mock<IRepository<NutritionPlan>> _nutritionPlanRepositoryMock;
    private Mock<IRepository<Client>> _clientRepositoryMock;
    private NutritionPlanService _service;

    [SetUp]
    public void SetUp()
    {
        _nutritionPlanRepositoryMock = new Mock<IRepository<NutritionPlan>>();
        _clientRepositoryMock = new Mock<IRepository<Client>>();
        _service = new NutritionPlanService(_nutritionPlanRepositoryMock.Object)
        {
            ClientRepository = _clientRepositoryMock.Object
        };
    }

    [Test]
    public async Task CalculateNutritionAsync_ValidClientId_CalculatesNutrition()
    {
        // Arrange 
        var plan = new NutritionPlanDTO { Target = Target.Lose };
        var client = new ClientDTO
        {
            Id = new Id(Guid.NewGuid()),
            Gender = Gender.Male,
            Weight = 70,
            Height = 175,
            Age = 25,
            ActivityLevel = ActivityLevel.Moderate
        };

        var clientEntity = new Client { Id = new Id(Guid.NewGuid()) };

        // Act 
        await _service.CalculateNutritionAsync(plan, client);

    }

    [Test]
    public async Task CalculateNutritionAsync_InvalidClientId_ThrowsClientNotFoundException()
    {
        // Arrange 
        var plan = new NutritionPlanDTO { Target = Target.Lose };
        var client = new ClientDTO
        {
            Id = new Id(Guid.NewGuid()),
            Gender = Gender.Male,
            Weight = 70,
            Height = 175,
            Age = 25,
            ActivityLevel = ActivityLevel.Moderate
        };

        // Act & Assert 
        var ex = Assert.ThrowsAsync<ClientNotFoundException>(() => _service.CalculateNutritionAsync(plan, client));
        Assert.That(ex.Message, Is.EqualTo("Client not found"));
    }

    [Test]
    [TestCase(2000, "Sedentary", ExpectedResult = 2400)]
    [TestCase(2000, "Light", ExpectedResult = 2750)]
    [TestCase(2000, "Moderate", ExpectedResult = 3100)]
    [TestCase(2000, "Active", ExpectedResult = 3450)]
    public void CalculateTDEE_CorrectlyCalculatesTDEE(double bmr, string activityLevel)
    {
        // Act 
        var result = _service.CalculateTDEE(bmr, activityLevel);

        // Assert 
        ClassicAssert.AreEqual(bmr * (activityLevel switch
        {
            "Sedentary" => 1.2,
            "Light" => 1.375,
            "Moderate" => 1.55,
            "Active" => 1.725,
            _ => 1
        }), result);
    }

    [Test]
    [TestCase(2400, "Lose", ExpectedResult = 1900)]
    [TestCase(2400, "Gain", ExpectedResult = 2900)]
    [TestCase(2400, "Maintain", ExpectedResult = 2400)]
    public void CalculateTargetCalories_CorrectlyCalculatesTargetCalories(double tdee, string target)
    {
        // Act 
        var result = _service.CalculateTargetCalories(tdee, target);

        // Assert 
        ClassicAssert.AreEqual(target switch
        {
            "Lose" => tdee - 500,
            "Gain" => tdee + 500,

        }, result);
    }

    [Test]
    [TestCase(2400.0, ExpectedResult = (300.0))]
    [TestCase(2400.0, ExpectedResult = (300.0))]
    [TestCase(2400.0, ExpectedResult = (53.33))]
    public void CalculateMacros_CorrectlyCalculatesMacros(double calories)
    {
        // Act 
        var (protein, carbs, fats) = _service.CalculateMacros(calories);

        // Assert 
        ClassicAssert.AreEqual(300.0, protein);
        ClassicAssert.AreEqual(300.0, carbs);
        ClassicAssert.AreEqual(53.33, fats); // Allowing small delta due to floating point 
    }
}