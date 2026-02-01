using Imobiliario.DataLayer.Models;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Imobiliario.API.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class OwnersController : ApiController
    {
        DataLayer.Imobiliario servico = new DataLayer.Imobiliario();

        [HttpGet]
        public IHttpActionResult Get()
        {
            return Ok(servico.GetAllOwners());
        }

        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            var owner = servico.GetOwner(id);
            if (owner == null) return NotFound();
            return Ok(owner);
        }

        [HttpPost]
        public IHttpActionResult Post([FromBody] Owner owner)
        {
            if (servico.AddOwner(owner)) return Ok("Dono adicionado.");
            return BadRequest("Erro ao adicionar dono.");
        }

        [HttpPut]
        public IHttpActionResult Put([FromBody] Owner owner)
        {
            if (servico.UpdateOwner(owner)) return Ok("Dono atualizado.");
            return NotFound();
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            if (servico.DeleteOwner(id)) return Ok("Dono apagado.");
            return NotFound();
        }
    }
}