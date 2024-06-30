using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using MyRestApi.Models;
using MyRestApi.Services;
namespace MyRestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnimalController : ControllerBase
    {
        private readonly IAnimalDatabaseService _animalDatabaseService;

        public AnimalController(IAnimalDatabaseService animalDatabaseService)
        {
            _animalDatabaseService = animalDatabaseService;
        }
        [HttpGet]
        public IActionResult GetAnimals(string ordered)
        {
            if (string.IsNullOrEmpty(ordered) || ordered == "name" || ordered == "description" || ordered == "category" || ordered == "area")
                return Ok(_animalDatabaseService.GetAnimals(ordered));
            else return BadRequest("Parameter is not correct.");
        }
        [HttpPost]
        public IActionResult AddAnimals(Animal animal)
        {
            return Ok(_animalDatabaseService.AddAnimals(animal));
        }
        [HttpPut]
        public IActionResult UpdateAnimals(Animal animal, int idAnimal)
        {
            if (_animalDatabaseService.animalExists(idAnimal)) 
                return Ok(_animalDatabaseService.UpdateAnimals(animal, idAnimal));
            else return NotFound("Animal with such ID was not found.");
        }
        [HttpDelete]
        public IActionResult DeleteAnimals(int idAnimal)
        {

            if (_animalDatabaseService.animalExists(idAnimal))
            {
                _animalDatabaseService.DeleteAnimals(idAnimal);
                return Ok();
            }   
            else return NotFound("Animal with such ID was not found.");
        }
    }
}