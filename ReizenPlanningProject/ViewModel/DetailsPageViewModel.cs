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
            GetCategories();
        }

        private void GetCategories()
        {
            this.Categories = _categoryRepository.GetCategories();
        }

    }
}
