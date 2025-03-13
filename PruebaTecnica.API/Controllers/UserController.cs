using Microsoft.AspNetCore.Mvc;
using PruebaTecnica.Application.Common;
using PruebaTecnica.Application.DTO;
using PruebaTecnica.Application.Services;
using PruebaTecnica.Domain.Entities;
using System.Linq.Expressions;
using System.Net;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PruebaTecnica.API.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController(IUserService userService) : ControllerBase
    {
        private readonly IUserService _userService = userService;

        /// <summary>
        /// Obtiene todos los usuarios registrados en la base de datos.
        /// </summary>
        /// <returns>Lista de usuarios en formato DTO.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(ResponseMessage<List<UserDTO>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ResponseMessage<List<UserDTO>>), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetAllUsers()
        {
            var response = await _userService.GetUsersAsync();
            return StatusCode((int)response.StatusCode, response);
        }

        /// <summary>
        /// Obtiene un usuario por su identificador único.
        /// </summary>
        /// <param name="id">El identificador único del usuario.</param>
        /// <returns>El usuario correspondiente si se encuentra.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ResponseMessage<UserDTO>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ResponseMessage<UserDTO>), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetUserById(int id)
        {
            var response = await _userService.GetUserByIdAsync(id);
            return StatusCode((int)response.StatusCode, response);
        }

        /// <summary>
        /// Obtiene usuarios filtrados dinámicamente según los parámetros proporcionados.
        /// </summary>
        /// <param name="nombre">Nombre del usuario (opcional).</param>
        /// <param name="apellido">Apellido del usuario (opcional).</param>
        /// <param name="cedula">Cédula del usuario (opcional).</param>
        /// <param name="correo">Correo del usuario (opcional).</param>
        /// <returns>Lista de usuarios que coincidan con los criterios de búsqueda.</returns>
        [HttpGet("filter")]
        [ProducesResponseType(typeof(ResponseMessage<List<UserDTO>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ResponseMessage<List<UserDTO>>), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetUsersByFilter(
            [FromQuery] string? nombre,
            [FromQuery] string? apellido,
            [FromQuery] string? cedula,
            [FromQuery] string? correo)
        {

            Expression<Func<User, bool>> filter = u =>
            (string.IsNullOrEmpty(nombre) || u.Nombre.Contains(nombre)) &&
            (string.IsNullOrEmpty(apellido) || u.Apellido.Contains(apellido)) &&
            (string.IsNullOrEmpty(cedula) || u.Cedula == cedula) &&
            (string.IsNullOrEmpty(correo) || u.Correo.Contains(correo));

            ResponseMessage<List<UserDTO>> response = await _userService.GetUsersByWhereAsync(filter);
            return StatusCode((int)response.StatusCode, response);
        }

        /// <summary>
        /// Crea un nuevo usuario en la base de datos.
        /// </summary>
        /// <param name="userDTO">Datos del usuario a registrar.</param>
        /// <returns>Usuario registrado.</returns>
        [HttpPost("register")]
        [ProducesResponseType(typeof(ResponseMessage<UserDTO>), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(ResponseMessage<UserDTO>), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateUser([FromBody] UserRegisterDTO userDTO)
        {
            var response = await _userService.RegisterUserAsync(userDTO);
            return StatusCode((int)response.StatusCode, response);
        }

        /// <summary>
        /// Endpoint para iniciar sesión en la aplicación.
        /// </summary>
        /// <param name="loginDto">DTO con correo y contraseña.</param>
        /// <returns>Mensaje de éxito o error.</returns>
        [HttpPost("login")]
        [ProducesResponseType(typeof(ResponseMessage<string>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ResponseMessage<string>), (int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(ResponseMessage<string>), (int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult<ResponseMessage<string>>> Login([FromBody] UserLoginDTO loginDto)
        {
            var response = await _userService.LoginAsync(loginDto);
            return StatusCode((int)response.StatusCode, response);
        }

        /// <summary>
        /// Actualiza los datos de un usuario existente.
        /// </summary>
        /// <param name="id">El identificador único del usuario.</param>
        /// <param name="userDTO">Datos actualizados del usuario.</param>
        /// <returns>Usuario actualizado si la operación es exitosa.</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ResponseMessage<UserDTO>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ResponseMessage<UserDTO>), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ResponseMessage<UserDTO>), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UserDTO userDTO)
        {
            ResponseMessage<UserDTO> response = await _userService.UpdateUserAsync(id, userDTO);
            return StatusCode((int)response.StatusCode, response);
        }

        /// <summary>
        /// Elimina un usuario de la base de datos por su identificador.
        /// </summary>
        /// <param name="id">El identificador único del usuario.</param>
        /// <returns>Mensaje de éxito si se eliminó correctamente.</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ResponseMessage<UserDTO>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ResponseMessage<UserDTO>), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var response = await _userService.DeleteUserAsync(id);
            return StatusCode((int)response.StatusCode, response);
        }
    }
}
