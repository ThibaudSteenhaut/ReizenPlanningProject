using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelAPI.Models;

namespace TravelAPI.Data.Repositories
{
    public class DataInitializer
    {

        private readonly ApplicationDbContext _context;


        public DataInitializer(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task InitializeDataAsync()
        {
            
            _context.Database.EnsureDeleted();
            if (_context.Database.EnsureCreated())
            {

                Trip t1 = new Trip() { Destination = "Rome", DepartureDate = new DateTime(2020, 12, 30), ReturnDate = new DateTime(2021, 1, 10) };
                Trip t2 = new Trip() { Destination = "Parijs", DepartureDate = new DateTime(2021, 1, 10), ReturnDate = new DateTime(2021, 2, 10) };
                Trip t3 = new Trip() { Destination = "Praag", DepartureDate = new DateTime(2021, 2, 21), ReturnDate = new DateTime(2021, 3, 10) };
                Trip t4 = new Trip() { Destination = "Praag", DepartureDate = new DateTime(2021, 2, 21), ReturnDate = new DateTime(2021, 3, 10) };
                Trip t5 = new Trip() { Destination = "Praag", DepartureDate = new DateTime(2021, 2, 21), ReturnDate = new DateTime(2021, 3, 10) };
                Trip t6 = new Trip() { Destination = "Praag", DepartureDate = new DateTime(2021, 2, 21), ReturnDate = new DateTime(2021, 3, 10) };
                Trip t7 = new Trip() { Destination = "Praag", DepartureDate = new DateTime(2021, 2, 21), ReturnDate = new DateTime(2021, 3, 10) };
                Trip t8 = new Trip() { Destination = "Praag", DepartureDate = new DateTime(2021, 2, 21), ReturnDate = new DateTime(2021, 3, 10) };

                Category c1 = new Category("Electronica");
                Category c2 = new Category("Toiletspullen");
                Category c3 = new Category("Kleren");
                Category c4 = new Category("Verzorging");

                _context.Categories.Add(c1);
                _context.Categories.Add(c2);
                _context.Categories.Add(c3);
                _context.Categories.Add(c4);

                Item i1 = new Item("Gsm","Electronica",2);
                Item i2 = new Item("Oplader","Electronica", 2);
                Item i3 = new Item("Fototoestel", "Electronica",1);
                Item i4 = new Item("Tandenborstel","Toiletspullen",2);
                Item i5 = new Item("Broek","Kleren", 2);
                Item i6 = new Item("T-shirt", "Kleren",4);

                TripItem tripItem1 = new TripItem { Trip = t1, Item = i1, Amount = 3 };
                TripItem tripItem2 = new TripItem { Trip = t1, Item = i2, Amount = 3 };
                TripItem tripItem3 = new TripItem { Trip = t1, Item = i3, Amount = 3 };
                TripItem tripItem4 = new TripItem { Trip = t1, Item = i4, Amount = 3 };


                _context.TripItems.Add(tripItem1);
                _context.TripItems.Add(tripItem2);
                _context.TripItems.Add(tripItem3);
                _context.TripItems.Add(tripItem4);
             
                c1.AddItem(i1);
                c1.AddItem(i2);
                c1.AddItem(i3);
                c2.AddItem(i4);
                c3.AddItem(i5);
                c3.AddItem(i6);

                _context.Trips.Add(t1);
                _context.Trips.Add(t2);
                _context.Trips.Add(t3);
                _context.Trips.Add(t4);
                _context.Trips.Add(t5);
                _context.Trips.Add(t6);
                _context.Trips.Add(t7);
                _context.Trips.Add(t8);

                _context.SaveChanges(); 
            }
        }
    }
}
