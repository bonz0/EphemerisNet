
namespace EphemerisNet.Coordinates
{
    /// <summary>
    /// Represents the Azimuth-Altitude coordinate system
    /// </summary>
    public class AzAltCoordinate
    {
        public AzAltCoordinate(double azimuthDegrees, double altitudeDegrees)
        {
            this.Azimuth = Angle.FromDegrees(azimuthDegrees);
            this.Altitude = Angle.FromDegrees(altitudeDegrees);
        }

        public AzAltCoordinate(Angle azimuth, Angle altitude)
        {
            this.Azimuth = azimuth;
            this.Altitude = altitude;
        }

        public Angle Azimuth { get; }

        public Angle Altitude { get;  }

        public override string ToString()
        {
            return $"{this.Azimuth.ToString(AngleFormat.Az)}/{this.Altitude.ToString(AngleFormat.Alt)}";
        }

        // TODO: Write equals, get hash code methods
    }
}
