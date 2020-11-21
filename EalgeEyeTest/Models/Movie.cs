using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EalgeEyeTest.Models
{
    public class Movie
    {
        public int Id { get; set; }
        [Required]
        public int MovieId { get; set; }
        [Required]
        public string Title { get; set; }

        [Required]
        public string Language { get; set; }

        [Required]
        [RegularExpression("^(2[0-3]|[01]?[0-9]):([0-5]?[0-9]):([0-5]?[0-9])$")]
        public string Duration { get; set; }

        [Required]
        public int ReleaseYear { get; set; }


    }
}
