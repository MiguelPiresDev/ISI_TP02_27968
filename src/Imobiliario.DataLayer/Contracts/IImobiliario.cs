using System.ServiceModel;

namespace Imobiliario.DataLayer.Contract // Atenção ao namespace da pasta
{
    // Esta interface herda todas as outras já criadas.
    [ServiceContract]
    public interface IImobiliario : IProperty, IOwner, IVisit, IUser, IClient, ISale
    {
    }
}   