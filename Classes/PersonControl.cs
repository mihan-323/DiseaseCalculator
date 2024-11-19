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
    class PersonControl : Canvas
    {
        Button diseases;
        Button parents;
        TextBox textbox;
        Label labelDisease, labelLuck;
        public readonly Person person;

        public PersonControl(Canvas diagram, Person _person)
        {
            diagram.Children.Add(this);
            person = _person;

            Width = 150;
            Height = 50;
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

            textbox = new TextBox();
            textbox.Width = 150;
            textbox.Height = 25;
            textbox.Margin = new Thickness(0, 12.5, 0, 0);
            textbox.Text = person.name;
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
            labelLuck.Width = 40;
            labelLuck.Height = 25;
            labelLuck.Background = Brushes.LightGreen;
            labelLuck.Margin = new Thickness(110, 50, 0, 0);
            labelLuck.BorderBrush = Brushes.Black;
            labelLuck.BorderThickness = new Thickness(0, 1, 0, 0);
            Children.Add(labelLuck);

            // cut me
            SetTop(this, 25);
        }


    }
}
