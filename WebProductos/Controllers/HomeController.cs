using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebProductos.Datos;
using WebProductos.Models;

namespace WebProductos.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            //declarar una lista vacia
            List<E_Producto> productos = new List< E_Producto >();
            try
            {
                //creamos un objeto de la capa de datos
                D_Producto datos = new D_Producto();
                //obtenemos la lista de productos de la capa de datos
                productos=datos.ObtenerTodos();
                //pasamos a la vista index los productos como modelo
                return View("Index", productos);

            }
            catch (Exception ex )
            {
                TempData["error"] = ex.Message;
                return View("Index",productos);


            }
         
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult IrAgregar()
        {
            return View("VistaAgregar");
        }

        public IActionResult IrEditar(int idProducto)
        {
            D_Producto datos = new D_Producto();
            //apartir de idProducto obtenemos los datos del prody¿ucto
            E_Producto producto = datos.ObtenerProductosPorId(idProducto);
            //vamos al formulario de editar pasando el producto como modelo
            return View("VistaEditar", producto);
        }
        
        public IActionResult Editar(E_Producto producto)
        {
            D_Producto datos = new D_Producto();
            //ejecutamos el metodo para actualizar
            datos.EditarProductos(producto);
            TempData["mensaje"] = $"El producto {producto.Descripcion} se modifico correctamente";
            return RedirectToAction("Index", producto);
        }




        public IActionResult Agregar(E_Producto producto)
        {
            //VALIDACIONES DONDE GUARDAMOS EL MENSAJE SI ESTA MAL 
            string validacion = string.Empty;
            if (producto.Descripcion.Count() < 4)
            {
                // += significa a lo que ya tenias se le agregan mas valores
                validacion += "La <b>descripcion</b> debe ser de al menos 4 caracteres<br>";
            }
            if (producto.Precio <= 0)
            {
                validacion += "El <b>precio</b> debe ser un numero positivo<br>";
            }
            if (producto.FechaIngreso > DateTime.Now)
            {
                validacion += "La <b>fecha de ingreso</b> no es valida<br>";
            }
            //si validacion es una cadena vacia, entonces los datos on correctos
            if (string.IsNullOrEmpty(validacion))
            {

                D_Producto datos = new D_Producto();
                datos.AgregarProducto(producto);
                //ejecutamos el metodo Agregar de la capa de datos
                TempData["mensaje"] = $"El producto {producto.Descripcion} se registro correctamente";
                //redirecionamos a la accion Index no a la vista
                return RedirectToAction("Index");
            }
            else
            {
                //los datos no son validos entonces no agregamos,mostramos el mensaje de validacion
                TempData["validacion"] = validacion;
                return View("VistaAgregar");
            }
        }
        
        public IActionResult Eliminar(int idProducto)
        {
            D_Producto datos = new D_Producto();
            datos.EliminarProducto(idProducto);
            TempData["mensaje"] = $"Se ha eliminado correctamente el registro {idProducto}";
            return RedirectToAction("Index");
        }


       
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}


