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
    public interface IVisit
    {
        #region Visit
        [OperationContract]
        bool AddVisit(Visit visit);

        [OperationContract]
        List<Visit> GetAllVisits();

        [OperationContract]
        List<Visit> GetVisitsByProperty(int propertyId);

        [OperationContract]
        bool DeleteVisit(int visitId);
        #endregion
    }
}
