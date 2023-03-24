using System.Collections.Generic;

namespace PierresTreats.Models
{
  public class User
  {
    public string UserName { get; set; }
    public List<UserTreat> JoinEntity2 { get; set; }
  }
}