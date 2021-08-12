using ReizenPlanningProject.Data.Repositories;
using ReizenPlanningProject.Model;
using ReizenPlanningProject.Model.Domain;
using ReizenPlanningProject.Model.IRepositories;
using ReizenPlanningProject.Model.Repositories;
using ReizenPlanningProject.ViewModel.Commands;
using ReizenPlanningProject.Views.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace ReizenPlanningProject.ViewModel.Trips
{
    public class TripDetailViewModel
    {

        #region Fields 

        private readonly IItemRepository _itemRepository = new ItemRepository();
        private readonly ITripRepository _tripRepository = new TripRepository();

        #endregion

        #region Properties

        public Trip Trip { get; set; }
        public List<Category> TripCategories { get; set; }

        public ObservableCollection<TripItem> TripItems { get; set; }
        public ObservableCollection<GroupTripItemList> GroupedTripItemsList = new ObservableCollection<GroupTripItemList>();

        #endregion

        #region Commands

        public RelayCommand SaveCommand { get; set; }
        public RelayCommand AddTripItemCommand { get; set; }
        public RelayCommand AddCategoryCommand { get; set; }
        public RelayCommand DeleteTripItemCommand { get; set; }

        #endregion
        
        public TripDetailViewModel()
        {
            SaveCommand = new RelayCommand(param => SaveItems());
            AddTripItemCommand = new RelayCommand(param => AddTripItem());
            AddCategoryCommand = new RelayCommand(param => AddCategory());
            DeleteTripItemCommand = new RelayCommand(param => DeleteTripItem(param));
        }

        public void Initialize()
        {

            this.TripItems = _tripRepository.GetTripItems(Trip.Id);
            this.TripCategories = _tripRepository.GetTripCategories(Trip.Id);

            foreach (TripItem tripItem in TripItems)
            {
                if(!TripCategories.Any(c => c.Id == tripItem.Item.Category.Id))
                {
                    TripCategories.Add(tripItem.Item.Category);
                }
            }

            BuildItemList();
        }

        private void BuildItemList()
        {

            this.GroupedTripItemsList.Clear();
            List<GroupTripItemList> groupedTripItems = GetTripItemsGrouped();

            //Add Categories to the grouped list that do not contain any items

            foreach (Category category in TripCategories)
            {
                List<TripItem> tripItems = groupedTripItems.SingleOrDefault(list => list.Key == category.Name);

                if (tripItems == null)
                {
                    GroupTripItemList group = new GroupTripItemList(new List<TripItem>())
                    {
                        Key = category.Name
                    };

                    groupedTripItems.Add(group);
                }
            }

            groupedTripItems.Sort((g1, g2) => string.Compare(g1.Key, g2.Key));
            groupedTripItems.ForEach(g => GroupedTripItemsList.Add(g));
        }

        public List<GroupTripItemList> GetTripItemsGrouped()
        {

            //groups the items into individual list per category

            var query = from tripItem in TripItems
                        group tripItem by tripItem.Item.Category.Name into g
                        orderby g.Key
                        select new GroupTripItemList(g) { Key = g.Key };

            return new List<GroupTripItemList>(query);

        }


        private void SaveItems()
        {
            _tripRepository.UpdateTripItems(Trip.Id, TripItems.ToList());
        }

        private async void AddTripItem()
        {
            AddTripItemDialog dialog = new AddTripItemDialog(new ObservableCollection<Category>(TripCategories));

            ContentDialogResult result = await dialog.ShowAsync();

            if (result == ContentDialogResult.Primary)
            {
                
                if (!String.IsNullOrEmpty(dialog.TripItemName) && dialog.SelectedCategory != null)
                {

                    Item i = new Item(dialog.TripItemName, dialog.SelectedCategory);
                    TripItem ti = new TripItem(i, dialog.Amount, false);
                    int id = await _tripRepository.AddTripItem(Trip.Id, ti);
                    ti.Id = id;
                    TripItems.Add(ti);
                    BuildItemList();
                }
            }
        }

        private async void AddCategory()
        {
            AddCategoryDialog dialog = new AddCategoryDialog();

            ContentDialogResult result = await dialog.ShowAsync();

            if (result == ContentDialogResult.Primary)
            {
                if (!String.IsNullOrEmpty(dialog.CategoryName))
                {
                    if (!TripCategories.Any(c => c.Name == dialog.CategoryName))
                    {
                        Category catToAdd = new Category(dialog.CategoryName);
                        int catId = await _tripRepository.AddTripCategory(Trip.Id, catToAdd);
                        catToAdd.Id = catId;
                        TripCategories.Add(catToAdd);
                        BuildItemList();
                    }
                }
            }
        }

        private void DeleteTripItem(object param)
        {
            TripItem ti = (TripItem)param;
            _tripRepository.DeleteTripItem(ti.Id);
            TripItems.Remove(ti);
            BuildItemList();
        }
    }
}
