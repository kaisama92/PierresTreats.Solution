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
      Flavor[] theseAuthors = _db.Flavors.ToArray();
      Treat[] theseBooks = _db.Treats.ToArray();
      Dictionary<string,object[]> model = new Dictionary<string, object[]>();
      model.Add("books", theseBooks);
      model.Add("authors", theseAuthors);
      return View(model);
    }
  }
}

