using AspNetMVCCheckRegister.Models;
using System.Web.Mvc;

namespace AspNetMVCCheckRegister.Controllers
{
  public class HomeController : Controller
  {
    public ActionResult Index(WebUser currentUser)
    {
      if (currentUser.UserName != null) { return View(currentUser); }
      return RedirectToAction("Login", "User");
    }

    public ActionResult Transaction(WebUser currentUser)
    {
      return View(currentUser);
    }

    public ActionResult Transactions(WebUser currentUser)
    {
      return View(currentUser);
    }
  }
}