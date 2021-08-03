using ReizenPlanningProject.Data.Repositories;
using ReizenPlanningProject.Model;
using ReizenPlanningProject.Model.Domain;
using ReizenPlanningProject.Model.IRepositories;
using ReizenPlanningProject.ViewModel.Commands;
using ReizenPlanningProject.Views.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace ReizenPlanningProject.ViewModel.Items
{
    public class GeneralItemsViewModel : INotifyPropertyChanged
    {

        #region Fields 

        private readonly IItemRepository _itemRepository = new ItemRepository();
        private bool _isProgressRingActive = false;

        #endregion

        #region Properties 

        public ObservableCollection<Item> Items { get; private set; } = new ObservableCollection<Item>();
        public ObservableCollection<GroupItemList> GroupedItemsList = new ObservableCollection<GroupItemList>();
        public ObservableCollection<Category> Categories = new ObservableCollection<Category>();

        public event PropertyChangedEventHandler PropertyChanged;

        public bool IsProgressRingActive
        {
            get { return this._isProgressRingActive; }
            set
            {
                this._isProgressRingActive = value;
                this.RisePropertyChanged(nameof(this.IsProgressRingActive));
            }
        }

        #endregion

        #region Commands 

        public RelayCommand AddItemCommand { get; set; }
        public RelayCommand DeleteItemCommand { get; set; }
        public RelayCommand AddCategoryCommand { get; set; }

        #endregion

        #region Constructors 

        public GeneralItemsViewModel()
        {
            this.IsProgressRingActive = _isProgressRingActive;
            Initialize();
            AddItemCommand = new RelayCommand(param => this.AddItem());
            AddCategoryCommand = new RelayCommand(param => this.AddCategory());
            DeleteItemCommand = new RelayCommand(param => this.DeleteItem(param));
        }

        #endregion

        #region Methods

        private void Initialize()
        {

            this.IsProgressRingActive = true;

            this.Items = _itemRepository.GetItems();
            this.Categories = _itemRepository.GetCategories();
            BuildItemList();

            this.IsProgressRingActive = false;

        }

        private async void AddItem()
        {
            AddItemDialog dialog = new AddItemDialog(Categories);

            ContentDialogResult result = await dialog.ShowAsync();

            if (result == ContentDialogResult.Primary)
            {
                if (!String.IsNullOrEmpty(dialog.Name) && dialog.SelectedCategory != null)
                {
                    Item itemToAdd = new Item(dialog.Name, dialog.SelectedCategory);
                    int id = await _itemRepository.Add(itemToAdd);
                    itemToAdd.Id = id;
                    Items.Add(itemToAdd);
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
                    if (!Categories.Any(c => c.Name == dialog.CategoryName))
                    {
                        Category catToAdd = new Category(dialog.CategoryName);
                        int catId = await _itemRepository.AddCategory(catToAdd);
                        catToAdd.Id = catId;
                        Categories.Add(catToAdd);
                        BuildItemList();
                    }
                }
            }
        }

        private void BuildItemList()
        {

            this.GroupedItemsList.Clear();

            List<GroupItemList> groupedItems = GetItemsGrouped();

            //Add Categories to the grouped list that do not contain any items

            foreach (Category category in Categories)
            {
                List<Item> items = groupedItems.SingleOrDefault(list => list.Key == category.Name);

                if (items == null)
                {
                    GroupItemList group = new GroupItemList(new List<Item>())
                    {
                        Key = category.Name
                    };

                    groupedItems.Add(group);
                }
            }

            groupedItems.Sort((g1, g2) => string.Compare(g1.Key, g2.Key));
            groupedItems.ForEach(g => GroupedItemsList.Add(g));
        }

        private void DeleteItem(object param)
        {

            Item i = (Item)param;
            Debug.WriteLine(i.Id);
            _itemRepository.Delete(i.Id);
            Items.Remove(i);
            BuildItemList();
        }

        public List<GroupItemList> GetItemsGrouped()
        {

            //groups the items into individual list per category

            var query = from item in Items
                        group item by item.Category.Name into g
                        orderby g.Key
                        select new GroupItemList(g) { Key = g.Key };

            return new List<GroupItemList>(query);

        }

        private void AddCategoriesWithoutItems(ObservableCollection<GroupItemList> groupedItemsList)
        {
            //List<string> categories = _itemRepository.GetCategories(); 

        }

        private void RisePropertyChanged(string propertyName)
        {
            PropertyChangedEventArgs e = new PropertyChangedEventArgs(propertyName);
            this.PropertyChanged?.Invoke(this, e);
        }

        #endregion

    }
}
