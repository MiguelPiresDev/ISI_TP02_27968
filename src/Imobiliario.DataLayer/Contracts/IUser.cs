using Imobiliario.DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Imobiliario.DataLayer.Contract
{
    [ServiceContract]
    public interface IUser
    {
        [OperationContract]
        User Login(string username, string password);
    }
}
