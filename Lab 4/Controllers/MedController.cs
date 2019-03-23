using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Lab_4.Models;
using Lab_4.Singleton;

namespace Lab_4.Controllers
{
    public class MedController : Controller
    {
        public ActionResult Index()
        {
            return View(Datos.Instancia.ListaMed);
        }

        public ActionResult Carga()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Carga(FormCollection collection)
        {
            TempData["NumeroDeDatos"] = collection["nodo"];
            return RedirectToAction("Upload");
        }
        public ActionResult Upload(HttpPostedFileBase file)
        {
            var model = Server.MapPath("~/uploads/") + file.FileName;
            if(file.ContentLength > 0)
            {
                file.SaveAs(model);
                Datos.Instancia.LecturaArchivo(model);
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Msg = "ERROR";
            }
            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            foreach (var item in Datos.Instancia.ListaMed)
            {
                if (item.id == id)
                {
                    return View(item);
                }
            }
            return View();
        }

        // GET: Med/Create
        public ActionResult Create()
        {
            return RedirectToAction("Create", "AgregarMed", "Pedido");
        }

        // POST: Med/Create
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


    }
}
