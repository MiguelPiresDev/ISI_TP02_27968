using System;

namespace Imobiliario.DataLayer.Models
{
    public class RegisterData
    {
        // Login
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; } // "Owner" ou "Client"

        // Perfil
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
    }
}