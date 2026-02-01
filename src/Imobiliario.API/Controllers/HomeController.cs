using System;
using System.Web.Http;

namespace Imobiliario.API.Controllers
{
    [RoutePrefix("")]
    public class HomeController : ApiController
    {
        [Route("")]
        [HttpGet]
        public IHttpActionResult Index()
        {
            // Redireciona para o Swagger
            return Redirect(new Uri("/swagger", UriKind.Relative));
        }
    }
}