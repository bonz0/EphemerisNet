
namespace EphemerisNetTests
{
    using EphemerisNet;
    using EphemerisNet.Coordinates;
    using EphemerisNet.Time;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;

    [TestClass]
    public class EphemerisTest
    {
        [TestMethod]
        public void Test()
        {

            var eq = new RaDecCoordinate(20, 42, 5.61, 45, 21, 13.5);
            var dateTime = new DateTime(2018, 12, 22, 1, 25, 0);
            //var geo = new GeoCoordinate(47, 36, 22.35, -122, 19, 55.45);
            var geo = new GeoCoordinate(4, 36, 22.35, 12, 19, 55.45);

            var ephemTime = new EphemerisTime(dateTime);
            //var lst = ephemTime.ToLST(Angle.FromDegrees(-64));
            var altAz = Ephemeris.ToHorizon(eq, dateTime, geo);
            var altAzStr = altAz.ToString();
            Ephemeris.THingi();
        }
    }
}
