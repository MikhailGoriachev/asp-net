using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace AnonymouseObject.Controllers
{
    public class InvoicesController : Controller
    {
        public new IActionResult View(string number)
        {
            return View(number as object);
        }

        public IActionResult Edit(string number)
        {
            return View(number as object);
        }
    }
}