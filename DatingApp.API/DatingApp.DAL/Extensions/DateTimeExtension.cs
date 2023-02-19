namespace DatingApp.DAL.Extensions;

public static class DateTimeExtension
{
    public static int GetYears(this DateOnly date)
    {
        var today = DateOnly.FromDateTime(DateTime.UtcNow);

        var age = today.Year - date.Year;

        if (date > today.AddYears(-age)) age--;

        return age;
    }
}