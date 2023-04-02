using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using PierresTreats.Models;
// using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Security.Claims;

namespace Library.Controllers
{
  [Authorize]
  public class TreatsController : Controller
  {
    private readonly PierresTreatsContext _db;
    private readonly UserManager<ApplicationUser> _userManager;

    public TreatsController(UserManager<ApplicationUser> userManager, PierresTreatsContext db)
    {
      _userManager = userManager;
      _db = db;
    }

    [AllowAnonymous]
    public ActionResult Index()
    {
      return View(_db.Treats.ToList());
    }

    public ActionResult Create()
    {
      ViewBag.FlavorId = new SelectList(_db.Flavors, "FlavorId", "FlavorName");
      return View();
    }

    [HttpPost]
    public ActionResult Create(Treat thisTreat, int flavorId)
    {
      if (!ModelState.IsValid)
      {
        ViewBag.FlavorId = new SelectList(_db.Flavors, "FlavorId", "FlavorName");
        return View(thisTreat);
      }
      else
      {
        _db.Treats.Add(thisTreat);
        _db.SaveChanges();
        if (flavorId != 0)
        {
          #nullable enable
          TreatFlavor? joinEntity = _db.TreatFlavors.FirstOrDefault(join => (join.FlavorId == flavorId && join.TreatId == thisTreat.TreatId));
          #nullable disable
          if (joinEntity == null && flavorId != 0)
          {
            _db.TreatFlavors.Add(new TreatFlavor() { FlavorId = flavorId, TreatId = thisTreat.TreatId });
            _db.SaveChanges();
          }
          
        }
        return RedirectToAction("Index");
      }
    }

    [AllowAnonymous]
    public ActionResult Details(int id)
    {
      Treat thisTreat = _db.Treats
          .Include(book => book.JoinEntities)
          .ThenInclude(join => join.Flavor)
          .FirstOrDefault(book => book.TreatId == id);
      return View(thisTreat);
    }

    public ActionResult Edit(int id)
    {
      Treat thisTreat = _db.Treats.FirstOrDefault(treat => treat.TreatId == id);
      ViewBag.FlavorId = new SelectList(_db.Flavors, "FlavorId", "FlavorName");
      return View(thisTreat);
    }

    [HttpPost]
    public ActionResult Edit(Treat treat, int stockQuantity)
    {
      treat.StockQuantity = stockQuantity;
      _db.Treats.Update(treat);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Delete(int id)
    {
      Treat thisTreat = _db.Treats.FirstOrDefault(treat => treat.TreatId == id);
      return View(thisTreat);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      Treat thisTreat = _db.Treats.FirstOrDefault(treat => treat.TreatId == id);
      _db.Treats.Remove(thisTreat);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult AddFlavor(int id)
    {
      Treat thisTreat = _db.Treats.FirstOrDefault(treat => treat.TreatId == id);
      ViewBag.FlavorId = new SelectList(_db.Flavors, "FlavorId", "FlavorName");
      return View(thisTreat);
    }

    [HttpPost]
    public ActionResult AddFlavor(Treat treat, int flavorId)
    {
      #nullable enable
      TreatFlavor? joinEntity = _db.TreatFlavors.FirstOrDefault(join => (join.FlavorId == flavorId && join.TreatId == treat.TreatId));
      #nullable disable
      if (joinEntity == null && flavorId != 0)
      {
        _db.TreatFlavors.Add(new TreatFlavor() { FlavorId = flavorId, TreatId = treat.TreatId });
        _db.SaveChanges();
      }
      return RedirectToAction("Details", new { id = treat.TreatId });
    }   

    [Authorize]
    public ActionResult Checkout(int id)
    {
      Treat thisTreat = _db.Treats.FirstOrDefault(model => model.TreatId == id);
      return View(thisTreat);
    }

    [HttpPost]
    public async Task<ActionResult> Checkout(Treat treat)
    {
      string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      ApplicationUser currentUser = await _userManager.FindByIdAsync(userId);
      // book.Copies = book.Copies - 1;
      #nullable enable
      UserTreat? joinEntity = _db.UserTreats.FirstOrDefault(join => (join.UserName == currentUser.UserName && join.TreatId == treat.TreatId));
      #nullable disable
      if (joinEntity == null)
      {
        Treat thisTreat = _db.Treats.FirstOrDefault(model => model.TreatId == treat.TreatId);
        thisTreat.StockQuantity = thisTreat.StockQuantity - 1;
        _db.Treats.Update(thisTreat);
        _db.UserTreats.Add(new UserTreat() { UserName = currentUser.UserName, TreatId = treat.TreatId });
        _db.SaveChanges();
      }
      return RedirectToAction("Details", new { id = treat.TreatId });
    }

    [HttpPost]
    public ActionResult Return(int joinId)
    {
      UserTreat join = _db.UserTreats.FirstOrDefault(entry => entry.UserTreatId == joinId);
      Treat treat = _db.Treats.FirstOrDefault(model => model.TreatId == join.TreatId);
      treat.StockQuantity += 1;
      _db.Treats.Update(treat);
      _db.UserTreats.Remove(join);
      _db.SaveChanges();
      return RedirectToAction("Index", "Account");
    }

    public ActionResult Error(Error error)
    {
      return View(error);
    }

    public ActionResult AddStock(int id)
    {
      Treat thisTreat = _db.Treats.FirstOrDefault(model => model.TreatId == id);
      return View(thisTreat);
    }

    [HttpPost]
    public ActionResult AddStock(Treat treat, int num)
    {
      if (num == 0)
      {
        Error error = new Error {};
        string errorMessage = "Input Needs To Be A NUMBER Greater Than 0 If You Want To Add Copies!";
        ViewBag.errorMessage1 = errorMessage;
        error.ErrorMessage = errorMessage;
        error.StoredId = treat.TreatId;
        return RedirectToAction("Error", error);
      }
      Treat thisTreat = _db.Treats.FirstOrDefault(model => model.TreatId == treat.TreatId);
      thisTreat.StockQuantity += num;
      _db.Update(thisTreat);
      _db.SaveChanges();
      return RedirectToAction("Details", new { id = thisTreat.TreatId });
    }

    [HttpPost]
    public ActionResult DeleteJoin(int joinId)
    {
      TreatFlavor joinEntry = _db.TreatFlavors.FirstOrDefault(entry => entry.TreatFlavorId == joinId);
      int TreatId = joinEntry.TreatId;
      _db.TreatFlavors.Remove(joinEntry);
      _db.SaveChanges();
      return RedirectToAction("Details", new{ id = TreatId});
    } 
  }
}