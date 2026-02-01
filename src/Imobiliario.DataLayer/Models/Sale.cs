using System;
using System.Runtime.Serialization;

namespace Imobiliario.DataLayer.Models
{
    [DataContract]
    public class Sale
    {
        [DataMember] public int SaleID { get; set; }
        [DataMember] public DateTime Date { get; set; }
        [DataMember] public decimal FinalPrice { get; set; }

        [DataMember] public int PropertyID { get; set; }
        [DataMember] public int ClientID { get; set; }
    }
}