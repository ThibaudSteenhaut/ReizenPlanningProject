using ReizenPlanningProject.Model.Domain;
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
        Task<int> Add(Item item);
        void Delete(int itemId); 
        ObservableCollection<Item> GetItems();
        ObservableCollection<Category> GetCategories();
        ObservableCollection<TripItem> GetTripItems(int tripId); 
    }
}
