using LiveCharts.Wpf;
using LiveCharts;
using System;
using System.Windows.Controls;
using System.Collections;

namespace WPF_App_Personal_finance_management.Charts
{
    /// <summary>
    /// Interaction logic for TotalLineChart.xaml
    /// </summary>
    public partial class TotalLineChart : UserControl
    {
        public TotalLineChart(ArrayList list_inc, ArrayList list_inc_tit, ArrayList list_outc, ArrayList list_outc_tit)
        {
            InitializeComponent();

            Labels = new[] { "Січ", "Лют", "Бер", "Квіт", "Трав", "Черв", "Лип", "Серп", "Вер", "Жовт", "Лист", "Груд" };

            SeriesCollection = new SeriesCollection();
            int j;
            
            if(list_inc.Count > 0 )
            {
                LineSeries x1 = new LineSeries();
                x1.Title = "Надходження";
                x1.Values = new ChartValues<double>();
                j = 0;
                for (int i = 1; i < Labels.Length; i++)
                {
                    int x = Convert.ToInt32(list_inc_tit[j]);
                    if (x == i)
                    {
                        x1.Values.Add(list_inc[j]);
                        if (j == list_inc_tit.Count - 1)
                        {
                            continue;
                        }
                        else
                        {
                            j++;
                        }
                    }
                    else
                    {
                        x1.Values.Add(double.NaN);
                    }
                }
                SeriesCollection.Add(x1);
            }

            if(list_outc.Count > 0 )
            {
                LineSeries x2 = new LineSeries();
                x2.Title = "Витрати";
                x2.Values = new ChartValues<double>();
                j = 0;
                for (int i = 1; i < Labels.Length; i++)
                {
                    int x = Convert.ToInt32(list_outc_tit[j]);
                    if (x == i)
                    {
                        x2.Values.Add(list_outc[j]);
                        if (j == list_outc_tit.Count - 1)
                        {
                            continue;
                        }
                        else
                        {
                            j++;
                        }
                    }
                    else
                    {
                        x2.Values.Add(double.NaN);
                    }

                }
                SeriesCollection.Add(x2);
            }

            DataContext = this;
        }

        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
    }
}
