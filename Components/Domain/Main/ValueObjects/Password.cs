﻿using Microsoft.Extensions.Configuration;
using System.Security.Cryptography;
using TaskList.Components.Domain.Shared.ValueObjects;

namespace TaskList.Components.Domain.Main.ValueObjects
{
    public class Password : ValueObject
    {
        public string PassWord { get; set; } = string.Empty;
        protected Password() { }
        public Password(string password)
        {          
            PassWord = Hashing(password);
        }

        private static string Hashing(string password)
        {
           
            password += Configuration.Secrets.PasswordSaltKey;

            using var algorithm = new Rfc2898DeriveBytes(password, 16, 500, HashAlgorithmName.SHA256);

            var key = Convert.ToBase64String(algorithm.GetBytes(32));
            var salt = Convert.ToBase64String(algorithm.Salt);

            return $"{500}{'.'}{salt}{'.'}{key}";
        }

        public static bool Verify(
        string hash,
        string password,
        short keySize = 32,
        int iterations = 500,
        char splitChar = '.')
        {
            password += Configuration.Secrets.PasswordSaltKey;

            var parts = hash.Split(splitChar, 3);
            if (parts.Length != 3)
                return false;

            var hashIterations = Convert.ToInt32(parts[0]);
            var salt = Convert.FromBase64String(parts[1]);
            var key = Convert.FromBase64String(parts[2]);

            if (hashIterations != iterations)
                return false;

            using var algorithm = new Rfc2898DeriveBytes(
                password,
                salt,
                iterations,
                HashAlgorithmName.SHA256);
            var keyToCheck = algorithm.GetBytes(keySize);

            return keyToCheck.SequenceEqual(key);
        }
    }
}
