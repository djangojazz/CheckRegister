﻿using AspNetMVCCheckRegister.Models;
using System.Web.Mvc;

namespace AspNetMVCCheckRegister.Controllers
{
  public class HomeController : Controller
  {
    public ActionResult Index(WebUser currentUser)
    {
      if (currentUser.UserName != null) { return View(); }
      return RedirectToAction("Login", "User", new { userName = string.Empty, ignore = true });
    }
  }
}