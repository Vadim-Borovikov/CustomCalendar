using System;
using System.Globalization;

namespace CustomCalendar
{
    public struct DateCustom
    {
        public readonly byte Day;
        public readonly DayOfWeekCustom DayOfWeek;
        public readonly byte Month;
        public readonly ushort Year;

        public DateCustom(byte day, byte month, ushort year)
        {
            if (month > 12)
            {
                throw new ArgumentOutOfRangeException(nameof(month));
            }

            if (day > 32)
            {
                throw new ArgumentOutOfRangeException(nameof(day));
            }

            if ((month == 1) && (day > DaysInJanuary(year)))
            {
                throw new ArgumentOutOfRangeException(nameof(day));
            }

            Day = day;
            Month = month;
            Year = year;
            DayOfWeek = DetermineDayOfWeek(day, month, year);
        }

        public static bool TryCreate(byte day, byte month, ushort year, out DateCustom dateCustom)
        {
            try
            {
                dateCustom = new DateCustom(day, month, year);
                return true;
            }
            catch (Exception)
            {
                dateCustom = new DateCustom(1, 1, 1);
                return false;
            }
        }

        private static DayOfWeekCustom DetermineDayOfWeek(byte day, byte month, ushort year)
        {
            if (month > 1)
            {
                return (DayOfWeekCustom) ((day - 1) % 8 + 1);
            }

            if (day < 9)
            {
                return (DayOfWeekCustom) day;
            }

            if (DateTime.IsLeapYear(year))
            {
                // 9-14 -> wed-eig
                return (DayOfWeekCustom) (day - 6);
            }
            // 9-13 -> thu-eig
            return (DayOfWeekCustom)(day - 5);
        }

        internal static byte DaysInJanuary(ushort year)
        {
            return (byte)(DateTime.IsLeapYear(year) ? 14 : 13);
        }

        public override string ToString()
        {
            string month = CultureInfo.InvariantCulture.DateTimeFormat.GetMonthName(Month);
            return string.Format($"{DayOfWeek}, {Day.ToString("D2")} {month} {Year.ToString("D4")}");
        }
    }
}
