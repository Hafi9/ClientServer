using Client.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers
{
    public class CobaController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
