using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace DiseaseCalculator.Classes
{
    internal class LineControl : Canvas
    {
        Line lineGraphic;
        PersonControl from;
        PersonControl to;

        public LineControl(PersonControl _from, PersonControl _to)
        {
            from = _from;
            to = _to;
            lineGraphic = new Line(); 
            lineGraphic.Fill = Brushes.Black;
            lineGraphic.Stroke = Brushes.Black;
            Update();
            Children.Add(lineGraphic);
        }
        double Distance(Point x, Point to)
        {
            return Math.Sqrt((x.X - to.X) * (x.X - to.X) + (x.Y - to.Y) * (x.Y - to.Y));
        }

        public Point CalculateCollision(PersonControl from, Point to)
        {
            Point p = from.Position;
            double d = 9999999;
            int k = 0;

            for (int i = 0; i < from.Collision.Count; i++)
            {
                Point p1 = new Point(from.Collision[i].X + from.Position.X, from.Collision[i].Y + from.Position.Y);
                double d1 = Distance(p1, to);
                if (d1 < d)
                {
                    d = d1;
                    p = p1;
                    k = i;
                }
            }

            return p;
        }

        public void Update()
        {
            Point p1 = CalculateCollision(from, to.Center());
            Point p2 = CalculateCollision(to, p1);
            lineGraphic.X1 = p1.X;
            lineGraphic.Y1 = p1.Y;
            lineGraphic.X2 = p2.X;
            lineGraphic.Y2 = p2.Y;
        }
    }
}
