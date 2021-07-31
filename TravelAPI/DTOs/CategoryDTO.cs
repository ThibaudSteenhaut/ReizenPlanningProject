using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelAPI.Models.Domain;

namespace TravelAPI.DTOs
{
    public class CategoryDTO
    {

        #region Properties 

        public int Id { get; set; }

        public string Name { get; set; }

        #endregion

        #region Constructors
        public CategoryDTO()
        {

        }

        public CategoryDTO(Category category)
        {
            Id = category.Id;
            Name = category.Name;
        }

        public CategoryDTO(string name)
        {
            Name = name;
        }
        #endregion  
    }
}
