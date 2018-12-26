
namespace EphemerisNet.Coordinates
{
    using System;

    /// <summary>
    /// Represents an angle
    /// </summary>
    public class Angle
    {
        private const string DS = "\x00B0";

        private readonly double _rawDegrees;

        private Angle(double rawDegrees)
        {
            this._rawDegrees = rawDegrees;
        }

        /// <summary>
        /// The total angle in degrees
        /// </summary>
        public double DecimalDegrees => this._rawDegrees;

        /// <summary>
        /// The total angle in hours
        /// </summary>
        public double DecimalHours => this._rawDegrees.DegreesToHours();

        /// <summary>
        /// The total angle in radians
        /// </summary>
        public double Radians => this._rawDegrees.DegreesToRadians();

        public override bool Equals(object obj)
        {
            var other = obj as Angle;
            return other != null && this.Equals(other);
        }

        public bool Equals(Angle other)
        {
            return this._rawDegrees.Equals(other._rawDegrees);
        }

        public override int GetHashCode()
        {
            return this._rawDegrees.GetHashCode();
        }

        public override string ToString()
        {
            return this._rawDegrees.ToString();
        }

        /// <summary>
        /// Prints an angle in a specified format
        /// </summary>
        /// <param name="format">The format specifier for an angle</param>
        /// <returns>The string representation of this angle</returns>
        public string ToString(AngleFormat format)
        {
            switch (format)
            {
                case AngleFormat.Ra:
                    return ToRaFormat(this);
                case AngleFormat.Dec:
                    return ToDecAltFormat(this);
                case AngleFormat.Az:
                    return ToAzFormat(this);
                case AngleFormat.Alt:
                    return ToDecAltFormat(this);
                case AngleFormat.Lat:
                    return ToLatFormat(this);
                case AngleFormat.Long:
                    return ToLongFormat(this);
                case AngleFormat.HMS:
                    return ToHMSFormat(this);
                case AngleFormat.DMS:
                    return ToDMSFormat(this);
                default:
                    return this.ToString();
            }
        }

        /// <summary>
        /// Creates an angle based on hours, minutes, and seconds
        /// </summary>
        public static Angle FromHours(double hours, double minutes = 0.0, double seconds = 0.0)
        {
            var totalHours = GetTotalAngle(hours, minutes, seconds);
            return new Angle(totalHours.HoursToDegrees());
        }

        /// <summary>
        /// Creates an angle based on degrees, minutes, and seconds
        /// </summary>
        public static Angle FromDegrees(double degrees, double minutes = 0.0, double seconds = 0.0)
        {
            var totalDegrees = GetTotalAngle(degrees, minutes, seconds);
            return new Angle(totalDegrees);
        }

        /// <summary>
        /// Creates an angle based on radians
        /// </summary>
        public static Angle FromRadians(double radians)
        {
            var degrees = radians.RadiansToDegrees();
            return new Angle(degrees);
        }

        /// <summary>
        /// Creates an angle based on a <see cref="TimeSpan"/>
        /// </summary>
        public static Angle FromTimeSpan(TimeSpan timeSpan)
        {
            return Angle.FromHours(timeSpan.TotalHours);
        }

        #region Private
        private double GetHMSMinutes()
        {
            var fractionalHours = this.DecimalHours - (int)this.DecimalHours;
            return (fractionalHours * CoordinateConstants.MinutesPerHour);
        }

        private double GetHMSSeconds()
        {
            var minutes = this.GetHMSMinutes();
            var fractionalMinutes = minutes - (int)minutes;
            return Math.Round(fractionalMinutes * CoordinateConstants.SecondsPerMinute, 2);
        }

        private double GetDMSMinutes()
        {
            var fractionalDegrees = this.DecimalDegrees - (int)this.DecimalDegrees;
            return (fractionalDegrees * CoordinateConstants.MinutesPerHour);
        }

        private double GetDMSSeconds()
        {
            var minutes = this.GetDMSMinutes();
            var fractionalMinutes = minutes - (int)minutes;
            return Math.Round(fractionalMinutes * CoordinateConstants.SecondsPerMinute, 2);
        }

        internal Angle ToNormalizedDegrees(double minDegrees, double maxDegrees)
        {
            return new Angle(Normalize(this._rawDegrees, minDegrees, maxDegrees));
        }

        internal Angle ToNormalizedDegreesInclusive(double minDegrees, double maxDegrees)
        {
            return new Angle(NormalizeInclusive(this._rawDegrees, minDegrees, maxDegrees));
        }

