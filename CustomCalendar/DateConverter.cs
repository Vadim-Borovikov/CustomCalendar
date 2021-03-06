﻿using System;

namespace CustomCalendar
{
    public static class DateConverter
    {
        public static DateCustom ConvertStandard(DateTime date)
        {
            ushort dayNumber = ToDayNumber(date);
            return DayNumberToCustom(dayNumber, date.Year);
        }

        public static DateTime ConvertCustom(DateCustom date)
        {
            ushort dayNumber = ToDayNumber(date);
            return DayNumberToStandard(dayNumber, (ushort) (date.Year - DateCustom.YearsDifference));
        }

        private static ushort ToDayNumber(DateTime date) => (ushort) date.DayOfYear;

        private static ushort ToDayNumber(DateCustom date)
        {
            if (date.Month == 1)
            {
                return date.Day;
            }

            return (ushort)(date.Day + DateCustom.DaysInJanuary(date.Year) + (date.Month - 2) * 32);
        }

        private static DateTime DayNumberToStandard(ushort dayNumber, ushort year)
        {
            return new DateTime(year, 1, 1).AddDays(dayNumber - 1);
        }

        private static DateCustom DayNumberToCustom(ushort dayNumber, int standartYear)
        {
            int day = dayNumber;
            byte month = 1;
            ushort year = (ushort) (standartYear + DateCustom.YearsDifference);

            int daysInJanuary = DateCustom.DaysInJanuary(year);
            if (dayNumber > daysInJanuary)
            {
                day = dayNumber - daysInJanuary;
                ++month;

                while (day > 32)
                {
                    day -= 32;
                    ++month;
                }
            }

            return new DateCustom((byte) day, month, year);
        }
    }
}
