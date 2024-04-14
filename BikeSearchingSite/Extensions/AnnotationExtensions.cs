using System.ComponentModel.DataAnnotations;

namespace BikeSearchingSite.Extensions
{
    public class YearRangeTillDateAttribute : RangeAttribute
    {
        public YearRangeTillDateAttribute(int StartYear) : base(StartYear, DateTime.Today.Year)
        {
        }
    }
}
