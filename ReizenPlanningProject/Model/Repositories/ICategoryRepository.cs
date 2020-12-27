using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReizenPlanningProject.Model.Repositories
{
    public interface ICategoryRepository
    {
        ObservableCollection<Category> GetAllCategoriesWithItems();
        Task<bool> Add(Category category);
        Task<bool> DeleteItemAsync(int categoryId, int itemId);

    }
}
