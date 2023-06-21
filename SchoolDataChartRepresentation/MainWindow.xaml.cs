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
using Newtonsoft.Json;
using SchoolDataChartRepresentation;

namespace SchoolDataChartRepresentation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private Dictionary<string, Dictionary<string, List<double>>> data;

        private InitializeChart initializer;
        private List<CheckboxHandler> checkboxHandlers;
        private string role = "Unlogged";

        public SeriesCollection SeriesCollection { get; set; }
        public List<string> Labels { get; set; }
        public string Name { get; set; }

        //public bool IsChecked { get; set; }

        private string comboBoxSelectedItem = "Wyniki Matur";

        public MainWindow()
        {

            checkboxHandlers = new List<CheckboxHandler>();
            data = new Dictionary<string, Dictionary<string, List<double>>>();
            data.Add("Lubelskie", new Dictionary<string, List<double>>() {
                {"Wyniki Matur" , new List<double> { 66.0, 68.2, 74.5 } },
                {"Ilość dotacji" , new List<double> { 1.72, 2.84, 3.55 } },
                {"Różnica w zależności od dotacji" , new List<double> { 3.2, -2.1, 1.0 } }
            });
            data.Add("Mazowieckie", new Dictionary<string, List<double>>() {
                {"Wyniki Matur" , new List<double> { 44.8, 52.7, 66.4 } },
                {"Ilość dotacji" , new List<double> { 5.73, 6.29, 10.14 } },
                {"Różnica w zależności od dotacji" , new List<double> { -1.4, -2.1, 0.9 } }
            });
            data.Add("Pomorskie", new Dictionary<string, List<double>>() {
                {"Wyniki Matur" , new List<double> { 72.1, 68.8, 69.4 } },
                {"Ilość dotacji" , new List<double> { 0.13, 1.04, 1.89 } },
                {"Różnica w zależności od dotacji" , new List<double> { -1.4, -2.1, -1.8 } }
            });
            InitializeComponent();
            createCheckboxes(data);
            initializer = new InitializeChart();
            SeriesCollection = initializer.SeriesCollection;
            Labels = initializer.Labels;
            DataContext = this;
        }

        private void handleInitializer(Dictionary<string, Dictionary<string, List<double>>> data)
        {

            int i = 0;
            foreach (var province in data)
            {
                foreach (var item in province.Value)
                {
                    if (checkboxHandlers[i].IsChecked)
                    {
                        string name = checkboxHandlers[i].Checkbox.Content.ToString();
                        initializer.RemoveChart(name);
                        initializer.AddNewChart(name, data[name][comboBoxSelectedItem]);
                    }
                }
                i++;
            }
        }

        private void createCheckboxes(Dictionary<string, Dictionary<string, List<double>>> data)
        {
            foreach(var province in data)
            {
                CheckboxHandler handler = new CheckboxHandler(province.Key);
                checkboxHandlers.Add(handler);
                CheckboxList.Items.Add(handler);
            }
        }

        private async void RetrieveData(string address)
        {
            string apiAddress = "https://127.0.0.1:8083" + address;

            RestDataRetriever dataRetriever = new RestDataRetriever();
            string responseData = await dataRetriever.GetDataFromAddress(apiAddress);

            List<Dictionary<string, object>> rows = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(responseData);

            foreach (Dictionary<string, object> row in rows)
            {
                string przedmiot = row["przedmiot"].ToString();
                string poziom = row["poziom"].ToString();
                string zdajacych = row["zdajacych"].ToString();
                string minS = row["minS"].ToString();
                string maxS = row["maxS"].ToString();
                string mediana = row["mediana"].ToString();
                string srednia = row["srednia"].ToString();
                string odchylenie = row["odchylenie"].ToString();
                string odsetek = row["odsetek"].ToString();
                string wojewodztwo = row["wojewodztwo"].ToString();
                int rok = Convert.ToInt32(row["rok"]);

                
            }
        }

        private Dictionary<string, object> ParseJsonData(string jsonData)
        {
            try
            {
                Dictionary<string, object> dataDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonData);
                return dataDictionary;
            } catch (JsonException ex)
            {
                Console.WriteLine(ex.Message);
            }

            return null;
        }
        private bool isInitialized = false;
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            string selectedItem = (string)comboBox.SelectedItem;

            ComboBoxManager comboBoxManager = (ComboBoxManager)Resources["ComboBoxManagerInstance"];
            //comboBoxManager.HandleComboBoxSelection(selectedItem);

            comboBoxSelectedItem = selectedItem;

            if (isInitialized)
            {
                handleInitializer(data);
            }
            else
            {
                isInitialized = true;
            }
        }

        public void Checkbox_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            string name = checkBox.Content.ToString();
            checkBox.IsChecked = true;
            initializer.AddNewChart(name, data[name][comboBoxSelectedItem]);
        }

        public void Checkbox_Unchecked(object sender, RoutedEventArgs e)
        {
            CheckBox checkbox = (CheckBox)sender;
            string name = checkbox.Content.ToString();
            checkbox.IsChecked = false;
            initializer.RemoveChart(name);
        }

        private async void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text;
            string password = PasswordBox.Password;

            AuthService authService = new AuthService();

            try
            {
                //string role = await authService.AuthenticateAndGetRole(username, password);
                //Faking authentication, delete if connected to server
                string role = authService.FakeAuthenticateAndGetRole(username, password);
                switch (role)
                {
                    case "Mod":
                        cb_Category.Visibility = Visibility.Hidden;
                        l_Category.Visibility = Visibility.Hidden;
                        CheckboxList.Visibility = Visibility.Visible;
                        break;
                    case "Admin":
                        CheckboxList.Visibility = Visibility.Visible;
                        cb_Category.Visibility = Visibility.Visible;
                        l_Category.Visibility = Visibility.Visible;
                        break;
                    default:
                        cb_Category.Visibility = Visibility.Hidden;
                        l_Category.Visibility = Visibility.Hidden;
                        CheckboxList.Visibility = Visibility.Hidden;
                        break;
                }
            } catch (Exception ex) {
                Console.WriteLine(ex.Message);
            }
        }


    }
}
