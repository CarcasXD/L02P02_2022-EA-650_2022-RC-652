using L02P02_2022_EA_650_2022_RC_652.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;

namespace L02P02_2022_EA_650_2022_RC_652.Controllers
{
    public class libroController : Controller
    {
        private readonly libroContext _libroContext;

        public libroController (libroContext libroContext)
        {
            _libroContext = libroContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult InicioVenta()
        {
            return View();
        }
        [HttpPost]
        public IActionResult InicioVenta(cliente cliente)
        {
            if (ModelState.IsValid)
            {
                _libroContext.clientes.Add(cliente);
                _libroContext.SaveChanges();

                PedidoEncabezado pedido = new PedidoEncabezado
                {
                    IdClientee = cliente.Id,
                    Fecha = DateTime.Now,
                    Estado = "P"
                };
                _libroContext.PedidoEncabezados.Add(pedido);
                _libroContext.SaveChanges();

                HttpContext.Session.SetInt32("idPedido",pedido.Id);

                return RedirectToAction("ListadoLibros");   
            }
            return View(cliente);
        }

    }
}
