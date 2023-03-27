using Microsoft.AspNetCore.Mvc;
using PierresTreats.Models;
using System.Collections.Generic;
using System.Linq;

namespace PierresTreats.Controllers
{
  public class HomeController: Controller
  {
    private readonly PierresTreatsContext _db;

    public HomeController(PierresTreatsContext db)
    {
      _db = db;
    }

    [HttpGet("/")]
    public ActionResult Index()
    {
      if (_db.Flavors.ToList().Count() == 0)
      {
        Flavor initialFlavor = new Flavor { FlavorName = "None" };
      _db.Flavors.Add(initialFlavor);
      _db.SaveChanges();
      }
      Flavor[] theseFlavors = _db.Flavors.ToArray();
      Treat[] theseTreats = _db.Treats.ToArray();
      Dictionary<string,object[]> model = new Dictionary<string, object[]>();
      model.Add("treats", theseTreats);
      model.Add("flavors", theseFlavors);
      return View(model);
    }
  }
}

