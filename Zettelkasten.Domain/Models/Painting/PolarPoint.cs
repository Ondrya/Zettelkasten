using System.Drawing;

namespace Zettelkasten.Domain.Models.Painting
{
    public class PolarPoint
    {
        public PolarPoint(double radius, double angleDeg)
        {
            if (radius < 0.0)
                throw new ArgumentException("Radius must be non-negative");
            if ((angleDeg < 0) || (angleDeg >= 360.0))
                throw new ArgumentException("Angle must be in range [0,360)");

            Radius = radius;
            AngleDeg = angleDeg;
        }


        // Polar coordinates
        public double Radius { get; set; }
        public double AngleDeg { get; set; }


        // Cartesian coordinates
        public double X
        {
            get { return Radius * Math.Cos(AngleDeg * Math.PI / 180.0); }
        }

        public double Y
        {
            get { return Radius * Math.Sin(AngleDeg * Math.PI / 180.0); }
        }


        public override string ToString()
        {
            return string.Format("({0},{1})", X, Y);
        }
    }


    public class PolarPointColored : PolarPoint
    {
        public Color Color { get; set; }

        public PolarPointColored(double radius, double angleDeg, Color color) : base(radius, angleDeg)
        {
            Color = color;
        }
    }
}
