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
                    Destination = "Rome, 2022",
                    DepartureDate = new DateTime(2022, 12, 30, 12, 30, 00),
                    ReturnDate = new DateTime(2023, 1, 10, 14, 30, 00),
                    User = user1,
                    TripItems = new List<TripItem>(),
                    TripTasks = new List<TripTask>()
                };
                Trip t2 = new Trip()
                {
                    Destination = "Parijs, 2022",
                    DepartureDate = new DateTime(2022, 1, 10, 13, 30, 00),
                    ReturnDate = new DateTime(2022, 2, 10, 21, 45, 00),
                    User = user1,
                    TripItems = new List<TripItem>(),
                    TripTasks = new List<TripTask>()
                };
                Trip t4 = new Trip()
                {
                    Destination = "Parijs",
                    DepartureDate = new DateTime(2021, 1, 10, 13, 30, 00),
                    ReturnDate = new DateTime(2021, 2, 10, 21, 45, 00),
                    User = user1,
                    TripItems = new List<TripItem>(),
                    TripTasks = new List<Models.Domain.TripTask>()
                };

                Trip t3 = new Trip()
                {
                    Destination = "Praag",
                    DepartureDate = new DateTime(2021, 2, 21, 6, 15, 00),
                    ReturnDate = new DateTime(2021, 3, 10, 23, 45, 00),
                    User = user2,
                    TripItems = new List<TripItem>(),
                    TripTasks = new List<Models.Domain.TripTask>()
                };


                GeneralCategory c1 = new GeneralCategory("Electronica", user1);
                GeneralCategory c2 = new GeneralCategory("Toiletspullen", user1);
                GeneralCategory c3 = new GeneralCategory("Kleren", user1);
                GeneralCategory c4 = new GeneralCategory("Boeken", user1);
                GeneralCategory c5 = new GeneralCategory("Prullen", user1);
                GeneralCategory c6 = new GeneralCategory("Alfatest", user1);
                
                TripCategory tc1 = new TripCategory("Books for in Rome", t1);
                TripCategory tc2 = new TripCategory("Swimming Gear", t1);
                TripCategory tc3 = new TripCategory("Navigation", t1);

                _context.Categories.Add(c1);
                _context.Categories.Add(c2);
                _context.Categories.Add(c3);
                _context.Categories.Add(c4);
                _context.Categories.Add(c5);
                _context.Categories.Add(c6);

                _context.TripCategories.Add(tc1);
                _context.TripCategories.Add(tc1);

                GeneralItem generalItem1 = new GeneralItem("Gsm", c1, user1);
                GeneralItem generalItem2 = new GeneralItem("Oplader", c1, user1);
                GeneralItem generalItem3 = new GeneralItem("Fototoestel", c1, user1);
                GeneralItem generalItem4 = new GeneralItem("Tandenborstel", c1, user1);
                GeneralItem generalItem5 = new GeneralItem("Broek", c3, user1);
                GeneralItem generalItem6 = new GeneralItem("T-shirt", c3, user1);
                GeneralItem generalItem7 = new GeneralItem("Laptop", c1, user1);
                GeneralItem generalItem8 = new GeneralItem("Laptop oplader", c1, user1);
                GeneralItem generalItem9 = new GeneralItem("Zwembroek", c3, user1);
                GeneralItem generalItem10 = new GeneralItem("Bikini", c3, user1);
                GeneralItem generalItem11 = new GeneralItem("Kam", c2, user1);
                GeneralItem generalItem12 = new GeneralItem("Rekkers", c2, user1);
                GeneralItem generalItem13 = new GeneralItem("Boek 1 : Met een iets langere titel", c4, user1);
                GeneralItem generalItem14 = new GeneralItem("Boek 2 : Blabla", c4, user1);
                GeneralItem generalItem15 = new GeneralItem("Boek 3 : Blabla", c4, user1);
                GeneralItem generalItem16 = new GeneralItem("Boek 4 : Blabla", c4, user1);
                GeneralItem generalItem17 = new GeneralItem("Rekkers", c5, user1);
                GeneralItem generalItem18 = new GeneralItem("Alfatest", c6, user1);

                TripItem tripItem1 = new TripItem("Map of Rome", 1, tc3, true);
                TripItem tripItem2 = new TripItem("Map of Musea in Rome", 1, tc3, true);
                TripItem tripItem3 = new TripItem("Swimming pants", 2,  tc2, true);
                TripItem tripItem4 = new TripItem("Love in Rome, JK rowling", 1, tc1, true);
                TripItem tripItem5 = new TripItem("History of Rome, JK rowling", 1, tc1, false);
                TripItem tripItem6 = new TripItem("From Rome with love, JK rowling", 1, tc1, false);
                TripItem tripItem7 = new TripItem("A night in Rome, JK rowling", 1, tc1, false);

                TripTask tt1 = new TripTask("Charge batteries for all devices");
                TripTask tt2 = new TripTask("Get covid tests done");
                TripTask tt3 = new TripTask("Check website for information about covid rules");
                TripTask tt4 = new TripTask("Get some maps about the city");
                TripTask tt5 = new TripTask("Check if we need extra medicines");
                TripTask tt6 = new TripTask("Sightseeing the Pantheon");
                TripTask tt7 = new TripTask("Sightseeing the AS ROMA Stadium");

                t1.TripItems.Add(tripItem1);
                t1.TripItems.Add(tripItem2);
                t1.TripItems.Add(tripItem3);
                t1.TripItems.Add(tripItem4);
                t1.TripItems.Add(tripItem5);
                t1.TripItems.Add(tripItem6);
                t1.TripItems.Add(tripItem7);

                t1.TripTasks.Add(tt1);
                t1.TripTasks.Add(tt2);
                t1.TripTasks.Add(tt3);
                t1.TripTasks.Add(tt4);
                t1.TripTasks.Add(tt5);
                t1.TripTasks.Add(tt6);
                t1.TripTasks.Add(tt7);

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
