using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;


namespace ReizenPlanningProject.Model
{
    public class Reis : INotifyPropertyChanged
    {
        private string bestemming;

        public string Bestemming
        {
            get { return bestemming; }
            set { bestemming = value; }
        }

        private DateTime vertrekDatum;

        public DateTime VertrekDatum
        {
            get { return vertrekDatum; }
            set { vertrekDatum = value; }
        }

        private DateTime terugDatum;

        public DateTime TerugDatum
        {
            get { return terugDatum; }
            set { terugDatum = value; }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
