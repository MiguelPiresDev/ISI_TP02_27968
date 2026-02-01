using Imobiliario.DataLayer.Models;
using System.Collections.Generic;
using System.ServiceModel;

namespace Imobiliario.DataLayer.Contract
{
    [ServiceContract]
    public interface IOwner
    {
        #region Owner
        [OperationContract]
        bool AddOwner(Owner ow);

        [OperationContract]
        List<Owner> GetAllOwners();
        Owner GetOwner(int id);

        [OperationContract]
        bool UpdateOwner(Owner ow);

        [OperationContract]
        bool DeleteOwner(int id);
        #endregion
    }
}
