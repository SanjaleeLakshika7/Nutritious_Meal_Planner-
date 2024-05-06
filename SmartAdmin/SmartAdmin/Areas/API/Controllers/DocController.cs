using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartAdmin.Areas.API.Controllers
{
    [Area("API")]
    public class DocController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Item()
        {
            return View();
        }
        public IActionResult MainCategories()
        {
            return View();
        }
        public IActionResult SubCategories()
        {
            return View();
        }
        public IActionResult Brands()
        {
            return View();
        }
        public IActionResult Settings()
        {
            return View();
        }

        public IActionResult Orders()
        {
            return View();
        }
        public IActionResult OrderItems()
        {
            return View();
        }
        public IActionResult OrderActions()
        {
            return View();
        }
    }
}
