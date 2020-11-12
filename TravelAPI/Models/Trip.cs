using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TravelAPI.Models
{
    public class Trip
    {
        public int Id { get; set; }

        [Required]
        public string destination { get; set; }

        public DateTime DepartureDate { get; set; }

        public DateTime ReturnDate { get; set; }
        //ICollection van categorieën, items om mee te nemen?

        public Trip()
        {

        }
    }
}
