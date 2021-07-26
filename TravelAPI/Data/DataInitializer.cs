using IdentityModel;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TravelAPI.Models;

namespace TravelAPI.Data.Repositories
{
    public class DataInitializer
    {

        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;


        public DataInitializer(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager; 
        }

        public async Task InitializeDataAsync()
        {
            
            _context.Database.EnsureDeleted();
            if (_context.Database.EnsureCreated())
            {

                IdentityUser user1 = new IdentityUser { UserName = "thibaud@mail.com", Email = "thibaud@mail.com" };
                IdentityUser user2 = new IdentityUser { UserName = "thibaud2@mail.com", Email = "thibaud2@mail.com" };


                await _userManager.CreateAsync(user1, "Test123456789");
                await _userManager.AddClaimAsync(user1, new Claim(JwtClaimTypes.Role, "user"));

                await _userManager.CreateAsync(user2, "Test123456789");
                await _userManager.AddClaimAsync(user2, new Claim(JwtClaimTypes.Role, "user"));

                Trip t1 = new Trip() 
                { 
                    Destination = "Rome", 
                    DepartureDate = new DateTime(2020, 12, 30, 12, 30, 00), 
                    ReturnDate = new DateTime(2021, 1, 10, 14, 30, 00),
                    User = user1
                };
                Trip t2 = new Trip() 
                { 
                    Destination = "Parijs", 
                    DepartureDate = new DateTime(2021, 1, 10, 13, 30, 00), 
                    ReturnDate = new DateTime(2021, 2, 10, 21, 45, 00),
                    User = user1
                };
                Trip t3 = new Trip()
                {
                    Destination = "Praag",
                    DepartureDate = new DateTime(2021, 2, 21, 6, 15, 00),
                    ReturnDate = new DateTime(2021, 3, 10, 23, 45, 00),
                    User = user2 
                };


                Item i1 = new Item("Gsm", "Electronica", user1);
                Item i2 = new Item("Oplader","Electronica", user1);;
                Item i3 = new Item("Fototoestel", "Electronica", user1);
                Item i4 = new Item("Tandenborstel","Toiletspullen", user1);
                Item i5 = new Item("Broek","Kleren", user1);
                Item i6 = new Item("T-shirt", "Kleren", user1);
                Item i7 = new Item("Laptop", "Electronica", user1);
                Item i8 = new Item("Laptop oplader", "Electronica", user1); 
                Item i9 = new Item("Zwembroek", "Kleren", user1);
                Item i10 = new Item("Bikini", "Kleren", user1);
                Item i11 = new Item("Kam", "Toiletspullen", user1);
                Item i12 = new Item("Rekkers", "Toiletspullen", user1);
                Item i13 = new Item("Boek 1 : Met een iets langere titel", "Boeken", user1);
                Item i14 = new Item("Boek 2 : Blabla", "Boeken", user1);
                Item i15 = new Item("Boek 3 : Blabla", "Boeken", user1);
                Item i16 = new Item("Boek 4 : Blabla", "Boeken", user1);
                Item i17 = new Item("Rekkers", "Prullen", user1);

                _context.Trips.Add(t1);
                _context.Trips.Add(t2);
                _context.Trips.Add(t3);

                _context.Items.Add(i1);
                _context.Items.Add(i2);
                _context.Items.Add(i3);
                _context.Items.Add(i4);
                _context.Items.Add(i5);
                _context.Items.Add(i6);
                _context.Items.Add(i7);
                _context.Items.Add(i8);
                _context.Items.Add(i9);
                _context.Items.Add(i10);
                _context.Items.Add(i11);
                _context.Items.Add(i12);
                _context.Items.Add(i13);
                _context.Items.Add(i14);
                _context.Items.Add(i15);
                _context.Items.Add(i16);
                _context.Items.Add(i17);

                _context.SaveChanges(); 
            }
        }
    }
}
