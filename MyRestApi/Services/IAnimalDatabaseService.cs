using System.Collections.Generic;
using MyRestApi.Models;

namespace MyRestApi.Services
{
    public interface IAnimalDatabaseService
    {
        IEnumerable<Animal> GetAnimals(string orderBy);
        Animal AddAnimals(Animal animal);
        Animal UpdateAnimals(Animal animal, int idAnimal);
        void DeleteAnimals(int idAnimal);
        bool animalExists(int idAnimal);//to do
    }
}