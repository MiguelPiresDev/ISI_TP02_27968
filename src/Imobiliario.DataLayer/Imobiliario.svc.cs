using Imobiliario.DataLayer.Contract;


namespace Imobiliario.DataLayer
{
    // Nota o "partial" aqui
    public partial class Imobiliario : IImobiliario
    {
        // A Connection String fica aqui e é partilhada com todos os outros ficheiros
       internal string connectionString = @"Server=tcp:imobiliarioserver-pires.database.windows.net,1433;Initial Catalog=ImobiliarioBD;Persist Security Info=False;User ID=AdminImob;Password=Bilocas12!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
    }
}