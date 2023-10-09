namespace VirtualClients_API.Models.Dtos
{
    public class ClienteDtoUpdate
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public int Estatus { get; set; }
    }
}
