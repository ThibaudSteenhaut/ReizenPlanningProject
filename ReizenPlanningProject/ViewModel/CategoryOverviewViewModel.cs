using ReizenPlanningProject.Data.Repositories;
using ReizenPlanningProject.Model;
using ReizenPlanningProject.Model.Repositories;
using ReizenPlanningProject.ViewModel.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReizenPlanningProject.ViewModel
{
    public class CategoryOverviewViewModel
    {

        private readonly ICategoryRepository _categoryRepository = new CategoryRepository();

        public ObservableCollection<Category> Categories { get; set; } = new ObservableCollection<Category>();

        public Item SelectedItem { get; set; }

        public Category SelectedCategory { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public RelayCommand DeleteItemCommand { get; set; }


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
            this.DeleteItemCommand = new RelayCommand((param) => DeleteItem(param));
        }

        private void DeleteItem(object param)
        {
            if(SelectedCategory != null && SelectedItem != null)
            {

                Category cat = this.Categories.FirstOrDefault(c => c == SelectedCategory);
                cat.Items.Remove(SelectedItem);
                _categoryRepository.DeleteItemAsync(SelectedCategory.Id, SelectedItem.Id); 
            }
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

        public void AddCategory(string name)
        {
            Category catToAdd = new Category(name);
            Debug.WriteLine(catToAdd); 
            _categoryRepository.Add(catToAdd);
            Categories.Add(catToAdd);
        }
    }
}
