﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TravelAPI.DTOs;
using TravelAPI.Models.Domain;

namespace TravelAPI.Models
{
    public class Trip
    {

        #region Properties 
        
        public int Id { get; set; }
        public string Destination { get; set; }
        public DateTime DepartureDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public IdentityUser User { get; set; }
        public List<TripItem> TripItems { get; set; }
        public List<Activity> Activities { get; set; }

        #endregion

        #region Constructors 

        public Trip()
        {

        }

        public Trip(string destination, DateTime departure, DateTime returnDate, IdentityUser user)
        {
            Destination = destination;
            DepartureDate = departure;
            ReturnDate = returnDate;
            User = user;
            TripItems = new List<TripItem>();
            Activities = new List<Activity>();
        }

        public Trip(TripDTO tripDTO, IdentityUser user)
        {
            Destination = tripDTO.Destination;
            DepartureDate = tripDTO.DepartureDate;
            ReturnDate = tripDTO.ReturnDate;
            User = user;
            TripItems = new List<TripItem>();
            Activities = new List<Activity>();
        }
        #endregion

    }
}
