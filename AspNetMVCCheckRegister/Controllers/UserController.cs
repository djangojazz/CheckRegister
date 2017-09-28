﻿using AspNetMVCCheckRegister.Models;
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
      return View();
    }

    [HttpPost]
    public ActionResult Login(Models.User user)
    {
      if (ModelState.IsValid)
      {
        if (user.IsValid(user.UserName, user.Password))
        {
          //FormsAuthentication.SetAuthCookie(user.UserName, user.RememberMe);
          return RedirectToAction("Index", "Home", new CheckRegister.Models.User("b", "b") { IsAuthenticated = true});
        }
        else
        {
          ModelState.AddModelError("", "Login data is incorrect!");
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