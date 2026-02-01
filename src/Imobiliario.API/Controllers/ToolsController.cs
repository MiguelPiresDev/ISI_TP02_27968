using System;
using System.Net.Http;
using System.Net.Http.Headers; // Importante
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors; // Importante para o CORS
using Newtonsoft.Json.Linq;

namespace Imobiliario.API.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/Tools")]
    public class ToolsController : ApiController
    {
        [HttpGet]
        [Route("Geocode")]
        public async Task<IHttpActionResult> GetCoordinates(string address)
        {
            if (string.IsNullOrEmpty(address)) return BadRequest("Escreve uma morada.");

            // Url do OpenStreetMap
            string url = $"https://nominatim.openstreetmap.org/search?format=json&q={address}";

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("User-Agent", "ProjetoImobiliarioAluno/1.0");

                try
                {
                    HttpResponseMessage response = await client.GetAsync(url);

                    if (response.IsSuccessStatusCode)
                    {
                        string jsonResult = await response.Content.ReadAsStringAsync();
                        JArray dados = JArray.Parse(jsonResult);

                        if (dados.Count > 0)
                        {
                            var item = dados[0];
                            return Ok(new
                            {
                                Address = (string)item["display_name"],
                                Lat = (string)item["lat"],
                                Lon = (string)item["lon"]
                            });
                        }
                        return NotFound(); // Não encontrou Barcelos
                    }
                    return BadRequest("Erro técnico ao contactar mapas.");
                }
                catch (Exception ex)
                {
                    return InternalServerError(ex);
                }
            }
        }
    }
}