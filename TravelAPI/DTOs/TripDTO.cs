using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TravelAPI.Models;
using TravelAPI.Models.Domain;

namespace TravelAPI.DTOs
{
    public class TripDTO
    {
        #region Properties

        public int Id { get; set; }
        public string Destination { get; set; }
        public DateTime DepartureDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public int PreparationCompleted { get; set; }

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
            PreparationCompleted = CalculatePreparation(trip.TripItems, trip.TripTasks); 
        }

        private int CalculatePreparation(List<TripItem> tripItems, List<TripTask> tripTasks)
        {
            int total = tripItems.Count + tripTasks.Count;
            int tiCompleted = tripItems.Count(ti => ti.CheckedIn == true);
            int ttCompleted = tripTasks.Count(tt => tt.Done == true);
            int completed = tripItems.Count(ti => ti.CheckedIn == true) + tripTasks.Count(tt => tt.Done == true);

            if (total == 0) return 0;

            return (int)Math.Ceiling((double)completed / (double)total * 100);
        }
        #endregion
    }
}
