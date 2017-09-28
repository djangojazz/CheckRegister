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
    [HttpGet]
    public ActionResult Login(string username, string password)
    {
        return View(new WebUser());
    }
      
    [HttpGet]
    public ActionResult NewUser()
    {
      return View();
    }

    [HttpPost]
    public ActionResult Login(WebUser user)
    {
      if (ModelState.IsValid)
      {
        if (user.Exists)
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

    public ActionResult Logout()
    {
      //FormsAuthentication.SignOut();
      return RedirectToAction("Index", "Home");
    }
  }
}