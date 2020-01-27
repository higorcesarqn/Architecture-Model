using System;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace Core.Cryptography
{
    public class PasswordHash : IPasswordHash
    {
        private const int SALT_BYTE_SIZE = 24;
        private const int HASH_BYTE_SIZE = 24;
        private const int PBKDF2_ITERATIONS = 1000;

        private const int ITERATION_INDEX = 0;
        private const int SALT_INDEX = 1;
        private const int PBKDF2_INDEX = 2;

        /// <summary>
        ///     Cria um o Hash e o Salt do Password
        /// </summary>
        /// <param name="password">Password</param>
        /// <returns>Hash do password.</returns>
        public string CreateHash(string password)
        {
            // gera um salt rondonic
            var csprng = RandomNumberGenerator.Create();
            var salt = new byte[SALT_BYTE_SIZE];
            csprng.GetBytes(salt);

            //Cria o Hash do password
            var hash = PBKDF2(password, salt, PBKDF2_ITERATIONS, HASH_BYTE_SIZE);
            return $@"{PBKDF2_ITERATIONS}:{Convert.ToBase64String(salt)}:{Convert.ToBase64String(hash)}";
        }

        /// <summary>
        ///     Valida uma senha dada a um hash correto.
        /// </summary>
        /// <param name="password">Password a ser validado.</param>
        /// <param name="correctHash">O Hash do password correto.</param>
        /// <returns>True se o password estiver correto. False caso contrário.</returns>
        public bool ValidatePassword(string password, string correctHash)
        {
            // Extract the parameters from the hash
            char[] delimiter = { ':' };
            var split = correctHash.Split(delimiter);
            var iterations = int.Parse(split[ITERATION_INDEX]);
            var salt = Convert.FromBase64String(split[SALT_INDEX]);
            var hash = Convert.FromBase64String(split[PBKDF2_INDEX]);

            var testHash = PBKDF2(password, salt, iterations, hash.Length);
            return SlowEquals(hash, testHash);
        }

        /// <summary>
        ///     Compara duas matrizes de bytes. esta comparação
        ///     é usado para que o hash do password não pode ser extraída a partir de
        ///     sistemas on-line usando um ataque de temporização e, em seguida, ataque off-line.
        /// </summary>
        /// <param name="a">Primeiro array de byte.</param>
        /// <param name="b">Segundo array de byte.</param>
        /// <returns>True se os array for iguais. False caso contrário.</returns>
        private static bool SlowEquals(IReadOnlyList<byte> a, IReadOnlyList<byte> b)
        {
            var diff = (uint)a.Count ^ (uint)b.Count;
            for (var i = 0; i < a.Count && i < b.Count; i++)
                diff |= (uint)(a[i] ^ b[i]);
            return diff == 0;
        }

        /// <summary>
        ///     Computes the PBKDF2-SHA1 hash of a password.
        /// </summary>
        /// <param name="password">Password.</param>
        /// <param name="salt">Salt.</param>
        /// <param name="iterations">Quantidade de interações PBKDF2.</param>
        /// <param name="outputBytes">Tamanho do Hash a ser gerado.</param>
        /// <returns>O Hash do Password.</returns>
        private static byte[] PBKDF2(string password, byte[] salt, int iterations, int outputBytes)
        {
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt) { IterationCount = iterations };
            return pbkdf2.GetBytes(outputBytes);
        }
    }
}