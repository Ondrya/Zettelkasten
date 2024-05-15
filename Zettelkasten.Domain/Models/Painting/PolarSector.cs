using System.Drawing;

namespace Zettelkasten.Domain.Models.Painting
{
    public class PolarSector : PolarBase
    {
        public PolarSector(double radiusMin, double radiusMax, double angleDegMin, double angleDegMax, int entityId, string tooltip) : base(entityId, tooltip)
        {
            if (radiusMin < 0.0)
                throw new ArgumentException("Radius must be non-negative");
            if (radiusMax < 0.0)
                throw new ArgumentException("Radius must be non-negative");
            if (radiusMin > radiusMax)
                throw new ArgumentException("Radius Min must be less then Radius Max");
            if ((angleDegMin < 0) || (angleDegMin >= 360.0))
                throw new ArgumentException("Angle must be in range [0,360)");
            if ((angleDegMax < 0) || (angleDegMax >= 360.0))
                throw new ArgumentException("Angle must be in range [0,360)");
            if (angleDegMin > angleDegMax)
                throw new ArgumentException("Angle Min must be less then Angle Max");


            RadiusMin = radiusMin;
            RadiusMax = radiusMax;
            AngleDegMin = angleDegMin;
            AngleDegMax = angleDegMax;
        }


        // Polar coordinates
        public double RadiusMin { get; set; }
        public double RadiusMax { get; set; }
        public double AngleDegMin { get; set; }
        public double AngleDegMax { get; set; }

        // Cartesian coordinates
        public PolarPoint A
        {
            get { return new PolarPoint(RadiusMin, AngleDegMin); }
        }

        public PolarPoint B
        {
            get { return new PolarPoint(RadiusMax, AngleDegMin); }
        }

        public PolarPoint C
        {
            get { return new PolarPoint(RadiusMax, AngleDegMax); }
        }

        public PolarPoint D
        {
            get { return new PolarPoint(RadiusMin, AngleDegMax); }
        }


        
        public override string ToString()
        {
            return string.Format("({0},{1},{2},{3})", A, B, C, D);
        }
    }


    public class PolarSectorColored : PolarSector
    {
        public Color Color { get; set; }

        public PolarSectorColored(double radiusMin, double radiusMax, double angleDegMin, double angleDegMax, Color color, int entityId, string tooltip) : base(radiusMin, radiusMax, angleDegMin, angleDegMax, entityId, tooltip)
        {
            Color = color;
        }
    }
}
