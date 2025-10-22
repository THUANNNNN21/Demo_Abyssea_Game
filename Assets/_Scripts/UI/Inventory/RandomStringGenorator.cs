public class RandomStringGenorator
{
    private const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
    public static string Generate(int length)
    {
        string randomString = "";
        for (int i = 0; i < length; i++)
        {
            int index = UnityEngine.Random.Range(0, chars.Length);
            randomString += chars[index];
        }
        return randomString;
    }
}