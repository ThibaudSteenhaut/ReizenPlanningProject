using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TravelAPI.DTOs;

namespace TravelAPI.Models
{
    public class Category
    {

        #region Fields
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        
        public ICollection<Item> Items { get; set; }

        #endregion

        #region Contstructor

        public Category()
        {
            Items = new List<Item>();
        }

        public Category(string name)
        {
            Name = name;
            Items = new List<Item>();
        }

        public Category(CategoryDTO dto)
        {
            Name = dto.Name;
            Items = new List<Item>();
        } 
        #endregion

        #region Methods
        public void AddItem(Item item)
        {
            Items.Add(item); 
        }

        public void RemoveItem(Item item)
        {
            Items.Remove(item);
        }

        #endregion  
    }
}
