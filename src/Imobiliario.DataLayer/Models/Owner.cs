using System.Runtime.Serialization;

namespace Imobiliario.DataLayer.Models
{
    [DataContract]
    public class Owner
    {
        [DataMember] public int OwnerID { get; set; }
        [DataMember] public string Name { get; set; }
        [DataMember] public string Email { get; set; }
        [DataMember] public string Phone { get; set; }
    }
}