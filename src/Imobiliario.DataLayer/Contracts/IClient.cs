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
    public interface IClient
    {
        #region Client
        [OperationContract]
        bool AddClient(Client c);
 
        [OperationContract]
        List<Client> GetAllClients();

        [OperationContract]
        Client GetClient(int id);

        [OperationContract]
        bool UpdateClient(Client c);

        [OperationContract]
        bool DeleteClient(int id);
        #endregion
    }
}
