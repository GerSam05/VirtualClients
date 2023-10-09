using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace VirtualClients_API.Models
{
    public class Cliente
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public int Estatus { get; set; }

        [JsonIgnore]
        [ForeignKey(nameof(Estatus))]
        public virtual Condicion Condicion { get; set; }
    }
}
