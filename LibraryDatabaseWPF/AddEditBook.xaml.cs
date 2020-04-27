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
    /// Interaction logic for AddEditBook.xaml
    /// </summary>
    public partial class AddEditBook : Page
    {
        public AddEditBook()
        {
            InitializeComponent();
        }

        //TODO: Add a constructor to allow the ability to edit a book in the database

        private void OnCancel_Click(object sender, RoutedEventArgs e)
        {
            if (NavigationService.CanGoBack)
            {
                NavigationService.GoBack();
            }
            else
            {
                NavigationService.Navigate(new LibraryDatabase());
            }
        }

        private void OnConfirm_Click(object sender, RoutedEventArgs e)
        {
            //TODO: add or update the book to the database
        }
    }
}
