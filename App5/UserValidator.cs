using System;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace App5
{
    public class UserValidator
    {
        private static readonly Regex EmailRegex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");

        public bool ValidateEmail(string email) => EmailRegex.IsMatch(email);
        public bool ValidatePassword(string password) => password?.Length >= 8;
        public bool ValidateConfirmPassword(string password, string confirmPassword) => password == confirmPassword;

        public string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hash = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hash);
            }
        }
    }
}   
