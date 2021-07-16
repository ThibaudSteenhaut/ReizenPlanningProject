using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReizenPlanningProject.Model.Repositories
{
    public interface ITripRepository
    {
        ObservableCollection<Trip> GetTrips();
        Task<Trip> GetTripByDestinationAsync(string destination);
        Task<bool> Add(Trip trip);
        Task<bool> Remove(int tripId);

    }
}
