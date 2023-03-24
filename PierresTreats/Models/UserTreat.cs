namespace PierresTreats.Models
{
  public class UserTreat
  {
    public int UserTreatId { get; set; }
    public int TreatId { get; set; }
    public Treat Treat { get; set; }
    public string UserName { get; set; }
    public User User { get; set; }
  }
}