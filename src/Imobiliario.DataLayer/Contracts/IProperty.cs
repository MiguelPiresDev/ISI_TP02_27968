using System.Collections.Generic;
using System.ServiceModel;
using Imobiliario.DataLayer.Models; // Atenção: Confirma se a pasta Models tem este namespace

namespace Imobiliario.DataLayer
{
    [ServiceContract]
    public interface IProperty
    {
        #region Property

        [OperationContract]
        bool AddProperty(Property p);

        [OperationContract]
        List<Property> GetAllProperties();

        [OperationContract]
        Property GetProperty(int id);

        [OperationContract]
        bool UpdateProperty(Property p);

        [OperationContract]
        bool DeleteProperty(int id);

        [OperationContract]
        bool RegisterUser(RegisterData data);
        #endregion
    }
}