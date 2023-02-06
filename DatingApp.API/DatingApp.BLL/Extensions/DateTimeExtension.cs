namespace DatingApp.BLL.Extensions;

public static class DateTimeExtension
{
    public static int GetYears(this DateTime date)
    {
        var age = DateTime.Now.Year - date.Year;
        if (date.AddYears(age) > DateTime.Today) age--;

        return age;
    }
}