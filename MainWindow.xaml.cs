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

            // create diagram
            diagram = new Diagram(1920, 1080, scroll);
            diagramContainer.Children.Add(diagram);
            
            // create persons
            Person vasya = new Person("Вася",true);
            Person vasya2 = new Person("Вася 2", true);
            Person vasya3 = new Person("Вася 3", true);
            Person vasya4 = new Person("Женщина", false);
            vasya4.diseases.Add(new PersonalDisease(Hemophilia.GetHemophiliaInstance(), true));
            Person vasya5 = new Person("Вася 5", true);

            // create graph
            PersonsGraph pg = new PersonsGraph(vasya);
            pg.AddVertex(vasya, vasya2);
            pg.AddVertex(vasya, vasya3);
            pg.AddVertex(vasya, vasya4);
            pg.AddVertex(vasya4, vasya5);
            pg.Recalculate();

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

            // test person control
            //PersonControl vasyaa = new PersonControl(diagram, new Person("Вася", true));
            //vasyaa.person.diseases.Add(new PersonalDisease(Hemophilia.GetHemophiliaInstance(), true));

            // create person controls
            diagram.CreateControlsByGraph(pg);

        }
    }
}
