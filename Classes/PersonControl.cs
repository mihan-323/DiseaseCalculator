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

        void SetupLabel(Label label, double w, double h, 
            double l = 0, double t = 0, double r = 0, double b = 0, int bl = 0, int bt = 0, int br = 0, int bb = 0)
        {
            label.Width = w;
            label.Height = h;
            label.Margin = new Thickness(l, t, r, b);
            label.Content = "test";
            label.BorderBrush = Brushes.Transparent;

            if(bl > 0 || bt > 0 || br > 0 || bb > 0)
            {
                label.BorderBrush = Brushes.Black;
                label.BorderThickness = new Thickness(bl, bt, br, bb);
            }

            Children.Add(label);
        }
        
        void SetupButton(Button btn, double w, double h, string s,
            double l = 0, double t = 0, double r = 0, double b = 0)
        {
            btn.Content = s;
            btn.Width = w;
            btn.Height = h;
            btn.Margin = new Thickness(l, t, r, b);
            Children.Add(btn);
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
            //           добавление родителя
            labelAddParent = new Label();
            SetupLabel(labelAddParent, 160, 25, 0, -85);
            labelAddParent.Content = "Добавить родителя:";
            labelAddParent.Visibility = Visibility.Collapsed;

            txtParentName = new TextBox();
            txtParentName.Width = 100;
            txtParentName.Height = 25;
            txtParentName.Margin = new Thickness(0, -35, 0, 0);
            txtParentName.Visibility = Visibility.Collapsed;
            Children.Add(txtParentName);

            btnAddParentGender = new Button();
            SetupButton(btnAddParentGender, 25, 25, "M", 105, -35, 0, 0);
            btnAddParentGender.Visibility = Visibility.Collapsed;
            btnAddParentGender.Click += BtnAddParentGender_Click;

            btnAddParentOk = new Button();
            SetupButton(btnAddParentOk, 25, 25 ,"Oк", 135, -35, 0, 0);
            btnAddParentOk.Visibility = Visibility.Collapsed;
            btnAddParentOk.Click += BtnAddParentOk_Click;

            btnAddParent = new Button();
            SetupButton(btnAddParent, 60, 15, "+", 50, 0);
            btnAddParent.Click += BtnAddParent_Click;

            //----------------------------------------
            //            добавление болезни
            btnAddDiseaseHemophilia = new Button();
            SetupButton(btnAddDiseaseHemophilia, 100, 25, "Гемофилия", 170);
            btnAddDiseaseHemophilia.Visibility = Visibility.Collapsed;
            btnAddDiseaseHemophilia.Click += BtnAddDiseaseHemophilia_Click;

            btnAddDisease = new Button();
            SetupButton(btnAddDisease, 25, 25, "+", 135, 12.5);
            btnAddDisease.Click += BtnAddDisease_Click;

            //----------------------------------------
            labelName = new Label();
            SetupLabel(labelName, 110, 25, 30, 15);
            
            labelGender = new Label();
            SetupLabel(labelGender, 30, 25, 0, 15);

            labelDisease = new Label();
            SetupLabel(labelDisease, 110, 25, 0, 50, 0, 0, 0, 1);

            labelProb = new Label();
            SetupLabel(labelProb, 50, 30, 110, 50, 0, 0, 0, 1);

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
