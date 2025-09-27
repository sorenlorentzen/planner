using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Planner.Core.Entities;

public class DateTimeOffsetConverter : ValueConverter<DateTimeOffset, DateTimeOffset>
{
    public DateTimeOffsetConverter()
        : base(
            d => d.ToUniversalTime(),
            d => d.ToUniversalTime())
    {
    }
}