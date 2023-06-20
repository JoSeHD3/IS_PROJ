using LiveCharts;
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

namespace SchoolDataChartRepresentation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<string> labels = new List<string> { "data1", "data2", "data3" };
        List<double> data1 = new List<double> { 66.0, 68.2, 74.5 };
        List<double> data2 = new List<double> { 44.8, 52.7, 66.4};
        List<double> data3 = new List<double> { 72.1, 68.8, 69.4 };

        private Dictionary<string, List<double>> data;

        private InitializeChart initializer;
        private List<CheckboxHandler> checkboxHandlers;

        public SeriesCollection SeriesCollection { get; set; }
        public List<string> Labels { get; set; }
        public string Name { get; set; }
        public bool IsChecked { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            checkboxHandlers = new List<CheckboxHandler>();
            data = new Dictionary<string, List<double>>();
            data.Add("Lubelskie", new List<double> { 66.0, 68.2, 74.5 });
            data.Add("Mazowieckie", new List<double> { 44.8, 52.7, 66.4 });
            data.Add("Pomorskie", new List<double> { 72.1, 68.8, 69.4 });
            createCheckboxes(data);
            initializer = new InitializeChart(labels, data1, data2, data3);
            SeriesCollection = initializer.SeriesCollection;
            Labels = initializer.Labels;
            DataContext = this;
        }

        private void createCheckboxes(Dictionary<string, List<double>> data)
        {
            foreach(var province in data)
            {
                CheckboxHandler handler = new CheckboxHandler(province.Key);
                checkboxHandlers.Add(handler);
                CheckboxList.Items.Add(handler);
            }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        public void Checkbox_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            string name = checkBox.Content.ToString();
            initializer.AddNewChart(name, data[name]);
        }

        public void Checkbox_Unchecked(object sender, RoutedEventArgs e)
        {
            CheckBox checkbox = (CheckBox)sender;
            string name = checkbox.Content.ToString();
            initializer.RemoveChart(name);
        }


    }
}
