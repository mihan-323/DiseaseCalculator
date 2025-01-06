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
        public MainWindow()
        {
            InitializeComponent();
            Person vasya = new Person("vasya",true);
            Person vasya2 = new Person("vasya2", true);
            Person vasya3 = new Person("vasya3", true);
            Person vasya4 = new Person("vasya4", false);
            vasya4.diseases.Add(new PersonalDisease(Hemophilia.GetHemophiliaInstance(), true));
            Person vasya5 = new Person("vasya5", true);
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
            PersonControl vasyaa = new PersonControl(diagram, new Person("vasya", true));
            vasyaa.person.diseases.Add(new PersonalDisease(Hemophilia.GetHemophiliaInstance(), true));
        }
    }
}
