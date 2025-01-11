using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;
using System.Windows.Input;
using System.Windows.Shapes;

namespace DiseaseCalculator.Classes
{
    class PersonControl : Canvas
    {
        Button btnAddDisease;
        Button btnAddParent;
        //TextBox textbox;
        Label labelName, labelGender;
        Label labelDisease, labelProb;
        public readonly Person person;
        Diagram diagram;
        protected Point pos_old, mouse_old;
        private bool isMoving;
        protected Point position;
        public Point Position { get => position; set => SetPosition(value); }
        public TriangleFan shape;
        Button btnAddDiseaseHemophilia;
        TextBox txtParentName;
        Button btnAddParentGender;
        Button btnAddParentOk;
        Label labelAddParent;

        Label SetupLabel(double w, double h, String s,
            double l = 0, double t = 0, double r = 0, double b = 0, 
            int bl = 0, int bt = 0, int br = 0, int bb = 0, 
            bool vis = true)
        {
            Label label = new Label();
            label.Width = w;
            label.Height = h;
            label.Margin = new Thickness(l, t, r, b);
            label.Content = s;
            label.BorderBrush = Brushes.Transparent;

            if(bl > 0 || bt > 0 || br > 0 || bb > 0)
            {
                label.BorderBrush = Brushes.Black;
                label.BorderThickness = new Thickness(bl, bt, br, bb);
            }

            if(vis)
                label.Visibility = Visibility.Visible;
            else
                label.Visibility = Visibility.Collapsed;

            Children.Add(label);

            return label;
        }

        Button SetupButton(double w, double h, String s,
            double l = 0, double t = 0, double r = 0, double b = 0, 
            bool vis = true, 
            RoutedEventHandler? clk = null)
        {
            Button btn = new Button();
            btn.Content = s;
            btn.Width = w;
            btn.Height = h;
            btn.Margin = new Thickness(l, t, r, b);

            if (vis)
                btn.Visibility = Visibility.Visible;
            else
                btn.Visibility = Visibility.Collapsed;

            if (clk != null)
                btn.Click += clk;

            Children.Add(btn);

            return btn;
        }

        TextBox SetupTextBox(double w, double h, String s,
            double l = 0, double t = 0, double r = 0, double b = 0, 
            bool vis = true, 
            TextChangedEventHandler? upd = null)
        {
            TextBox tb = new TextBox();
            tb.Text = s;
            tb.Width = w;
            tb.Height = h;
            tb.Margin = new Thickness(l, t, r, b);

            if (vis)
                tb.Visibility = Visibility.Visible;
            else
                tb.Visibility = Visibility.Collapsed;

            if (upd != null)
                tb.TextChanged += upd;

            Children.Add(tb);

            return tb;
        }

        public PersonControl(Diagram _diagram, Person _person)
        {
            diagram = _diagram;
            //diagram.Children.Add(this);
            person = _person;

            Width = 160;
            Height = 80;
            Background = Brushes.LightGray;

            //----------------------------------------
            // добавление родителя
            labelAddParent = SetupLabel(160, 25, "Добавить родителя:", 0, -85, 0, 0, 0, 0, 0, 0, false);
            txtParentName = SetupTextBox(100, 25, ":name:", 0, -35, 0, 0, false, null);
            btnAddParentGender = SetupButton(25, 25, "M", 105, -35, 0, 0, false, BtnAddParentGender_Click);
            btnAddParentOk = SetupButton(25, 25, "Oк", 135, -35, 0, 0, false, BtnAddParentOk_Click);
            btnAddParent = SetupButton(60, 15, "+", 50, 0, 0, 0, true, BtnAddParent_Click);

            //----------------------------------------
            // добавление болезни
            btnAddDiseaseHemophilia = SetupButton(100, 25, "Гемофилия", 170, 0, 0, 0, false, BtnAddDiseaseHemophilia_Click);
            btnAddDisease = SetupButton(25, 25, "+", 135, 12.5, 0, 0, true, BtnAddDisease_Click);

            //----------------------------------------
            labelName = SetupLabel(110, 25, ":name:", 30, 15);
            labelGender = SetupLabel(30, 25, ":M/F:", 0, 15);
            labelDisease = SetupLabel(110, 25, ":disease:", 0, 50, 0, 0, 0, 1);
            labelProb = SetupLabel(50, 30, ":prob:", 110, 50, 0, 0, 0, 1);

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
            //diagram.Children.Add(this);

            PointCollection quad = new PointCollection();
            quad.Add(new Point(0, 0));
            quad.Add(new Point(160, 0));
            quad.Add(new Point(160, 80));
            quad.Add(new Point(0, 80));

            shape = TriangleFan.CreateTriangleFan(quad, 2, 2);

            UpdateLabelsText();
        }

        private void BtnAddParentGender_Click(object sender, RoutedEventArgs e)
        {
            if ((String)btnAddParentGender.Content == "M")
                btnAddParentGender.Content = "F";
            else
                btnAddParentGender.Content = "M";
        }

        private void BtnAddParentOk_Click(object sender, RoutedEventArgs e)
        {
            bool gender = (String)btnAddParentGender.Content == "M"; // true == M
            String name = txtParentName.Text;
            Point position = new Point(Position.X, Position.Y + 250);
            Person person = new Person(name, gender);
            PersonControl control = diagram.CreatePersonControl(person);
            control.Position = position;
            diagram.AddVertexGraph(control, this);
        }

        private void BtnAddParent_Click(object sender, RoutedEventArgs e)
        {
            if(labelAddParent.Visibility == Visibility.Visible)
            {
                labelAddParent.Visibility = Visibility.Collapsed;
                txtParentName.Visibility = Visibility.Collapsed;
                btnAddParentGender.Visibility = Visibility.Collapsed;
                btnAddParentOk.Visibility = Visibility.Collapsed;
            }
            else
            {
                labelAddParent.Visibility = Visibility.Visible;
                txtParentName.Visibility = Visibility.Visible;
                btnAddParentGender.Visibility = Visibility.Visible;
                btnAddParentOk.Visibility = Visibility.Visible;
            }
        }

        private void BtnAddDiseaseHemophilia_Click(object sender, RoutedEventArgs e)
        {
            if (person.SearchHemophilia())
                person.RemoveHemophilia();
            else
                person.AddHemophilia();

            diagram.RecalculateGraph();
            UpdateLabelsText();
        }

        private void BtnAddDisease_Click(object sender, RoutedEventArgs e)
        {
            if(btnAddDiseaseHemophilia.Visibility == Visibility.Visible)
                btnAddDiseaseHemophilia.Visibility = Visibility.Collapsed;
            else
                btnAddDiseaseHemophilia.Visibility = Visibility.Visible;
        }

        void UpdateLabelsText()
        {
            labelName.Content = person.name;
            labelGender.Content = person.gender ? "M" : "F";

            if (person.diseases.Count > 0)
            {
                labelDisease.Content = "Гемофилия";
                labelProb.Content = person.diseases[0].calculated_probability;
            }
            else
            {
                labelDisease.Content = "-";
                labelProb.Content = "-";
            }
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

                diagram.UpdateVP(this);

                SetPosition(pos);

                diagram.UpdateLines();

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

        public Point Center()
        {
            Point pos = Position;
            pos.X += this.Width * 0.5;
            pos.Y += this.Height * 0.5;
            return pos;
        }

        public void AddLine(PersonControl _to)
        {
            diagram.CreateLineControl(this, _to);
        }

        public void CalculateDisease()
        {
            person.Calculate();
            UpdateLabelsText();
        }
    }
}
