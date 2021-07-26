using ReizenPlanningProject.Data.Repositories;
using ReizenPlanningProject.Model;
using ReizenPlanningProject.Model.Domain;
using ReizenPlanningProject.Model.IRepositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public ObservableCollection<GroupItemList> GroupedItemsList { get; set; }

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

        #region Constructors 

        public GeneralItemsViewModel()
        {
            GetItems();
        }

        #endregion

        #region Methods

        private void GetItems()
        {

            this.IsProgressRingActive = true;

            this.Items = _itemRepository.GetItems();
            GetItemsGroupedAsync();

            this.IsProgressRingActive = false;

        }

        public void GetItemsGroupedAsync()
        {

            //groups the items into individual list per category

            var query = from item in Items
                        group item by item.Category into g
                        orderby g.Key
                        select new GroupItemList(g) { Key = g.Key };

            ObservableCollection<GroupItemList> groupedItemsList = new ObservableCollection<GroupItemList>(query);

            this.GroupedItemsList = groupedItemsList;
        }

        private void RisePropertyChanged(string propertyName)
        {
            PropertyChangedEventArgs e = new PropertyChangedEventArgs(propertyName);
            this.PropertyChanged?.Invoke(this, e);
        }

        #endregion

    }
}
