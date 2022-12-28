using CustomTools.Utilities;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Serialization;

namespace CustomTools.Cryptography;

public class RSA
{
    public static void InvokeRSAExample()
    {
        var cryptoServiceProvider = new RSACryptoServiceProvider(2048); //key size (choose 1024 or 2048)

        RSAParameters privateKey = cryptoServiceProvider.ExportParameters(true); //Generate private key
        RSAParameters publicKey = cryptoServiceProvider.ExportParameters(false); //Generate public key

        string publicKeyString = GetKeyString(publicKey);
        string privateKeyString = GetKeyString(privateKey);

        Console.WriteLine("Public key: ");
        Console.WriteLine(publicKeyString);
        Console.WriteLine("-------------------------------------------");

        Console.WriteLine("Private key: ");
        Console.WriteLine(privateKeyString);
        Console.WriteLine("-------------------------------------------");

        string textToEncrypt = RandomUtility.GenerateTestString();
        Console.WriteLine("Plain Text: ");
        Console.WriteLine(textToEncrypt);
        Console.WriteLine("-------------------------------------------");

        string encryptedText = Encrypt(textToEncrypt, publicKeyString);
        Console.WriteLine("Encrypted Text: ");
        Console.WriteLine(encryptedText);
        Console.WriteLine("-------------------------------------------");

        string decryptedText = Decrypt(encryptedText, privateKeyString);

        Console.WriteLine("Decrypted Text: ");
        Console.WriteLine(decryptedText);

        Console.WriteLine($"Does decryptedText equal textToEncrypt? Result: {decryptedText == textToEncrypt}");
    }

    public static string GetKeyString(RSAParameters key)
    {
        var stringWriter = new System.IO.StringWriter();
        var xmlSerializer = new XmlSerializer(typeof(RSAParameters));
        xmlSerializer.Serialize(stringWriter, key);
        return stringWriter.ToString();
    }

    public static string Encrypt(string textToEncrypt, string publicKeyString)
    {
        var bytesToEncrypt = Encoding.UTF8.GetBytes(textToEncrypt);

        using RSACryptoServiceProvider rsa = new(2048);

        try
        {
            rsa.FromXmlString(publicKeyString.ToString());
            var encryptedData = rsa.Encrypt(bytesToEncrypt, true);
            var base64Encrypted = Convert.ToBase64String(encryptedData);
            return base64Encrypted;
        }
        finally
        {
            rsa.PersistKeyInCsp = false;
        }
    }

    public static string Decrypt(string textToDecrypt, string privateKeyString)
    {
        using RSACryptoServiceProvider rsa = new(2048);

        try
        {
            // server decrypting data with private key
            rsa.FromXmlString(privateKeyString);

            var resultBytes = Convert.FromBase64String(textToDecrypt);
            var decryptedBytes = rsa.Decrypt(resultBytes, true);
            var decryptedData = Encoding.UTF8.GetString(decryptedBytes);
            return decryptedData.ToString();
        }
        finally
        {
            rsa.PersistKeyInCsp = false;
        }
    }
}