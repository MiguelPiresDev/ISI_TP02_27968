    using System;
    using System.Web.Http;
    using System.Web.Http.Cors;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;
    using Microsoft.IdentityModel.Tokens;
    using Imobiliario.DataLayer.Models;

    namespace Imobiliario.API.Controllers
    {
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public class UserController : ApiController
        {
            DataLayer.Imobiliario servico = new DataLayer.Imobiliario();

            private string segredo = "MinhaChaveSecretaSuperSeguraParaOProjetoImobiliario2025";

            [HttpPost]
            [Route("api/Register")]
            public IHttpActionResult Register([FromBody] RegisterData data)
            {
                if (data == null) return BadRequest("Dados vazios.");

                try
                {
                    if (data.Role != "Owner" && data.Role != "Client")
                        return BadRequest("Tens de escolher ser Proprietário ou Cliente.");

                    bool result = servico.RegisterUser(data);

                    if (result) return Ok("Conta criada com sucesso!");
                    else return BadRequest("Erro ao criar conta.");
                }
                catch (Exception ex)
                {
                    return BadRequest("Erro: " + ex.Message);
                }
            }

            [HttpPost]
            [Route("api/Login")]
            public IHttpActionResult Login([FromBody] LoginRequest pedido)
            {
                if (pedido == null) return BadRequest("Dados inválidos.");

                try
                {
                    // 1. Pergunta ao DataLayer se o user existe e a pass está certa
                    // (Tens de ter o método .Login() no teu Imobiliario.cs)
                    User user = servico.Login(pedido.Username, pedido.Password);

                    // 2. Se não existir ou pass errada
                    if (user == null) return Content(System.Net.HttpStatusCode.Unauthorized, "Login incorreto.");

                    // 3. Se existir, cria o Token
                    var tokenString = GerarTokenJwt(user);

                    // 4. Devolve TUDO o que o Frontend precisa (Role, ID, Nome e Token)
                    return Ok(new
                    {
                        Token = tokenString,
                        UserID = user.UserID,
                        Username = user.Username,
                        Role = user.Role,
                        OwnerID = user.OwnerID
                    });
                }
                catch (Exception ex)
                {
                    return BadRequest("Erro no login: " + ex.Message);
                }
            }

            // ==========================================
            // MÉTODOS AUXILIARES (Gerar Token)
            // ==========================================
            private string GerarTokenJwt(User user)
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(segredo);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Name, user.Username),
                        new Claim(ClaimTypes.Role, user.Role),
                        new Claim("UserID", user.UserID.ToString())
                    }),
                    Expires = DateTime.UtcNow.AddHours(2),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                return tokenHandler.WriteToken(token);
            }
        }
        public class LoginRequest
        {
            public string Username { get; set; }
            public string Password { get; set; }
        }
    }