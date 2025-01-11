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
        ScrollViewer vp;
        List<PersonControl> persons;
        List<LineControl> lines;
        PersonsGraph? graph;

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

            persons = new List<PersonControl>();
            lines = new List<LineControl>();
        }

        public LineControl CreateLineControl(PersonControl _from, PersonControl _to)
        {
            LineControl lc = new LineControl(_from, _to);
            Children.Add(lc);
            lines.Add(lc);
            return lc;
        }

        public PersonControl CreatePersonControl(Person person)
        {
            PersonControl control = new PersonControl(this, person);
            persons.Add(control);
            Children.Add(control);
            return control;
        }

        public void UpdateLines()
        {
            foreach(LineControl lc in lines)
                lc.Update();
        }

        public void UpdateVP(PersonControl control)
        {
            if (control.Position.X > vp.ActualWidth + vp.HorizontalOffset - control.Width)
                vp.LineRight();
            if (control.Position.X < vp.HorizontalOffset)
                vp.LineLeft();
            if (control.Position.Y > vp.ActualHeight + vp.VerticalOffset - control.Height)
                vp.LineDown();
            if (control.Position.Y < vp.VerticalOffset)
                vp.LineUp();
        }

        public void InitializeGraph(PersonsGraph _graph)
        {
            graph = _graph;
        }

        // На случай когда control требует перерасчет болезней
        public void RecalculateGraph()
        {
            if (graph == null)
                throw new InvalidOperationException("Контролы не знают о существовании графа");

            graph.Recalculate();

            //RTVB.Text = "";
            foreach (var vert in graph.graph.Vertices)
            {
                //RTVB.Text += " " + vert.person;
            }
        }

        public void AddVertexGraph(PersonControl parent, PersonControl child)
        {
            if (graph == null)
                throw new InvalidOperationException("Контролы не знают о существовании графа");

            graph.AddVertex(parent, child);
        }
    }
}
