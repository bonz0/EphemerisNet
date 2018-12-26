using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EphemerisNet.Coordinates
{
    /// <summary>
    /// General utility methods
    /// </summary>
    internal static class CoordinateExtensions
    {
        public static double VerifyInRange(this double valueToVerify, string paramName, double min, double max)
        {
            if (valueToVerify < min || valueToVerify >= max)
            {
                throw new ArgumentOutOfRangeException(paramName, $"Parameter must be in range [{min}, {max})");
            }

            return valueToVerify;
        }

        public static double ReduceToRange(double valueToReduce, double rangeMin, double rangeMax)
        {
            if (valueToReduce >= rangeMin && valueToReduce <= rangeMax)
            {
                return valueToReduce;
            }

            return (valueToReduce < rangeMin)
                ? ReduceToRange(valueToReduce + rangeMax, rangeMin, rangeMax)
                : ReduceToRange(valueToReduce - rangeMax, rangeMin, rangeMax);
        }

        public static double ReduceTo24Hours(double valueToReduce)
        {
            return ReduceToRange(valueToReduce, 0.0, 24.0);
        }

        public static double HoursToDegrees(this double hours)
        {
            return (hours * CoordinateConstants.DegreesPerHour);
        }

        public static double DegreesToHours(this double degrees)
        {
            return (degrees / CoordinateConstants.DegreesPerHour);
        }

        public static double DegreesToRadians(this double degrees)
        {
            return degrees * (Math.PI / 180.0);
        }

        public static double RadiansToDegrees(this double radians)
        {
            return (radians * 180.0) / Math.PI;
        }
    }
}