        internal Angle ToNormalizedHours(double minHours, double maxHours)
        {
            return new Angle(Normalize(this._rawDegrees,
                minHours.HoursToDegrees(),
                maxHours.HoursToDegrees()));
        }

        private static string ToDMSFormat(Angle angle)
        {
            var sign = (angle.DecimalDegrees < 0.0) ? "-" : "";
            var degrees = Math.Abs((int)angle.DecimalDegrees);
            var minutes = Math.Abs((int)angle.GetDMSMinutes());
            var seconds = Math.Abs(angle.GetDMSSeconds());
            return $"{sign}{degrees}{DS}{minutes}'{seconds}\"";
        }

        private static string ToHMSFormat(Angle angle)
        {
            var sign = (angle.DecimalDegrees < 0.0) ? "-" : "";
            var absoluteAngle = new Angle(Math.Abs(angle.DecimalDegrees));
            var hours = (int)absoluteAngle.DecimalHours;
            var minutes = (int)absoluteAngle.GetHMSMinutes();
            var seconds = absoluteAngle.GetHMSSeconds();
            return $"{sign}{hours}h{minutes}m{seconds}s";
        }

        private static string ToRaFormat(Angle angle)
        {
            var normAngle = angle.ToNormalizedHours(0.0, 24.0);
            return ToHMSFormat(normAngle);
        }

        private static string ToDecAltFormat(Angle angle)
        {
            var normAngle = angle.ToNormalizedDegreesInclusive(-90.0, 90.0);
            return ToDMSFormat(normAngle);
        }

        private static string ToAzFormat(Angle angle)
        {
            var normAngle = angle.ToNormalizedDegrees(0.0, 360.0);
            return ToDMSFormat(normAngle);
        }

        private static string ToLatFormat(Angle angle)
        {
            var normAngle = angle.ToNormalizedDegreesInclusive(-90, 90);
            var northSouth = (normAngle.DecimalDegrees < 0.0) ? 'S' : 'N';
            var absoluteAngle = new Angle(Math.Abs(normAngle.DecimalDegrees));
            var absoluteAngleStr = ToDMSFormat(absoluteAngle);
            return $"{absoluteAngleStr}{northSouth}";
        }

        private static string ToLongFormat(Angle angle)
        {
            var normAngle = angle.ToNormalizedDegreesInclusive(-180.0, 180.0);
            var eastWest = (normAngle.DecimalDegrees < 0.0) ? 'W' : 'E';
            var absoluteAngle = new Angle(Math.Abs(normAngle.DecimalDegrees));
            var absoluteAngleStr = ToDMSFormat(absoluteAngle);
            return $"{absoluteAngleStr}{eastWest}";
        }

        private static double Normalize(double valueToNormalize, double inclusiveMin, double exclusiveMax)
        {
            if (valueToNormalize >= inclusiveMin && valueToNormalize < exclusiveMax)
            {
                return valueToNormalize;
            }

            return (valueToNormalize < inclusiveMin)
                ? Normalize(valueToNormalize + exclusiveMax, inclusiveMin, exclusiveMax)
                : Normalize(valueToNormalize - exclusiveMax, inclusiveMin, exclusiveMax);
        }

        private static double NormalizeInclusive(double valueToNormalize, double inclusiveMin, double inclusiveMax)
        {
            if (valueToNormalize >= inclusiveMin && valueToNormalize <= inclusiveMax)
            {
                return valueToNormalize;
            }

            return (valueToNormalize < inclusiveMin)
                ? NormalizeInclusive(valueToNormalize + inclusiveMax, inclusiveMin, inclusiveMax)
                : NormalizeInclusive(valueToNormalize - inclusiveMax, inclusiveMin, inclusiveMax);
        }

        private static double GetTotalAngle(double hoursOrDegrees, double minutes, double seconds)
        {
            minutes = minutes.VerifyInRange(nameof(minutes), 0.0, 60.0);
            seconds = seconds.VerifyInRange(nameof(seconds), 0.0, 60.0);

            if (hoursOrDegrees < 0.0)
            {
                minutes *= -1;
                seconds *= -1;
            }

            return hoursOrDegrees
                + (minutes / CoordinateConstants.MinutesPerHour)
                + (seconds / CoordinateConstants.SecondsPerHour);
        }
        #endregion Private
    }

    public enum AngleFormat
    {
        Ra,
        Dec,
        Az,
        Alt,
        Lat,
        Long,
        HMS,
        DMS
    }
}
