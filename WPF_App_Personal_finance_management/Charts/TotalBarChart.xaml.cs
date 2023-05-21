using LiveCharts;
using LiveCharts.Wpf;
using System.Windows.Controls;
using System.Windows.Media;

namespace WPF_App_Personal_finance_management.Charts
{
    /// <summary>
    /// Interaction logic for TotalBarChart.xaml
    /// </summary>
    public partial class TotalBarChart : UserControl
    {
        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public TotalBarChart(double inc_val, double outc_val)
        {
            InitializeComponent();

            SeriesCollection = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "Надходження",
                    Values = new ChartValues<double> { inc_val },
                    Fill = Brushes.SeaGreen,
                    MaxColumnWidth = 70
                },

                new ColumnSeries
                {
                    Title = "Витрати",
                    Values = new ChartValues<double> { outc_val },
                    Fill = Brushes.Orange,
                    MaxColumnWidth = 70
                }

            };

            Labels = new[] { "" };

            DataContext = this;
        }
    }
}
