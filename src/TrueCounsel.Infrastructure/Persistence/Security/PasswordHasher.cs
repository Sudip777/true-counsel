using Konscious.Security.Cryptography;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using TrueCounsel.Application.Common.Abstractions;

namespace TrueCounsel.Infrastructure.Persistence.Security
{
    public class PasswordHasher : IPasswordHasher
    {
        public string HashPassword(string password)
        {
            var salt = RandomNumberGenerator.GetBytes(16);
            // ❓: Why using is used??
           // Using statement ensures Argon2id resources are properly disposed to prevent memory leaks

            using (var argon2 = new Argon2id(Encoding.UTF8.GetBytes(password)))
            {
                argon2.Salt = salt;
                argon2.DegreeOfParallelism = 8;
                argon2.Iterations = 4;
                argon2.MemorySize = 65536;

                var hash = argon2.GetBytes(32);
                var result = new byte[salt.Length + hash.Length];
                Buffer.BlockCopy(salt, 0, result, 0, salt.Length);
                Buffer.BlockCopy(hash, 0, result, salt.Length, hash.Length);

                return Convert.ToBase64String(result);
            }
        }


        public bool VerifyPassword(string password, string storedHash)
        {
            var bytes = Convert.FromBase64String(storedHash);
            var salt = new byte[16];
            Buffer.BlockCopy(bytes, 0, salt, 0, 16);

            var storedPasswordHash = new byte[32];
            Buffer.BlockCopy(bytes, 16, storedPasswordHash, 0, 32);
            // Dispose Argon2id after verification to release unmanaged resources

            using var argon2 = new Argon2id(Encoding.UTF8.GetBytes(password))
            {
                Salt = salt,
                DegreeOfParallelism = 8,
                Iterations = 4,
                MemorySize = 65536
            };

            var hash = argon2.GetBytes(32);
            return hash.SequenceEqual(storedPasswordHash);      
        }
    }
}