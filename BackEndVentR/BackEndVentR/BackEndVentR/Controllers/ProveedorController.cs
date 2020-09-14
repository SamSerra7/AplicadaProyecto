using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web.Helpers;
using Entidad;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.VisualBasic.CompilerServices;
using Nancy.Json;
using Nancy.Json.Simple;
using Negocio;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using static System.Text.Json.JsonElement;

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
        public IEnumerable<SyncronizationType> sincronizacion(int idProveedor, string llave)
        {
            return proveedorNegocio.sincronizarProveedor(idProveedor, llave);
        }



        [HttpPost("{idProveedor}/{llave}/recibirProductos")]
        //api/Proveedor/2/Llave/3/recibirProductos
        public void agregarProducto(int idProveedor, string llave,
            [FromBody] JsonElement productos)
        {

            if (proveedorNegocio.validarProveedor(idProveedor, llave))
            {

                List<Producto> listaProductos = new List<Producto>();

                var list = JsonConvert.DeserializeObject<List<ProductoProveedorModel>>(productos.ToString());
                foreach (ProductoProveedorModel prod in  list)
                {
                    listaProductos.Add(
                            new Producto(prod.id_producto,
                                prod.nombre,
                                SqlMoney.Parse(prod.precio.ToString()),
                                prod.url_img,
                                prod.descripcion,
                                prod.cantidad,
                                proveedorNegocio.buscarProveedor(prod.id_proveedor))
                            );
                }
                   
                proveedorNegocio.agregarProductosProveedor(listaProductos);
                
            }
            

        }
    }
}



  


