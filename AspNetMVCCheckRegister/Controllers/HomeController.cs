﻿using CheckRegister.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AspNetMVCCheckRegister.Controllers
{
  public class HomeController : Controller
  {
    public ActionResult Index(User currentUser)
    {
      if (currentUser.IsAuthenticated) { return View(); }
      return RedirectToAction("Login", "User");
    }
      
  }
}