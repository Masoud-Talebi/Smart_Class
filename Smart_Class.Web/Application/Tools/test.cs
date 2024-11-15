using Microsoft.AspNetCore.Mvc;
using System;
using System.Globalization;
using System.Text.RegularExpressions;

public static class dateee
{
    public static string ConvertDate(string persianDate)
    {
        // استفاده از Regex برای استخراج تاریخ شمسی
        var regex = new Regex(@"(\d{1,4})\s+(\D+)\s+(\d{1,2})");
        var match = regex.Match(persianDate);

        if (match.Success)
        {
            // استخراج سال، ماه و روز
            int year = int.Parse(match.Groups[1].Value);
            int day = int.Parse(match.Groups[3].Value);
            int month = GetPersianMonthNumber(match.Groups[2].Value);

            if (month != -1)
            {
                // تبدیل تاریخ شمسی به میلادی
                PersianCalendar persianCalendar = new PersianCalendar();
                DateTime gregorianDate = persianCalendar.ToDateTime(year, month, day, 0, 0, 0, 0);

                // فرمت تاریخ میلادی
                string formattedDate = gregorianDate.ToString("yyyy/MM/dd");

                // بازگشت تاریخ میلادی
                return formattedDate;
            }
        }

        return "";
    }

    private static int GetPersianMonthNumber(string monthName)
    {
        switch (monthName.Trim())
        {
            case "فروردین": return 1;
            case "اردیبهشت": return 2;
            case "خرداد": return 3;
            case "تیر": return 4;
            case "مرداد": return 5;
            case "شهریور": return 6;
            case "مهر": return 7;
            case "آبان": return 8;
            case "آذر": return 9;
            case "دی": return 10;
            case "بهمن": return 11;
            case "اسفند": return 12;
            default: return -1; // نام ماه نامعتبر است
        }
    }
}
