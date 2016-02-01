namespace Tests.Utils
{
    public static class CurrencyUtils
    {
        public static string NormalizeCurrencyRate (string str)
        {
            string result = str.Replace(" ", "");
            return result.Trim();
        }
    }
}
