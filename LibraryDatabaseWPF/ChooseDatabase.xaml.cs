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
    /// Interaction logic for ChooseDatabase.xaml
    /// </summary>
    public partial class ChooseDatabase : Page
    {
        public ChooseDatabase()
        {
            InitializeComponent();
        }

        private void OnOpenLibraryDatabase(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new LibraryDatabase());
        }

        private void OnOpenUserDatabase(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new UserDatabase());
        }
    }
}
