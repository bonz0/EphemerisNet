
namespace EphemerisNet.Coordinates
{
    /// <summary>
    /// Represents the geographical coordinate system:
    /// Latitude and Longitude
    /// </summary>
    public class GeoCoordinate
    {
        // TODO: Validate inputs
        public GeoCoordinate(int latitudeDegrees, int latitudeMinutes, double latitudeSeconds,
            int longitudeDegrees, int longitudeMinutes, double longitudeSeconds)
        {
            this.Latitude = Angle.FromDegrees(latitudeDegrees, latitudeMinutes, latitudeSeconds);
            this.Longitude = Angle.FromDegrees(longitudeDegrees, longitudeMinutes, longitudeSeconds);
        }

        public GeoCoordinate(double latitudeDegrees, double longitudeDegrees)
        {
            this.Latitude = Angle.FromDegrees(latitudeDegrees);
            this.Longitude = Angle.FromDegrees(longitudeDegrees);
        }

        public Angle Latitude { get; }

        public Angle Longitude { get; }

        public override string ToString()
        {
            return $"{this.Latitude.ToString(AngleFormat.Lat)} {this.Longitude.ToString(AngleFormat.Long)}";
        }

        // TODO: Write equals, get hash code methods
    }
}
