using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using Sports_Nutrition_Application.Foods;
using Sports_Nutrition_Core;
using Sports_Nutrition_Core.DTOs;
using Sports_Nutrition_Core.Entities;
using Microsoft.Extensions.Logging;
using Sports_Nutrition_Core.Exceptions;

namespace Sports_Nutrition_Api.Endpoints
{
    public static class FoodEndPoint
    {
        public static void FoodEndpoints(this IEndpointRouteBuilder routes)
        {
            routes.MapPost("api/food/create", CreateFood).WithTags("Food");
            routes.MapGet("api/food/{foodId}", GetFoodById).WithTags("Food");
        }

        [HttpGet]
        private static async Task<IResult> GetFoodById(Guid foodId, FoodService service, ILogger logger, CancellationToken cancellationToken)
        {
            try
            {
                await service.GetFoodById(foodId, cancellationToken);
                return Results.Ok();

            }
            catch (FoodNotFoundException ex)
            {
                logger.LogError(ex, "Failed to find food.");
                return Results.BadRequest($"An food with ID '{foodId}' is not found.");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while creating the food.");
                return Results.StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        private static async Task<IResult> CreateFood(FoodDTO food, FoodService service, HttpContext context, ILogger logger)
        {
            try
            {
                await service.CreateOrUpdateFoodAsync(food);
                return Results.Created($"api/food/{food.Id}", food);
            }
            catch (SimilarFoodTitleException ex)
            {
                logger.LogError(ex, "Failed to create food due to similar name.");
                return Results.BadRequest($"An food with the name '{food.Name}' already exists.");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while creating the food.");
                return Results.StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
