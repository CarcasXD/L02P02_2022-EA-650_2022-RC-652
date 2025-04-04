using L02P02_2022_EA_650_2022_RC_652.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace L02P02_2022_EA_650_2022_RC_652.Controllers
{
    public class libroController : Controller
    {
        private readonly libroContext _libroContext;

        public libroController(libroContext libroContext)
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
        public IActionResult InicioVenta(clientes cliente)
        {
            if (ModelState.IsValid)
            {
                _libroContext.clientes.Add(cliente);
                _libroContext.SaveChanges();

                pedido_encabezado pedido = new pedido_encabezado
                {
                    id_cliente = cliente.id,
                    cantidad_libro = 0,
                    total = 0
                };
                _libroContext.pedido_encabezado.Add(pedido);
                _libroContext.SaveChanges();

                HttpContext.Session.SetInt32("idPedido", pedido.id);

                return RedirectToAction("ListadoLibros");
            }
            return View(cliente);
        }

        public IActionResult ListadoLibros()
        {

            var libros = _libroContext.libros.ToList();


            int? idPedido = HttpContext.Session.GetInt32("idPedido");
            if (idPedido == null)
            {
              
                return RedirectToAction("InicioVenta");
            }

            int totalItems = _libroContext.pedido_detalle.Count(pd => pd.id_pedido == idPedido);
            ViewBag.TotalItems = totalItems;

            return View(libros);
        }

        [HttpPost]
        public IActionResult AgregarLibro(int idLibro)
        {
            int? idPedido = HttpContext.Session.GetInt32("idPedido");
            if (idPedido == null)
            {
                return RedirectToAction("InicioVenta");
            }

            var libroSeleccionado = _libroContext.libros.FirstOrDefault(l => l.id == idLibro);
            if (libroSeleccionado == null)
            {
                return RedirectToAction("ListadoLibros");
            }

            pedido_detalle detalle = new pedido_detalle
            {
                id_pedido = idPedido.Value,
                id_libro = idLibro,
                created_at = DateTime.Now
            };

            _libroContext.pedido_detalle.Add(detalle);
            _libroContext.SaveChanges();

            return RedirectToAction("ListadoLibros");
        }

        public IActionResult CompletarVenta()
        {
            return RedirectToAction("CierreVenta");
        }

        public IActionResult CierreVenta()
        {
            int? idPedido = HttpContext.Session.GetInt32("idPedido");
            if (idPedido == null)
            {
                return RedirectToAction("InicioVenta");
            }

            var pedido = _libroContext.pedido_encabezado.FirstOrDefault(p => p.id == idPedido);
            if (pedido == null)
            {
                return RedirectToAction("InicioVenta");
            }
            var cliente = _libroContext.clientes.FirstOrDefault(c => c.id == pedido.id_cliente);
            ViewBag.Cliente = cliente;

            var detalles = (from pd in _libroContext.pedido_detalle
                            join l in _libroContext.libros on pd.id_libro equals l.id
                            where pd.id_pedido == idPedido
                            select new
                            {
                                l.nombre,       
                                l.precio,
                                Cantidad = 1,   
                                Subtotal = l.precio
                            }).ToList();

            int cantidadTotal = detalles.Count;
            double totalVenta = detalles.Sum(d => d.Subtotal);

            pedido.cantidad_libro = cantidadTotal;
            pedido.total = totalVenta;
            _libroContext.pedido_encabezado.Update(pedido);
            _libroContext.SaveChanges();

            ViewBag.Total = totalVenta;
            ViewBag.Detalles = detalles;


            return View();
        }

        [HttpPost]
        public IActionResult CerrarVenta()
        {
            int? idPedido = HttpContext.Session.GetInt32("idPedido");
            if (idPedido == null)
            {
                return RedirectToAction("InicioVenta");
            }

            var pedido = _libroContext.pedido_encabezado.FirstOrDefault(p => p.id == idPedido);
            if (pedido == null)
            {
                return RedirectToAction("InicioVenta");
            }

            HttpContext.Session.Remove("idPedido");
            TempData["Mensaje"] = "La venta se cerró correctamente.";
            return RedirectToAction("MensajeConfirmacion");
        }

        public IActionResult MensajeConfirmacion()
        {
            ViewBag.Mensaje = TempData["Mensaje"];
            return View();
        }
    }
}
