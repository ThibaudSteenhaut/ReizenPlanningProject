using ReizenPlanningProject.Data.Repositories;
using ReizenPlanningProject.Model;
using ReizenPlanningProject.Model.Domain;
using ReizenPlanningProject.Model.Domain.GroupedLists;
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

        private readonly ITripRepository _tripRepository = new TripRepository();

        #endregion

        #region Properties

        public Trip Trip { get; set; }
        public List<Category> TripCategories { get; set; }
        public List<Model.Domain.Activity> Activities { get; set; }

        public ObservableCollection<TripItem> TripItems { get; set; }
        public ObservableCollection<GroupTripItemList> GroupedTripItemsList = new ObservableCollection<GroupTripItemList>();
        public ObservableCollection<GroupActivityList> GroupedActivitiesList = new ObservableCollection<GroupActivityList>(); 

        #endregion

        #region Commands

        public RelayCommand SaveCommand { get; set; }
        public RelayCommand AddTripItemCommand { get; set; }
        public RelayCommand DeleteTripItemCommand { get; set; }
        public RelayCommand AddCategoryCommand { get; set; }
        public RelayCommand DeleteCategoryCommand { get; set; }
        public RelayCommand AddGeneralItemCommand { get; set; }

        #endregion

        public TripDetailViewModel()
        {
            SaveCommand = new RelayCommand(param => SaveItems());
            AddTripItemCommand = new RelayCommand(param => AddTripItem());
            AddGeneralItemCommand = new RelayCommand(param => AddGeneralItem());
            AddCategoryCommand = new RelayCommand(param => AddCategory());
            DeleteTripItemCommand = new RelayCommand(param => DeleteTripItem(param));
            DeleteCategoryCommand = new RelayCommand(param => DeleteCategory());
        }

        public void Initialize()
        {

            this.TripItems = _tripRepository.GetTripItems(Trip.Id);
            this.TripCategories = _tripRepository.GetTripCategories(Trip.Id);
            this.Activities = _tripRepository.GetActivities(Trip.Id);

            BuildActivityList();
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

        public void BuildActivityList()
        {

            this.GroupedActivitiesList.Clear();
            List<GroupActivityList> groupedActivitiesList = GetGroupActivityLists();
            groupedActivitiesList.ForEach(g => GroupedActivitiesList.Add(g));


            foreach (GroupActivityList list in groupedActivitiesList)
            {
                Debug.WriteLine(list.Key.ToLongDateString());

                foreach (Model.Domain.Activity a in list)
                {
                    Debug.WriteLine(a);
                }
            }
        }

        private List<GroupActivityList> GetGroupActivityLists()
        {

            //groups the items into individual list per category

            var query = from activity in Activities
                        group activity by activity.Day into g
                        orderby g.Key
                        select new GroupActivityList(g) { Key = g.Key };

            return new List<GroupActivityList>(query);

        }

        public List<GroupTripItemList> GetTripItemsGrouped()
        {

            //groups the items into individual list per category

            var query = from tripItem in TripItems
                        group tripItem by tripItem.Category.Name into g
                        orderby g.Key
                        select new GroupTripItemList(g) { Key = g.Key };

            return new List<GroupTripItemList>(query);

        }

        public async void AddGeneralItem()
        {

            AddGeneralItemDialog dialog = new AddGeneralItemDialog();

            ContentDialogResult result = await dialog.ShowAsync();

            if (result == ContentDialogResult.Primary)
            {
                if (dialog.SelectedItem != null && dialog.SelectedCategory != null)
                {

                    if (!TripCategories.Any(c => c.Id == dialog.SelectedCategory.Id))
                    {
                        int id = await _tripRepository.AddTripCategory(Trip.Id, new Category(dialog.SelectedCategory.Name));
                        dialog.SelectedCategory.Id = id; 
                    }

                    await AddItem(dialog.SelectedItem.Name, dialog.SelectedCategory, dialog.Amount);
                    BuildItemList();
                }
            }
        }

        private void SaveItems()
        {
            _tripRepository.UpdateTripItems(TripItems.ToList());
        }

        private async void AddTripItem()
        {
            AddTripItemDialog dialog = new AddTripItemDialog(new ObservableCollection<Category>(TripCategories));

            ContentDialogResult result = await dialog.ShowAsync();
            
            if (result == ContentDialogResult.Primary)
            {
                if (!String.IsNullOrEmpty(dialog.TripItemName) && dialog.SelectedCategory != null)
                {
                    await AddItem(dialog.TripItemName, dialog.SelectedCategory, dialog.Amount);
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

        private async void DeleteCategory()
        {
            DeleteCategoryDialog dialog = new DeleteCategoryDialog(new ObservableCollection<Category>(TripCategories));

            ContentDialogResult result = await dialog.ShowAsync();

            if (result == ContentDialogResult.Primary)
            {
                if (dialog.SelectedCategory != null)
                {
                    _tripRepository.DeleteCategoryWithItems(dialog.SelectedCategory.Id);
                    TripCategories.Remove(dialog.SelectedCategory);
                    DeleteItemsWithCategory(dialog.SelectedCategory);
                    BuildItemList();
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

        private async Task AddItem(string name, Category category, int amount)
        {
            TripItem ti = new TripItem(name, category, amount);
            int id = await _tripRepository.AddTripItem(Trip.Id, ti);
            ti.Id = id;
            TripItems.Add(ti);
        }

        private void DeleteItemsWithCategory(Category selectedCategory)
        {

            //Use toList to copy the original collection or we get a collection modified exception 

            foreach (TripItem item in TripItems.ToList())
            {
                if (item.Category.Id == selectedCategory.Id)
                {
                    TripItems.Remove(item);
                }
            }
        }
    }
}
