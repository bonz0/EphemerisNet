
namespace EphemerisNet.Time
{
    using Coordinates;
    using System;

    public class EphemerisTime
    {
        private const double OAToJulianDateConversion = 2415018.5;
        private const double J2000JulianDate = 2451545.0;

        private readonly DateTime _dateTime;

        public EphemerisTime(DateTime dateTime)
        {
            this._dateTime = new DateTime(dateTime.Ticks, DateTimeKind.Utc);
        }

        public double JulianDate => EphemerisTime.ToJulianDate(this._dateTime);

        public TimeSpan GST => EphemerisTime.ToGST(this._dateTime);

        private static double ToJulianDate(DateTime dateTime)
        {
            return dateTime.ToOADate() + OAToJulianDateConversion;
        }

        /// <remarks>PAWYC S.12</remarks>
        private static TimeSpan ToGST(DateTime dateTime)
        {
            var date = dateTime.Date;
            var julianDate = ToJulianDate(date);
            var S = julianDate - J2000JulianDate;
            var T = S / 36525.0;
            var T0temp = (6.697374558) + (2400.051336 * T) + (0.000025862 * Math.Pow(T, 2));
            var T0 = CoordinateExtensions.ReduceTo24Hours(T0temp);
            var timeInHours = dateTime.TimeOfDay.TotalHours;
            var timeInHoursConversion = timeInHours * 1.002737909;
            var gstTemp = T0 + timeInHoursConversion;
            var gst = CoordinateExtensions.ReduceTo24Hours(gstTemp);
            return TimeSpan.FromHours(gst);
        }

        private static TimeSpan ToLST(DateTime dateTime, Angle longitudeDegrees)
        {
            var gst = EphemerisTime.ToGST(dateTime);
            var lst = Angle.FromHours(gst.TotalHours + longitudeDegrees.DecimalHours);
            return TimeSpan.FromHours(lst.ToNormalizedHours(0.0, 24.0).DecimalHours);
        }

        public TimeSpan ToLST(GeoCoordinate geoCoordinate)
        {
            return this.ToLST(geoCoordinate.Longitude);
        }

        public TimeSpan ToLST(Angle longitudeDegrees)
        {
            return EphemerisTime.ToLST(this._dateTime, longitudeDegrees);
        }

        public static EphemerisTime FromJulianDate(double julianDate)
        {
            return new EphemerisTime(DateTime.FromOADate(julianDate - OAToJulianDateConversion));
        }
    }
}
