using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TravelAPI.Models;

namespace TravelAPI.DTOs
{
    public class TripDTO
    {
        #region Fields 
        public int Id { get; set; }

        [Required]
        public string Destination { get; set; }

        [Required]
        public DateTime DepartureDate { get; set; }

        [Required]
        public DateTime ReturnDate { get; set; }

        public ICollection<Category> Categories { get; set; }


        #endregion

        #region Constructors
        public TripDTO()
        {

        }

        public TripDTO(Trip trip)
        {
            Id = trip.Id;
            Destination = trip.Destination;
            DepartureDate = trip.DepartureDate;
            ReturnDate = trip.ReturnDate;
            //Categories = trip.Categories;
        }
        #endregion
    }
}
