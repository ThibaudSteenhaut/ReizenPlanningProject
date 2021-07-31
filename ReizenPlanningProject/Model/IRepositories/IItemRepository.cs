using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReizenPlanningProject.Model.IRepositories
{
    public interface IItemRepository
    {
        Task<int> AddCategory(Category category);
        void Add(Item item);
        ObservableCollection<Item> GetItems();
        ObservableCollection<Category> GetCategories(); 
    }
}
