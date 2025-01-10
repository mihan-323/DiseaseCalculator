using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;

namespace DiseaseCalculator.Classes
{
    class Diagram : Canvas
    {
        public ScrollViewer vp;
        public List<PersonControl> controls;

        public Diagram(double diagramWidth, double diagramHeight, ScrollViewer _vp)
        {
            Background = (Brush)Application.Current.FindResource("diagramBackgroundCell");

            Width = diagramWidth;
            Height = diagramHeight;

            vp = _vp;

            Rect viewport = new Rect(0, 0, Width, Height);
            Arrange(viewport);

            HorizontalAlignment = HorizontalAlignment.Left;
            VerticalAlignment = VerticalAlignment.Top;
        }

        public void CreateControlsByGraph(PersonsGraph graph)
        {
        }
    }
}
