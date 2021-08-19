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
using TripTask = ReizenPlanningProject.Model.Domain.TripTask;

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
        public List<Model.Domain.TripTask> Activities { get; set; }

        public ObservableCollection<TripItem> TripItems { get; set; }
        public ObservableCollection<GroupTripItemList> GroupedTripItemsList = new ObservableCollection<GroupTripItemList>();
        public ObservableCollection<TripTask> TripTasks { get; set; }


        #endregion

        #region Commands

        public RelayCommand AddTripItemCommand { get; set; }
        public RelayCommand DeleteTripItemCommand { get; set; }
        public RelayCommand AddGeneralItemCommand { get; set; }
        public RelayCommand SaveItemsCommand { get; set; }

        public RelayCommand AddCategoryCommand { get; set; }
        public RelayCommand DeleteCategoryCommand { get; set; }

        public RelayCommand AddTripTaskCommand { get; set; }
        public RelayCommand DeleteTripTaskCommand { get; set; }
        public RelayCommand SaveTasksCommand { get; set; }

        #endregion

        #region Constructor

        public TripDetailViewModel()
        {
            AddTripItemCommand = new RelayCommand(param => AddTripItem());
            AddGeneralItemCommand = new RelayCommand(param => AddGeneralItem());
            DeleteTripItemCommand = new RelayCommand(param => DeleteTripItem(param));
            SaveItemsCommand = new RelayCommand(param => SaveItems());
           
            AddCategoryCommand = new RelayCommand(param => AddCategory());
            DeleteCategoryCommand = new RelayCommand(param => DeleteCategory());

            AddTripTaskCommand = new RelayCommand(param => AddTripTask());
            DeleteTripTaskCommand = new RelayCommand(param => DeleteTripTask(param));
            SaveTasksCommand = new RelayCommand(param => SaveTasks());
            
        }

        #endregion

        #region Methods 

        private async void AddTripTask()
        {
            AddTripTaskDialog dialog = new AddTripTaskDialog();

            ContentDialogResult result = await dialog.ShowAsync();

            if (result == ContentDialogResult.Primary)
            {
                if (!String.IsNullOrEmpty(dialog.Description))
                {
                    TripTask tripTask = new TripTask(dialog.Description, false);
                    int tripTaskId = await _tripRepository.AddTripTask(Trip.Id, tripTask);
                    tripTask.Id = tripTaskId;
                    TripTasks.Add(tripTask); 
                }
            }
        }

        public void Initialize()
        {

            this.TripItems = _tripRepository.GetTripItems(Trip.Id);
            this.TripCategories = _tripRepository.GetTripCategories(Trip.Id);
            this.TripTasks = _tripRepository.GetTripTasks(Trip.Id);

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

        private void SaveTasks()
        {
            _tripRepository.UpdateTripTasks(TripTasks.ToList());
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

        private void DeleteTripTask(object param)
        {
            TripTask tripTask = (TripTask)param;
            _tripRepository.DeleteTripTask(tripTask.Id);
            TripTasks.Remove(tripTask);
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

        #endregion 
    }
}
