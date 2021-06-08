﻿using AutoMapper;
using DevFitness.API.Core.Entities;
using DevFitness.API.Models.InputModels;
using DevFitness.API.Models.ViewModels;
using DevFitness.API.Persistence;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace DevFitness.API.Controllers 
{
    // api/users/4/meals
    [Route("api/users/{userId}/meals")]
    public class MaelsController : ControllerBase
    {
        private readonly DevFitnessDbContext _dbContext;
        private readonly IMapper _mapper;

        public MaelsController(DevFitnessDbContext dbContext,
                               IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        // api/users/4/meals HTTP GET
        [HttpGet]
        public IActionResult GetAll(int userId)
        {
            var allMeals = _dbContext.Meals.Where(m => m.UserId == userId && m.Active);

            var allMealsViewModels = allMeals
                .Select(m => new MealViewModel(m.Id, m.Description, m.Calories, m.Date));


            return Ok(allMealsViewModels);
        }

        // api/users/4/meals/16 HTTP GET
        [HttpGet("{mealId}")]
        public IActionResult Get(int userId, int mealId)
        {
            var meal = _dbContext.Meals.SingleOrDefault(m => m.Id == mealId && m.UserId == userId);

            if (meal == null)
                return NotFound();

            //var mealViewModel = new MealViewModel(meal.Id, meal.Description, meal.Calories, meal.Date);
            var mealViewModel = _mapper.Map<MealViewModel>(meal);

            return Ok(mealViewModel);
        }

        // api/users/4/meals HTTP POST
        [HttpPost]
        public IActionResult Post(int userId, [FromBody] CreateMealInputModel InputModel)
        {
            //var meal = new Meal(InputModel.description, InputModel.Calories, InputModel.Date, userId);
            var meal = _mapper.Map<Meal>(InputModel);

            _dbContext.Meals.Add(meal);
            _dbContext.SaveChanges();

            return CreatedAtAction(nameof(Get), new { userId = userId, mealId = meal.Id}, InputModel);
        }

        // api/users/4/meals/16 HTTP PUT
        [HttpPut("{mealId}")]
        public IActionResult Put(int userId, int mealId, [FromBody] UpdateMealInputModel InputModel) 
        {
            var meal = _dbContext.Meals.SingleOrDefault(m => m.UserId == userId && m.Id == mealId);

            if (meal == null)
                return NotFound();

            meal.Update(InputModel.Description, InputModel.Calories, InputModel.Date);
            _dbContext.SaveChanges();

            return NoContent();
        }

        // api/users/4/meals/16 HTTP DELETE
        [HttpDelete("{mealId}")]
        public IActionResult Delete(int userId, int mealId)
        {
            var meal = _dbContext.Meals.SingleOrDefault(m => m.UserId == userId && m.Id == mealId);

            if (meal == null)
                return NotFound();

            meal.Deactive();

            _dbContext.SaveChanges();


            return NoContent();
        }
    }
}
