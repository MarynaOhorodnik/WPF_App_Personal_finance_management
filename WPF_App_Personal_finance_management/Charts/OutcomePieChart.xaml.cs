using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using System;
using System.Collections;
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

namespace WPF_App_Personal_finance_management.Charts
{
    /// <summary>
    /// Interaction logic for OutcomePieChart.xaml
    /// </summary>
    public partial class OutcomePieChart : UserControl
    {
        public SeriesCollection SeriesCollection { get; set; }
        public OutcomePieChart(ArrayList list_title, ArrayList list_var)
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
