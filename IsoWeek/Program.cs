using System;
using System.Globalization;

namespace IsoWeek
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            var date = args.Length > 0 ? args[0] : new Func<string>(() => {Console.WriteLine("Enter a date (DD/MM/YYY)"); return Console.ReadLine();}).Invoke();
            Console.WriteLine(Iso8601WeekOfYear.GetWeekNumber(date).ToResponseSentence());
            Console.ReadLine();
        }
    }

    internal static class Iso8601WeekOfYear
    {
        private static int GetWeekNumber(DateTime date)
        {
            if (date == DateTime.MinValue)
                return 0;

            var day = CultureInfo.InvariantCulture.Calendar.GetDayOfWeek(date);
            if (day >= DayOfWeek.Monday && day <= DayOfWeek.Wednesday)
                date = date.AddDays(3);

            // Return the week of our adjusted day
            return CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(date, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
        }

        internal static int GetWeekNumber(string date)
        {
            DateTime result;
            return DateTime.TryParse(date, out result) ? GetWeekNumber(result) : 0;
        }

        public static string ToResponseSentence(this int weekNumber)
        {
            return $"Week : {weekNumber}";
        }
    }
}
