using ReizenPlanningProject.Model;
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

namespace ReizenPlanningProject.ViewModel.Itinerary
{
    public class ItineraryViewModel
    {

        #region Fields

        private readonly ITripRepository _tripRepository = new TripRepository();
      
        #endregion

        #region Properties

        public Trip Trip { get; set; }
        public string HeaderText { get; set; }

        private List<ItineraryItem> _itineraryItems { get; set; }
        public ObservableCollection<ItineraryItem> ItineraryItems { get; set; } = new ObservableCollection<ItineraryItem>();

        #endregion

        #region Command

        public RelayCommand DeleteItineraryItemCommand { get; set; }
        public RelayCommand AddItineraryItemCommand { get; set; }

        #endregion

        #region Constructors

        public ItineraryViewModel()
        {

            DeleteItineraryItemCommand = new RelayCommand(param => DeleteItineraryItem(param));
            AddItineraryItemCommand = new RelayCommand(param => AddItineraryItem());
        }

        public void Initialize()
        {
            _itineraryItems = _tripRepository.GetItineraryItems(Trip.Id).ToList();
            HeaderText = "Itinerary for " + Trip.Destination;
            BuildItineraryList();
        }

        private async void AddItineraryItem()
        {
            AddItineraryItemDialog dialog = new AddItineraryItemDialog(Trip);

            ContentDialogResult result = await dialog.ShowAsync();

            if (result == ContentDialogResult.Primary)
            {
                if (!String.IsNullOrEmpty(dialog.Description) && dialog.SelectedDate != null && dialog.SelectedTime != null)
                {
                    DateTime time = dialog.SelectedDate.AddHours(dialog.SelectedTime.Hours).AddMinutes(dialog.SelectedTime.Minutes);
                    ItineraryItem ii = new ItineraryItem(dialog.Description, time, (time.Date - DateTime.Now).Days);
                    int id = await _tripRepository.AddItineraryItem(Trip.Id, ii);
                    ii.Id = id;
                    ii.Description = $"{ii.Description} at {ii.Date:H:mm}";
                    _itineraryItems.Add(ii);
                    BuildItineraryList();
                }
            }
        }

        private void BuildItineraryList()
        {
            ItineraryItems.Clear();
            _itineraryItems.Sort((x, y) => x.Date.CompareTo(y.Date));
            _itineraryItems.ForEach(ii => ItineraryItems.Add(ii));
        }

        private void DeleteItineraryItem(object param)
        {
            ItineraryItem ii = (ItineraryItem)param; 
            _tripRepository.DeleteItineraryItem(ii.Id);
            ItineraryItems.Remove(ii);
        }

        #endregion
    }
}
