using System.ComponentModel.DataAnnotations;

namespace VirtualClients_API.Models.Dtos
{
    public class ClienteDtoCreate
    {
        [Required(ErrorMessage = "Campo Nombre es requerido")]
        [MaxLength(30, ErrorMessage = "El Campo Nombre no debe exceder los 30 Caracteres")]
        [RegularExpression(@"^[a-zA-Z ]+$", ErrorMessage = "El Campo Nombre sólo admite letras")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Campo Apellido es requerido")]
        [MaxLength(30, ErrorMessage = "El Campo Apellido no debe exceder los 30 Caracteres")]
        [RegularExpression(@"^[a-zA-Z ]+$", ErrorMessage = "El Campo Apellido sólo admite letras")]
        public string Apellido { get; set; }

        [Required]
        [Range(1, 10, ErrorMessage = "El campo Estatus requerido debe ser mayor que cero (0) y menor o igual a diez (10)")]
        public int Estatus { get; set; }
    }
}
