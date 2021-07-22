using Microsoft.AspNetCore.Identity;
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

        public string Destination { get; set; }

        public DateTime DepartureDate { get; set; }

        public DateTime ReturnDate { get; set; }

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
        }
        #endregion
    }
}
