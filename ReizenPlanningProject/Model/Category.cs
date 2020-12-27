using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReizenPlanningProject.Model
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ObservableCollection<Item> Items { get; set; }


        public Category()
        {

        }

        public Category(string name)
        {
            Name = name;
            Items = new ObservableCollection<Item>(); 
        }

        public override string ToString()
        {
            return String.Concat($"id: {Id}, " +
                $"Name: {Name}, " +
                $"Items: {Items}");
        }
    }
}
