using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PierresTreats.Models
{
  public class Treat
  {
    public int TreatId { get; set; }
    [Required(ErrorMessage = "You must enter a treat name!")]
    public string TreatName { get; set; }
    [Range(1, int.MaxValue, ErrorMessage = "You must enter a number greater than or equal to 1.")]
    public int StockQuantity { get; set; }
    public List<TreatFlavor> JoinEntities { get; set; }
    public List<UserTreat> JoinEntities2 { get; set; }
  }
}