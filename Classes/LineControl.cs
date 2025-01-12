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
        Polygon arrowGraphic;
        TextBlock textGraphic;

        public LineControl(PersonControl _from, PersonControl _to)
        {
            from = _from;
            to = _to;
            lineGraphic = new Line(); 
            lineGraphic.Fill = Brushes.Black;
            lineGraphic.Stroke = Brushes.Black;
            arrowGraphic = new Polygon();
            arrowGraphic.Points.Add(new Point());
            arrowGraphic.Points.Add(new Point());
            arrowGraphic.Points.Add(new Point());
            arrowGraphic.Stroke = Brushes.Black;
            arrowGraphic.Fill = Brushes.Black;
            textGraphic = new TextBlock();
            textGraphic.Text = _to.Person.gender ? "Отец" : "Мать";
            textGraphic.FontSize = 12;
            Update();
            Children.Add(lineGraphic);
            Children.Add(arrowGraphic);
            Children.Add(textGraphic);
        }
        double Distance(Point x, Point to)
        {
            return Math.Sqrt((x.X - to.X) * (x.X - to.X) + (x.Y - to.Y) * (x.Y - to.Y));
        }

        public Point CalculateCollision(PersonControl from, Point to)
        {
            Point p = from.Position;
            double d = Double.MaxValue;
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
            Vector v1 = p2 - p1;
            v1.Normalize(); // dir
            Vector v2 = new Vector(-v1.Y, v1.X); // ortho
            Point p3 = p2 - v1 * 10 - v2 * 5;
            Point p4 = p2 - v1 * 10 + v2 * 5;
            arrowGraphic.Points[0] = p2; // front
            arrowGraphic.Points[1] = p3; // left
            arrowGraphic.Points[2] = p4; // right
            Point p5 = TriangleFan.Subdivide(p1, p2, 0.5);
            textGraphic.Margin = new Thickness(p5.X, p5.Y, 0, 0);
            double a = Math.Atan2(v1.Y, v1.X) * 57.2957795131;
            a = a < -90 || a > 90 ? 180 + a : a; // rotation angle degrees
            textGraphic.RenderTransform = new RotateTransform(a);
        }

        public void Show(bool line, bool arrow, bool text)
        {
            lineGraphic.Visibility = line ? Visibility.Visible : Visibility.Collapsed;
            arrowGraphic.Visibility = arrow ? Visibility.Visible : Visibility.Collapsed;
            textGraphic.Visibility = text ? Visibility.Visible : Visibility.Collapsed;
        }
    }
}
