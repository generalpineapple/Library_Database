using LibraryDatabaseWPF.Models;
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
            if (uxListBox.SelectedItem is Users user)
            {
                NavigationService.Navigate(new AddEditUser(user));
            }
        }

        private void OnSearch_Click(object sender, RoutedEventArgs e)
        {
            if(DataContext is ViewModel viewModel)
            {
                viewModel.UserList = viewModel.usersRepository.FetchAllUsers().ToList();
                if (!String.IsNullOrWhiteSpace(uxSearchText.Text))
                {
                    var selectedTag = ((ComboBoxItem)uxSearchBy.SelectedItem).Tag.ToString();
                    switch (selectedTag)
                    {
                        case "name":
                            viewModel.UserList = viewModel.UserList.Where(user => user.Name.Contains(uxSearchText.Text)).ToList();
                            break;
                        case "phone":
                            viewModel.UserList = viewModel.UserList.Where(user => user.PhoneNumber.Contains(uxSearchText.Text)).ToList();
                            break;
                        case "email":
                            viewModel.UserList = viewModel.UserList.Where(user => user.Email.Contains(uxSearchText.Text)).ToList();
                            break;
                        case "id":
                            int x;
                            if(Int32.TryParse(uxSearchText.Text, out x))
                                viewModel.UserList = viewModel.UserList.Where(user => user.UserId == x).ToList();
                            break;
                    }
                }
            }
        }

        private void OnGetTopUsers_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is ViewModel viewModel)
            {
                viewModel.UserList = viewModel.usersRepository.GetTopUsers().ToList();
            }
        }

        private void OnGetUserReport_Click(object sender, RoutedEventArgs e)
        {
            if (uxListBox.SelectedItem is Users user)
            {
                if (DataContext is ViewModel viewModel)
                {
                    UserReport userReport = viewModel.usersRepository.CreateUserReport(user.Name);
                    UserReportWindow userReportWindow = new UserReportWindow(userReport);
                }
            }

        }
    }
}
