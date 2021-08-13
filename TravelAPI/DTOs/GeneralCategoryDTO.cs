using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelAPI.Models.Domain;

namespace TravelAPI.DTOs
{
    public class GeneralCategoryDTO
    {

        #region Properties 

        public int Id { get; set; }

        public string Name { get; set; }

        #endregion

        #region Constructors
        public GeneralCategoryDTO()
        {

        }

        public GeneralCategoryDTO(GeneralCategory category)
        {
            Id = category.Id;
            Name = category.Name;
        }

        public GeneralCategoryDTO(string name)
        {
            Name = name;
        }
        #endregion  
    }
}
