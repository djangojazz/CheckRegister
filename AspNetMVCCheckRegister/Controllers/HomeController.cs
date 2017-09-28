using AspNetMVCCheckRegister.Models;
using System.Web.Mvc;

namespace AspNetMVCCheckRegister.Controllers
{
  public class HomeController : Controller
  {
    public ActionResult Index(User currentUser)
    {
      if (currentUser.Authenticated) { return View(); }
      return RedirectToAction("Login", "User");
    }
  }
}