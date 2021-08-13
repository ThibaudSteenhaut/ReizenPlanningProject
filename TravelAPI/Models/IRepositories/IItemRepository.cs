using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelAPI.DTOs;
using TravelAPI.Models.Domain;

namespace TravelAPI.Models
{
    public interface IItemRepository
    {

        #region GeneralItem 

        IEnumerable<GeneralItem> GetGeneralItems(string userId);
        GeneralItem GetBy(int itemId);
        void Add(GeneralItem item);
        void Update(GeneralItem item);
        void Delete(GeneralItem item);

        #endregion

        #region GeneralCategory

        IEnumerable<GeneralCategory> GetCategories(string userId);
        GeneralCategory GetCategoryBy(int categoryId);
        void AddCategory(GeneralCategory category);
        void DeleteCategoryWithItems(GeneralCategory category);

        #endregion

        void SaveChanges();
    }
}
