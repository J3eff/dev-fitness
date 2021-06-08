using AutoMapper;
using DevFitness.API.Core.Entities;
using DevFitness.API.Models.InputModels;
using DevFitness.API.Models.ViewModels;
using DevFitness.API.Persistence;
using Microsoft.AspNetCore.Http;
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
        /// <summary>
        /// Lista de refeições do usuário 
        /// </summary>
        /// <param name="userId">Identificador do Usuario</param>
        /// <returns>Lista de refeições</returns>
        /// /// <response code="200">Lista com refeições</response>
        /// <response code="404">Lista de refeições vazia</response>
        [HttpGet]
        public IActionResult GetAll(int userId)
        {
            var allMeals = _dbContext.Meals.Where(m => m.UserId == userId && m.Active);

            var allMealsViewModels = allMeals
                .Select(m => new MealViewModel(m.Id, m.Description, m.Calories, m.Date));


            return Ok(allMealsViewModels);
        }

        // api/users/4/meals/16 HTTP GET
        /// <summary>
        /// Busca de produto especifico 
        /// </summary>
        /// <param name="userId">Identificador de usuário</param>
        /// <param name="mealId">Identificador da refeição</param>
        /// <returns>Retorna refeição especificada</returns>
        /// <response code="201">Objeto criado com sucesso.</response>
        /// <response code="404">Refeição não existe ou não é do ID informado</response> 
        [HttpGet("{mealId}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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
        /// <summary>
        /// Cadastro de refeição
        /// </summary>
        /// <param name="userId">Identificador do usuario</param>
        /// <param name="InputModel">Objeto com dados de cadastro da refeição</param>
        /// <response code="201">Objeto criado com sucesso.</response>
        /// <response code="400">Dados inválidos</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Post(int userId, [FromBody] CreateMealInputModel InputModel)
        {
            //var meal = new Meal(InputModel.description, InputModel.Calories, InputModel.Date, userId);
            var meal = _mapper.Map<Meal>(InputModel);

            _dbContext.Meals.Add(meal);
            _dbContext.SaveChanges();

            return CreatedAtAction(nameof(Get), new { userId = userId, mealId = meal.Id}, InputModel);
        }

        // api/users/4/meals/16 HTTP PUT
        /// <summary>
        /// Atualiza informação da refeição
        /// </summary>
        /// <param name="userId">Identificador do usuario</param>
        /// <param name="mealId">Iedentificador da refeição</param>
        /// <param name="InputModel">Objeto com dados de alteração da refeição</param>
        /// <response code="204">Objeto atualizado com sucesso.</response>
        /// <response code="400">Dados inválidos</response>
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
        /// <summary>
        /// Desativa refeição do usuario 
        /// </summary>
        /// <param name="userId">Identificador do usuario</param>
        /// <param name="mealId">Identificador da refeição</param>
        /// <returns>Objeto desativado.</returns>
        /// <response code="204">Objeto desativado com sucesso.</response>
        /// <response code="404">Refeição informada não existe ou não e do Usuario especificado</response>
        [HttpDelete("{mealId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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
