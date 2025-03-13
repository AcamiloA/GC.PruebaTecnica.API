using System.Security.Cryptography;
using System.Text;

namespace PruebaTecnica.Application.Helpers
{
    public class PasswordHelper
    {
        /// <summary>
        /// Genera un hash seguro de la contraseña usando HMACSHA512.
        /// </summary>
        public static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        /// <summary>
        /// Verifica si la contraseña ingresada coincide con el hash almacenado.
        /// </summary>
        public static bool VerifyPassword(string password, byte[] storedHash, byte[] storedSalt)
        {
            using (var hmac = new HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                return CompareByteArrays(computedHash, storedHash);
            }
        }

        /// <summary>
        /// Compara dos arreglos de bytes de manera segura.
        /// </summary>
        private static bool CompareByteArrays(byte[] a, byte[] b)
        {
            if (a.Length != b.Length) return false;
            for (int i = 0; i < a.Length; i++)
            {
                if (a[i] != b[i]) return false;
            }
            return true;
        }
    }
}
