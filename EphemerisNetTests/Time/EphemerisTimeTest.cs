
namespace EphemerisNetTests.Time
{
    using EphemerisNet.Time;
    using FluentAssertions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;

    [TestClass]
    public class EphemerisTimeTest
    {
        [TestMethod]
        public void TestJulianDate()
        {
            var ephemerisTime = new EphemerisTime(new DateTime(1985, 2, 17, 6, 0, 0));
            var julianDate = ephemerisTime.JulianDate;
            julianDate.Should().Be(2446113.75);

            ephemerisTime = new EphemerisTime(new DateTime(2018, 12, 25, 10, 23, 31));
            julianDate = ephemerisTime.JulianDate;
            julianDate.Should().Be(2458477.9329976854);
        }

        [TestMethod]
        public void TestGST()
        {
            var ephemerisTime = new EphemerisTime(new DateTime(1999, 9, 9, 9, 9, 9));
            var gst = ephemerisTime.GST;

            var d = new DateTime();
            var local = new DateTime(2018, 12, 31, 12, 0, 0, DateTimeKind.Local);
            var gmt = new DateTime(2018, 12, 31, 12, 0, 0, DateTimeKind.Utc);
            var meh = new DateTime(2018, 12, 31, 12, 0, 0, DateTimeKind.Unspecified);
            var gmtFromLocal = new DateTime(local.Ticks, DateTimeKind.Utc);
            var gmtFromGmt = new DateTime(gmt.Ticks, DateTimeKind.Utc);
            var gmtFromMeh = new DateTime(meh.Ticks, DateTimeKind.Utc);
            var localToLocal = gmtFromLocal.ToLocalTime();
            var gmtToLocal = gmtFromGmt.ToLocalTime();
            var mehToLocal = gmtFromMeh.ToLocalTime();
            //julianDate.Should().Be(2446113.75);

            //ephemerisTime = new EphemerisTime(new DateTime(2018, 12, 25, 10, 23, 31));
            //julianDate = ephemerisTime.JulianDate;
            //julianDate.Should().Be(2458477.9329976854);
        }
    }
}
