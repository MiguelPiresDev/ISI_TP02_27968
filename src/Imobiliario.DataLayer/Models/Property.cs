using System;
using System.Runtime.Serialization;

namespace Imobiliario.DataLayer.Models
{
    [DataContract]
    public class Property
    {
        [DataMember] public int PropertyID { get; set; }
        [DataMember] public string Name { get; set; }
        [DataMember] public string Address { get; set; }
        [DataMember] public decimal Price { get; set; }
        [DataMember] public double Area { get; set; }
        [DataMember] public bool State { get; set; }
        [DataMember] public string Description { get; set; }
        [DataMember] public string Type { get; set; }
        [DataMember] public int YearBuilt { get; set; }   
        [DataMember] public int OwnerID { get; set; }
        [DataMember] public double? Latitude { get; set; }
        [DataMember] public double? Longitude { get; set; }
    }
}