﻿using Microsoft.AspNetCore.Mvc;

namespace ProjectOop.Controllers
{
    public class CategoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
