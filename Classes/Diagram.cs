using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Input;

namespace DiseaseCalculator.Classes
{
    class Diagram : Canvas
    {
        ScrollViewer vp;
        List<PersonControl> persons;
        List<LineControl> lines;
        Grid container;
        PersonsGraph graph;
        TextBlock footer;
        PersonControl? last; // focus
        bool showArrows = false;

        public Diagram(Grid _container, ScrollViewer _vp, PersonsGraph _graph, TextBlock _footer, double diagramWidth, double diagramHeight)
        {
            Background = (Brush)Application.Current.FindResource("diagramBackgroundCell");

            Width = diagramWidth;
            Height = diagramHeight;

            container = _container;
            vp = _vp;
            graph = _graph;
            footer = _footer;

            Rect viewport = new Rect(0, 0, Width, Height);
            Arrange(viewport);

            HorizontalAlignment = HorizontalAlignment.Left;
            VerticalAlignment = VerticalAlignment.Top;

            persons = new List<PersonControl>();
            lines = new List<LineControl>();

            last = null;

            MouseDown += Diagram_MouseDown;
            MouseMove += Diagram_MouseMove;

            container.Children.Add(this);

        }

        private void Diagram_MouseMove(object sender, MouseEventArgs e)
        {
        }

        private void Diagram_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.RightButton == MouseButtonState.Pressed)
            {
                if (last != null)
                    last.SetBorderStyle(false);

                last = null;
            }
        }

        public void SetFocus(PersonControl? next)
        {
            if (last != null)
                last.SetBorderStyle(false);

            if (next != null)
                next.SetBorderStyle(true);

            last = next;
        }

        public LineControl CreateLineControl(PersonControl _from, PersonControl _to)
        {
            LineControl lc = new LineControl(_from, _to);
            lc.Show(true, showArrows, showArrows);
            Children.Add(lc);
            lines.Add(lc);
            return lc;
        }

        public PersonControl CreatePersonControl(Person person, double x = 0, double y = 0)
        {
            PersonControl control = new PersonControl(this, person);
            control.Position = new Point(x, y);
            persons.Add(control);
            //Children.Add(control);
            return control;
        }

        public void UpdateLines()
        {
            foreach(LineControl lc in lines)
            {
                lc.Update();
                lc.Show(true, showArrows, showArrows);
            }
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

        // На случай когда control требует перерасчет болезней
        public void Recalculate()
        {
            graph.Recalculate();
            UpdateFooter();
        }

        public void AddVertex(PersonControl parent, PersonControl child)
        {
            graph.AddVertex(parent, child);
        }

        public void Close()
        {
            graph.Close();

            foreach (var person in persons)
            {
                Children.Remove(person);
            }

            persons.Clear();

            foreach (var line in lines)
            {
                Children.Remove(line);  
            }

            lines.Clear();

            footer.Text = "";
        }

        public void UpdateFooter()
        {
            footer.Text = "";

            foreach (var person in persons)
            {
                footer.Text += person.Person + "\n";
            }
        }

        public void ShowArrows(bool show)
        {
            showArrows = show;
            UpdateLines();
        }
    }
}
