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
      Flavor[] thesetreats = _db.Flavors.ToArray();
      Treat[] theseflavors = _db.Treats.ToArray();
      Dictionary<string,object[]> model = new Dictionary<string, object[]>();
      model.Add("treats", thesetreats);
      model.Add("flavors", theseflavors);
      return View(model);
    }
  }
}

