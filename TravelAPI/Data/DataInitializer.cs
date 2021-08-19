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
                    Destination = "Italy, Rome 2022",
                    DepartureDate = new DateTime(2022, 1, 10),
                    ReturnDate = new DateTime(2022, 2, 10),
                   
                    User = user1,
                    TripItems = new List<TripItem>(),
                    TripTasks = new List<TripTask>(),
                    ItineraryItems = new List<ItineraryItem>()
                };
                Trip t2 = new Trip()
                {
                    Destination = "France, Paris 2022",
                    DepartureDate = new DateTime(2022, 12, 30),
                    ReturnDate = new DateTime(2023, 1, 10),
                    User = user1,
                    TripItems = new List<TripItem>(),
                    TripTasks = new List<TripTask>(),
                    ItineraryItems = new List<ItineraryItem>()
                };
                Trip t4 = new Trip()
                {
                    Destination = "France, Paris 2021",
                    DepartureDate = new DateTime(2021, 1, 10),
                    ReturnDate = new DateTime(2021, 2, 10),
                    User = user1,
                    TripItems = new List<TripItem>(),
                    TripTasks = new List<TripTask>(),
                    ItineraryItems = new List<ItineraryItem>()
                };
                Trip t3 = new Trip()
                {
                    Destination = "Prague",
                    DepartureDate = new DateTime(2021, 2, 21, 6, 15, 00),
                    ReturnDate = new DateTime(2021, 3, 10, 23, 45, 00),
                    User = user2,
                    TripItems = new List<TripItem>(),
                    TripTasks = new List<TripTask>(),
                    ItineraryItems = new List<ItineraryItem>()
                };
                Trip t5 = new Trip()
                {
                    Destination = "Russia, Moscou 2023",
                    DepartureDate = new DateTime(2023, 2, 21, 6, 15, 00),
                    ReturnDate = new DateTime(2023, 3, 10, 23, 45, 00),
                    User = user1,
                    TripItems = new List<TripItem>(),
                    TripTasks = new List<TripTask>(),
                    ItineraryItems = new List<ItineraryItem>()
                };

                GeneralCategory c1 = new GeneralCategory("Electronics", user1);
                GeneralCategory c2 = new GeneralCategory("Toiletries", user1);
                GeneralCategory c3 = new GeneralCategory("Clothes", user1);
                GeneralCategory c4 = new GeneralCategory("Books", user1);
                GeneralCategory c5 = new GeneralCategory("Trifles", user1);
                GeneralCategory c6 = new GeneralCategory("Legal stuff", user1);
                
                TripCategory tc1 = new TripCategory("Books for in Rome", t1);
                TripCategory tc2 = new TripCategory("Swimming Gear", t1);
                TripCategory tc3 = new TripCategory("Navigation", t1);
                TripCategory tc4 = new TripCategory("Clothes", t1);

                _context.Categories.Add(c1);
                _context.Categories.Add(c2);
                _context.Categories.Add(c3);
                _context.Categories.Add(c4);
                _context.Categories.Add(c5);
                _context.Categories.Add(c6);

                _context.TripCategories.Add(tc1);
                _context.TripCategories.Add(tc1);

                GeneralItem generalItem1 = new GeneralItem("Mobile phone", c1, user1);
                GeneralItem generalItem2 = new GeneralItem("Charger", c1, user1);
                GeneralItem generalItem3 = new GeneralItem("Camera", c1, user1);
                GeneralItem generalItem4 = new GeneralItem("Toothbrush", c1, user1);
                GeneralItem generalItem5 = new GeneralItem("Pants", c3, user1);
                GeneralItem generalItem6 = new GeneralItem("T-shirts", c3, user1);
                GeneralItem generalItem7 = new GeneralItem("Laptop", c1, user1);
                GeneralItem generalItem8 = new GeneralItem("Laptop charger", c1, user1);
                GeneralItem generalItem9 = new GeneralItem("Swimming trunks", c3, user1);
                GeneralItem generalItem10 = new GeneralItem("Bikini", c3, user1);
                GeneralItem generalItem11 = new GeneralItem("Comb", c2, user1);
                GeneralItem generalItem12 = new GeneralItem("Stretchers", c2, user1);
                GeneralItem generalItem13 = new GeneralItem("The Da Vinci Code, Dan Brown", c4, user1);
                GeneralItem generalItem14 = new GeneralItem("Harry Potter and the Philosopher's Stone, J.K. Rowling", c4, user1);
                GeneralItem generalItem15 = new GeneralItem("Twilight, Meyer Stephenie", c4, user1);
                GeneralItem generalItem16 = new GeneralItem("The Eye of The World, Robert Jordan", c4, user1);
                GeneralItem generalItem17 = new GeneralItem("Watch", c5, user1);
                GeneralItem generalItem18 = new GeneralItem("All Id cards", c6, user1);
                GeneralItem generalItem19 = new GeneralItem("Negative covid tests", c6, user1);
                GeneralItem generalItem20 = new GeneralItem("Drivers license", c6, user1);
               
           
                TripItem tripItem1 = new TripItem("Map of Rome", 1, tc3, true);
                TripItem tripItem2 = new TripItem("Map of Musea in Rome", 1, tc3, true);
                TripItem tripItem3 = new TripItem("Swimming pants", 2,  tc2, true);
                TripItem tripItem4 = new TripItem("The history of Rome", 1, tc1, true);
                TripItem tripItem5 = new TripItem("100 Things to see in Rome", 1, tc1, false);
                TripItem tripItem6 = new TripItem("The most attractive spots in Rome", 1, tc1, false);
                TripItem tripItem7 = new TripItem("Must-see Restaurants in Rome", 1, tc1, false);
                TripItem tripItem8 = new TripItem("T-shirts", 5, tc4, false);
                TripItem tripItem9 = new TripItem("Pants", 3, tc4, true);
                TripItem tripItem10 = new TripItem("Sweater", 5, tc4, true);
                TripItem tripItem11 = new TripItem("Socks", 8, tc4, true);
                TripItem tripItem12 = new TripItem("Underwear", 8, tc4, false);

                TripTask tt1 = new TripTask("Charge batteries for all devices");
                TripTask tt2 = new TripTask("Get covid tests done");
                TripTask tt3 = new TripTask("Check website for information about covid rules");
                TripTask tt4 = new TripTask("Get some maps about the city");
                TripTask tt5 = new TripTask("Check if we need extra medicines");
                TripTask tt6 = new TripTask("Eating a real italian pizza");
                TripTask tt7 = new TripTask("Going to the beach and relax");

                ItineraryItem ii1 = new ItineraryItem("Take a taxi to the airport", new DateTime(2022, 1, 10, 6, 30, 0));
                ItineraryItem ii2 = new ItineraryItem("Plane to Rome", new DateTime(2022, 1, 10, 8, 30, 0));
                ItineraryItem ii3 = new ItineraryItem("Take the bus to the hotel", new DateTime(2022, 1, 10, 14, 15, 0));
                ItineraryItem ii4 = new ItineraryItem("Take the bus back to the airport", new DateTime(2022, 2, 10, 6, 15, 0));
                ItineraryItem ii5 = new ItineraryItem("Plane back to Belgium", new DateTime(2022, 2, 10, 8, 15, 0));
                ItineraryItem ii6 = new ItineraryItem("Dad drives us back home", new DateTime(2022, 2, 10, 14, 30, 0));

                t1.TripItems.Add(tripItem1);
                t1.TripItems.Add(tripItem2);
                t1.TripItems.Add(tripItem3);
                t1.TripItems.Add(tripItem4);
                t1.TripItems.Add(tripItem5);
                t1.TripItems.Add(tripItem6);
                t1.TripItems.Add(tripItem7);
                t1.TripItems.Add(tripItem8);
                t1.TripItems.Add(tripItem9);
                t1.TripItems.Add(tripItem10);
                t1.TripItems.Add(tripItem11);
                t1.TripItems.Add(tripItem12);

                t1.ItineraryItems.Add(ii1);
                t1.ItineraryItems.Add(ii2);
                t1.ItineraryItems.Add(ii3);
                t1.ItineraryItems.Add(ii4);
                t1.ItineraryItems.Add(ii5);
                t1.ItineraryItems.Add(ii6);

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
                _context.Trips.Add(t5); 

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
                _context.Items.Add(generalItem19);
                _context.Items.Add(generalItem20);

                _context.SaveChanges();
            }
        }
    }
}
