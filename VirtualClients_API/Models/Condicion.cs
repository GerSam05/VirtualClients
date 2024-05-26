using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace VirtualClients_API.Models
{
    public class Condicion
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Estatus { get; set; }

        [JsonIgnore]
        public virtual ICollection<Cliente> Clientes { get; set; }
    }
}
