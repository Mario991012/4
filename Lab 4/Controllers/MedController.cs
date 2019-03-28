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

        public static int Grado = 5;

        public ActionResult Index()
        {
            Datos.Instancia.MedBuscados.Clear();
            return View(Datos.Instancia.ListaMed);
        }

        public ActionResult Carga()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Carga(HttpPostedFileBase file, FormCollection collection)
        {
            Grado = int.Parse(collection["Grado"]);
            Upload(file, Grado);
            return RedirectToAction("Upload");
        }
        public ActionResult Upload(HttpPostedFileBase file, int grado)
        {
            var informacion = Server.MapPath("~/uploads/Nodos.txt");

            var model = Server.MapPath("~/uploads/") + file.FileName;
            if(file.ContentLength > 0)
            {
                file.SaveAs(model);
                Datos.Instancia.LecturaArchivo(model, grado, informacion);
                ViewBag.Msg = "";
            }
            else
            {
                ViewBag.Msg = "ERROR";
            }
            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            return View(Datos.Instancia.ListaMed[id]);
        }

        // GET: Med/Create
        public ActionResult Create()
        {   
            return RedirectToAction("AgregarMed", "Pedido");
        }

        [HttpGet]
        public ActionResult AgregarMed()
        {
            PedidoController pedido = new PedidoController();
            pedido.AgregarMed(100);
            return RedirectToAction("AgregarMed", "Pedido");
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


        public ActionResult Buscar()
        {
            return View("Index");
        }

        [HttpPost]
        public ActionResult Buscar(FormCollection collection)
        {
            string NombreBuscado = collection["Buscado"];

            int id = Datos.Instancia.ArbolBMed.Buscador(NombreBuscado, Datos.Instancia.ArbolBMed.Raiz);

            if (id == -1)
            {
                ViewBag.Error = "No se encontró";
            }
            else
            {
                Datos.Instancia.MedBuscados.Add(Datos.Instancia.ListaMed[id]);
            }
            return View(Datos.Instancia.MedBuscados);
        }

    }
}
