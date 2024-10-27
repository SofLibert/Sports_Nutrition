using NUnit.Framework;
using Sports_Nutrition_Application.Foods;
using Sports_Nutrition_Core;
using Sports_Nutrition_Core.Entities;
using Sports_Nutrition_Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sports_Nutrition_Application_Tests.Foods
{
    public class FoodServiceTest
    {
        [TestCase("name1", "Desc", "2020-01-01", 10, 10.99)]
        [TestCase("name2", "Desc", "2021-01-01", 9, 0.99)]
        [TestCase("name3", "Desc", "2022-01-01", 11, 1.99)]
        [Test]
        public async Task CreateOrUpdateFood_WhenFoodForUpdateDoesntExists_ThrowsException()
        {
            // Arrange
            var repo = new FakeRepository<Food>();
            var food = new Food
            {
                Id = new Id(Guid.NewGuid()),
                Name = "Test"
            };
            await repo.AddRange([food]);

            var service = new FoodService(repo);
            var toChange = new Food { Id = new Id(Guid.NewGuid()), Name = "NewName" };

            // Act
            AsyncTestDelegate act = async delegate { await service.CreateOrUpdateFoodAsync(toChange); };

            // Assert
            Assert.ThrowsAsync<FoodNotFoundException>(act);
        }
    }
}
