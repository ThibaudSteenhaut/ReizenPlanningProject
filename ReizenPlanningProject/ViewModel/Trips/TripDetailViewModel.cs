using ReizenPlanningProject.Data.Repositories;
using ReizenPlanningProject.Model;
using ReizenPlanningProject.Model.Domain;
using ReizenPlanningProject.Model.IRepositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReizenPlanningProject.ViewModel.Trips
{
    public class TripDetailViewModel
    {

        #region Fields 

        private readonly IItemRepository _itemRepository = new ItemRepository();

        #endregion

        #region Properties

        public Trip Trip { get; set; }

        public ObservableCollection<TripItem> TripItems { get; set; }
        public ObservableCollection<Category> Categories = new ObservableCollection<Category>();
        public ObservableCollection<GroupTripItemList> GroupedTripItemsList = new ObservableCollection<GroupTripItemList>();

        #endregion

        public TripDetailViewModel()
        {

        }

        public void Initialize()
        {

            this.TripItems = _itemRepository.GetTripItems(Trip.Id);
            this.Categories = _itemRepository.GetCategories(); 

            BuildItemList();
        }

        private void BuildItemList()
        {

            this.GroupedTripItemsList.Clear();

            List<GroupTripItemList> groupedTripItems = GetTripItemsGrouped();

            //Add Categories to the grouped list that do not contain any items

            //foreach (Category category in Categories)
            //{
            //    List<TripItem> tripItems = groupedTripItems.SingleOrDefault(list => list.Key == category.Name);

            //    if (tripItems == null)
            //    {
            //        GroupTripItemList group = new GroupTripItemList(new List<TripItem>())
            //        {
            //            Key = category.Name
            //        };

            //        groupedTripItems.Add(group);
            //    }
            //}

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
    }
}
