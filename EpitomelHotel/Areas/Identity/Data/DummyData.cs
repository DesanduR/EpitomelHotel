using System.Numerics;
using EpitomelHotel.Areas.Identity.Data;
using EpitomelHotel.Models;
using Microsoft.CodeAnalysis;

namespace EpitomelHotel.Data
{
    public class DatabaseStartup
    {
        public static void StartUp(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var Context = serviceScope.ServiceProvider.GetService<EpitomelHotelDbContext>();
                Context.Database.EnsureCreated();

                if (Context.Guest.Any() || Context.Rooms.Any() || Context.Bookings.Any() || Context.Payments.Any() || Context.Services.Any() || Context.BookingService.Any() || Context.Staff.Any() || Context.Status.Any())
                {
                    return;
                }
                var guests = new Guest[]     
                {
                        new Guest { Firstname = "Kimlong", Lastname = "Chour", Email = "kimlongc@gmail.com", Phone = "0223535356" },
                        new Guest { Firstname = "John", Lastname = "Smith", Email = "johndoe@gmail.com", Phone = "0227545856"},
                        new Guest { Firstname = "Alice", Lastname = "Smith", Email = "alice.smith@example.com", Phone = "0987654321" },
                        new Guest { Firstname = "Bob", Lastname = "Johnson", Email = "bob.johnson@example.com", Phone = "0456781234" },
                        new Guest { Firstname = "Sarah", Lastname = "Williams", Email = "sarah.williams@example.com", Phone = "0223344556" },
                        new Guest { Firstname = "David", Lastname = "Brown", Email = "david.brown@example.com", Phone = "0212345678" },
                        new Guest { Firstname = "Eve", Lastname = "Davis", Email = "eve.davis@example.com", Phone = "0435678901" }

                };
                Context.Guest.AddRange(guests);
                Context.SaveChanges();

                var services = new Services[]     
                {
                        new Services { ServiceName = "Room Service" },
                        new Services { ServiceName = "Laundry" },
                        new Services { ServiceName = "Spa" },
                        new Services { ServiceName = "Fitness Center" },
                        new Services { ServiceName = "Airport Shuttle" },
                        new Services { ServiceName = "Concierge" },
                        new Services { ServiceName = "Housekeeping" }
            };
                Context.Services.AddRange(services);
                Context.SaveChanges();


                var staff = new Staff[]     
                {
                        new Staff { Firstname = "William", Lastname = "Bob", Phonenumber = "02407483987", Role = "Receptionist", },
                        new Staff { Firstname = "Olivia", Lastname = "Smith", Phonenumber = "0546757467", Role = "Manager", },
                        new Staff { Firstname = "Lucas", Lastname = "Johnson", Phonenumber = "0240546676", Role = "Security", },
                        new Staff { Firstname = "Emma", Lastname = "Brown", Phonenumber = "02454864867", Role = "Cleaner", },
                        new Staff { Firstname = "James", Lastname = "Taylor", Phonenumber = "02546646556", Role = "Chef", },
                };
                Context.Staff.AddRange(staff);
                Context.SaveChanges();

                var status = new Status[]     
                {
                        new Status { StatusName = "Free", },
                        new Status { StatusName = "Booked", },

                };
                Context.Status.AddRange(status);
                Context.SaveChanges();

                var bookings = new Bookings[]     
                {
                        new Bookings { CheckIn = new DateTime(2024, 7, 23, 9, 15, 0),CheckOut = new DateTime(2024, 8, 21, 2, 12, 0), TotalAmount = 165, PaymentStatus = "Paid", GuestID = 1 },
                        new Bookings { CheckIn = new DateTime(2024, 7, 24, 10, 0, 0),CheckOut = new DateTime(2024, 8, 25, 21, 11, 0), TotalAmount = 160, PaymentStatus = "Paid", GuestID = 2 },
                        new Bookings { CheckIn = new DateTime(2024, 7, 24, 11, 12, 0),CheckOut = new DateTime(2024, 8, 22, 20, 11, 0), TotalAmount = 175, PaymentStatus = "Not Paid", GuestID = 3 },
                        new Bookings { CheckIn = new DateTime(2024, 7, 26, 12, 33, 0),CheckOut = new DateTime(2024, 8, 28, 19, 15, 0), TotalAmount = 180, PaymentStatus = "Not Paid", GuestID = 4 },
                        new Bookings { CheckIn = new DateTime(2024, 7, 26, 14, 11, 0),CheckOut = new DateTime(2024, 8, 27, 21, 10, 0), TotalAmount = 120, PaymentStatus = "Paid", GuestID = 5 },
                };
                Context.Bookings.AddRange(bookings);
                Context.SaveChanges();


                var payments = new Payments[]     // There are 30 dummy datas for the Appointment table //
                {
                        new Payments { PaymentDate = new DateTime(2024, 7, 23, 9, 15, 0),TotalAmount = 130, Price = 60, BookingID = 1 },
                        new Payments { PaymentDate = new DateTime(2024, 7, 24, 10, 0, 0),TotalAmount = 145, Price = 50, BookingID = 2 },
                        new Payments { PaymentDate = new DateTime(2024, 7, 24, 11, 12, 0),TotalAmount = 160, Price = 70, BookingID = 3 },
                        new Payments { PaymentDate = new DateTime(2024, 7, 26, 12, 33, 0),TotalAmount = 135, Price = 65, BookingID = 4 },
                        new Payments { PaymentDate = new DateTime(2024, 7, 26, 14, 11, 0),TotalAmount = 170, Price = 75, BookingID = 5 },
                };
                Context.Payments.AddRange(payments);
                Context.SaveChanges();


                var bookingservice = new BookingService[]     // There are 30 dummy datas for the Appointment table //
               {
                        new BookingService { ServiceName = "Room Service", ServiceCost = 60, ServiceID = 1, RoomID = 1 },
                        new BookingService { ServiceName = "Cleaning Service", ServiceCost = 50, ServiceID = 2, RoomID = 2 },
                        new BookingService { ServiceName = "Valet Parking", ServiceCost = 10, ServiceID = 3, RoomID = 3 },
                        new BookingService { ServiceName = "Pet Services", ServiceCost = 45, ServiceID = 4, RoomID = 4 },
                        
               };
                Context.BookingService.AddRange(bookingservice);
                Context.SaveChanges();





            }
        }
    }
}
