using System.Security.Cryptography;

namespace api.ExtensionMethods;

public static class PasswordExtensions
{
    public static bool CheckPassword(this string password, string hashedPassword)
    {
        /* Fetch the stored value */
        /* Extract the bytes */
        byte[] hashBytes = Convert.FromBase64String(hashedPassword);
        /* Get the salt */
        byte[] salt = new byte[16];
        Array.Copy(hashBytes, 0, salt, 0, 16);
        /* Compute the hash on the password the user entered */
        var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100000);
        byte[] hash = pbkdf2.GetBytes(20);
        if (Enumerable.Range(0, 20).Any(i => hashBytes[i + 16] != hash[i]))
        {
            return false;
        }
        return true;
    }

    public static string HashPassword(this string password)
    {
        var salt = new byte[16];
        RandomNumberGenerator.Create().GetBytes(salt);
        var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100000);
        var hash = pbkdf2.GetBytes(20);
        var hashBytes = new byte[36];

        Array.Copy(salt, 0, hashBytes, 0, 16);
        Array.Copy(hash, 0, hashBytes, 16, 20);

        return Convert.ToBase64String(hashBytes);
    }
}