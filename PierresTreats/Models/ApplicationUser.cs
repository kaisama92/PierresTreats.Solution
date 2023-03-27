using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace PierresTreats.Models
{
    public class ApplicationUser : IdentityUser
    {
      public float Balance { get; set; }
      public Dictionary<string, int> ItemsBought { get; set; }
    }
}