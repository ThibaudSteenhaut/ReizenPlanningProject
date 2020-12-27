using Newtonsoft.Json;
using ReizenPlanningProject.Model;
using ReizenPlanningProject.ViewModel.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Diagnostics;
using ReizenPlanningProject.Model.Repositories;
using ReizenPlanningProject.Data.Repositories;
using ReizenPlanningProject.Converters;

namespace ReizenPlanningProject.ViewModel
{
    public class DetailsPageViewModel
    {
        public ObservableCollection<Category> Categories { get; set; } = new ObservableCollection<Category>();
        public Trip Trip { get; set; }
        private readonly ICategoryRepository _categoryRepository = new CategoryRepository();

        public DetailsPageViewModel(Trip trip)
        {
            this.Trip = trip;
            GenerateItemsPerCat();
            GetCategories();
        }

        private void GetCategories()
        {
            this.Categories = _categoryRepository.GetAllCategoriesWithItems();
        }

        public void AddItemsToTrip(List<Category> categories)
        {
            foreach (Category category in categories)
            {
                foreach (Item item in category.Items)
                {
                    Trip.TripItems.Add(new TripItem(Trip, item));
                   // Trip.Items.Add(item);
                }
            }
            GenerateItemsPerCat();
        }

        private ObservableCollection<GroupInfosList> _items = new ObservableCollection<GroupInfosList>();
        public ObservableCollection<GroupInfosList> Items
        {
            get => _items;
            set => value = _items;
        }

        public void GenerateItemsPerCat()
        {
            Items.Clear();
            var items = Trip.Items;
            var query = from item in items
                        group item by item.Category into g
                        orderby g.Key
                        select new { Category = g.Key, Items = g };

            foreach (var g in query)
            {
                GroupInfosList info = new GroupInfosList();
                info.Key = g.Category + " (" + g.Items.Count() + ")";

                foreach (var item in g.Items)
                {
                    info.Add(item);
                }

                Items.Add(info);
            }
        }

    }
    public class GroupInfosList : List<object>
    {
        public object Key { get; set; }
    }
}
