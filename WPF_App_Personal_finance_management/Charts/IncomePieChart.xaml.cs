using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using System;
using System.Collections;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WPF_App_Personal_finance_management.Charts
{
    /// <summary>
    /// Interaction logic for IncomePieChart.xaml
    /// </summary>
    public partial class IncomePieChart : UserControl
    {
        public SeriesCollection SeriesCollection { get; set; }
        public IncomePieChart(ArrayList list_title, ArrayList list_var)
        {
            InitializeComponent();

            SeriesCollection = new SeriesCollection();

            int v = 0;
            PieSeries p;
            for (int i = 0; i < list_title.Count; i++)
            {
                p = new PieSeries();
                p.Title = (string)list_title[v];
                double d = Convert.ToDouble(list_var[v]);
                p.Values = new ChartValues<ObservableValue> { new ObservableValue(d) };
                p.DataLabels = true;
                p.LabelPosition = PieLabelPosition.OutsideSlice;

                string str = list_title[v].ToString() + " ";
                p.LabelPoint = point => str + point.Y; ;
                p.Foreground = Brushes.Black;
                p.FontSize = 14;
                p.FontWeight = FontWeights.Regular;

                SeriesCollection.Add(p);
                v += 1;
            }

            DataContext = this;
        }
    }
}
