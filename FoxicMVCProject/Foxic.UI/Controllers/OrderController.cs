﻿using Microsoft.AspNetCore.Mvc;

namespace Foxic.UI.Controllers;

public class OrderController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
