using AutoMapper;
using PruebaTecnica.Application.Common;
using PruebaTecnica.Application.DTO;
using PruebaTecnica.Application.Helpers;
using PruebaTecnica.Application.Repositories;
using PruebaTecnica.Application.Services;
using PruebaTecnica.Domain.Entities;
using System;
using System.Linq.Expressions;
using System.Net;

namespace PruebaTecnica.Infrastructure.Services
{
    public class UserService(IRepository repository, IMapper mapper, TokenService tokenService) : IUserService
    {
        private readonly IRepository _repository = repository;
        private readonly IMapper _mapper = mapper;
        private readonly TokenService _tokenService = tokenService;

        /// <inheritdoc/>
        public async Task<ResponseMessage<UserDTO>> RegisterUserAsync(UserRegisterDTO userDto)
        {
            try
            {
                User validate = await _repository.GetOneByWhereAsync<User>(u => u.Correo == userDto.Correo);
                if (validate is not null)
                    return ResponseMessage<UserDTO>.ErrorResponse("El correo ya está registrado.", HttpStatusCode.BadRequest);

                User newUser = _mapper.Map<User>(userDto);

                PasswordHelper.CreatePasswordHash(userDto.Password, out byte[] passwordHash, out byte[] passwordSalt);
                newUser.PasswordHash = passwordHash;
                newUser.PasswordSalt = passwordSalt;

                await _repository.AddAsync(newUser);

                UserDTO userDtoResponse = _mapper.Map<UserDTO>(newUser);

                return ResponseMessage<UserDTO>.SuccessResponse(userDtoResponse, "Usuario registrado correctamente.");
            }
            catch (Exception ex)
            {
                return ResponseMessage<UserDTO>.ErrorResponse($"Error en el registro: {ex.Message}", HttpStatusCode.InternalServerError);
            }
        }

        /// <inheritdoc/>
        public async Task<ResponseMessage<UserDTO>> DeleteUserAsync(int id)
        {
            try
            {
                User? user = await _repository.GetByIdAsync<User>(id);
                if (user is null)
                    return ResponseMessage<UserDTO>.ErrorResponse("Usuario no encontrado.", HttpStatusCode.NotFound);

                await _repository.DeleteAsync<User>(user.Id);

                return ResponseMessage<UserDTO>.SuccessResponse(null, "Usuario eliminado correctamente.");
            }
            catch (Exception ex)
            {
                return ResponseMessage<UserDTO>.ErrorResponse($"Error al eliminar usuario: {ex.Message}", HttpStatusCode.InternalServerError);
            }
        }

        /// <inheritdoc/>
        public async Task<ResponseMessage<UserDTO>> GetUserByIdAsync(int id)
        {
            try
            {
                User? user = await _repository.GetByIdAsync<User>(id);
                if (user is null)
                    return ResponseMessage<UserDTO>.ErrorResponse("Usuario no encontrado.", HttpStatusCode.NotFound);

                UserDTO userDTO = _mapper.Map<UserDTO>(user);
                return ResponseMessage<UserDTO>.SuccessResponse(userDTO);
            }
            catch (Exception ex)
            {
                return ResponseMessage<UserDTO>.ErrorResponse($"Error al obtener usuario: {ex.Message}", HttpStatusCode.InternalServerError);
            }
        }

        /// <inheritdoc/>
        public async  Task<ResponseMessage<List<UserDTO>>> GetUsersByWhereAsync(Expression<Func<User, bool>> predicate)
        {
            try
            {
                List<User> users = await _repository.GetByWhereAsync<User>(predicate);

                if (users is null || users.Count == 0)
                    return ResponseMessage<List<UserDTO>>.ErrorResponse("No se encontraron usuarios con los criterios especificados.", HttpStatusCode.NotFound);

                List<UserDTO> userDTOs = _mapper.Map<List<UserDTO>>(users);
                return ResponseMessage<List<UserDTO>>.SuccessResponse(userDTOs);
            }
            catch (Exception ex)
            {
                return ResponseMessage<List<UserDTO>>.ErrorResponse($"Error al buscar usuarios: {ex.Message}", HttpStatusCode.InternalServerError);
            }
        }

        /// <inheritdoc/>
        public async Task<ResponseMessage<List<UserDTO>>> GetUsersAsync()
        {
            try
            {
                List<User> users = await _repository.GetAllAsync<User>();
                if (users is null || users.Count == 0)
                    return ResponseMessage<List<UserDTO>>.ErrorResponse("No hay usuarios registrados.", HttpStatusCode.NotFound);

                List<UserDTO> userDTOs = _mapper.Map<List<UserDTO>>(users);

                return ResponseMessage<List<UserDTO>>.SuccessResponse(userDTOs);
            }
            catch (Exception ex)
            {
                return ResponseMessage<List<UserDTO>>.ErrorResponse($"Error al obtener usuarios: {ex.Message}", HttpStatusCode.InternalServerError);
            }
        }

        /// <inheritdoc/>
        public async Task<ResponseMessage<UserDTO>> UpdateUserAsync(int id, UserDTO userDTO)
        {
            try
            {
                userDTO.UltimoAcceso = DateTime.Now;
                User? user = await _repository.GetByIdAsync<User>(id);
                if (user is null)
                    return ResponseMessage<UserDTO>.ErrorResponse("Usuario no encontrado.", HttpStatusCode.NotFound);

                _mapper.Map(userDTO, user);
                await _repository.UpdateAsync(user.Id, user);

                UserDTO updatedUserDTO = _mapper.Map<UserDTO>(user);
                return ResponseMessage<UserDTO>.SuccessResponse(updatedUserDTO, "Usuario actualizado correctamente.");
            }
            catch (Exception ex)
            {
                return ResponseMessage<UserDTO>.ErrorResponse($"Error al actualizar usuario: {ex.Message}", HttpStatusCode.InternalServerError);
            }
        }

        /// <inheritdoc/>
        public async Task<ResponseMessage<string>> LoginAsync(UserLoginDTO loginDto)
        {
            User user = await _repository.GetOneByWhereAsync<User>(u => u.Correo == loginDto.Correo);

            if (user == null || !PasswordHelper.VerifyPassword(loginDto.Password, user.PasswordHash, user.PasswordSalt))
            {
                return ResponseMessage<string>.ErrorResponse("Credenciales incorrectas.", HttpStatusCode.Unauthorized);
            }

            user.UltimoAcceso = DateTime.UtcNow;
            await _repository.UpdateAsync(user.Id, user);

            string token = _tokenService.GenerateToken(user);

            return ResponseMessage<string>.SuccessResponse($"Bearer {token}", "Inicio de sesión exitoso");
        }

    }
}
