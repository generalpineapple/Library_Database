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

namespace LibraryDatabaseWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //enter your connection string here
        //private readonly string connectionString = @"Data Source = mssql.cs.ksu.edu; Initial Catalog = sbrunner5124; User ID = sbrunner5124; Password=Adpyr235124";
        private readonly string connectionString = @"Data Source = mssql.cs.ksu.edu; Database = sbrunner5124; User ID = sbrunner5124; Password=Adpyr235124";

        public MainWindow()
        {
            InitializeComponent();
            ViewModel viewModel = new ViewModel(new SqlBookRepository(connectionString), new SqlUserRepository(connectionString), new SqlCheckedOutRepository(connectionString));
            DataContext = viewModel;
        }

        /// <summary>
        /// allows user to go back to the previous page at any time
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnBack(object sender, RoutedEventArgs e)
        {
            if (DatabaseUI.NavigationService.CanGoBack)
            {
                DatabaseUI.NavigationService.GoBack();
            }
            else
            {
                DatabaseUI.NavigationService.Navigate(new ChooseDatabase());
            }
        }

        /// <summary>
        /// allows your to return to the choose database page at any time
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnReturn(object sender, RoutedEventArgs e)
        {
            DatabaseUI.NavigationService.Navigate(new ChooseDatabase());
        }
    }
}
