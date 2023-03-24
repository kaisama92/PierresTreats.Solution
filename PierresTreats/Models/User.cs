using System.Collections.Generic;

namespace PierresTreats.Models
{
  public class User 
  {
    public int UserId { get; set; }
    public string UserName { get; set; }
    public List<UserTreat> JoinEntity2 { get; set; }
  }
}