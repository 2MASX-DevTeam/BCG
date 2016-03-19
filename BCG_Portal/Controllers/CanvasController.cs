using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BCG_Portal.Controllers
{
    public class CanvasController : Controller
    {
        // GET: Canvas
        public ActionResult CanvasIndex()
        {
            return View();
        }

        // GET: Canvas/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Canvas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Canvas/Create
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

        // GET: Canvas/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Canvas/Edit/5
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

        // GET: Canvas/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Canvas/Delete/5
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
