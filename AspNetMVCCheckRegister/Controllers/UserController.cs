using AspNetMVCCheckRegister.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AspNetMVCCheckRegister.Controllers
{
  public class UserController : Controller
  {
    //
    // GET: /User/
    public ActionResult Index()
    {
      return View();
    }

    [HttpGet]
    public ActionResult Login()
    {
      return View(new User());
    }

    [HttpGet]
    public ActionResult NewUser()
    {
      return View();
    }

    [HttpPost]
    public ActionResult Login(User user)
    {
      if (ModelState.IsValid)
      {
        if (user.Exists)
          //user.IsValid(user.UserName, user.Password))
        {
          return RedirectToAction("Index", "Home", new User(user.UserName, user.Password));
        }
        else
        {
          ModelState.AddModelError("", "Login data is missing and required");
        }
      }
      return View(user);
    }

    public ActionResult Logout()
    {
      //FormsAuthentication.SignOut();
      return RedirectToAction("Index", "Home");
    }
  }
}