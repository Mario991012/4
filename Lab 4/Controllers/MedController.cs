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
        
        public int grado { get; set; }

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
            grado = int.Parse(collection["Grado"]);
            Upload(file, grado);
            return RedirectToAction("Upload");
        }
        public ActionResult Upload(HttpPostedFileBase file, int grado)
        {
            var model = Server.MapPath("~/uploads/") + file.FileName;
            if(file.ContentLength > 0)
            {
                file.SaveAs(model);
                Datos.Instancia.LecturaArchivo(model, grado);
                ViewBag.Msg = "";
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
        public ActionResult Create(string Name)
        {
            PedidoController pedido = new PedidoController();
            pedido.AgregarMed(Name, grado);
            
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
            Med agregado = new Med();
            agregado.Nombre = collection["Buscado"];

            Med medEncontrado = Datos.Instancia.ArbolMed.CrearNodo(agregado);
            int posicion = medEncontrado.id;
            if (posicion == -1)
            {
                ViewBag.Error = "No se encontró";
            }
            else
            {
                foreach(var item in Datos.Instancia.ListaMed)
                {
                    if(posicion == item.id)
                    {
                        Datos.Instancia.MedBuscados.Add(item);
                        break;
                    }
                }
            }
            return View(Datos.Instancia.MedBuscados);
        }

    }
}
