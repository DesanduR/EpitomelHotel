using EpitomelHotel.Areas.Identity.Data;
using EpitomelHotel.Models;

namespace EpitomelHotel.Data
{
    public class DummyData
    {
        public static void SeedData(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var Context = serviceScope.ServiceProvider.GetService<EpitomelHotelDbContext>();

                // Ensure database is created
                Context.Database.EnsureCreated();

                // Prevent duplication
                if (Context.Users.Any() || Context.Rooms.Any() || Context.Bookings.Any() ||
                    Context.Services.Any() || Context.Staff.Any() || Context.Payments.Any() || Context.BookingService.Any())
                {
                    return;
                }

                // ------------------- USERS -------------------
                var ApplUser = new ApplUser[]
                {
                    new ApplUser { Firstname = "John", Lastname = "Doe", UserName = "john.doe@hotel.com", Email = "john.doe@hotel.com", Phone = "+64210000001" },
                    new ApplUser { Firstname = "Jane", Lastname = "Smith", UserName = "jane.smith@hotel.com", Email = "jane.smith@hotel.com", Phone = "+64210000002" },
                    new ApplUser { Firstname = "Alice", Lastname = "Brown", UserName = "alice.brown@hotel.com", Email = "alice.brown@hotel.com", Phone = "+64210000003" },
                    new ApplUser { Firstname = "Bob", Lastname = "Taylor", UserName = "bob.taylor@hotel.com", Email = "bob.taylor@hotel.com", Phone = "+64210000004" },
                    new ApplUser { Firstname = "Emma", Lastname = "Wilson", UserName = "emma.wilson@hotel.com", Email = "emma.wilson@hotel.com", Phone = "+64210000005" },
                    new ApplUser { Firstname = "Liam", Lastname = "Martin", UserName = "liam.martin@hotel.com", Email = "liam.martin@hotel.com", Phone = "+64210000006" },
                    new ApplUser { Firstname = "Olivia", Lastname = "Clark", UserName = "olivia.clark@hotel.com", Email = "olivia.clark@hotel.com", Phone = "+64210000007" },
                    new ApplUser { Firstname = "Noah", Lastname = "Hall", UserName = "noah.hall@hotel.com", Email = "noah.hall@hotel.com", Phone = "+64210000008" },
                    new ApplUser { Firstname = "Sophia", Lastname = "Allen", UserName = "sophia.allen@hotel.com", Email = "sophia.allen@hotel.com", Phone = "+64210000009" },
                    new ApplUser { Firstname = "Lucas", Lastname = "King", UserName = "lucas.king@hotel.com", Email = "lucas.king@hotel.com", Phone = "+64210000010" }
                };
                Context.Users.AddRange(ApplUser);
                Context.SaveChanges();

                // ------------------- STATUS -------------------
                var Status = new Status[]
                {
                    new Status { StatusName = "Available" },
                    new Status { StatusName = "Occupied" },
                    new Status { StatusName = "Cleaning" },
                    new Status { StatusName = "Maintenance" },
                    new Status { StatusName = "Reserved" },
                    new Status { StatusName = "Out of Service" },
                    new Status { StatusName = "Pending" },
                    new Status { StatusName = "Checked Out" },
                    new Status { StatusName = "No Show" },
                    new Status { StatusName = "Late Checkout" }
                };
                Context.Status.AddRange(Status);
                Context.SaveChanges();

                // ------------------- ROOMS -------------------
                var Rooms = new Rooms[]
                {
                    new Rooms { RoomType = "Single", RoomNumber = "101", Price = 120, Capacity = 1, StatusID = Status[0].StatusID },
                    new Rooms { RoomType = "Double", RoomNumber = "102", Price = 150, Capacity = 2, StatusID = Status[1].StatusID },
                    new Rooms { RoomType = "Suite", RoomNumber = "103", Price = 300, Capacity = 4, StatusID = Status[2].StatusID },
                    new Rooms { RoomType = "Single", RoomNumber = "104", Price = 130, Capacity = 1, StatusID = Status[3].StatusID },
                    new Rooms { RoomType = "Double", RoomNumber = "105", Price = 180, Capacity = 2, StatusID = Status[4].StatusID },
                    new Rooms { RoomType = "Suite", RoomNumber = "201", Price = 320, Capacity = 5, StatusID = Status[5].StatusID },
                    new Rooms { RoomType = "Single", RoomNumber = "202", Price = 110, Capacity = 1, StatusID = Status[6].StatusID },
                    new Rooms { RoomType = "Double", RoomNumber = "203", Price = 170, Capacity = 2, StatusID = Status[7].StatusID },
                    new Rooms { RoomType = "Suite", RoomNumber = "301", Price = 350, Capacity = 5, StatusID = Status[8].StatusID },
                    new Rooms { RoomType = "Single", RoomNumber = "302", Price = 140, Capacity = 1, StatusID = Status[9].StatusID }
                };
                Context.Rooms.AddRange(Rooms);
                Context.SaveChanges();

                // ------------------- SERVICES -------------------
                var Services = new Services[]
                {
                    new Services { ServiceName = "Breakfast" },
                    new Services { ServiceName = "Airport Shuttle" },
                    new Services { ServiceName = "Spa" },
                    new Services { ServiceName = "Gym Access" },
                    new Services { ServiceName = "Laundry" },
                    new Services { ServiceName = "Mini Bar" },
                    new Services { ServiceName = "Wi-Fi" },
                    new Services { ServiceName = "Parking" },
                    new Services { ServiceName = "Room Cleaning" },
                    new Services { ServiceName = "Pool Access" }
                };
                Context.Services.AddRange(Services);
                Context.SaveChanges();

                // ------------------- STAFF -------------------
                var Staff = new Staff[]
                {
                    new Staff { Firstname = "Ethan", Lastname = "Miller", Profession = "Receptionist", Phonenumber = "+64220000001" },
                    new Staff { Firstname = "Ava", Lastname = "Davis", Profession = "Cleaner", Phonenumber = "+64220000002" },
                    new Staff { Firstname = "Mason", Lastname = "Lopez", Profession = "Chef", Phonenumber = "+64220000003" },
                    new Staff { Firstname = "Isabella", Lastname = "Hill", Profession = "Manager", Phonenumber = "+64220000004" },
                    new Staff { Firstname = "James", Lastname = "Scott", Profession = "Porter", Phonenumber = "+64220000005" },
                    new Staff { Firstname = "Grace", Lastname = "Adams", Profession = "Waiter", Phonenumber = "+64220000006" },
                    new Staff { Firstname = "Henry", Lastname = "Baker", Profession = "Security", Phonenumber = "+64220000007" },
                    new Staff { Firstname = "Zoe", Lastname = "Nelson", Profession = "Cleaner", Phonenumber = "+64220000008" },
                    new Staff { Firstname = "Owen", Lastname = "Carter", Profession = "Receptionist", Phonenumber = "+64220000009" },
                    new Staff { Firstname = "Amelia", Lastname = "Mitchell", Profession = "Concierge", Phonenumber = "+64220000010" }
                };
                Context.Staff.AddRange(Staff);
                Context.SaveChanges();

                // ------------------- BOOKINGS -------------------
                var Bookings = new Bookings[]
                {
                    new Bookings { ApplUserID = ApplUser[0].Id, RoomID = Rooms[0].RoomID, CheckIn = new DateTime(2025, 9, 1), CheckOut = new DateTime(2025, 9, 3), TotalAmount = 240, PaymentStatus = "Paid" },
                    new Bookings { ApplUserID = ApplUser[1].Id, RoomID = Rooms[1].RoomID, CheckIn = new DateTime(2025, 9, 2), CheckOut = new DateTime(2025, 9, 5), TotalAmount = 450, PaymentStatus = "Pending" },
                    new Bookings { ApplUserID = ApplUser[2].Id, RoomID = Rooms[2].RoomID, CheckIn = new DateTime(2025, 9, 4), CheckOut = new DateTime(2025, 9, 6), TotalAmount = 600, PaymentStatus = "Paid" },
                    new Bookings { ApplUserID = ApplUser[3].Id, RoomID = Rooms[3].RoomID, CheckIn = new DateTime(2025, 9, 7), CheckOut = new DateTime(2025, 9, 8), TotalAmount = 130, PaymentStatus = "Pending" },
                    new Bookings { ApplUserID = ApplUser[4].Id, RoomID = Rooms[4].RoomID, CheckIn = new DateTime(2025, 9, 9), CheckOut = new DateTime(2025, 9, 11), TotalAmount = 360, PaymentStatus = "Paid" },
                    new Bookings { ApplUserID = ApplUser[5].Id, RoomID = Rooms[5].RoomID, CheckIn = new DateTime(2025, 9, 12), CheckOut = new DateTime(2025, 9, 14), TotalAmount = 640, PaymentStatus = "Pending" },
                    new Bookings { ApplUserID = ApplUser[6].Id, RoomID = Rooms[6].RoomID, CheckIn = new DateTime(2025, 9, 15), CheckOut = new DateTime(2025, 9, 16), TotalAmount = 110, PaymentStatus = "Paid" },
                    new Bookings { ApplUserID = ApplUser[7].Id, RoomID = Rooms[7].RoomID, CheckIn = new DateTime(2025, 9, 17), CheckOut = new DateTime(2025, 9, 19), TotalAmount = 340, PaymentStatus = "Paid" },
                    new Bookings { ApplUserID = ApplUser[8].Id, RoomID = Rooms[8].RoomID, CheckIn = new DateTime(2025, 9, 20), CheckOut = new DateTime(2025, 9, 22), TotalAmount = 700, PaymentStatus = "Pending" },
                    new Bookings { ApplUserID = ApplUser[9].Id, RoomID = Rooms[9].RoomID, CheckIn = new DateTime(2025, 9, 23), CheckOut = new DateTime(2025, 9, 24), TotalAmount = 140, PaymentStatus = "Paid" }
                };
                Context.Bookings.AddRange(Bookings);
                Context.SaveChanges();

                // ------------------- PAYMENTS -------------------
                var Payments = new Payments[]
                {
                    new Payments { BookingID = Bookings[0].BookingID, Price = 120, TotalAmount = 240, PaymentDate = new DateTime(2025, 9, 1), PaymentMethod = "Credit Card" },
                    new Payments { BookingID = Bookings[1].BookingID, Price = 150, TotalAmount = 450, PaymentDate = new DateTime(2025, 9, 2), PaymentMethod = "PayPal" },
                    new Payments { BookingID = Bookings[2].BookingID, Price = 300, TotalAmount = 600, PaymentDate = new DateTime(2025, 9, 4), PaymentMethod = "Credit Card" },
                    new Payments { BookingID = Bookings[3].BookingID, Price = 130, TotalAmount = 130, PaymentDate = new DateTime(2025, 9, 7), PaymentMethod = "Cash" },
                    new Payments { BookingID = Bookings[4].BookingID, Price = 180, TotalAmount = 360, PaymentDate = new DateTime(2025, 9, 9), PaymentMethod = "Credit Card" },
                    new Payments { BookingID = Bookings[5].BookingID, Price = 320, TotalAmount = 640, PaymentDate = new DateTime(2025, 9, 12), PaymentMethod = "Bank Transfer" },
                    new Payments { BookingID = Bookings[6].BookingID, Price = 110, TotalAmount = 110, PaymentDate = new DateTime(2025, 9, 15), PaymentMethod = "Credit Card" },
                    new Payments { BookingID = Bookings[7].BookingID, Price = 170, TotalAmount = 340, PaymentDate = new DateTime(2025, 9, 17), PaymentMethod = "PayPal" },
                    new Payments { BookingID = Bookings[8].BookingID, Price = 350, TotalAmount = 700, PaymentDate = new DateTime(2025, 9, 20), PaymentMethod = "Credit Card" },
                    new Payments { BookingID = Bookings[9].BookingID, Price = 140, TotalAmount = 140, PaymentDate = new DateTime(2025, 9, 23), PaymentMethod = "Cash" }
                };
                Context.Payments.AddRange(Payments);
                Context.SaveChanges();

                // ------------------- BOOKING SERVICES -------------------
                var BookingService = new BookingService[]
                {
                    new BookingService { RoomID = Rooms[0].RoomID, ServiceID = Services[0].ServiceID, ServiceCost = 20 },
                    new BookingService { RoomID = Rooms[1].RoomID, ServiceID = Services[1].ServiceID, ServiceCost = 30 },
                    new BookingService { RoomID = Rooms[2].RoomID, ServiceID = Services[2].ServiceID, ServiceCost = 50 },
                    new BookingService { RoomID = Rooms[3].RoomID, ServiceID = Services[3].ServiceID, ServiceCost = 15 },
                    new BookingService { RoomID = Rooms[4].RoomID, ServiceID = Services[4].ServiceID, ServiceCost = 25 },
                    new BookingService { RoomID = Rooms[5].RoomID, ServiceID = Services[5].ServiceID, ServiceCost = 40 },
                    new BookingService { RoomID = Rooms[6].RoomID, ServiceID = Services[6].ServiceID, ServiceCost = 10 },
                    new BookingService { RoomID = Rooms[7].RoomID, ServiceID = Services[7].ServiceID, ServiceCost = 35 },
                    new BookingService { RoomID = Rooms[8].RoomID, ServiceID = Services[8].ServiceID, ServiceCost = 20 },
                    new BookingService { RoomID = Rooms[9].RoomID, ServiceID = Services[9].ServiceID, ServiceCost = 45 }
                };
                Context.BookingService.AddRange(BookingService);
                Context.SaveChanges();
            }
        }
    }
}
