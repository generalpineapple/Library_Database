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
        private Books book;
        /// <summary>
        /// confirms that all buttons are enabled except the checkout button
        /// </summary>
        public UserDatabase()
        {
            InitializeComponent();
            uxAdd.IsEnabled = true;
            uxEdit.IsEnabled = true;
            uxReport.IsEnabled = true;
            uxTop.IsEnabled = true;
            uxCheckout.IsEnabled = false;
        }

        /// <summary>
        /// confirms all buttons are disabled but the checkout button
        /// </summary>
        /// <param name="book"></param>
        public UserDatabase(Books book)
        {
            InitializeComponent();
            this.book = book;
            uxAdd.IsEnabled = false;
            uxEdit.IsEnabled = false;
            uxReport.IsEnabled = false;
            uxTop.IsEnabled = false;
            uxCheckout.IsEnabled = true;
        }

        /// <summary>
        /// navigates to the add user page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnAddUser_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddEditUser());
        }

        /// <summary>
        /// navigates to the edit user page with a user as a paramiter
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnEditUser_Click(object sender, RoutedEventArgs e)
        {
            if (uxListBox.SelectedItem is Users user)
            {
                NavigationService.Navigate(new AddEditUser(user));
            }
        }

        /// <summary>
        /// gets the tag out of the comboBox
        /// the tag tells it which kind of search it is doing
        /// uses linq to filter though the database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// calls on the userRepo to activate a report quarry
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnGetTopUsers_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is ViewModel viewModel)
            {
                viewModel.UserList = viewModel.usersRepository.GetTopUsers().ToList();
            }
        }

        /// <summary>
        /// calls on the userRepo to activate a report quarry on a specified user
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        private void OnCheckout_Click(object sender, RoutedEventArgs e)
        {
            if(uxListBox.SelectedItem is Users user)
            {
                if(DataContext is ViewModel viewModel)
                {
                    viewModel.checkedOutRepository.CreateCheckedOut(book.BookId, user.UserId);
                    NavigationService.Navigate(new ChooseDatabase());
                }
            }
        }
    }
}
