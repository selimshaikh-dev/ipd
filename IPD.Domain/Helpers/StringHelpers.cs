using System.Security.Cryptography;
using System.Text;

namespace IPD.Domain.Helpers
{
    public static class StringHelpers
    {
        private static readonly char[] Chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();

        public static string GetRandomString(int size = 2)
        {
            var data = new byte[4 * size];
            using (var crypto = RandomNumberGenerator.Create())
            {
                crypto.GetBytes(data);
            }
            var result = new StringBuilder(size);
            for (var i = 0; i < size; i++)
            {
                var rnd = BitConverter.ToUInt32(data, i * 4);
                var charIndex = rnd % Chars.Length;

                result.Append(Chars[charIndex]);
            }
            return result.ToString();
        }
    }
}
