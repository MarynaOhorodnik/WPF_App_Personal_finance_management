using System.Windows;
using WPF_App_Personal_finance_management.Admin_ViewModels;

namespace WPF_App_Personal_finance_management
{
    /// <summary>
    /// Interaction logic for AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        public AdminWindow()
        {
            InitializeComponent();

            DataContext = new AdmViewModel();
        }

        private void AdmUserView_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new AdmUserViewModel();
        }

        private void AdmEditInfoView_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new AdmEditInfoViewModel();
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

        private void Admin_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new AdmViewModel();
        }
    }
}
