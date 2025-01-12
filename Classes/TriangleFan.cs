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
    public class TriangleFan
    {
        PointCollection points;
        PointCollection pointsAdditional;
        PointCollection pointsTesstlated;
        Polygon polygon;
        Polyline polyline;

        public PointCollection Points { get => points; }
        public PointCollection PointsTesselated { get => pointsTesstlated; }
        public PointCollection PointsAdditional { get => pointsAdditional; }
        public Polygon Polygon { get => polygon; }
        public Polyline Polyline { get => polyline; }


        // Длина отрезка
        static public double Distance(Point x, Point to)
        {
            return Math.Sqrt((x.X - to.X) * (x.X - to.X) + (x.Y - to.Y) * (x.Y - to.Y));
        }
        
        // Разделение отрезка в нужной пропорции
        static public Point Subdivide(Point x, Point to, double fraction)
        {
            return new Point(x.X * fraction + to.X * (1.0 - fraction), x.Y * fraction + to.Y * (1.0 - fraction));
        }

        public TriangleFan(PointCollection pointsFan, int maxTessLevel, int minDistPixels = 1)
        {
            points = pointsFan;
            pointsTesstlated = new PointCollection();
            pointsAdditional = new PointCollection();

            // generate collision points
            for (int i = 0; i < pointsFan.Count; i++)
            {
                Point currPoint = pointsFan[i];
                Point nextPoint = pointsFan[(i + 1) % pointsFan.Count];

                double dist = Distance(currPoint, nextPoint);

                // set max subdivision less than pixel size
                int currTessLevel = (int)(dist / minDistPixels + 0.5);

                if (currTessLevel > maxTessLevel)
                    currTessLevel = maxTessLevel;

                for (int j = 0; j < currTessLevel; j++)
                {
                    double tessDist = (double)j / currTessLevel;
                    Point tessPoint = Subdivide(currPoint, nextPoint, tessDist);
                    pointsTesstlated.Add(tessPoint);

                    if(tessPoint != nextPoint)
                        pointsAdditional.Add(tessPoint);
                }
            }

            // create border
            polyline = new Polyline();

            foreach (var it in pointsFan)
                polyline.Points.Add(it);

            if(pointsFan.Count > 0)
                polyline.Points.Add(pointsFan[0]);

            polyline.Stroke = Brushes.Black;
            polyline.StrokeThickness = 1;

            // create triangles
            polygon = new Polygon();

            polygon.Fill = Brushes.Red; // test

            int primCount = pointsFan.Count - 2;
            int vertCount = primCount * 3;

            // create indices array for each primitive
            int[] indices = new int[vertCount];
            for (int i = 0; i < primCount; i++)
            {
                indices[i * 3 + 0] = 0;
                indices[i * 3 + 1] = i + 1;
                indices[i * 3 + 2] = i + 2;
            }

            // convert fan points into polygon points
            for (int i = 0; i < vertCount; i++)
                polygon.Points.Add(pointsFan[indices[i]]);
        }
    }
}
