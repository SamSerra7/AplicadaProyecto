using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text.Json;
using System.Threading.Tasks;
using Entidad;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic.CompilerServices;
using Negocio;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BackEndVentR.Controllers
{
    [Route("api/[controller]")]
    [ApiController]


    public class ProveedorController : ControllerBase
    {

        private ProveedorNegocio proveedorNegocio = new ProveedorNegocio();

        private ProductoNegocio productoNegocio = new ProductoNegocio();
        // GET: api/<ProveedorController>
        [HttpGet]
        public IEnumerable<Proveedor> Get()
        {
            return proveedorNegocio.listarProveedoresActivos();

        }

        // GET: api/proveedor/ListarProveedores
        [HttpGet("ListarProveedores")]
        public IEnumerable<Proveedor> ListarProveedores()
        {
            return proveedorNegocio.listarProveedores();

        }
        // GET api/<ProveedorController>/5
        [HttpGet("{idProveedor}")]
        public Proveedor Get(int idProveedor)
        {
            return proveedorNegocio.buscarProveedorActivo(idProveedor);
        }

        // GET api/BuscarProveedor/5
        [HttpGet("BuscarProveedor/{idProveedor}")]
        public Proveedor BuscarProveedor(int idProveedor)
        {
            return proveedorNegocio.buscarProveedor(idProveedor);
        }

        //GET api/Proveedor/ActivarProveedor/1    Activa un proveedor especifico por su id
        [HttpGet("ActivarProveedor/{IdProveedor}")]
        public Boolean ActivarProveedor(int IdProveedor)
        {
            return proveedorNegocio.ActivarProveedor(IdProveedor);
        }

        //GET api/Proveedor/DesactivarProveedor/1      Desactiva un proveedor especifico por su id
        [HttpGet("DesactivarProveedor/{IdProveedor}")]
        public Boolean DesactivarProveedor(int IdProveedor)
        {
            return proveedorNegocio.DesactivarProveedor(IdProveedor);
        }

        //DELETE api/Proveedor/EliminarProveedor/2
        [HttpDelete("EliminarProveedor/{IdProveedor}")]
        public Boolean EliminarProveedor(int IdProveedor)
        {
            return proveedorNegocio.eliminarProveedor(IdProveedor);
        }
        //Recibe y Agrega un nuevo proveedor, recibe en el modelo solo el Nombre, el id lo genera la base y 
        //el estado es default true

        // POST api/Proveedor
        [HttpPost]
        public bool Post([FromBody] Proveedor proveedor)
        {
            return proveedorNegocio.AgregarProveedor(proveedor);
        }

        [HttpPut]
        public bool ModificarProveedor([FromBody] Proveedor proveedor)
        {
            return proveedorNegocio.modificarProveedor(proveedor);
        }


        [HttpGet("{idProveedor}/{llave}/sincronizacion")]
        public IEnumerable<SyncronizationType> sincronizacion(int idProveedor,string llave)
        {
            return proveedorNegocio.sincronizarProveedor(idProveedor, llave);
        }

        
        //api/idProveedor/2/llave/3/recibirProductos

      
        [HttpPost("{idProveedor}/{llave}/recibirProductos")]
        //Proveedor/2/Llave/3/recibirProductos
        public void agregarProducto(int idProveedor, string llave, [FromBody] JsonElement producto)
        {

            if (proveedorNegocio.validarProveedor(idProveedor,llave))
            {

           
                List<Producto> listaProductos = new List<Producto>();
           

                foreach (JsonElement lista in producto.EnumerateArray()) { 
                         listaProductos.Add(new Producto(
                        lista.GetProperty("id_producto_proveedor").GetInt32(), 
                        lista.GetProperty("nombre").GetString(),
                        lista.GetProperty("precio").GetDecimal(),
                        lista.GetProperty("url_img").GetString(), 
                        lista.GetProperty("detalle").GetString(), 
                        lista.GetProperty("cantidad").GetInt32(),
                        proveedorNegocio.buscarProveedor(lista.GetProperty("id_proveedor").GetInt32())));
                }


                proveedorNegocio.agregarProductosProveedor(listaProductos);
            }
        }

    }


}
