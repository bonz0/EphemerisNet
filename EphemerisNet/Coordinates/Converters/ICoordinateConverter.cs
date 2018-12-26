
namespace EphemerisNet.Coordinates.Converters
{
    using Time;

    public interface ICoordinateConverter
    {
        AzAltCoordinate ToHorizon(RaDecCoordinate equatorial,
            EphemerisTime datetime, GeoCoordinate geo);
    }
}
