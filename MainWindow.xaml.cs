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
            Person alexey_1 = new Person("Алексей", true);
            Person alexandra_2 = new Person("Александра", false);
            Person nikolayII_3 = new Person("Николай II", true);
            Person ludvig_4 = new Person("Людвиг", true);
            Person alisa_5 = new Person("Алиса", false);
            Person albert_6 = new Person("Альберт", true);
            Person victoria_7 = new Person("Виктория", false);

            // add diseases
            //victoria_7.diseases.Add(new PersonalDisease(Hemophilia.GetHemophiliaInstance(), false));

            // create diagram
            diagram = new Diagram(1920, 1080, scroll);
            diagramContainer.Children.Add(diagram);

            // create controls
            PersonControl c_alexe = diagram.CreatePersonControl(alexey_1);
            c_alexe.SetPosition(new Point(300, 50));
            PersonControl c_alexa = diagram.CreatePersonControl(alexandra_2);
            c_alexa.SetPosition(new Point(150, 250));
            PersonControl c_nikol = diagram.CreatePersonControl(nikolayII_3);
            c_nikol.SetPosition(new Point(400, 250));
            PersonControl c_ludvi = diagram.CreatePersonControl(ludvig_4);
            c_ludvi.SetPosition(new Point(150, 450));
            PersonControl c_alisa = diagram.CreatePersonControl(alisa_5);
            c_alisa.SetPosition(new Point(400, 450));
            PersonControl c_alber = diagram.CreatePersonControl(albert_6);
            c_alber.SetPosition(new Point(150, 650));
            PersonControl c_victo = diagram.CreatePersonControl(victoria_7);
            c_victo.SetPosition(new Point(400, 650));

            // create graph
            PersonsGraph pg = new PersonsGraph(c_alexe);
            pg.AddVertex(c_alexa, c_alexe);
            pg.AddVertex(c_nikol, c_alexe);
            pg.AddVertex(c_ludvi, c_alexa);
            pg.AddVertex(c_alisa, c_alexa);
            pg.AddVertex(c_alber, c_alisa);
            pg.AddVertex(c_victo, c_alisa);
            diagram.InitializeGraph(pg);
            diagram.RecalculateGraph();
        }
    }
}
