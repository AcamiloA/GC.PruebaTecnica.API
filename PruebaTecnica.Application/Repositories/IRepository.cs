using System.Linq.Expressions;

namespace PruebaTecnica.Application.Repositories
{
    public interface IRepository
    {
        /// <summary>
        /// Obtiene todas las entidades de un tipo específico en la base de datos.
        /// </summary>
        /// <typeparam name="T">El tipo de entidad a obtener.</typeparam>
        /// <returns>Lista de todas las entidades de tipo <typeparamref name="T"/>.</returns>
        Task<List<T>> GetAllAsync<T>() where T : class;

        /// <summary>
        /// Busca una entidad por su identificador único.
        /// </summary>
        /// <typeparam name="T">El tipo de entidad a buscar.</typeparam>
        /// <param name="id">El identificador único de la entidad.</param>
        /// <returns>La entidad de tipo <typeparamref name="T"/> si se encuentra, de lo contrario, null.</returns>
        Task<T?> GetByIdAsync<T>(int id) where T : class;

        /// <summary>
        /// Obtiene una lista de entidades que cumplen con una condición específica.
        /// </summary>
        /// <typeparam name="T">El tipo de entidad a buscar.</typeparam>
        /// <param name="predicate">Expresión lambda con la condición de búsqueda.</param>
        /// <returns>Lista de entidades que cumplen con la condición dada.</returns>
        Task<List<T>> GetByWhereAsync<T>(Expression<Func<T, bool>> predicate) where T : class;

        /// <summary>
        /// Obtiene una entidad que cumpla con una condición específica.
        /// </summary>
        /// <typeparam name="T">El tipo de entidad a buscar.</typeparam>
        /// <param name="predicate">Expresión lambda con la condición de búsqueda.</param>
        /// <returns>Lista de entidades que cumplen con la condición dada.</returns>
        Task<T> GetOneByWhereAsync<T>(Expression<Func<T, bool>> predicate) where T : class;

        /// <summary>
        /// Agrega una nueva entidad a la base de datos.
        /// </summary>
        /// <typeparam name="T">El tipo de entidad a agregar.</typeparam>
        /// <param name="entity">La entidad que se va a agregar.</param>
        /// <returns>Tarea asincrónica que representa la operación de agregado.</returns>
        Task AddAsync<T>(T entity) where T : class;

        /// <summary>
        /// Actualiza una entidad existente en la base de datos.
        /// </summary>
        /// <typeparam name="T">El tipo de entidad a actualizar.</typeparam>
        /// <param name="id">El identificador único de la entidad.</param>
        /// <param name="entity">La entidad con los nuevos valores.</param>
        /// <returns>Tarea asincrónica que representa la operación de actualización.</returns>
        Task UpdateAsync<T>(int id, T entity) where T : class;

        /// <summary>
        /// Elimina una entidad de la base de datos por su identificador.
        /// </summary>
        /// <typeparam name="T">El tipo de entidad a eliminar.</typeparam>
        /// <param name="id">El identificador único de la entidad.</param>
        /// <returns>Tarea asincrónica que representa la operación de eliminación.</returns>
        Task DeleteAsync<T>(int id) where T : class;
    }
}
