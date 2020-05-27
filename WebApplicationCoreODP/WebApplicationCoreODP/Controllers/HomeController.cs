using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplicationCoreODP.Models;

namespace WebApplicationCoreODP.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";
            DataAccess da = new DataAccess();
            da.CallFunction(20);

            int deptId = 3;
            ViewData["dptId"] = deptId;
            string res = da.Connect(deptId);
            da.FetchEmployees(deptId);
            ViewData["toto"] = res;

            int nbDepts;
            nbDepts = da.FetchDataSets();
            ViewData["nbDepts"] = nbDepts;

            int nbEmpl;
            nbEmpl = da.FetchEmployees(deptId);
            ViewData["nbEmpls"] = nbEmpl;
            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

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
