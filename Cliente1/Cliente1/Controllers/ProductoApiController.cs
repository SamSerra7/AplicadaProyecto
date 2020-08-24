using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Cliente1.Controllers
{
    public class ProductoApiController : ApiController
    {
        // GET: api/ProductoApi
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/ProductoApi/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/ProductoApi
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/ProductoApi/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/ProductoApi/5
        public void Delete(int id)
        {
        }
    }
}
