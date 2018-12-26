using EphemerisNet.Coordinates;
using EphemerisNet.Coordinates.Converters;
using EphemerisNet.Time;
using MathNet.Numerics.LinearAlgebra.Double;
using System;

namespace EphemerisNet
{
    public class Ephemeris
    {
        public static void THingi()
        {
            var matrix = DenseMatrix.OfArray(new double[,]
            {
                {1, 1, 3 },
                {1, 1, 3 },
                {1, 1, 3 },
            });
            var matrix2 = DenseMatrix.OfArray(new double[,]
            {
                {1, 1, 3 },
                {1, 1, 3 },
                {1, 1, 3 },
            });
            var result = matrix.Multiply(matrix2);
        }

        public static Matrix GetHaDecToAltAzConversionMatrix(double latitudeDegrees)
        {
            var latitudeRadians = latitudeDegrees.DegreesToRadians();
            var cosLat = Math.Cos(latitudeRadians);
            var sinLat = Math.Sin(latitudeRadians);
            return DenseMatrix.OfArray(new double[,]
            {
                { -sinLat, 0.0, cosLat },
                { 0.0, -1.0, 0.0 },
                { cosLat, 0.0, sinLat },
            });
        }

        public static AzAltCoordinate ToHorizon(RaDecCoordinate eq, DateTime dateTime, GeoCoordinate geo)
        {
            //var ephemerisTime = new EphemerisTime(dateTime);
            //var lst = ephemerisTime.ToLST(geo.Longitude);
            //var hourAngleDegrees = lst.HoursToDegrees() - eq.Ra;
            //var hourAngleRadians = hourAngleDegrees.DegreesToRadians();
            //var sinHa = Math.Sin(hourAngleRadians);
            //var decRadians = eq.Dec.Radians;
            //var latRadians = geo.Latitude.Radians;
            //var sinDec = Math.Sin(decRadians);
            //var sinLat = Math.Sin(latRadians);
            //var cosDec = Math.Cos(decRadians);
            //var cosLat = Math.Cos(latRadians);
            //var cosHa = Math.Cos(hourAngleRadians);
            //var sinAlt = (sinDec * sinLat + cosDec * cosLat * cosHa);
            //var altRadians = Math.Asin(sinAlt);
            //var cosAlt = Math.Cos(altRadians);
            //var cosAz = (sinDec - (sinLat * sinAlt)) / (cosLat * cosAlt);
            //var az = Math.Acos(cosAz).RadiansToDegrees();
            //var alt = altRadians.RadiansToDegrees();
            //{
            //var haDecVector = GetVectorFromCoordinate(hourAngleDegrees, eq.Dec);
            //    var matrix = GetHaDecToAltAzConversionMatrix(geo.Latitude);
            //    var altAzVector = matrix.Multiply(haDecVector);
            //    var altAz = GetCoordinateFromVector(altAzVector);
            ////}
            //return new AzAltCoordinate((sinHa < 0.0) ? az : 360.0 - az, alt);
            var converter = new CoordinateConverter();
            var altAz = converter.ToHorizon(eq, new EphemerisTime(dateTime), geo);
            return altAz;
        }

        public static DenseVector GetVectorFromCoordinate(double u, double v)
        {
            var x = Math.Cos(u.DegreesToRadians()) * Math.Cos(v.DegreesToRadians());
            var y = Math.Sin(u.DegreesToRadians()) * Math.Cos(v.DegreesToRadians());
            var z = Math.Sin(v.DegreesToRadians());
            return DenseVector.OfArray(new double[] { x, y, z });
        }

        public static Tuple<double, double> GetCoordinateFromVector(MathNet.Numerics.LinearAlgebra.Vector<double> vector)
        {
            var m = vector[0];
            var n = vector[1];
            var p = vector[2];
            var coord1 = Math.Atan(n / m);
            var coord2 = Math.Asin(p);
            return Tuple.Create(coord1.RadiansToDegrees(), coord2.RadiansToDegrees());
        }
    }
}
