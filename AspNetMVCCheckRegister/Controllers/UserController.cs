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
    DataController _dataController = new DataController();

    [HttpGet]
    public ActionResult Login(string userName, bool ignore = false)
    {
      if (ignore) { return View(new WebUser()); }

      var exists = _dataController.Exists(userName);
      if (!exists) { return RedirectToAction("NewUser", "User", new WebUser(userName, "a")); }


      return View(new WebUser());
    }

    [HttpPost]
    public ActionResult Login(WebUser user)
    {
      if (ModelState.IsValid)
      {

        if (user != null)
        {
          return RedirectToAction("Index", "Home", new WebUser(user.UserName, user.Password));
        }
        else
        {
          ModelState.AddModelError("", "Login data is missing and required");
        }
      }
      return View(user);
    }

    [HttpGet]
    public ActionResult NewUser(string userName)
    {
      return View(new WebUser());
    }

    [HttpPost]
    public ActionResult NewUser(WebUser user)
    {
      if (ModelState.IsValid)
      {
        if (user != null)
        {
          return RedirectToAction("Login", "User", user.UserName);
        }
        else
        {
          ModelState.AddModelError("", "Password is missing and required");
        }
      }

      return View(user);
    }

    public ActionResult Logout()
    {
      return RedirectToAction("Index", "Home", null);
    }
  }
}