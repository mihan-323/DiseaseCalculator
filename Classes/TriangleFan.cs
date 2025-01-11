using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace DiseaseCalculator.Classes
{
    public class TriangleFan
    {
        public PointCollection points;
        public PointCollection pointsAdditional;
        public PointCollection pointsTesstlated;

        static public double Distance(Point x, Point to)
        {
            return Math.Sqrt((x.X - to.X) * (x.X - to.X) + (x.Y - to.Y) * (x.Y - to.Y));
        }
        
        static public Point Subdivide(Point x, Point to, double fraction)
        {
            return new Point(x.X * fraction + to.X * (1.0 - fraction), x.Y * fraction + to.Y * (1.0 - fraction));
        }

        public TriangleFan(PointCollection pointsFan, int maxTessLevel, int minDistPixels = 1)
        {
            PointCollection collision = new PointCollection();
            PointCollection append = new PointCollection();

            for (int i = 0; i < pointsFan.Count; i++)
            {
                Point currPoint = pointsFan[i];
                Point nextPoint = pointsFan[(i + 1) % pointsFan.Count];

                double dist = Distance(currPoint, nextPoint);
                int currTessLevel = (int)(dist / minDistPixels + 0.5);

                if (currTessLevel > maxTessLevel)
                    currTessLevel = maxTessLevel;

                for (int j = 0; j < currTessLevel; j++)
                {
                    double tessDist = (double)j / currTessLevel;
                    Point tessPoint = Subdivide(currPoint, nextPoint, tessDist);
                    collision.Add(tessPoint);

                    if(tessPoint != nextPoint)
                        append.Add(tessPoint);
                }
            }

            Polyline polyline = new Polyline();

            foreach (var it in pointsFan)
                polyline.Points.Add(it);

            if(pointsFan.Count > 0)
                polyline.Points.Add(pointsFan[0]);

            polyline.Stroke = Brushes.Black;
            polyline.StrokeThickness = 1;

            Polygon polygon = new Polygon();

            polygon.Fill = Brushes.Red; // test

            int primCount = pointsFan.Count - 2;
            int vertCount = primCount * 3;

            int[] indices = new int[vertCount];
            for (int i = 0; i < primCount; i++)
            {
                indices[i * 3 + 0] = 0;
                indices[i * 3 + 1] = i + 1;
                indices[i * 3 + 2] = i + 2;
            }

            for(int i = 0; i < vertCount; i++)
                polygon.Points.Add(pointsFan[indices[i]]);

            points = pointsFan;
            pointsTesstlated = collision;
            pointsAdditional = append;
        }
    }
}
