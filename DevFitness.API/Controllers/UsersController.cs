using AutoMapper;
using DevFitness.API.Core.Entities;
using DevFitness.API.Models.InputModels;
using DevFitness.API.Models.ViewModels;
using DevFitness.API.Persistence;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevFitness.API.Controllers
{
    //api/users
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly DevFitnessDbContext _dbContext;
        private readonly IMapper _mapper;

        public UsersController(DevFitnessDbContext dbContext, 
                               IMapper mapper )
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        // api/users/1 método HTTP GET
        /// <summary>
        /// Retorna detalhes de usuário
        /// </summary>
        /// <param name="id">Identificador de usuário</param>
        /// <returns>Objetos de detalhes de usuário</returns>
        /// <response code="404">Usuário não encontrado</response>
        /// <response code="200">Usuário encontrado com sucesso.</response>
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var user = _dbContext.Users.SingleOrDefault(u => u.Id == id);

            if (user == null)
                return NotFound();

            //var userViewModel = new UserViewModel(user.Id, user.FullName, user.Height, user.Weight, user.BirthDate);
            var userViewModel = _mapper.Map<UserViewModel>(user);
            
            return Ok(userViewModel);
        }

        // api/users método HTTP POST
        /// <summary>
        /// Cadastrar um usuário
        /// </summary>
        /// <remarks>
        /// Requisição de exemplo:
        /// {
        /// "fullName": "Jefferson Dev",
        /// "height": 1.72,
        /// "weight": 70,
        /// "birthDate": "1999-01-01 00:00:00"
        /// }
        /// </remarks>
        /// <param name="inputModel">Objeto com dados de cadastro de Usuário</param>
        /// <returns>Objeto recém-criado.</returns>
        /// <response code="201">Objeto criado com sucesso.</response>
        /// <response code="400">Dados inválidos</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Post([FromBody] CreateUserInputModel inputModel) 
        {
            //var user = new User(inputModel.FullName, inputModel.Height, inputModel.Weight, inputModel.BirthDate);
            var user = _mapper.Map<User>(inputModel);

            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();

            return CreatedAtAction(nameof(Get), new { id = user.Id }, inputModel);
        }

        // api/users/id HTTP PUT
        /// <summary>
        /// Atualiza informações de usuario 
        /// </summary>
        /// <param name="id">Identificador do usuario </param>
        /// <param name="inputModel">Objeto com dados de alteração do Usuário</param>
        /// <returns>Objeto atualizado.</returns>
        /// <response code="204">Objeto atualizado com sucesso.</response>
        /// <response code="400">Dados inválidos</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Put(int id,[FromBody] UpdateUserInputModel inputModel)
        {
            var user = _dbContext.Users.SingleOrDefault(u => u.Id == id);

            if (user == null)
                return NotFound();

            user.Update(inputModel.Height, inputModel.Weight);

            _dbContext.SaveChanges();

            return NoContent();            
        }
    }
}
