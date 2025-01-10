using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;
using System.Windows.Input;

namespace DiseaseCalculator.Classes
{
    class PersonControl : Canvas
    {
        Button diseases;
        Button parents;
        //TextBox textbox;
        Label textbox;
        Label labelDisease, labelLuck;
        public readonly Person person;
        Diagram diagram;
        protected Point pos_old, mouse_old;
        private bool isMoving;
        protected Point position;
        public Point Position { get => position; set => SetPosition(value); }

        public PersonControl(Diagram _diagram, Person _person)
        {
            diagram = _diagram;
            //diagram.Children.Add(this);
            person = _person;

            Width = 160;
            Height = 80;
            Background = Brushes.LightGray;

            diseases = new Button();
            diseases.Content = "+";
            diseases.Width = 25;
            diseases.Height = 25;
            diseases.Margin = new Thickness(150, 12.5, 0, 0);
            Children.Add(diseases);

            parents = new Button();
            parents.Content = "+";
            parents.Width = 25;
            parents.Height = 25;
            parents.Margin = new Thickness(75 - 12.5, -25, 0, 0);
            Children.Add(parents);

            //textbox = new TextBox();
            textbox = new Label();
            textbox.Width = 150;
            textbox.Height = 25;
            textbox.Margin = new Thickness(0, 12.5, 0, 0);
            //textbox.Text = person.name;
            textbox.Content = person.name;
            textbox.BorderBrush = Brushes.Transparent;
            Children.Add(textbox);

            labelDisease = new Label();
            labelDisease.Content = "Гемофилия";
            labelDisease.Width = 110;
            labelDisease.Height = 25;
            labelDisease.Background = Brushes.LightGray;
            labelDisease.Margin = new Thickness(0, 50, 0, 0);
            labelDisease.BorderBrush = Brushes.Black;
            labelDisease.BorderThickness = new Thickness(0, 1, 0, 0);
            Children.Add(labelDisease);

            labelLuck = new Label();
            labelLuck.Content = "100%";
            labelLuck.Width = 50;
            labelLuck.Height = 30;
            labelLuck.Background = Brushes.LightGreen;
            labelLuck.Margin = new Thickness(110, 50, 0, 0);
            labelLuck.BorderBrush = Brushes.Black;
            labelLuck.BorderThickness = new Thickness(0, 1, 0, 0);
            Children.Add(labelLuck);

            // cut me
            SetTop(this, 25);

            MouseDown += Element_MouseDown;
            MouseMove += Element_MouseMove;
            MouseUp += Element_MouseUp;

            isMoving = false;
            position = new Point(0, 0);
            SetLeft(this, 0);
            SetTop(this, 0);

            // наверное тут
            diagram.Children.Add(this);
        }
        public void Element_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                mouse_old = e.GetPosition(diagram);
                pos_old = Position;
                CaptureMouse();
                isMoving = true;
                return;
            }
        }

        public void Element_MouseMove(object sender, MouseEventArgs e)
        {
            if (!IsMouseCaptured)
                return;

            if (isMoving)
            {
                Vector offset = e.GetPosition(diagram) - mouse_old;
                Point pos = pos_old + offset;

                if (pos.X > diagram.vp.ActualWidth + diagram.vp.HorizontalOffset - this.Width)
                    diagram.vp.LineRight();
                if (pos.X < diagram.vp.HorizontalOffset)
                    diagram.vp.LineLeft();
                if (pos.Y > diagram.vp.ActualHeight + diagram.vp.VerticalOffset - this.Height)
                    diagram.vp.LineDown();
                if (pos.Y < diagram.vp.VerticalOffset)
                    diagram.vp.LineUp();

                SetPosition(pos);

                return;
            }
        }

        public void Element_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (isMoving)
                isMoving = false;

            ReleaseMouseCapture();
        }

        public void SetPosition(Point P)
        {
            P.X = Math.Clamp(P.X, 0, diagram.ActualWidth - this.Width);
            P.Y = Math.Clamp(P.Y, 0, diagram.ActualHeight - this.Height);
            position = P;
            SetLeft(this, P.X);
            SetTop(this, P.Y);
        }

    }
}
