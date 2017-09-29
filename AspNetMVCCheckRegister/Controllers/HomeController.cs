using AspNetMVCCheckRegister.Models;
using System.Linq;
using System.Web.Mvc;

namespace AspNetMVCCheckRegister.Controllers
{
  public class HomeController : Controller
  {
    [HttpGet]
    public ActionResult Transactions(WebUser user)
    {
      using (var data = new DataController())
      {
        user.Transactions = data.Get(user.UserName);
        user.Balance = user.Transactions?.OrderByDescending(x => x.TransactionId)?.FirstOrDefault()?.RunningTotal ?? 0;
      }
      
      return View(user);
    }

    [HttpGet]
    public ActionResult Transaction(WebUser user, bool empty = false)
    {
      return View(user);
    }

    [HttpPost]
    public ActionResult Transaction(WebUser user)
    {
      if (user.TransactionRequest?.Amount > 0)
      {
        user.TransactionRequest.UserName = user.UserName;
        using (var data = new DataController()) { data.Post(user.TransactionRequest); }
        return RedirectToAction("Transactions", "Home", user);
      }
      else
      {
        ModelState.AddModelError("", "Amount is missing and required");
      }

      return View(user);
    }
  }
}