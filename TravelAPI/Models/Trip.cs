using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TravelAPI.DTOs;

namespace TravelAPI.Models
{
    public class Trip
    {

        #region Fields 
        public int Id { get; set; }

        [Required]
        public string Destination { get; set; }

        [Required]
        public DateTime DepartureDate { get; set; }

        [Required]
        public DateTime ReturnDate { get; set; }

        public ICollection<TripItem> TripItems { get; set; }


        #endregion

        #region Constructors 
        public Trip()
        {
        }

        public Trip(string destination, DateTime departure, DateTime returnDate)
        {
            Destination = destination;
            DepartureDate = departure;
            ReturnDate = returnDate;
        }

        public Trip(TripDTO tripDTO)
        {
            Destination = tripDTO.Destination;
            DepartureDate = tripDTO.DepartureDate;
            ReturnDate = tripDTO.ReturnDate;
        }
        #endregion

    }
}
