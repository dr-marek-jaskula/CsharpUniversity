using System.Security.Cryptography;
using System.Text;

namespace CustomTools.Random;

public class CustomRandom
{
    public static string GenerateTestString()
    {
        Guid opportinityId = Guid.NewGuid();
        Guid systemUserId = Guid.NewGuid();
        string currentTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

        StringBuilder sb = new StringBuilder();
        sb.AppendFormat("opportunityid={0}", opportinityId.ToString());
        sb.AppendFormat("&systemuserid={0}", systemUserId.ToString());
        sb.AppendFormat("&currenttime={0}", currentTime);

        return sb.ToString();
    }

    public static byte[] GetRandomData(int length)
    {
        return RandomNumberGenerator.GetBytes(length);
    }
}