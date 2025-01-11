using DiseaseCalculator.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DiseaseCalculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Diagram diagram;

        public MainWindow()
        {
            InitializeComponent();

            // create persons
            Person vasya = new Person("Вася", true);
            Person vasya2 = new Person("Вася 2", true);
            Person vasya3 = new Person("Вася 3", true);
            Person vasya4 = new Person("Женщина", false);
            Person vasya5 = new Person("Вася 5", true);

            // add diseases
            //vasya4.diseases.Add(new PersonalDisease(Hemophilia.GetHemophiliaInstance(), true));

            // create diagram
            diagram = new Diagram(1920, 1080, scroll);
            diagramContainer.Children.Add(diagram);

            // create controls
            PersonControl cvasya = diagram.CreatePersonControl(vasya);
            cvasya.SetPosition(new Point(500, 400));
            PersonControl cvasya2 = diagram.CreatePersonControl(vasya2);
            cvasya2.SetPosition(new Point(250, 550));
            PersonControl cvasya3 = diagram.CreatePersonControl(vasya3);
            cvasya3.SetPosition(new Point(750, 550));
            PersonControl cvasya4 = diagram.CreatePersonControl(vasya4);
            cvasya4.SetPosition(new Point(500, 550));
            PersonControl cvasya5 = diagram.CreatePersonControl(vasya5);
            cvasya5.SetPosition(new Point(600, 700));

            // create graph
            PersonsGraph pg = new PersonsGraph(cvasya);
            pg.AddVertex(cvasya, cvasya2);
            pg.AddVertex(cvasya, cvasya3);
            pg.AddVertex(cvasya, cvasya4);
            pg.AddVertex(cvasya4, cvasya5);
            diagram.InitializeGraph(pg);
            diagram.RecalculateGraph();

            // debug
            Person[] pes = new Person[5];
            pes[0]=vasya;
            pes[1]=vasya2;
            pes[2]=vasya3;
            pes[3]=vasya4;
            pes[4]=vasya5;

            foreach (var item in pes)
            {
                RTVB.Text += " " + item;
            }
        }
    }
}
