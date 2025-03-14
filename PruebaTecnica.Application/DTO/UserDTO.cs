using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PruebaTecnica.Application.DTO
{
    /// <summary>
    /// DTO para la transferencia de datos de usuario.
    /// </summary>
    public class UserDTO
    {
        public int Id { get; set; }
        /// <summary>
        /// Nombre del usuario.
        /// Debe contener solo letras y tener una longitud máxima de 50 caracteres.
        /// </summary>
        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(50, ErrorMessage = "El nombre no puede tener más de 50 caracteres.")]
        [RegularExpression(@"^[a-zA-ZÀ-ÿ\s]+$", ErrorMessage = "El nombre solo puede contener letras y espacios.")]
        public string Nombre { get; set; } = string.Empty;

        /// <summary>
        /// Apellido del usuario.
        /// Debe contener solo letras y tener una longitud máxima de 50 caracteres.
        /// </summary>
        [Required(ErrorMessage = "El apellido es obligatorio.")]
        [StringLength(50, ErrorMessage = "El apellido no puede tener más de 50 caracteres.")]
        [RegularExpression(@"^[a-zA-ZÀ-ÿ\s]+$", ErrorMessage = "El apellido solo puede contener letras y espacios.")]
        public string Apellido { get; set; } = string.Empty;


        /// <summary>
        /// Número de cédula del usuario.
        /// Debe contener solo números y tener entre 6 y 10 dígitos.
        /// </summary>
        [Required(ErrorMessage = "La cédula es obligatoria.")]
        [RegularExpression(@"^\d{6,10}$", ErrorMessage = "La cédula debe contener entre 6 y 10 dígitos numéricos.")]
        public string Cedula { get; set; } = string.Empty;

        /// <summary>
        /// Correo electrónico del usuario.
        /// Debe tener un formato de correo válido.
        /// </summary>
        [Required(ErrorMessage = "El correo es obligatorio.")]
        [StringLength(100, ErrorMessage = "El correo no puede tener más de 100 caracteres.")]
        [EmailAddress(ErrorMessage = "El formato del correo es inválido.")]
        [RegularExpression(@"^[\w\.-]+@[a-zA-Z\d\.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "El correo debe tener un formato válido.")]
        public string Correo { get; set; } = string.Empty;


        /// <summary>
        /// Última fecha de acceso del usuario.
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public DateTime? UltimoAcceso { get; set; } = null;
    }
}
