
namespace EphemerisNet.Coordinates
{
    /// <summary>
    /// Represents the equatorial coordinate system with
    /// Right Ascension and Declination
    /// </summary>
    public class RaDecCoordinate
    {
        public RaDecCoordinate(double raHours, double raMinutes, double raSeconds,
            double decDegrees, double decMinutes, double decSeconds)
        {
            this.Ra = Angle.FromHours(raHours, raMinutes, raSeconds);
            this.Dec = Angle.FromDegrees(decDegrees, decMinutes, decSeconds);
        }

        public Angle Ra { get; }

        public Angle Dec { get; }

        public override string ToString()
        {
            return $"{this.Ra.ToString(AngleFormat.Ra)}/{this.Dec.ToString(AngleFormat.Dec)}";
        }

        // TODO: Write equals, get hash code methods
    }
}
