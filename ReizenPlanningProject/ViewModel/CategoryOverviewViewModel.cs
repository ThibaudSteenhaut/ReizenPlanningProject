using ReizenPlanningProject.Data.Repositories;
using ReizenPlanningProject.Model;
using ReizenPlanningProject.Model.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReizenPlanningProject.ViewModel
{
    public class CategoryOverviewViewModel
    {

        private readonly ICategoryRepository _categoryRepository = new CategoryRepository();

        public ObservableCollection<Category> Categories { get; set; } = new ObservableCollection<Category>();

        public event PropertyChangedEventHandler PropertyChanged;

        private bool isProgressRingActive = false;
        public bool IsProgressRingActive
        {
            get { return this.isProgressRingActive; }
            set
            {
                this.isProgressRingActive = value;
                this.RisePropertyChanged(nameof(this.IsProgressRingActive));
            }
        }

        public CategoryOverviewViewModel()
        {
            GetCategories();
        }

        public void GetCategories()
        {
            this.IsProgressRingActive = true;

            this.Categories = _categoryRepository.GetAllCategoriesWithItems();

            this.IsProgressRingActive = false;

        }

        private void RisePropertyChanged(string propertyName)
        {
            PropertyChangedEventArgs e = new PropertyChangedEventArgs(propertyName);
            this.PropertyChanged?.Invoke(this, e);
        }
    }
}
