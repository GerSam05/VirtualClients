using System.ComponentModel.DataAnnotations;

namespace VirtualClients_API.Models.Dtos
{
    public class CondicionDtoUpdate
    {
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Campo Id requerido debe ser mayor que cero (0)")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Campo Estatus es requerido")]
        [MaxLength(30, ErrorMessage = "El Campo Status no debe exceder los 30 Caracteres")]
        [RegularExpression(@"^[a-zA-Z ]+$", ErrorMessage = "El Campo Estatus sólo admite letras")]
        public string Estatus { get; set; }
    }
}
