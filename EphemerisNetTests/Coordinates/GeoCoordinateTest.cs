
namespace EphemerisNetTests.Coordinates
{
    using EphemerisNet.Coordinates;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using FluentAssertions;

    [TestClass]
    public class GeoCoordinateTest
    {
        private const string dS = "\x00B0";

        [TestMethod]
        public void TestHappyCaseGeoCoordinate()
        {
            var geoCoordinate = new GeoCoordinate(0, 0, 0, 0, 0, 0);
            //geoCoordinate.Latitude.Should().Be(Angle.FromDegrees(0.0));
            //geoCoordinate.Longitude.Should().Be(Angle.FromDegrees(0));
            geoCoordinate.ToString().Should().Be($"0{dS}0'0\"N 0{dS}0'0\"E");

            geoCoordinate = new GeoCoordinate(1, 0, 0, -1, 0, 0);
            //geoCoordinate.Latitude.Should().Be(1.0);
            //geoCoordinate.Longitude.Should().Be(-1.0);
            geoCoordinate.ToString().Should().Be($"1{dS}0'0\"N 1{dS}0'0\"W");

            geoCoordinate = new GeoCoordinate(-1, 0, 0, 1, 0, 0);
            //geoCoordinate.Latitude.Should().Be(-1.0);
            //geoCoordinate.Longitude.Should().Be(1.0);
            geoCoordinate.ToString().Should().Be($"1{dS}0'0\"S 1{dS}0'0\"E");

            geoCoordinate = new GeoCoordinate(40, 30, 0, 122, 45, 0);
            //geoCoordinate.Latitude.Should().Be(40.50);
            //geoCoordinate.Longitude.Should().Be(122.75);
            geoCoordinate.ToString().Should().Be($"40{dS}30'0\"N 122{dS}45'0\"E");

            geoCoordinate = new GeoCoordinate(-40, 45, 0, -122, 30, 0);
            //geoCoordinate.Latitude.Should().Be(-40.75);
            //geoCoordinate.Longitude.Should().Be(-122.50);
            geoCoordinate.ToString().Should().Be($"40{dS}45'0\"S 122{dS}30'0\"W");

            geoCoordinate = new GeoCoordinate(2, 30, 30, 3, 45, 45);
            //geoCoordinate.Latitude.DecimalDegrees.Should().BeApproximately(2.5083333, 0.00001);
            //geoCoordinate.Longitude.Should().Be(3.7625);
            geoCoordinate.ToString().Should().Be($"2{dS}30'30\"N 3{dS}45'45\"E");
        }
    }
}
