using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PruebaTecnica.Domain.Entities
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string Nombre { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string Apellido { get; set; } = string.Empty;

        [Required]
        [StringLength(10)]
        [Column(TypeName = "varchar(10)")]
        public string Cedula { get; set; } = string.Empty;

        [Required]
        [StringLength(30)]
        [Column(TypeName = "varchar(30)")]
        public string Correo { get; set; } = string.Empty;

        public DateTime UltimoAcceso { get; set; }

        /// <summary>
        /// Hash de la contraseña (No almacena la contraseña en texto plano).
        /// </summary>
        [Required]
        public byte[] PasswordHash { get; set; }

        /// <summary>
        /// Salt para mejorar la seguridad del hash de la contraseña.
        /// </summary>
        [Required]
        public byte[] PasswordSalt { get; set; }
    }
}
