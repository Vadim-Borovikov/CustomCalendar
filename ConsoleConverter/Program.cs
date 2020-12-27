using System;
using System.Globalization;
using System.Linq;
using CustomCalendar;

namespace ConsoleConverter
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            while (true)
            {
                Console.Write("Enter date (dd.mm.yyyy): ");
                string input = Console.ReadLine();
                if (string.IsNullOrEmpty(input))
                {
                    return;
                }

                int[] digits = GetDigits(input);
                if (digits == null)
                {
                    Console.WriteLine("Incorrect format!");
                    continue;
                }

                DateTime asStandard;
                bool standard = TryCreateDateTime(digits[0], digits[1], digits[2], out asStandard);
                if (standard)
                {
                    DateCustom dateCustom = DateConverter.ConvertStandard(asStandard);
                    Console.WriteLine($"To custom:   {asStandard.AsString()} -> {dateCustom}");
                }

                DateCustom asCustom;
                bool custom = DateCustom.TryCreate((byte)digits[0], (byte)digits[1],
                                                   (ushort)digits[2], out asCustom);
                if (custom)
                {
                    DateTime dateStandard = DateConverter.ConvertCustom(asCustom);
                    Console.WriteLine($"To standard: {asCustom} -> {dateStandard.AsString()}");
                }

                if (standard || custom)
                {
                    Console.WriteLine();
                }
                else
                {
                    Console.WriteLine("Incorrect date!");
                }
            }
        }

        private static int[] GetDigits(string input)
        {
            string[] parts = input?.Split('.');
            if ((parts == null) || (parts.Length != 3))
            {
                return null;
            }
            try
            {
                return parts.Select(int.Parse).ToArray();
            }
            catch
            {
                return null;
            }
        }

        private static bool TryCreateDateTime(int day, int month, int year, out DateTime dateTime)
        {
            try
            {
                dateTime = new DateTime(year, month, day);
                return true;
            }
            catch (Exception)
            {
                dateTime = new DateTime(1, 1, 1);
                return false;
            }
        }

        private static string AsString(this DateTime dateTime)
        {
            return dateTime.ToString("D", CultureInfo.InvariantCulture);
        }
    }
}
