namespace TestTaskWindowsAuth.Server.Extensions
{
    public static class StringExtensions
    {
        public static string SplitAndGetLastElement(this string s, char[] separator)
        {
            return s.Split(separator)[^1];
        }

        public static string SplitAndGetLastElement(this string s)
        {
            return s.SplitAndGetLastElement(new[] {'/', '\\'});
        }

        public static string SplitAndGetFirstElement(this string s, char[] separator)
        {
            return s.Split(separator)[0];
        }

        public static string SplitAndGetFirstElement(this string s)
        {
            return s.SplitAndGetFirstElement(new[] {'/', '\\'});
        }
    }
}