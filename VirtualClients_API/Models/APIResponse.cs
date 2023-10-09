using System.Net;

namespace VirtualClients_API.Models
{
    public class APIResponse
    {
        public HttpStatusCode StatusCode { get; set; }
        public bool IsExitoso { get; set; } = true;
        public string ErrorMessage { get; set; }
        public List<string> ExceptionMessages { get; set; }
        public Object Resultado { get; set; }


        public Object Guardado(int id) => Resultado = new { message = $"Guardado exitosamente con Id={id}" };
        public Object Editado() => Resultado = new { message = "Editado exitosamente" };
        public Object Eliminado() => Resultado = new { message = "Eliminado exitosamente" };

        public string ErrorNotFound(int id) => this.ErrorMessage = $"El Id={id} no existe en la base de datos";
        public string ErrorGuardar() => this.ErrorMessage = $"Error al guardar. No se ha guardado el registro";
        public string ErrorEditar() => this.ErrorMessage = $"Error al editar. El registro no ha sido editado";
        public string ErrorEliminar() => this.ErrorMessage = $"Error al eliminar. Ningún registro ha sido eliminado";
    }
}
