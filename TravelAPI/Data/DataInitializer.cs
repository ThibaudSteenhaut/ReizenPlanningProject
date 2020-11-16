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

                Item i1 = new Item("Gsm", 2);
                Item i2 = new Item("Oplader", 2);
                Item i3 = new Item("Fototoestel", 1);
                Item i4 = new Item("Tandenborstel", 2);
                Item i5 = new Item("Broek", 2);
                Item i6 = new Item("T-shirt", 4);

                c1.AddItem(i1);
                c1.AddItem(i2);
                c1.AddItem(i3);
                c2.AddItem(i4);
                c3.AddItem(i5);
                c3.AddItem(i6);

                t1.AddCategory(c1);
                t1.AddCategory(c2);
                t1.AddCategory(c3);
                t1.AddCategory(c4);

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
