using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;


namespace ReizenPlanningProject.Model
{
    public class Trip 
    {
        public int Id { get; set; }
        public string Destination { get; set; }
        public DateTime DepartureDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public ICollection<Item> Items { get; set; }
        public ICollection<Category> Categories { get; set; }
        /*
        public Trip(string destination, DateTime departureDate, DateTime returnDate)
        {
            this.Destination = destination;
            this.DepartureDate = departureDate;
            this.ReturnDate = returnDate;
        }

        public Trip()
        {

        }*/
    

      
       
    }
}
