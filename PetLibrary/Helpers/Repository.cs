using PetLibrary.Properties;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using static System.Net.Mime.MediaTypeNames;

namespace PetLibrary
{
    public class Repository
    {
        private readonly string PetRepositoryFilePath;
        private readonly string BreedRepositoryFilePath;

        public Repository()
        {
            var domain = AppDomain.CurrentDomain;
            var folderPath = domain.BaseDirectory.Split(domain.FriendlyName)[0];
            PetRepositoryFilePath = folderPath + "PetRepository.txt";
            BreedRepositoryFilePath = folderPath + "BreedRepository.txt";
            SeedRepositoryBreeds();
        }

        // Create default breeds in repository (will replace existing file just in case)
        // @post:   serialized .txt file of allowable pet breeds in Repository/BreedRepository.txt
        public void SeedRepositoryBreeds()
        {
            var breeds = new List<Breed>
            {
                new Breed() { Name = "dog", AnimalType = "mammal", NumberOfLegs = 4 },
                new Breed() { Name = "cat", AnimalType = "mammal", NumberOfLegs = 4 },
                new Breed() { Name = "lizard", AnimalType = "reptile", NumberOfLegs = 4 },
                new Breed() { Name = "snake", AnimalType = "reptile", NumberOfLegs = 0 },
                new Breed() { Name = "parrot", AnimalType = "bird", NumberOfLegs = 2 },
                new Breed() { Name = "cockatiel", AnimalType = "bird", NumberOfLegs = 2 },
            };

            var formatter = new BinaryFormatter();
            var stream = RetrieveRepositoryFile(BreedRepositoryFilePath);
            if (stream == null) stream = CreateRepositoryFile(BreedRepositoryFilePath);
            formatter.Serialize(stream, breeds);
            stream.Close();
        }

        // Read List of Breeds from Serialized File
        // @pre:    assumes repository exists, correctly formatted in appropriate location
        // @post:   list of breed objects
        public List<Breed> RetrieveBreedsFromRepository()
        {
            var formatter = new BinaryFormatter();
            var stream = RetrieveRepositoryFile(BreedRepositoryFilePath);
            if (stream == null) return null;
            var breeds = formatter.Deserialize(stream) as List<Breed>;
            stream.Close();
            return breeds;
        }

        // Write list of pets to serialized file and replace existing pets, if any
        // @pre:    list of pet objects to serialize
        // @post:   serialized .txt file containing only pet list in Repository/PetRepository.txt
        public void WritePetsToRepository(List<Pet> data)
        {
            var formatter = new BinaryFormatter();
            var stream = RetrieveRepositoryFile(PetRepositoryFilePath);
            if (stream == null) stream = CreateRepositoryFile(PetRepositoryFilePath);
            formatter.Serialize(stream, data);
            stream.Close();
        }

        // Read List of Pets from Serialized File
        // @pre:    assumes repository exists, correctly formatted in appropriate location
        // @post:   llist of pet objects
        public List<Pet> RetrievePetsFromRepository()
        {
            var formatter = new BinaryFormatter();
            var stream = RetrieveRepositoryFile(PetRepositoryFilePath);
            if (stream == null) return null;
            var pets = formatter.Deserialize(stream) as List<Pet>;
            stream.Close();
            return pets;
        }

        // Retrieves repository file for pet data
        // @pre:    filepath must be valid
        // @post:   returns pet repository file if found, otherwise null
        private FileStream RetrieveRepositoryFile(string filepath)
        {
            return (File.Exists(filepath)) ? File.Open(filepath, FileMode.Open) : null;
        }

        // Creates repository file for pet data
        // @pre:    filepath must be valid
        // @post:   returns created file at repository path
        private FileStream CreateRepositoryFile(string filepath)
        {
            return File.Create(filepath);
        }

    }
}
