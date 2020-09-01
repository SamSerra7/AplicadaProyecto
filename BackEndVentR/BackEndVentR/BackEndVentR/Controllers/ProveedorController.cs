using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entidad;
using Microsoft.AspNetCore.Mvc;
using Negocio;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BackEndVentR.Controllers
{
    [Route("api/[controller]")]
    [ApiController]


    public class ProveedorController : ControllerBase
    {

        private ProveedorNegocio proveedorNegocio = new ProveedorNegocio();
        // GET: api/<ProveedorController>
        [HttpGet]
        public IEnumerable<Proveedor> Get()
        {
            return proveedorNegocio.listarProveedores();

        }

        // GET api/<ProveedorController>/5
        [HttpGet("{idProveedor}")]
        public Proveedor Get(int idProveedor)
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

        //Recibe y Agrega un nuevo proveedor, recibe en el modelo solo el Nombre, el id lo genera la base y 
        //el estado es default true
        // POST api/Proveedor
        [HttpPost]
        public bool Post([FromBody] Proveedor proveedor)
        {
            return proveedorNegocio.AgregarProveedor(proveedor);
        }

        // PUT api/<ProveedorController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ProveedorController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }


}