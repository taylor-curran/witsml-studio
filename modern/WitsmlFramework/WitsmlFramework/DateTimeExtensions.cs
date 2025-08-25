using System;

namespace WitsmlFramework;

public static class DateTimeExtensions
{
    private static readonly DateTimeOffset _epochTime = new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero);
    private const long TicksToMicroSeconds = 10L;

    public static DateTimeOffset FromUnixTimeMicroseconds(long microseconds, TimeSpan? offset = null)
    {
        return new DateTimeOffset(ToUnixTicks(microseconds), offset ?? TimeSpan.Zero);
    }

    public static long ToUnixTimeMicroseconds(this DateTimeOffset dateTimeOffset)
    {
        return FromUnixTicks(dateTimeOffset.UtcTicks);
    }

    public static long? ToUnixTimeMicroseconds(this DateTimeOffset? dateTimeOffset)
    {
        return dateTimeOffset?.ToUnixTimeMicroseconds();
    }

    public static long ToUnixTimeMicroseconds(this DateTime dateTime)
    {
        return DateTimeOffset.Parse(dateTime.ToString("o")).ToUnixTimeMicroseconds();
    }

    public static long? ToUnixTimeMicroseconds(this DateTime? dateTime)
    {
        return dateTime?.ToUnixTimeMicroseconds();
    }

    public static DateTimeOffset ToOffsetTime(this DateTimeOffset value, TimeSpan? offset)
    {
        if (!offset.HasValue)
            return value;

        if (value.Offset.CompareTo(offset) == 0)
            return value;

        return FromUnixTimeMicroseconds(value.ToUnixTimeMicroseconds(), value.Offset).ToOffset(offset.Value);
    }

    private static long ToUnixTicks(long microseconds)
    {
        return microseconds * TicksToMicroSeconds + _epochTime.UtcTicks;
    }

    private static long FromUnixTicks(long ticks)
    {
        return (ticks - _epochTime.UtcTicks) / TicksToMicroSeconds;
    }

    public static string ToTimeZone(this TimeSpan offset)
    {
        return $"{offset.Hours:00}:{offset.Minutes:00}";
    }

    public static DateTime TruncateToHours(this DateTime dateTime)
    {
        return dateTime.AddTicks(-dateTime.Ticks % TimeSpan.TicksPerHour);
    }

    public static DateTime TruncateToMinutes(this DateTime dateTime)
    {
        return dateTime.AddTicks(-dateTime.Ticks % TimeSpan.TicksPerMinute);
    }

    public static DateTime TruncateToSeconds(this DateTime dateTime)
    {
        return dateTime.AddTicks(-dateTime.Ticks % TimeSpan.TicksPerSecond);
    }

    public static DateTime TruncateToMilliseconds(this DateTime dateTime)
    {
        return dateTime.AddTicks(-dateTime.Ticks % TimeSpan.TicksPerMillisecond);
    }

    public static DateTimeOffset TruncateToHours(this DateTimeOffset DateTimeOffset)
    {
        return DateTimeOffset.AddTicks(-DateTimeOffset.Ticks % TimeSpan.TicksPerHour);
    }

    public static DateTimeOffset TruncateToMinutes(this DateTimeOffset DateTimeOffset)
    {
        return DateTimeOffset.AddTicks(-DateTimeOffset.Ticks % TimeSpan.TicksPerMinute);
    }

    public static DateTimeOffset TruncateToSeconds(this DateTimeOffset DateTimeOffset)
    {
        return DateTimeOffset.AddTicks(-DateTimeOffset.Ticks % TimeSpan.TicksPerSecond);
    }

    public static DateTimeOffset TruncateToMilliseconds(this DateTimeOffset DateTimeOffset)
    {
        return DateTimeOffset.AddTicks(-DateTimeOffset.Ticks % TimeSpan.TicksPerMillisecond);
    }
}
