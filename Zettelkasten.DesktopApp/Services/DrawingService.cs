using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Shapes;
using Zettelkasten.Domain.Models.Painting;

namespace Zettelkasten.DesktopApp.Services
{
    public class DrawingService
    {
        private readonly int _poinShift;
        private readonly int _size;
        private readonly int _centrX;
        private readonly int _centrY;

        public DrawingService(int poinShift, int size, int centrX, int centrY)
        {
            _poinShift = poinShift;
            _size = size;
            _centrX = centrX;
            _centrY = centrY;
        }

        public List<Ellipse> CreateEllipses(List<PolarPointPolyColored> first)
        {
            var res = new List<Ellipse>();
            foreach (var item in first)
            {
                if (item.Colors.Count == 1)
                {
                    // цвет заливки
                    var currentColor = item.Colors[0];
                    var shape = CreateEllipse(item, currentColor);
                    res.Add(shape);
                }
                else
                {
                    foreach (var currentColor in item.Colors)
                    {
                        var shape = CreateEllipse(item, currentColor);
                        res.Add(shape);
                    }
                }
            }
            return res;
        }

        public List<Polygon> CreatePolygones(List<PolarPointPolyColored> first)
        {
            var res = new List<Polygon>();
            foreach (var item in first)
            {
                if (item.Colors.Count == 1)
                {
                    // цвет заливки
                    var currentColor = item.Colors[0];
                    var shape = CreatePolygon(item, currentColor);
                    res.Add(shape);
                }
                else
                {
                    var count = item.Colors.Count;
                    // чтобы отобразить точки со сдвигом сделаем count радиусов
                    // и по ним сделаем сдвиги
                    var angle = (double)360 / count;
                    for (int i = 0; i < count; i++)
                    {
                        var currentColor = item.Colors[i];
                        var poinShift = new PolarPoint(_poinShift, angle * i, -1, "");
                        var shape = CreatePolygon(item, currentColor, poinShift);
                        res.Add(shape);
                    }
                }
            }
            return res;
        }

        public Polygon CreatePolygon(PolarPointPolyColored item, System.Drawing.Color currentColor, PolarPoint poinShift = null)
        {
            var newColor = GetFillColor(currentColor);
            var fill = new SolidColorBrush(newColor);

            var x1 = 0;
            var y1 = 0;

            if (poinShift != null)
            {
                x1 = (int)poinShift.X;
                y1 = (int)poinShift.Y;
            }

            var p1 = new System.Windows.Point(_centrX + item.X + x1, _centrY + item.Y + y1);
            var p2 = new System.Windows.Point(_centrX + item.X + x1 + _size, _centrY + item.Y + y1);
            var p3 = new System.Windows.Point(_centrX + item.X + x1 + _size, _centrY + item.Y + y1 + _size);
            var p4 = new System.Windows.Point(_centrX + item.X + x1, _centrY + item.Y + y1 + _size);
            var p5 = new System.Windows.Point(_centrX + item.X + x1, _centrY + item.Y + y1);
            var points = new PointCollection(
                new List<System.Windows.Point>() { p1, p2, p3, p4, p5 });

            var shape = new Polygon()
            {
                ToolTip = item.Tooltip,
                Fill = fill,
                Points = points
            };

            return shape;
        }

        public Ellipse CreateEllipse(PolarPointPolyColored item, System.Drawing.Color currentColor)
        {
            var newColor = GetFillColor(currentColor);
            var fill = new SolidColorBrush(newColor);


            var shape = new Ellipse()
            {
                Height = _size,
                Width = _size,
                ToolTip = item.Tooltip,
                Fill = fill,
            };

            return shape;
        }


        public static System.Windows.Media.Color GetFillColor(System.Drawing.Color currentColor)
        {
            return System.Windows.Media.Color.FromArgb(currentColor.A, currentColor.R, currentColor.G, currentColor.B);
        }
    }
}
