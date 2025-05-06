using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using EpitomelHotel.Models;
using Microsoft.AspNetCore.Identity;

namespace EpitomelHotel.Areas.Identity.Data;

// Add profile data for application users by adding properties to the ApplUser class
public class ApplUser : IdentityUser
{
    public ICollection<Bookings> Bookings { get; set; }


    [Required, MinLength(1), MaxLength(20), RegularExpression(@"^[A-Z][a-z\s]*$", ErrorMessage = "First Name required.")]
    public string Firstname { get; set; }

    [Required, MinLength(1), MaxLength(20), RegularExpression(@"^[A-Z][a-z\s]*$", ErrorMessage = "Last Name required.")]
    public string Lastname { get; set; }

    [Required, MinLength(1), MaxLength(20), RegularExpression(@"^\+?\d{1,3}[- ]?\(?\d{3}\)?[- ]?\d{3}[- ]?\d{4}$", ErrorMessage = "Phone Number required.")]
    public string Phone { get; set; }
}

