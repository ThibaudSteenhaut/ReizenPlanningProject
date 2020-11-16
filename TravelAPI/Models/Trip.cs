using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

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

        public ICollection<Category> Categories { get; set; }
        #endregion

        #region Constructors 
        public Trip()
        {
            Categories = new List<Category>(); 
        }

        public Trip(string destination, DateTime departure, DateTime returnDate)
        {
            Destination = destination;
            DepartureDate = departure;
            ReturnDate = returnDate;
        }
        #endregion

        #region Methods
        public void AddCategory(Category c)
        {
            Categories.Add(c); 
        }

        #endregion
    }
}
