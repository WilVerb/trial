using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAppEFsql.Models;

namespace WebAppEFsql.Controllers
{
    public class PersonsAgeAddressController : Controller
    {
        // GET: PersonsAgeAddress
        public ActionResult Index()
        {
            //ViewBag.Tutu = "ABC";
            PersonsUtil persons = new PersonsUtil();
            ViewBag.People = persons.GetPeopleAndAddress("Los Angeles", 45);
            return View();
        }

        // GET: PersonsAgeAddress/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PersonsAgeAddress/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PersonsAgeAddress/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: PersonsAgeAddress/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PersonsAgeAddress/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: PersonsAgeAddress/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PersonsAgeAddress/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
