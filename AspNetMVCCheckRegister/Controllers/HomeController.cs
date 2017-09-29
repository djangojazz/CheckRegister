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
      var x = user;

      //if (ModelState.IsValid)
      //{
      //  if (user.Password != null)
      //  {
      //    using (var data = new DataController()) { data.CreateUser(user); }
      //    return RedirectToAction("Login", "User", new { userName = user.UserName, password = user.Password });
      //  }
      //  else
      //  {
      //    ModelState.AddModelError("", "Password is missing and required");
      //  }
      //}

      return View(user);
    }
  }
}