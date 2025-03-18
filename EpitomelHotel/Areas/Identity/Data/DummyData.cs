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
                var guests = new Guest[]     // There are 30 dummy datas for the Diagnosis table //
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

                var services = new Services[]     // There are 30 dummy datas for the Hospital table //
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


                var staff = new Staff[]     // There are 30 dummy datas for the Patient table //
                {
                        new Staff { Firstname = "William", Lastname = "Bob", Phonenumber = "02407483987", Role = "Receptionist", },
                        new Staff { Firstname = "Olivia", Lastname = "Smith", Phonenumber = "0546757467", Role = "Manager", },
                        new Staff { Firstname = "Lucas", Lastname = "Johnson", Phonenumber = "0240546676", Role = "Security", },
                        new Staff { Firstname = "Emma", Lastname = "Brown", Phonenumber = "02454864867", Role = "Cleaner", },
                        new Staff { Firstname = "James", Lastname = "Taylor", Phonenumber = "02546646556", Role = "Chef", },
                };
                Context.Staff.AddRange(staff);
                Context.SaveChanges();

                var status = new Status[]     // There are 30 dummy datas for the Doctor table //
                {
                        new Status { StatusName = "Free", },
                        new Status { StatusName = "Booked", },

                };
                Context.Status.AddRange(status);
                Context.SaveChanges();

                var AppointmentTimes = new Appointment[]     // There are 30 dummy datas for the Appointment table //
                {
                        new Appointment { Date = new DateTime(2024, 7, 23, 9, 15, 0), PatientId = 1, DoctorId = 1 },
                        new Appointment { Date = new DateTime(2024, 7, 24, 10, 0, 0), PatientId = 2, DoctorId = 2 },
                        new Appointment { Date = new DateTime(2024, 7, 25, 11, 30, 0), PatientId = 3, DoctorId = 3 },
                        new Appointment { Date = new DateTime(2024, 7, 26, 9, 15, 0), PatientId = 4, DoctorId = 4 },
                        new Appointment { Date = new DateTime(2024, 7, 27, 13, 45, 0), PatientId = 5, DoctorId = 5 },
                        new Appointment { Date = new DateTime(2024, 7, 28, 8, 30, 0), PatientId = 6, DoctorId = 6 },
                        new Appointment { Date = new DateTime(2024, 7, 29, 14, 0, 0), PatientId = 7, DoctorId = 7 },
                        new Appointment { Date = new DateTime(2024, 7, 30, 15, 15, 0), PatientId = 8, DoctorId = 8 },
                        new Appointment { Date = new DateTime(2024, 7, 31, 10, 45, 0), PatientId = 9, DoctorId = 9 },
                        new Appointment { Date = new DateTime(2024, 8, 1, 12, 0, 0), PatientId = 10, DoctorId = 10 },
                        new Appointment { Date = new DateTime(2024, 8, 2, 16, 30, 0), PatientId = 11, DoctorId = 11 },
                        new Appointment { Date = new DateTime(2024, 8, 3, 9, 0, 0), PatientId = 12, DoctorId = 12 },
                        new Appointment { Date = new DateTime(2024, 8, 4, 11, 0, 0), PatientId = 13, DoctorId = 13 },
                        new Appointment { Date = new DateTime(2024, 8, 5, 13, 30, 0), PatientId = 14, DoctorId = 14 },
                        new Appointment { Date = new DateTime(2024, 8, 6, 14, 45, 0), PatientId = 15, DoctorId = 15 },
                        new Appointment { Date = new DateTime(2024, 8, 7, 9, 15, 0), PatientId = 16, DoctorId = 16 },
                        new Appointment { Date = new DateTime(2024, 8, 8, 10, 0, 0), PatientId = 17, DoctorId = 17 },
                        new Appointment { Date = new DateTime(2024, 8, 9, 11, 30, 0), PatientId = 18, DoctorId = 18 },
                        new Appointment { Date = new DateTime(2024, 8, 10, 9, 15, 0), PatientId = 19, DoctorId = 19 },
                        new Appointment { Date = new DateTime(2024, 8, 11, 13, 45, 0), PatientId = 20, DoctorId = 20 },
                        new Appointment { Date = new DateTime(2024, 8, 12, 8, 30, 0), PatientId = 21, DoctorId = 21 },
                        new Appointment { Date = new DateTime(2024, 8, 13, 14, 0, 0), PatientId = 22, DoctorId = 22 },
                        new Appointment { Date = new DateTime(2024, 8, 14, 15, 15, 0), PatientId = 23, DoctorId = 23 },
                        new Appointment { Date = new DateTime(2024, 8, 15, 10, 45, 0), PatientId = 24, DoctorId = 24 },
                        new Appointment { Date = new DateTime(2024, 8, 16, 12, 0, 0), PatientId = 25, DoctorId = 25 },
                        new Appointment { Date = new DateTime(2024, 8, 17, 16, 30, 0), PatientId = 26, DoctorId = 26 },
                        new Appointment { Date = new DateTime(2024, 8, 18, 9, 0, 0), PatientId = 27, DoctorId = 27 },
                        new Appointment { Date = new DateTime(2024, 8, 19, 11, 0, 0), PatientId = 28, DoctorId = 28 },
                        new Appointment { Date = new DateTime(2024, 8, 20, 13, 30, 0), PatientId = 29, DoctorId = 29 },
                        new Appointment { Date = new DateTime(2024, 8, 21, 14, 45, 0), PatientId = 30, DoctorId = 30 }
                };
                Context.AppointmentTime.AddRange(AppointmentTimes);
                Context.SaveChanges();
            }
        }
    }
}
