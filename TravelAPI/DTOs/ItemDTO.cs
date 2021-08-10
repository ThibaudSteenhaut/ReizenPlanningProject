using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TravelAPI.Models;
using TravelAPI.Models.Domain;

namespace TravelAPI.DTOs
{
    public class ItemDTO
    {

        #region Properties 

        public int Id { get; set; }

        public string Name { get; set; }
        
        public CategoryDTO Category { get; set; }

        public bool IsGeneralItem { get; set; }

        #endregion

        #region Constructors
        public ItemDTO() { }

        public ItemDTO(Item item)
        {
            Id = item.Id;
            Name = item.Name;
            Category = new CategoryDTO(item.Category);
            IsGeneralItem = item.IsGeneralItem;
        }

        public ItemDTO(string name, CategoryDTO category, bool isGeneralItem)
        {
            Name = name;
            Category = category;
            IsGeneralItem = isGeneralItem;
        }
        #endregion  
    }
}
