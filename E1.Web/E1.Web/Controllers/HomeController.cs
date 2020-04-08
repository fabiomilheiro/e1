using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using E1.Web.DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using E1.Web.Models;

namespace E1.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPersonRepository personRepository;
        private readonly ILogger<HomeController> logger;

        public HomeController(IPersonRepository personRepository, ILogger<HomeController> logger)
        {
            this.personRepository = personRepository;
            this.logger = logger;
        }

        public IActionResult Index()
        {
            var person = personRepository.GetPerson(1);
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
