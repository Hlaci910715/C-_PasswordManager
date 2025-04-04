using System.Security.Cryptography;
using System.Text;

class EncryptionHelper
{
    // Encrypt a string
    public static string EncryptString(string plainText, string key)
    {
        using (Aes aes = Aes.Create())
        {
            aes.Key = Encoding.UTF8.GetBytes(key.PadRight(32).Substring(0, 32)); // Ensure 32-byte key
            aes.IV = new byte[16]; // Zero IV for simplicity (not secure for real-world use)

            using (var encryptor = aes.CreateEncryptor(aes.Key, aes.IV))
            {
                byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);
                byte[] encryptedBytes = encryptor.TransformFinalBlock(plainBytes, 0, plainBytes.Length);
                return Convert.ToBase64String(encryptedBytes);
            }
        }
    }

    // Decrypt a string
    public static string DecryptString(string encryptedText, string key)
    {
        using (Aes aes = Aes.Create())
        {
            aes.Key = Encoding.UTF8.GetBytes(key.PadRight(32).Substring(0, 32)); // Ensure 32-byte key
            aes.IV = new byte[16]; // Zero IV for simplicity (not secure for real-world use)

            using (var decryptor = aes.CreateDecryptor(aes.Key, aes.IV))
            {
                byte[] encryptedBytes = Convert.FromBase64String(encryptedText);
                byte[] decryptedBytes = decryptor.TransformFinalBlock(encryptedBytes, 0, encryptedBytes.Length);
                return Encoding.UTF8.GetString(decryptedBytes);
            }
        }
    }
}

class Hash
{
    public byte[] CalculateSHA256(string str)
    {
        SHA256 sha256 = SHA256Managed.Create();
        byte[] hashValue;
        UTF8Encoding objUtf8 = new UTF8Encoding();
        hashValue = sha256.ComputeHash(objUtf8.GetBytes(str));

        return hashValue;
    }
}