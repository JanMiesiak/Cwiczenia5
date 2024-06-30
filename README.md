# Cwiczenia5
Solution for 2024 APBD task

I used my own database [APBD 2024] set up in a local Docker Container instead of [db-******.pjwstk.edu.pl]

I could not find script for database creation in Gakko class materials so I used my own:

CREATE TABLE Animal (
    IdAnimal INT PRIMARY KEY,
    Name NVARCHAR(200) NOT NULL,
    Description NVARCHAR(200),
    Category NVARCHAR(200) NOT NULL,
    Area NVARCHAR(200) NOT NULL
);

INSERT INTO Animal (IdAnimal, Name, Description, Category, Area) VALUES
(1, 'Lion', 'Large carnivorous feline', 'Mammal', 'Savannah'),
(2, 'Elephant', 'Largest land animal', 'Mammal', 'Savannah'),
(3, 'Crocodile', 'Aquatic reptile', 'Reptile', 'Wetlands'),
(4, 'Penguin', 'Flightless bird', 'Bird', 'Antarctica'),
(5, 'Kangaroo', 'Marsupial with powerful legs', 'Mammal', 'Australia');

Example JSON file for testing: 

{
  "name": "Penguin",
  "description": "Flightless bird",
  "category": "Bird",
  "area": "Antarctica"
}


