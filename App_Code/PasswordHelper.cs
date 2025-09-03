using System;
using System.Text;
using System.Security.Cryptography;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for PasswordHelper
/// </summary>
public class PasswordHelper
{
    public static string HashPassword(string password)
    {
        using (SHA256 sha256 = SHA256.Create())
        {
            // Password ko byte array me convert karein
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);

            // Hash calculate karein
            byte[] hashBytes = sha256.ComputeHash(passwordBytes);

            // Hash ko string me convert karein
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < hashBytes.Length; i++)
            {
                builder.Append(hashBytes[i].ToString("x2"));
            }
            return builder.ToString();
        }
    }
}