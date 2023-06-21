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
using System.Threading;

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
            /*data.Add("Lubelskie", new Dictionary<string, List<double>>() {
                {"Wyniki Matur" , new List<double> { 66.0, 68.2, 74.5 } },
                {"Ilość dotacji" , new List<double> { 1.72, 2.84, 3.55 } },
                {"Różnica w zależności od dotacji" , new List<double> { 3.2, -2.1, 1.0 } },
                {"2014-2020" , new List<double> { 1.2,2.2,3.2,4.2,5.2,6.2 } }
            });
            data.Add("Mazowieckie", new Dictionary<string, List<double>>() {
                {"Wyniki Matur" , new List<double> { 44.8, 52.7, 66.4 } },
                {"Ilość dotacji" , new List<double> { 5.73, 6.29, 10.14 } },
                {"Różnica w zależności od dotacji" , new List<double> { -1.4, -2.1, 0.9 } },
                {"2014-2020" , new List<double> { 1.5,2.5,3.5,4.5,5.5,6.5 } }
            });
            data.Add("Pomorskie", new Dictionary<string, List<double>>() {
                {"Wyniki Matur" , new List<double> { 72.1, 68.8, 69.4 } },
                {"Ilość dotacji" , new List<double> { 0.13, 1.04, 1.89 } },
                {"Różnica w zależności od dotacji" , new List<double> { -1.4, -2.1, -1.8 } },
                {"2014-2020" , new List<double> { 1,2,3,4,5,6 } }
            });*/
            InitializeComponent();
            createDataArray();
            createCheckboxes(data);
            initializer = new InitializeChart();
            initializer.initializeChart();
            SeriesCollection = initializer.SeriesCollection;
            Labels = initializer.Labels;
            //yAxis.Labels = new[] { "2014", "2015", "2016", "2017", "2018", "2019", "2020" };
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

        private void createDataArray()
        {
            string[] wojs = {
                "dolnoslaskie",
                "kujawsko-pomorskie"
                /*"lubelskie",
                "lubuskie",
                "lodzkie",
                "malopolskie",
                "mazowieckie",
                "opolskie",
                "podkarpackie",
                "podlaskie",
                "pomorskie",
                "slaskie",
                "swietokrzyskie",
                "warminsko-mazurskie",
                "wielkopolskie",
                "zachodnio-pomorskie"*/
            };

            foreach(string woj in wojs)
            {
                data.Add(woj, new Dictionary<string, List<double>>());
                data[woj].Add("Wyniki Matur - Matematyka", new List<double>());
                data[woj].Add("Wyniki Matur - Biologia", new List<double>());
                data[woj].Add("Wyniki Matur - Chemia", new List<double>());
                data[woj].Add("Wielkosc Dotacji", new List<double>());
                RetrieveData($"/wojewodztwoWyniki/zlozony/{woj}/P/matematyka", MaturaResultsData, "Matematyka");
                RetrieveData($"/wojewodztwoWyniki/zlozony/{woj}/R/biologia", MaturaResultsData, "Biologia");
                RetrieveData($"/wojewodztwoWyniki/zlozony/{woj}/R/chemia", MaturaResultsData, "Chemia");
                RetrieveData($"/wojewodztwoWyniki/zlozony/{woj}/P/matematyka", DotacjeResultsData);
                Thread.Sleep(1000);
            }

            //RetrieveData("/wojewodztwoWyniki/zlozony/dolnoslaskie/P/matematyka", "dolnoslaskie", "wynikiWKolejnychLatach");

        }

        private async void RetrieveData(string address, Action<string> method)
        {

            string apiAddress = "http://127.0.0.1:8083" + address;
            RestDataRetriever dataRetriever = new RestDataRetriever();
            string responseData = await dataRetriever.GetDataFromAddress(apiAddress);

            if (responseData != null)
                method.Invoke(responseData);
        }

        private async void RetrieveData(string address, Action<string, string> method, string subject)
        {

            string apiAddress = "http://127.0.0.1:8083" + address;
            RestDataRetriever dataRetriever = new RestDataRetriever();
            string responseData = await dataRetriever.GetDataFromAddress(apiAddress);

            if (responseData != null)
                method.Invoke(responseData, subject);
        }

        private void MaturaResultsData(string responseData, string subject)
        {
            ResultsModel model = JsonConvert.DeserializeObject<ResultsModel>(responseData);

            string wojStr = model.Nazwa;
            data[wojStr]["Wyniki Matur - " + subject] = model.WynikiWKolejnychLatach;
            /*consoleOutput.Text = $"Values: {string.Join(", ", model.WynikiWKolejnychLatach.ToList())}";
            consoleOutput.UpdateLayout();*/
        }

        private void DotacjeResultsData(string responseData)
        {
            ResultsModel model = JsonConvert.DeserializeObject<ResultsModel>(responseData);
            string wojStr = model.Nazwa; data[wojStr]["Wielkosc Dotacji"] = model.DotacjeWKolejnychLatach;
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
                //Faking authentication, delete if connected to a server
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
