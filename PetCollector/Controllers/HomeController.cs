using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using PetCollector.Models;
using PetLibrary;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace PetCollector.Controllers
{
    public class HomeController : Controller
    {
        private Repository Repository { get; set; }

        public HomeController()
        {
            Repository = new Repository();
        }

        public IActionResult Index()
        {
            var pets = Repository.RetrievePetsFromRepository();
            return View(pets);
        }

        public IActionResult Import()
        {
            return View();
        }

        // Imports CSV file and saves data to pet repository
        // @pre:    assumes file is of type CSV (formatted correctly) and only contains allowable breeds, breeds already seeded in repository
        // @post:   pets saved to serialized file in repository, if failed returns with error message
        [HttpPost]
        public IActionResult Import(IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                try
                {
                    var pets = RetrievePetsFromCsvFile(file);
                    Repository.WritePetsToRepository(pets);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ViewBag.FileMessage = "Error: " + ex.Message.ToString();
                }
            }
            else
            {
                ViewBag.FileMessage = "Error: Please specify a file.";
            }

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        // Retrieve Pets from CSV file
        // @pre:    properly formatted CSV file containing pet data
        // @post:   list of Pet objects from file
        private List<Pet> RetrievePetsFromCsvFile(IFormFile file)
        {
            var pets = new List<Pet>();
            var breeds = Repository.RetrieveBreedsFromRepository();

            using (var reader = new StreamReader(file.OpenReadStream()))
            {
                // Retrieve CSV Column Headers
                var columns = reader.ReadLine().ToLower().Split(',');
                var requiredColumns = new string[] { "name", "type", "age", "gender" };
                if (requiredColumns.Except(columns).Any()) throw (new Exception("Invalid CSV Columns. Columns 'name', 'type', 'age', and 'gender' required."));

                // Retrieve Pet Contents from File
                var csvLine = string.Empty;
                while (!string.IsNullOrEmpty(csvLine = reader.ReadLine()))
                {
                    var pet = CreatePetFromCsvLine(columns, csvLine, breeds);
                    if (!TryValidateModel(pet)) throw (new Exception(GetValidationErrors()));
                    pets.Add(pet);
                }
            }

            return pets;
        }

        // Returns Pet Object from Data in CSV Line
        // @pre:    assumes columns contains name of each CSV column in the appropriate order and all data is of appropriate datatype,
        //          breeds populated with eligible pet breeds
        //              ex: gender is a string of, age is an integer
        // @post:   Pet object containing name, type, age, gender, and problem (if applicable)
        private Pet CreatePetFromCsvLine(string[] columns, string csvLine, List<Breed> breeds)
        {
            var contents = csvLine.ToLower().Split(',');

            return new Pet()
            {
                Name = contents[columns.IndexOf("name")],
                Type = breeds.SingleOrDefault(x => x.Name.Equals(contents[columns.IndexOf("type")].ToLower())),
                //Type = contents[columns.IndexOf("type")],
                Age = Convert.ToInt32(contents[columns.IndexOf("age")]),
                Gender = contents[columns.IndexOf("gender")],
                Problem = (columns.IndexOf("problem") != -1) ? contents[columns.IndexOf("problem")] : null
            };
        }

        // Returns concatenated string of all errors
        // @pre:    assumes model state has an error
        // @post:   comma concatenated string of first error from each property with errors from model state
        private string GetValidationErrors()
        {
            var errors = ModelState
                .Where(x => x.Value.Errors.Count > 0)
                .Select(x => x.Value.Errors[0].ErrorMessage)
                .ToArray();
            return string.Join(",", errors);
        }
    }
}
