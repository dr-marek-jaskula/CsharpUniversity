using CustomTools.Random;
using System.Security.Cryptography;
using System.Text;

namespace CustomTools.Cryptography;

public class AES
{
    public static void InvokeAESExample()
    {
        var key = CustomRandom.GetRandomData(256 / 8);

        Console.WriteLine("Symmetric key: ");
        Console.WriteLine(string.Join(", ", key));
        Console.WriteLine("-------------------------------------------");

        string textToEncrypt = CustomRandom.GenerateTestString();
        Console.WriteLine("Plain Text: ");
        Console.WriteLine(textToEncrypt);
        Console.WriteLine("-------------------------------------------");

        string encryptedText = Encrypt(textToEncrypt, key, out var iv);
        Console.WriteLine("Encrypted Text: ");
        Console.WriteLine(encryptedText);
        Console.WriteLine("-------------------------------------------");

        string decryptedText = Decrypt(encryptedText, key, iv);

        Console.WriteLine("Decrypted Text: ");
        Console.WriteLine(decryptedText);

        Console.WriteLine($"Does decryptedText equal textToEncrypt? Result: {decryptedText == textToEncrypt}");
    }

    //More info:
    //https://stackoverflow.com/questions/1220751/how-to-choose-an-aes-encryption-mode-cbc-ecb-ctr-ocb-cfb/22958889#22958889

    #region Byte[] base use

    public static byte[] Encrypt(byte[] data, byte[] key, out byte[] iv)
    {
        using Aes aes = Aes.Create();

        // You should adjust the mode depending on what you want to encrypt.
        // However, some mode may be weak or require additional security steps such as CBC: https://docs.microsoft.com/en-us/dotnet/standard/security/vulnerabilities-cbc-mode?WT.mc_id=DT-MVP-5003978
        aes.Mode = CipherMode.CBC;
        aes.Key = key;
        aes.GenerateIV(); // You must use a new IV for each encryption for security purpose

        using var cryptoTransform = aes.CreateEncryptor();
        iv = aes.IV;
        return cryptoTransform.TransformFinalBlock(data, 0, data.Length);
    }

    public static byte[] Decrypt(byte[] data, byte[] key, byte[] iv)
    {
        using Aes aes = Aes.Create();
        aes.Key = key;
        aes.IV = iv;
        aes.Mode = CipherMode.CBC; // same as for encryption

        using var cryptoTransform = aes.CreateDecryptor();
        return cryptoTransform.TransformFinalBlock(data, 0, data.Length);
    }

    #endregion Byte[] base use

    #region String. My use

    public static string Encrypt(string textToEncrypt, byte[] key, out byte[] iv)
    {
        var data = Encoding.UTF8.GetBytes(textToEncrypt);

        using Aes aes = Aes.Create();

        // You should adjust the mode depending on what you want to encrypt.
        // However, some mode may be weak or require additional security steps such as CBC: https://docs.microsoft.com/en-us/dotnet/standard/security/vulnerabilities-cbc-mode?WT.mc_id=DT-MVP-5003978
        aes.Mode = CipherMode.CBC;
        aes.Key = key;
        aes.GenerateIV(); // You must use a new IV for each encryption for security purpose

        using var cryptoTransform = aes.CreateEncryptor();
        iv = aes.IV;
        byte[] encryptedBytes = cryptoTransform.TransformFinalBlock(data, 0, data.Length);
        return Convert.ToBase64String(encryptedBytes);
    }

    public static string Decrypt(string textToDecrypt, byte[] key, byte[] iv)
    {
        var data = Convert.FromBase64String(textToDecrypt);

        using Aes aes = Aes.Create();
        aes.Key = key;
        aes.IV = iv;
        aes.Mode = CipherMode.CBC; // same as for encryption

        using var cryptoTransform = aes.CreateDecryptor();
        byte[] decryptedBytes = cryptoTransform.TransformFinalBlock(data, 0, data.Length);
        return Encoding.UTF8.GetString(decryptedBytes);
    }

    #endregion String. My use
}