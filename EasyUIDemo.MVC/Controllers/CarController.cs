﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EasyUIDemo.MVC.Controllers
{
    public class CarController : Controller
    {
        //
        // GET: /Car/

        public ActionResult CarList()
        {
            return View();
        }

    }
}