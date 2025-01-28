using System.Globalization;

namespace Gold.Services;

public class PersianService
{
    PersianCalendar _persianCalendar = new();

    string[] _monthsNames = [
        "فروردین",
        "اردیبهشت",
        "خرداد",
        "تیر",
        "مرداد",
        "شهریور",
        "مهر",
        "آبان",
        "آذر",
        "دی",
        "بهمن",
        "اسفند",
        ];

    public string GetDate() => $"{_persianCalendar.GetYear(DateTime.Now):0000}/{_persianCalendar.GetMonth(DateTime.Now):00}/{_persianCalendar.GetDayOfMonth(DateTime.Now):00}";

    public string GetTime() => $"{_persianCalendar.GetHour(DateTime.Now):00}:{_persianCalendar.GetMinute(DateTime.Now):00}";

    public int GetMonthDay() => _persianCalendar.GetDayOfMonth(DateTime.Now);
    public string GetMonthByName() => $"{_monthsNames[_persianCalendar.GetMonth(DateTime.Now)-1]}";

    public string GetSimpleCode(string nationalId= "2939960232")
    {
        string code = nationalId.Substring(Random.Shared.Next(0, 6), 4);
        if (code.StartsWith("0")) code = nationalId.Substring(Random.Shared.Next(0, 5), 4);
        if (code.StartsWith("0")) code = nationalId.Substring(Random.Shared.Next(0, 4), 4);
        if (code.StartsWith("0")) code = nationalId.Substring(Random.Shared.Next(0, 3), 4);
        return code;
    }

    public string ToPersian(DateTime at)
    {
        return $"{_persianCalendar.GetYear(DateTime.Now):0000}/{_persianCalendar.GetMonth(DateTime.Now):00}/{_persianCalendar.GetDayOfMonth(DateTime.Now):00} {_persianCalendar.GetHour(DateTime.Now):00}:{_persianCalendar.GetMinute(DateTime.Now):00}";
    }
}