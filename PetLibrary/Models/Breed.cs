using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PetLibrary
{
    [Serializable]
    public class Breed
    {
        [Key]
        [Required]
        [DisplayName("Breed Name")]
        public string Name { get; set; }

        [Required]
        [DisplayName("Type of Animal")]
        public string AnimalType { get; set; }

        [Required]
        [DisplayName("Number of Legs")]
        public int NumberOfLegs { get; set; }
    }
}
