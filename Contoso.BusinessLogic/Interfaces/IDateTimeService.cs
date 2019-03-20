using System;

namespace Contoso.BusinessLogic.Interfaces
{
    public interface IDateTimeService
    {
        DateTime BuildCurrentDateTime();

        int YearsBetween(DateTime startDate, DateTime endDate);
    }
}
