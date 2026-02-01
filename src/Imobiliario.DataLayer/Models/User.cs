using System.Runtime.Serialization;

namespace Imobiliario.DataLayer.Models
{
    [DataContract]
    public class User
    {
        [DataMember] public int UserID { get; set; }
        [DataMember] public string Username { get; set; }
        [DataMember] public string PasswordHash { get; set; }
        [DataMember] public string Role { get; set; }
        [DataMember] public int? OwnerID { get; set; }
    }
}