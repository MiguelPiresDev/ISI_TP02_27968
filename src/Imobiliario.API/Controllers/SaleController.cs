using Imobiliario.DataLayer.Models;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Imobiliario.API.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class SalesController : ApiController
    {
        DataLayer.Imobiliario servico = new DataLayer.Imobiliario();

        [HttpGet]
        public IHttpActionResult Get()
        {
            return Ok(servico.GetAllSales());
        }

        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            var sale = servico.GetSale(id);
            if (sale == null) return NotFound();
            return Ok(sale);
        }

        [HttpPost]
        public IHttpActionResult Post([FromBody] Sale sale)
        {
            if (servico.AddSale(sale)) return Ok("Venda registada!");
            return BadRequest();
        }

        [HttpPut]
        public IHttpActionResult Put([FromBody] Sale sale)
        {
            if (servico.UpdateSale(sale)) return Ok("Venda atualizada.");
            return NotFound();
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            if (servico.DeleteSale(id)) return Ok("Venda apagada.");
            return NotFound();
        }
    }
}