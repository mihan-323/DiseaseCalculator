using DiseaseCalculator.Classes;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
        PersonsGraph graph;

        public MainWindow()
        {
            InitializeComponent();
            graph = new PersonsGraph();
            diagram = new Diagram(diagramContainer, scroll, graph, footer, 2000, 2000);
        }

        private void GraphCreate(object sender, RoutedEventArgs e)
        {
            // create persons
            Person alexey_1 = new Person("Алексей", true);
            Person alexandra_2 = new Person("Александра", false);
            Person nikolayII_3 = new Person("Николай II", true);
            Person ludvig_4 = new Person("Людвиг", true);
            Person alisa_5 = new Person("Алиса", false);
            Person albert_6 = new Person("Альберт", true);
            Person victoria_7 = new Person("Виктория", false);

            // create controls
            PersonControl c_alexe = diagram.CreatePersonControl(alexey_1, 300, 50);
            PersonControl c_alexa = diagram.CreatePersonControl(alexandra_2, 150, 250);
            PersonControl c_nikol = diagram.CreatePersonControl(nikolayII_3, 400, 250);
            PersonControl c_ludvi = diagram.CreatePersonControl(ludvig_4, 150, 450);
            PersonControl c_alisa = diagram.CreatePersonControl(alisa_5, 400, 450);
            PersonControl c_alber = diagram.CreatePersonControl(albert_6, 150, 650);
            PersonControl c_victo = diagram.CreatePersonControl(victoria_7, 400, 650);

            graph.SetTarget(c_alexa);

            graph.AddVertex(c_alexa, c_alexe);
            graph.AddVertex(c_nikol, c_alexe);
            graph.AddVertex(c_ludvi, c_alexa);
            graph.AddVertex(c_alisa, c_alexa);
            graph.AddVertex(c_alber, c_alisa);
            graph.AddVertex(c_victo, c_alisa);

            diagram.Recalculate();

            UpdateMenu();
        }

        // пока не работет
        private void GraphOpen(object sender, RoutedEventArgs e)
        {
            FileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Двоичный файл (*.bin)|*.bin";

            if (dialog.ShowDialog() == true)
            {
            }

            UpdateMenu();
        }

        // пока не работет
        private void GraphSaveToJSON(object sender, RoutedEventArgs e)
        {
            FileDialog dialog = new SaveFileDialog();
            dialog.Filter = "JSON файл (*.json)|*.json";

            if (dialog.ShowDialog() == true)
            {
            }
        }

        private void GraphExportToJPG(object sender, RoutedEventArgs e)
        {
            FileDialog dialog = new SaveFileDialog();
            dialog.Filter = "Изображение (*.jpg)|*.jpg";

            if (dialog.ShowDialog() == true)
            {
                RenderTargetBitmap target = new RenderTargetBitmap((int)diagram.Width, (int)diagram.Height, 96, 96, PixelFormats.Default);
                target.Render(diagramContainer);

                BitmapEncoder encoder = new JpegBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(target));

                FileStream stream = new FileStream(dialog.FileName, FileMode.Create);
                encoder.Save(stream);
                stream.Close();
            }
        }

        private void GraphClose(object sender, RoutedEventArgs e)
        {
            diagram.Close();
            UpdateMenu();
        }

        void UpdateMenu()
        {
            if (graph.isOpen)
            {
                graphOpen.IsEnabled = false;
                graphCreate.IsEnabled = false;
                graphSave.IsEnabled = true;
                graphExport.IsEnabled = true;
                graphClose.IsEnabled = true;
            }
            else
            {
                graphOpen.IsEnabled = true;
                graphCreate.IsEnabled = true;
                graphSave.IsEnabled = false;
                graphExport.IsEnabled = false;
                graphClose.IsEnabled = false;
            }
        }
    }
}
