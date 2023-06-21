using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SchoolDataChartRepresentation
{
    internal class InitializeChart : UserControl
    {
        public SeriesCollection SeriesCollection { get; set; }
        private List<string> labels;

        public InitializeChart()
        { 

            SeriesCollection = new SeriesCollection();

            //initializeChart();

        }

        public List<string> Labels { get => labels; set => labels = value; }

        public void initializeChart()
        {
            SeriesCollection.Clear();
            //Labels.Clear();

        }
        public void AddNewChart(string name, List<double> data)
        {
            SeriesCollection.Add(new LineSeries
            {
                Title = name,
                Values = new ChartValues<double>(data)
            });
        }

        public void RemoveChart(string name)
        {
            var seriesToRemove = SeriesCollection.FirstOrDefault(s => s.Title == name);

            if(seriesToRemove != null) SeriesCollection.Remove(seriesToRemove);
        }
    }
}
