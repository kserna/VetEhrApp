using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetLibrary
{
    [Serializable]
    public class Pet
    {
        [Required]
        [DisplayName("Pet's Name")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Name must be between 1 and 50 characters.")]
        public string Name { get; set; }

        [Required]
        public virtual Breed Type { get; set; }

        [Required]
        [DisplayName("Age")]
        public int Age { get; set; }

        [Required]
        [RegularExpression(@"^[mMfF]{1}$", ErrorMessage = "Gender must be male 'M' or female 'F'")]
        [DisplayName("Gender")]
        public string Gender { get; set; }

        [DisplayName("Health Concerns")]
        [StringLength(250, ErrorMessage = "Health concerns must be less than 250 characters.")]
        public string Problem { get; set; }

        //[Required]
        //[DisplayName("Breed")]
        //public string Type { get; set; }
        //
        //[DisplayName("Number of Legs")]
        //public int? LegCount
        //{
        //    get
        //    {
        //        switch (Type)
        //        {
        //            case "dog":
        //            case "cat":
        //            case "lizard":
        //                return 4;

        //            case "parrot":
        //            case "cockatiel":
        //                return 2;

        //            case "snake":
        //                return 0;

        //            default:
        //                return null;
        //        }
        //    }
        //}

        //[DisplayName("Mammal")]
        //public string Group
        //{
        //    get
        //    {
        //        switch (Type)
        //        {
        //            case "dog":
        //            case "cat":
        //                return "mammal";

        //            case "parrot":
        //            case "cockatiel":
        //                return "bird";

        //            case "lizard":
        //            case "snake":
        //                return "reptile";

        //            default:
        //                return null;
        //        }
        //    }
        //}
    }
}
