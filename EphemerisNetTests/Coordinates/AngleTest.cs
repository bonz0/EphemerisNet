
namespace EphemerisNetTests.Coordinates
{
    using EphemerisNet.Coordinates;
    using FluentAssertions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Collections.Generic;

    [TestClass]
    public class AngleTest
    {
        [TestMethod]
        public void TestAngleValues()
        {
            AssertAngleStringRepresentations(Angle.FromDegrees(22, 33, 44),
                new Dictionary<AngleFormat, string>
                {
                    { AngleFormat.Alt, $"22{CoordinateConstants.DegreeSymbol}33'44\"" },
                    { AngleFormat.Az, $"22{CoordinateConstants.DegreeSymbol}33'44\"" },
                    { AngleFormat.Dec, $"22{CoordinateConstants.DegreeSymbol}33'44\"" },
                    { AngleFormat.Ra, "1h30m14.93s" },
                    { AngleFormat.DMS, $"22{CoordinateConstants.DegreeSymbol}33'44\"" },
                    { AngleFormat.HMS, "1h30m14.93s" },
                    { AngleFormat.Lat, $"22{CoordinateConstants.DegreeSymbol}33'44\"N" },
                    { AngleFormat.Long, $"22{CoordinateConstants.DegreeSymbol}33'44\"E" }
                });

            AssertAngleStringRepresentations(Angle.FromDegrees(-33, 22, 11),
                new Dictionary<AngleFormat, string>
                {
                    { AngleFormat.Alt, $"-33{CoordinateConstants.DegreeSymbol}22'11\"" },
                    { AngleFormat.Az, $"326{CoordinateConstants.DegreeSymbol}37'49\"" },
                    { AngleFormat.Dec, $"-33{CoordinateConstants.DegreeSymbol}22'11\"" },
                    { AngleFormat.Ra, "21h46m31.27s" },
                    { AngleFormat.DMS, $"-33{CoordinateConstants.DegreeSymbol}22'11\"" },
                    { AngleFormat.HMS, "-2h13m28.73s" },
                    { AngleFormat.Lat, $"33{CoordinateConstants.DegreeSymbol}22'11\"S" },
                    { AngleFormat.Long, $"33{CoordinateConstants.DegreeSymbol}22'11\"W" }
                });

            AssertAngleStringRepresentations(Angle.FromHours(25, 45, 34.67),
                new Dictionary<AngleFormat, string>
                {
                    { AngleFormat.Alt, $"26{CoordinateConstants.DegreeSymbol}23'40.05\"" },
                    { AngleFormat.Az, $"26{CoordinateConstants.DegreeSymbol}23'40.05\"" },
                    { AngleFormat.Dec, $"26{CoordinateConstants.DegreeSymbol}23'40.05\"" },
                    { AngleFormat.Ra, "1h45m34.67s" },
                    { AngleFormat.DMS, $"386{CoordinateConstants.DegreeSymbol}23'40.05\"" },
                    { AngleFormat.HMS, "25h45m34.67s" },
                    { AngleFormat.Lat, $"26{CoordinateConstants.DegreeSymbol}23'40.05\"N" },
                    { AngleFormat.Long, $"26{CoordinateConstants.DegreeSymbol}23'40.05\"E" },
                });
        }

        private static void AssertAngleStringRepresentations(Angle angle,
            IEnumerable<KeyValuePair<AngleFormat, string>> strReps)
        {
            foreach (var kvPair in strReps)
            {
                var format = kvPair.Key;
                var expectedStr = kvPair.Value;

                var angleStr = angle.ToString(format);

                angleStr.Should().Be(expectedStr);
            }
        }
    }
}
