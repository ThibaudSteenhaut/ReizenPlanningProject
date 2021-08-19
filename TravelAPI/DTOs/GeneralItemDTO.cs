using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TravelAPI.Models;
using TravelAPI.Models.Domain;

namespace TravelAPI.DTOs
{
    public class GeneralItemDTO
    {

        #region Properties 

        public int Id { get; set; }
        public string Name { get; set; }
        public GeneralCategoryDTO Category { get; set; }

        #endregion

        #region Constructors

        public GeneralItemDTO() { }

        public GeneralItemDTO(GeneralItem item)
        {
            Id = item.Id;
            Name = item.Name;
            Category = new GeneralCategoryDTO(item.Category);
        }

        public GeneralItemDTO(string name, GeneralCategoryDTO category)
        {
            Name = name;
            Category = category;
        }
        #endregion  
    }
}
