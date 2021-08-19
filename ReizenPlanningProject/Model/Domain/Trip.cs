using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;


namespace ReizenPlanningProject.Model
{
    public class Trip 
    {

        #region Properties 

        public int Id { get; set; }
        public string Destination { get; set; }
        public DateTime DepartureDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public int PreparationCompleted { get; set; }

        #endregion

        #region Constructors 

        public Trip(string destination, DateTime departureDate, DateTime returnDate, int prepCompleted)
        {
            this.Destination = destination;
            this.DepartureDate = departureDate;
            this.ReturnDate = returnDate;
            this.PreparationCompleted = prepCompleted;
        }

        public Trip()
        {

        }

        #endregion

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
