namespace dotnetnbpgold.nbp.client.Extensions
{
    internal static class DateTimeExtensions
    {
        internal static string ParseDate(this DateTime date)
        {
            return date.ToString("yyyy-MM-dd");
        }
    }
}