using System.Text;

namespace Tienda.Domain.Core
{
    public static class TextHelper
    {
        public static bool IsNull(this string strValue)
        {

            return strValue is null || string.IsNullOrEmpty(strValue) || string.IsNullOrWhiteSpace(strValue);


        }

        public static string GetClearText(this string vl)
        {
            try
            {
                return Encoding.UTF8.GetString(Convert.FromBase64String(vl));
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        public static string ToBase64Text(this string clearText)
        {
            try
            {
                return Convert.ToBase64String(Encoding.UTF8.GetBytes(clearText));
            }
            catch (Exception)
            {
                return string.Empty;
            }

        }
        public static StringContent ToStringContent(this object bodyContent)
        {
            return new StringContent(bodyContent.ToJson(), Encoding.UTF8, "application/json");
        }

        public static void CreateDirectory(this DirectoryInfo? folder)
        {
            if (folder is not null && !Directory.Exists(folder.FullName))
                Directory.CreateDirectory(folder.FullName);
        }
        public static string FormatBytes(this object obj)
        {
            long bytes = Convert.ToInt64(obj);
            string[] Suffix = { "B", "KB", "MB", "GB", "TB" };
            int i;
            double dblSByte = bytes;
            for (i = 0; i < Suffix.Length && bytes >= 1024; i++, bytes /= 1024)
            {
                dblSByte = bytes / 1024.0;
            }

            return string.Format("{0:0.##} {1}", dblSByte, Suffix[i]);
        }


        public static uint GetHashCodeFromString(char fullName) => GetHashCodeFromString(fullName.ToString());

        public static uint GetHashCodeFromString(string? fullName)
        {
            if (fullName.IsNull())
            {
                return 0;
            }

            byte[] btValue = Encoding.UTF8.GetBytes(fullName);
            return Crc32.Compute(btValue);
        }
    }
}
