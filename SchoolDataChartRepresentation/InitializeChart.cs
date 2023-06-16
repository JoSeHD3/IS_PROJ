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
        private List<double> data1;
        private List<double> data2;
        private List<double> data3;

        public InitializeChart(List<string> labels, List<double> data1, List<double> data2, List<double> data3)
        { 
            this.Labels = labels;
            this.Data1 = data1;
            this.Data2 = data2;
            this.Data3 = data3;

            SeriesCollection = new SeriesCollection();

            initializeChart();

        }

        public List<string> Labels { get => labels; set => labels = value; }
        public List<double> Data1 { get => data1; set => data1 = value; }
        public List<double> Data2 { get => data2; set => data2 = value; }
        public List<double> Data3 { get => data3; set => data3 = value; }

        private void initializeChart()
        {
            SeriesCollection.Clear();
            Labels.Clear();

            SeriesCollection.Add(new LineSeries
            {
                Title = "Data1",
                Values = new ChartValues<double>(data1)
            });
            SeriesCollection.Add(new LineSeries
            {
                Title = "Data2",
                Values = new ChartValues<double>(data2)
            });
            SeriesCollection.Add(new LineSeries
            {
                Title = "Data3",
                Values = new ChartValues<double>(data3)
            });
        }


    }
}
