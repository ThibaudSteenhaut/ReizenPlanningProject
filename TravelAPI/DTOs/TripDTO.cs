using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TravelAPI.DTOs
{
    public class TripDTO
    {
        [Required]
        public string Destination { get; set; }
    }
}
