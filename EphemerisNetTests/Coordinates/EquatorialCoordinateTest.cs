
namespace EphemerisNetTests.Coordinates
{
    using EphemerisNet.Coordinates;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class EquatorialCoordinateTest
    {
        [TestMethod]
        public void Test()
        {
            var eqCoordinates = new RaDecCoordinate(18, 31, 27.012, 182, 31, 27);
            var ra = eqCoordinates.Ra;
            var dec = eqCoordinates.Dec;
        }
    }
}
