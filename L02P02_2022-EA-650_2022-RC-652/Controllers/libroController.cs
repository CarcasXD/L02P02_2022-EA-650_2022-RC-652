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

        
        [HttpPost]
        public IActionResult InicioVenta(clientes cliente)
        {
            if (ModelState.IsValid)
            {
                _libroContext.clientes.Add(cliente);
                _libroContext.SaveChanges();

                pedido_encabezado pedido = new pedido_encabezado
                {
                    IdClientee = cliente.Id,
                    Fecha = DateTime.Now,
                    Estado = "P"
                };
                _libroContext.pedido_encabezado.Add(pedido);
                _libroContext.SaveChanges();

                HttpContext.Session.SetInt32("idPedido",pedido.Id);

                return RedirectToAction("ListadoLibros");   
            }
            return View(cliente);
        }

    }
}
