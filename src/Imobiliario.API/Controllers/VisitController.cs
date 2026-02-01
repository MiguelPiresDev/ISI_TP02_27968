using Imobiliario.DataLayer.Models;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Imobiliario.API.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class VisitsController : ApiController
    {
        DataLayer.Imobiliario servico = new DataLayer.Imobiliario();

        // GET: api/Visits (Todas as visitas do sistema)
        [HttpGet]
        public IHttpActionResult Get()
        {
            return Ok(servico.GetAllVisits());
        }

        // GET: api/Visits/Property/5 (Visitas de uma casa específica)
        // Criámos esta rota especial para usar a função GetVisitsByProperty
        [HttpGet]
        [Route("api/Visits/Property/{id}")]
        public IHttpActionResult GetByProperty(int id)
        {
            var visitas = servico.GetVisitsByProperty(id);
            return Ok(visitas);
        }

        // POST: api/Visits (Marcar nova visita)
        [HttpPost]
        public IHttpActionResult Post([FromBody] Visit visit)
        {
            if (servico.AddVisit(visit)) return Ok("Visita marcada.");
            return BadRequest();
        }
    }
}