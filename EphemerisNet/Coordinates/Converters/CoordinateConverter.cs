using EphemerisNet.Time;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;
using System;

namespace EphemerisNet.Coordinates.Converters
{
    internal class CoordinateConverter
    {
        public AzAltCoordinate ToHorizon(RaDecCoordinate equatorial, EphemerisTime dateTime, GeoCoordinate geo)
        {
            var lst = dateTime.ToLST(geo);
            var hourAngle = Angle.FromHours(lst.TotalHours - equatorial.Ra.DecimalHours);
            var haDecVector = GetVectorFromCoordinate(hourAngle, equatorial.Dec);
            var conversionMatrix = GetHaDecToAltAzConversionMatrix(geo);
            var altAzVector = conversionMatrix.Multiply(haDecVector);
            var altAzTuple = GetCoordinateFromVector(altAzVector);
            return new AzAltCoordinate(altAzTuple.Item1, altAzTuple.Item2);
        }

        protected static DenseVector GetVectorFromCoordinate(Angle u, Angle v)
        {
            var uRadians = u.Radians;
            var vRadians = v.Radians;
            var cosU = Math.Cos(uRadians);
            var cosV = Math.Cos(vRadians);
            var sinU = Math.Sin(uRadians);
            var sinV = Math.Sin(vRadians);
            var x = cosU * cosV;
            var y = sinU * cosV;
            var z = sinV;
            return DenseVector.OfArray(new double[] { x, y, z });
        }

        protected static Matrix GetHaDecToAltAzConversionMatrix(GeoCoordinate geo)
        {
            var latitudeRadians = geo.Latitude.Radians;
            var cosLat = Math.Cos(latitudeRadians);
            var sinLat = Math.Sin(latitudeRadians);
            return DenseMatrix.OfArray(new double[,]
            {
                { -sinLat,  0.0,  cosLat },
                { 0.0,     -1.0,  0.0    },
                { cosLat,   0.0,  sinLat },
            });
        }

        //protected static Matrix GetRaDecToHaDecConversionMatrix(Angle localSiderialTime)
        //{
        //    var lstRadians = localSiderialTime.Radians;
        //    var sinLst = Math.Sin(lstRadians);
        //    var cosLst = Math.Cos(lstRadians);
        //    return DenseMatrix.OfArray(new double[,]
        //    {
        //        { cosLst, sinLst,  0   },
        //        { sinLst, -cosLst, 0   },
        //        { 0.0,    0.0,     1.0 }
        //    });
        //}

        protected static Tuple<Angle, Angle> GetCoordinateFromVector(Vector<double> vector)
        {
            var m = vector[0];
            var n = vector[1];
            var p = vector[2];
            var coord1 = Math.Atan(n / m);
            var coord2 = Math.Asin(p);
            return Tuple.Create(Angle.FromRadians(coord1), Angle.FromRadians(coord2));
        }
    }
}
