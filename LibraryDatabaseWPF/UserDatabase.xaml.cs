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
    /// Interaction logic for UserDatabase.xaml
    /// </summary>
    public partial class UserDatabase : Page
    {
        public UserDatabase()
        {
            InitializeComponent();
        }

        private void OnAddUser_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddEditUser());
        }

        private void OnEditUser_Click(object sender, RoutedEventArgs e)
        {
            //TODO: Navigate to AddEditUser with selected user as parameter
        }

        private void OnDeleteUser_Click(object sender, RoutedEventArgs e)
        {
            //TODO: Delete User From the database
        }

        private void OnSearch_Click(object sender, RoutedEventArgs e)
        {
            //TODO: Use Linq to filter users and display result to listbox
        }

        private void OnGetTopUsers_Click(object sender, RoutedEventArgs e)
        {
            //TODO
        }

        private void OnGetUserReport_Click(object sender, RoutedEventArgs e)
        {
            //TODO
        }
    }
}
