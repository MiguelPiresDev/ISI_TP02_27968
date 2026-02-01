using Imobiliario.DataLayer.Models;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Imobiliario.API.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class PropertyController : ApiController
    {
        DataLayer.Imobiliario servico = new DataLayer.Imobiliario();

        [HttpGet]
        public IHttpActionResult Get()
        {
            var lista = servico.GetAllProperties();
            return Ok(lista);
        }

        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            var prop = servico.GetProperty(id);
            if (prop == null) return NotFound();
            return Ok(prop);
        }

        [HttpPost]
        public IHttpActionResult Post([FromBody] Property p)
        {
            if (p == null) return BadRequest();

            bool result = servico.AddProperty(p);

            if (result) return Ok("Propriedade criada com sucesso.");
            else return BadRequest("Erro ao criar propriedade.");
        }

        [HttpPut]
        public IHttpActionResult Put([FromBody] Property p)
        {
            if (p == null) return BadRequest();

            bool result = servico.UpdateProperty(p);

            if (result) return Ok("Propriedade atualizada.");
            else return NotFound();
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                bool result = servico.DeleteProperty(id);
                if (result) return Ok("Propriedade apagada.");
                else return NotFound();
            }
            catch
            {
                return BadRequest("Não é possível apagar esta propriedade (pode ter visitas ou vendas associadas).");
            }
        }
    }
}