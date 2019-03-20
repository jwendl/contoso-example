using Contoso.BusinessLogic.Interfaces;
using System;

namespace Contoso.BusinessLogic
{
    public class DateTimeService
        : IDateTimeService
    {
        public DateTime BuildCurrentDateTime()
        {
            return DateTime.UtcNow;
        }

        public int YearsBetween(DateTime startDate, DateTime endDate)
        {
            var years = endDate.Subtract(startDate).TotalDays / 365.25;
            return (int)years;
        }
    }
}
