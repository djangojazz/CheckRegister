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
    DataController _dataController = new DataController();

    [HttpGet]
    public ActionResult Login(string userName, string password)
    {
      return View(new WebUser(userName, password));
    }

    [HttpPost]
    public ActionResult Login(WebUser user)
    {
      if (ModelState.IsValid)
      {
        var exists = _dataController.Exists(user.UserName);
        if (!exists) { return RedirectToAction("NewUser", "User", new { userName = user.UserName }); }

        if (user != null)
        {
          FormsAuthentication.SetAuthCookie(user.UserName, false);
          return RedirectToAction("Index", "Home", new WebUser(user.UserName, user.Password));
        }
        else
        {
          ModelState.AddModelError("", "Login data is missing and required");
        }
      }
      return View(user);
    }

    #region NewUser
    [HttpGet]
    public ActionResult NewUser(string userName)
    {
      return View(new WebUser(userName, string.Empty));
    }

    [HttpPost]
    public ActionResult NewUser(WebUser user)
    {
      if (ModelState.IsValid)
      {
        if (user.Password != null)
        {
          _dataController.CreateUser(user);
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

    public ActionResult Logout()
    {
      return RedirectToAction("Index", "Home", null);
    }
  }
}