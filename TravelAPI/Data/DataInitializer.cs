﻿using System;
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
                Category c5 = new Category("Boeken");
                Category c6 = new Category("Prullen");


                _context.Categories.Add(c1);
                _context.Categories.Add(c2);
                _context.Categories.Add(c3);
                _context.Categories.Add(c4);
                _context.Categories.Add(c5);
                _context.Categories.Add(c6);

                Item i1 = new Item("Gsm", "Electronica", 2);
                Item i2 = new Item("Oplader","Electronica", 2);;
                Item i3 = new Item("Fototoestel", "Electronica",1);
                Item i4 = new Item("Tandenborstel","Toiletspullen",2);
                Item i5 = new Item("Broek","Kleren", 2);
                Item i6 = new Item("T-shirt", "Kleren",4);
                Item i7 = new Item("Laptop", "Electronica", 1);
                Item i8 = new Item("Laptop oplader", "Electronica", 1); 
                Item i9 = new Item("Zwembroek", "Kleren", 2);
                Item i10 = new Item("Bikini", "Kleren", 2);
                Item i11 = new Item("Kam", "Toiletspullen", 2);
                Item i12 = new Item("Rekkers", "Toiletspullen", 5);
                Item i13 = new Item("Boek 1 : Met een iets langere titel", "Boeken", 1);
                Item i14 = new Item("Boek 2 : Blabla", "Boeken", 1);
                Item i15 = new Item("Boek 3 : Blabla", "Boeken", 1);
                Item i16 = new Item("Boek 4 : Blabla", "Boeken", 1);
                Item i17 = new Item("Rekkers", "Prullen", 1);


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
                c1.AddItem(i7);
                c1.AddItem(i8);
                c2.AddItem(i4);
                c2.AddItem(i11);
                c2.AddItem(i12);
                c3.AddItem(i5);
                c3.AddItem(i6);
                c3.AddItem(i9);
                c3.AddItem(i10);
                c5.AddItem(i13);
                c5.AddItem(i14);
                c5.AddItem(i15);
                c5.AddItem(i16);
                c6.AddItem(i17);
               


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
