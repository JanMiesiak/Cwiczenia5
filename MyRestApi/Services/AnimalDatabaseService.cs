using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using MyRestApi.Models;
using Microsoft.Extensions.Configuration;


namespace MyRestApi.Services
{
        public class AnimalAnimalDatabaseService : IAnimalDatabaseService
        {
            private IConfiguration _configuration;
            

            public AnimalAnimalDatabaseService(IConfiguration configuration)
            {
                _configuration = configuration;
            }

            public IEnumerable<Animal> GetAnimals(string orderBy)
            {
                List<Animal> animals = new List<Animal>();
                using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("ProductionDb")))
                {
                    SqlCommand command = new SqlCommand();
                    command.Connection = connection;
                    connection.Open();
                    if (string.IsNullOrEmpty(orderBy))
                    {
                        command.CommandText = "Select * from Animal ORDER BY NAME ASC";
                    }
                    else if (orderBy == "name" || orderBy == "description" || orderBy == "category" ||
                             orderBy == "area")
                    {
                        command.CommandText = "Select * from Animal ORDER BY " + orderBy;
                    }

                    SqlDataReader dr = command.ExecuteReader();
                    while (dr.Read())
                    {
                        Animal animal = new Animal
                        {
                            Name = dr["Name"].ToString(),
                            Description = dr["Description"].ToString(),
                            Category = dr["Category"].ToString(),
                            Area = dr["Area"].ToString()
                        };
                        animals.Add(animal);
                    }
                }
                return animals;
            }

            public Animal AddAnimals(Animal animal)
            {
                using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("ProductionDb")))
                {
                    int id = findMaxAnimalId();
                    SqlCommand command = new SqlCommand();
                    command.Connection = connection;
                    connection.Open();
                    command.CommandText =
                        "INSERT INTO ANIMAL([idAnimal], [name], [description], [category], [area]) VALUES (@id, @name, @description, @category, @area)";
                    command.Parameters.AddWithValue("@id", id);
                    command.Parameters.AddWithValue("@name", animal.Name);
                    command.Parameters.AddWithValue("@description", animal.Description);
                    command.Parameters.AddWithValue("@category", animal.Category);
                    command.Parameters.AddWithValue("@area", animal.Area);
                    command.ExecuteNonQuery();
                }

                return animal;
            }

            public Animal UpdateAnimals(Animal animal, int idAnimal)
            {
                using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("ProductionDb")))
                {
                    SqlCommand command = new SqlCommand();
                    command.Connection = connection;
                    connection.Open();
                    command.CommandText =
                        "UPDATE Animal SET Name = @name, Description = @description, Category = @category, Area = @area WHERE idAnimal =@idAnimal";
                    command.Parameters.AddWithValue("@name", animal.Name);
                    command.Parameters.AddWithValue("@description", animal.Description);
                    command.Parameters.AddWithValue("@category", animal.Category);
                    command.Parameters.AddWithValue("@area", animal.Area);
                    command.Parameters.AddWithValue("@idAnimal", idAnimal);
                    command.ExecuteNonQuery();
                }

                return animal;
            }
            
            public void DeleteAnimals(int idAnimal)
            {
                using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("ProductionDb")))
                {
                    SqlCommand command = new SqlCommand();
                    command.Connection = connection;
                    connection.Open();
                    command.CommandText = "DELETE FROM ANIMAL WHERE idAnimal =@idAnimal";
                    command.Parameters.AddWithValue("@idAnimal", idAnimal);
                    command.ExecuteNonQuery();
                }
                
            }

            private int findMaxAnimalId()
            {
                int id = 1;
                string queryString = "SELECT MAX(idAnimal) AS MaxAnimalId FROM Animal;";
                    
                using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("ProductionDb")))
                {
                    SqlCommand command = new SqlCommand(queryString, connection);
                    
                    connection.Open();
                    object result = command.ExecuteScalar();
                    if (result != DBNull.Value && result != null)
                    {
                        id = Convert.ToInt32(result) + 1;
                    }
                }
                return id;
            }
            public bool animalExists(int idAnimal)
            {
                using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("ProductionDb")))
                {
                    SqlCommand command = new SqlCommand();
                    command.Connection = connection;
                    connection.Open();
                    command.CommandText = "SELECT COUNT(IdAnimal) FROM ANIMAL WHERE idAnimal = @idAnimal";
                    command.Parameters.AddWithValue("@idAnimal", idAnimal);
                    int exists = (int)command.ExecuteScalar();
                    if (exists == 0) return false;
                    return true;
                }
            }
        }
}