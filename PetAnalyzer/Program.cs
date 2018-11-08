using PetLibrary;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PetAnalyzer
{
    class Program
    {
        private static List<Pet> Pets { get; set; }

        #region Methods
        // Retrieve number of all male pets
        // @pre:    pets is not null
        // @post:   integer total of male pets returned
        static int GetNumberOfMales()
        {
            return Pets.Where(x => x.Gender.ToLower().Equals("m")).Count();
        }

        // Retrieve number of all pets with four legs
        // @pre:    pets is not null
        // @post:   integer total of four legged pets returned
        static int GetNumberOfFourLeggedPets()
        {
            return Pets.Where(x => x.Type.NumberOfLegs.Equals(4)).Count();
        }

        // Retrieve average age of all dogs
        // @pre:    pets is not null
        // @post:   double average of all dog ages returned
        static double GetAverageDogAge()
        {
            return Pets.Where(x => x.Type.Name.Equals("dog")).Average(x => x.Age);
        }

        // Retrieve all problems for reptiles
        // @pre:    pets is not null
        // @post:   non-unique enumerable of all reptile problems sorted alphabetically
        static IEnumerable<string> GetAllReptileProblems()
        {
            return Pets
                .Where(x => x.Type.AnimalType.Equals("reptile") && !string.IsNullOrEmpty(x.Problem))
                .OrderBy(x => x.Problem)
                .Select(x => x.Problem);
        }

        // Retrieve names of all mammals
        // @pre:    pets is not null
        // @post:   enumerable of each mammal's name sorted alphabetically
        static IEnumerable<string> GetAllMammalNames()
        {
            return Pets
                .Where(x => x.Type.AnimalType.Equals("mammal"))
                .OrderBy(x => x.Name)
                .Select(x => x.Name);
        }
        #endregion

        static void Main(string[] args)
        {
            Pets = new Repository().RetrievePetsFromRepository();

            if (Pets != null)
            {
                Console.WriteLine("How many pets are male? " + GetNumberOfMales());
                Console.WriteLine();

                Console.WriteLine("How many pets have 4 legs? " + GetNumberOfFourLeggedPets());
                Console.WriteLine();

                Console.WriteLine("What is the average age of the dogs? " + GetAverageDogAge());
                Console.WriteLine();

                Console.WriteLine("What are all the problems reported for reptiles? ");
                var reptileProblems = GetAllReptileProblems();
                foreach (var problem in reptileProblems) Console.WriteLine("   " + problem);
                Console.WriteLine();

                Console.WriteLine("What are the names of all pets that are mammals? ");
                var mammalNames = GetAllMammalNames();
                foreach (var name in mammalNames) Console.WriteLine("   " + name);
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("No pets found. Please make sure data has been imported via PetCollector.");
            }

            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }
    }
}
