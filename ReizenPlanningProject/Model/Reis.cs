using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;


namespace ReizenPlanningProject.Model
{
    public class Reis : INotifyPropertyChanged
    {
        
        public string Bestemming { get; set; }
        public DateTime VertrekDatum { get; set; }
        public DateTime TerugDatum { get; set; }

        public Reis(string bestemming, DateTime vertrek, DateTime terug)
        {
            this.Bestemming = bestemming;
            this.VertrekDatum = vertrek;
            this.TerugDatum = terug;
        }

        public Reis()
        {

        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
