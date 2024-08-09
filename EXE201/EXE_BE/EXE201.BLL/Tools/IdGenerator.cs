using System.Text;

namespace Tools.Tools
{
    public static class IdGenerator
    {
        private static readonly string AllowedCharacters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        private static readonly string AllowedCode = "0123456789";
        private static readonly Random Random = new();

        public static string GenerateId()
        {
            var sb = new StringBuilder();
            for (int i = 0; i < 5; i++)
            {
                int index = Random.Next(AllowedCharacters.Length);
                sb.Append(AllowedCharacters[index]);
            }
            return sb.ToString();
        }

        //public static string GeneratePassword()
        //{
        //    var sb = new StringBuilder();
        //    for (int i = 0; i < 8; i++)
        //    {
        //        int index = Random.Next(AllowedCharacters.Length);
        //        sb.Append(AllowedCharacters[index]);
        //    }
        //    return sb.ToString();
        //}

        public static string GenerateRandomVerifyCode()
        {
            var sb = new StringBuilder();
            sb.Append((char)(Random.Next(1, 10) + '0'));
            for (int i = 1; i < 6; i++)
            {
                int index = Random.Next(10);
                sb.Append(AllowedCode[index]);
            }
            return sb.ToString();
        }
    }
}
