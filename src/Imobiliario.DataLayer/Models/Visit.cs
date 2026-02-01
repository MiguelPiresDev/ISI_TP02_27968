using System;
using System.Runtime.Serialization;

namespace Imobiliario.DataLayer.Models
{
    [DataContract]
    public class Visit
    {
        [DataMember] public int VisitID { get; set; }
        [DataMember] public DateTime VisitDate { get; set; }
        [DataMember] public int PropertyID { get; set; }
        [DataMember] public int ClientID { get; set; }
    }
}