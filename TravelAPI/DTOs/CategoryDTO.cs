using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TravelAPI.Models;

namespace TravelAPI.DTOs
{
    public class CategoryDTO
    {

        #region Fields 
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public ICollection<Item> Items { get; set; }

        #endregion

        #region Constructors 
        public CategoryDTO()
        {

        }

        public CategoryDTO(Category c)
        {
            Id = c.Id;
            Name = c.Name;
            Items = c.Items;
        }
        #endregion
    }
}
