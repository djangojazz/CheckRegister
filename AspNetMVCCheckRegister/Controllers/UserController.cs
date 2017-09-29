using AspNetMVCCheckRegister.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace AspNetMVCCheckRegister.Controllers
{
  public class UserController : Controller
  {
    [HttpGet]
    public ActionResult Login(string userName, string password) => View(new WebUser(userName, password));

    [HttpPost]
    public ActionResult Login(WebUser user)
    {
      if (ModelState.IsValid)
      {
        using (var data = new DataController())
        {
          var exists = data.Exists(user.UserName);
          if (!exists) { return RedirectToAction("NewUser", "User", new { userName = user.UserName }); }
          
          user.Authenticated = data.Authenticate(user.UserName, user.Password);
        }
        

        if (user.Authenticated)
        {
          return RedirectToAction("Transactions", "Home", user);
        }
        else { ModelState.AddModelError("", "UserName or Password is incorrect"); }
      }

      return View(user);
    }

    #region NewUser
    [HttpGet]
    public ActionResult NewUser(string userName) => View(new WebUser(userName, string.Empty));

    [HttpPost]
    public ActionResult NewUser(WebUser user)
    {
      if (ModelState.IsValid)
      {
        if (user.Password != null)
        {
          using (var data = new DataController()) { data.CreateUser(user); }
          return RedirectToAction("Login", "User", new { userName = user.UserName, password = user.Password });
        }
        else
        {
          ModelState.AddModelError("", "Password is missing and required");
        }
      }

      return View(user);
    } 
    #endregion

    public ActionResult Logout() => RedirectToAction("Login", "User", null);
  }
}