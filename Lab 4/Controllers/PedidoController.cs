using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Lab_4.Models;
using Lab_4.Singleton;

namespace Lab_4.Controllers
{
    public class PedidoController : Controller
    {
        public static int Grado = 5;
        // GET: Pedido
        public ActionResult Index()
        {
            return View(Datos.Instancia.ListaPedidos);
        }

        // GET: Pedido/Details/5
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

        // GET: Pedido/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Pedido/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection, Pedido pedido)
        {
            try
            {
                if (string.IsNullOrEmpty(pedido.NombreCliente) || string.IsNullOrEmpty(pedido.Direccion) || string.IsNullOrEmpty(pedido.nit))
                {
                    ViewBag.Error = "Se necesita llenar todos los campos vacios";
                    return View(pedido);
                }
                else
                {
                        Pedido Vendido = new Pedido();
                        Vendido.NombreCliente = collection["NombreCliente"];
                        Vendido.nit = collection["nit"];
                        Vendido.Direccion = collection["Direccion"];
                        foreach(var med in Datos.Instancia.MedAVender)
                        {
                            Vendido.MedComprados = Vendido.MedComprados + ", " + med.Nombre;
                        }
                        Vendido.total = Datos.Instancia.TotalPedido;
                        Datos.Instancia.TotalPedido = 0;

                        Datos.Instancia.ListaPedidos.Add(Vendido);
                        Datos.Instancia.MedAVender.Clear();
                        ViewBag.Msg = "Pedido agregado correctamente";
                    
                    return RedirectToAction("Index");
                }
            }
            catch
            {
                return View();
            }
        }  



        [HttpPost]
        public ActionResult AgregarMed(int id)
        {
            if(Datos.Instancia.ListaMed[id].Existencia > 0)
            {
                Datos.Instancia.ListaMed[id].Existencia--;

                Datos.Instancia.TotalPedido += Datos.Instancia.ListaMed[id].Precio;
                Datos.Instancia.MedAVender.Add(Datos.Instancia.ListaMed[id]);
            }
            else
            {
                ViewBag.Error = "No hay cantidad necesaria.";
            }
            
            Datos.Instancia.MedBuscados.Clear();
            return View(Datos.Instancia.ListaMed);
        }

    }
}
