using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReizenPlanningProject.Model
{
    public static class DummyDataSource
    {
        public static List<Trip> Trips { get; set; } = new List<Trip>()
                {
                    new Trip() { Destination="Rome", DepartureDate=new DateTime(2020,12,30), ReturnDate=new DateTime(2021,1,10)},
                    new Trip() { Destination="Parijs", DepartureDate=new DateTime(2021,1,10),ReturnDate=new DateTime(2021,2,10)},
                    new Trip() { Destination="Praag", DepartureDate=new DateTime(2021,2,21), ReturnDate=new DateTime(2021,3,10)},
                    new Trip() { Destination="Praag", DepartureDate=new DateTime(2021,2,21), ReturnDate=new DateTime(2021,3,10)},
                    new Trip() { Destination="Praag", DepartureDate=new DateTime(2021,2,21), ReturnDate=new DateTime(2021,3,10)},
                    new Trip() { Destination="Praag", DepartureDate=new DateTime(2021,2,21), ReturnDate=new DateTime(2021,3,10)},
                    new Trip() { Destination="Praag", DepartureDate=new DateTime(2021,2,21), ReturnDate=new DateTime(2021,3,10)},
                    new Trip() { Destination="Praag", DepartureDate=new DateTime(2021,2,21), ReturnDate=new DateTime(2021,3,10)},
             };
    }
}


