using System.Windows;
using WPF_App_Personal_finance_management.Classes;
using WPF_App_Personal_finance_management.User_ViewModels;

namespace WPF_App_Personal_finance_management
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            TextHello.Text = "Привіт, " + _CurrentUser.Name;
            DataContext = new MainViewModel();
        }

        private void MainView_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new MainViewModel();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new AddViewModel();
        }

        private void CtgButton_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new CtgViewModel();
        }

        private void ChartButton_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new ChartViewModel();
        }

        private void EditInfoView_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new EditInfoViewModel();
        }

        private void AuthButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Ви бажаєте вийти з кабінету?", "Підтвердження", MessageBoxButton.YesNo);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    AuthWindow authWindow = new AuthWindow();
                    authWindow.Show();
                    this.Close();
                    break;

                case MessageBoxResult.No:
                    break;
            }
        }

    }
}
