using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Constraints.Controllers;
public class InvoicesController : Controller
{
    public new IActionResult View(string docNumber) {
        return View(docNumber as object);
    }

    public IActionResult Edit(string docNumber) {
        return View(docNumber as object);
    }
}
