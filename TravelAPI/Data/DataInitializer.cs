using IdentityModel;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TravelAPI.Models;
using TravelAPI.Models.Domain;

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
                    DepartureDate = new DateTime(2022, 12, 30, 12, 30, 00),
                    ReturnDate = new DateTime(2023, 1, 10, 14, 30, 00),
                    User = user1,
                    TripItems = new List<TripItem>()
                };
                Trip t2 = new Trip()
                {
                    Destination = "Parijs, 2022",
                    DepartureDate = new DateTime(2022, 1, 10, 13, 30, 00),
                    ReturnDate = new DateTime(2022, 2, 10, 21, 45, 00),
                    User = user1,
                    TripItems = new List<TripItem>()
                };
                Trip t4 = new Trip()
                {
                    Destination = "Parijs",
                    DepartureDate = new DateTime(2021, 1, 10, 13, 30, 00),
                    ReturnDate = new DateTime(2021, 2, 10, 21, 45, 00),
                    User = user1,
                    TripItems = new List<TripItem>()
                };

                Trip t3 = new Trip()
                {
                    Destination = "Praag",
                    DepartureDate = new DateTime(2021, 2, 21, 6, 15, 00),
                    ReturnDate = new DateTime(2021, 3, 10, 23, 45, 00),
                    User = user2
                };


                Category c1 = new Category("Electronica", user1, true);
                Category c2 = new Category("Toiletspullen", user1, true);
                Category c3 = new Category("Kleren", user1, true);
                Category c4 = new Category("Boeken", user1, true);
                Category c5 = new Category("Prullen", user1, true);
                Category c6 = new Category("Alfatest", user1, true);
                Category c7 = new Category("Books for in Rome", user1, false, t1);
                Category c8 = new Category("Swimming Gear", user1, false, t1);

                _context.Categories.Add(c1);
                _context.Categories.Add(c2);
                _context.Categories.Add(c3);
                _context.Categories.Add(c4);
                _context.Categories.Add(c5);
                _context.Categories.Add(c6);
                _context.Categories.Add(c7);
                _context.Categories.Add(c8);

                Item generalItem1 = new Item("Gsm", c1, user1, true);
                Item generalItem2 = new Item("Oplader", c1, user1, true);
                Item generalItem3 = new Item("Fototoestel", c1, user1, true);
                Item generalItem4 = new Item("Tandenborstel", c1, user1, true);
                Item generalItem5 = new Item("Broek", c3, user1, true);
                Item generalItem6 = new Item("T-shirt", c3, user1, true);
                Item generalItem7 = new Item("Laptop", c1, user1, true);
                Item generalItem8 = new Item("Laptop oplader", c1, user1, true);
                Item generalItem9 = new Item("Zwembroek", c3, user1, true);
                Item generalItem10 = new Item("Bikini", c3, user1, true);
                Item generalItem11 = new Item("Kam", c2, user1, true);
                Item generalItem12 = new Item("Rekkers", c2, user1, true);
                Item generalItem13 = new Item("Boek 1 : Met een iets langere titel", c4, user1, true);
                Item generalItem14 = new Item("Boek 2 : Blabla", c4, user1, true);
                Item generalItem15 = new Item("Boek 3 : Blabla", c4, user1, true);
                Item generalItem16 = new Item("Boek 4 : Blabla", c4, user1, true);
                Item generalItem17 = new Item("Rekkers", c5, user1, true);
                Item generalItem18 = new Item("Alfatest", c6, user1, true);

                Item item1 = new Item("Map of Paris", c5, user1, false);
                Item item2 = new Item("Sunglasses", c3, user1, false);
                Item item3 = new Item("Hat", c3, user1, false);
                Item item4 = new Item("Love in Paris, JK rowling", c4, user1, false);
                Item item5 = new Item("Parisland, JK rowling", c4, user1, false);
                Item item6 = new Item("From Paris with love, JK rowling", c4, user1, false);
                Item item7 = new Item("A night in Paris, JK rowling", c4, user1, false);


                TripItem tripItem1 = new TripItem(item1, 2, false);
                TripItem tripItem2 = new TripItem(item2, 2, false);
                TripItem tripItem3 = new TripItem(item3, 3, false);
                TripItem tripItem4 = new TripItem(item4, 1, false);
                TripItem tripItem5 = new TripItem(item5, 1, false);
                TripItem tripItem6 = new TripItem(item6, 1, false);
                TripItem tripItem7 = new TripItem(item7, 1, false);

                t1.TripItems.Add(tripItem1);
                t1.TripItems.Add(tripItem2);
                t1.TripItems.Add(tripItem3);
                t1.TripItems.Add(tripItem4);
                t1.TripItems.Add(tripItem5);
                t1.TripItems.Add(tripItem6);
                t1.TripItems.Add(tripItem7);

                _context.Trips.Add(t1);
                _context.Trips.Add(t2);
                _context.Trips.Add(t3);
                _context.Trips.Add(t4);

                _context.Items.Add(generalItem1);
                _context.Items.Add(generalItem2);
                _context.Items.Add(generalItem3);
                _context.Items.Add(generalItem4);
                _context.Items.Add(generalItem5);
                _context.Items.Add(generalItem6);
                _context.Items.Add(generalItem7);
                _context.Items.Add(generalItem8);
                _context.Items.Add(generalItem9);
                _context.Items.Add(generalItem10);
                _context.Items.Add(generalItem11);
                _context.Items.Add(generalItem12);
                _context.Items.Add(generalItem13);
                _context.Items.Add(generalItem14);
                _context.Items.Add(generalItem15);
                _context.Items.Add(generalItem16);
                _context.Items.Add(generalItem17);
                _context.Items.Add(generalItem18);

                _context.SaveChanges();
            }
        }
    }
}
