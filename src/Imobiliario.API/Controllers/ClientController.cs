using Imobiliario.DataLayer.Models;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Imobiliario.API.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ClientsController : ApiController
    {
        DataLayer.Imobiliario servico = new DataLayer.Imobiliario();

        [HttpGet]
        public IHttpActionResult Get()
        {
            return Ok(servico.GetAllClients());
        }

        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            var client = servico.GetClient(id);
            if (client == null) return NotFound();
            return Ok(client);
        }

        [HttpPost]
        public IHttpActionResult Post([FromBody] Client client)
        {
            if (servico.AddClient(client)) return Ok("Cliente criado.");
            return BadRequest();
        }

        [HttpPut]
        public IHttpActionResult Put([FromBody] Client client)
        {
            if (servico.UpdateClient(client)) return Ok("Cliente atualizado.");
            return NotFound();
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            if (servico.DeleteClient(id)) return Ok("Cliente apagado.");
            return NotFound();
        }
    }
}