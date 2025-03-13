using PruebaTecnica.Application.Common;
using PruebaTecnica.Application.DTO;
using PruebaTecnica.Domain.Entities;
using System.Linq.Expressions;

namespace PruebaTecnica.Application.Services
{
    public interface IUserService
    {
        /// <summary>
        ///     Obtiene un usuario de la base de datos por ID
        /// </summary>
        /// <returns>Respuesta del tipo ResponseMessage </returns>
        Task<ResponseMessage<UserDTO>> GetUserByIdAsync(int id);

        /// <summary>
        ///     Obtiene uno o varios usuarios de la base de datos por condiciones
        /// </summary>
        /// <returns>Respuesta del tipo ResponseMessage </returns>
        Task<ResponseMessage<List<UserDTO>>> GetUsersByWhereAsync(Expression<Func<User, bool>> predicate);

        /// <summary>
        ///     Obtiene una lista de todos los usuarios de la base de datos
        /// </summary>
        /// <returns>Respuesta del tipo ResponseMessage </returns>
        Task<ResponseMessage<List<UserDTO>>> GetUsersAsync();

        /// <summary>
        ///     Registra un usuario a la base de datos
        /// </summary>
        /// <returns>Respuesta del tipo ResponseMessage </returns>
        Task<ResponseMessage<UserDTO>> RegisterUserAsync(UserRegisterDTO userDTO);

        /// <summary>
        ///     actualiza un usuario de la base de datos por id
        /// </summary>
        /// <returns>Respuesta del tipo ResponseMessage </returns>
        Task<ResponseMessage<UserDTO>> UpdateUserAsync(int id, UserDTO userDTO);

        /// <summary>
        ///     elimina un usuario de la base de datos
        /// </summary>
        /// <returns>Respuesta del tipo ResponseMessage </returns>
        Task<ResponseMessage<UserDTO>> DeleteUserAsync(int id);

        Task<ResponseMessage<string>> LoginAsync(UserLoginDTO loginDTO);
    }
}
