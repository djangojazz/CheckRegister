using AspNetMVCCheckRegister.Models;
using System.Linq;
using System.Web.Mvc;

namespace AspNetMVCCheckRegister.Controllers
{
  public class HomeController : Controller
  {
    public ActionResult Transaction(WebUser user)
    {
      return View(user);
    }

    public ActionResult Transactions(WebUser user)
    {
      using (var data = new DataController())
      {
        user.Transactions = data.Get(user.UserName);
        user.Balance = user.Transactions?.OrderByDescending(x => x.TransactionId)?.FirstOrDefault()?.RunningTotal ?? 0;
      }
      
      return View(user);
    }
  }
}