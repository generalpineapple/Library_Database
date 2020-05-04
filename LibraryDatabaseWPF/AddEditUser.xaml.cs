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
    /// Interaction logic for AddEditUser.xaml
    /// </summary>
    public partial class AddEditUser : Page
    {
        //default to -1 as there are no negative keys
        private int userId = -1;
        public AddEditUser()
        {
            InitializeComponent();
        }

        /// <summary>
        /// sets all textboxes to the current info of the user
        /// </summary>
        /// <param name="users"></param>
        public AddEditUser(Users users)
        {
            userId = users.UserId;
            uxName.Text = users.Name;
            uxEmail.Text = users.Email;
            uxNumber.Text = users.PhoneNumber;
            //TODO: add Address
        }

        /// <summary>
        /// disregards any information put in and returns to previous page if possible
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnCancel_Click(object sender, RoutedEventArgs e)
        {
            if (NavigationService.CanGoBack)
            {
                NavigationService.GoBack();
            }
            else
            {
                NavigationService.Navigate(new UserDatabase());
            }
        }

        /// <summary>
        /// checks all textboxes are filled then either updates or creates user.
        /// it then returns you to the previous page.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnConfirm_Click(object sender, RoutedEventArgs e)
        {
            if(DataContext is ViewModel viewModel)
            {
                //do I need to validate that the information is correct? like numbers and emails?
                //Remember to delete this comment
                if (!String.IsNullOrWhiteSpace(uxName.Text) && !String.IsNullOrWhiteSpace(uxAddress.Text) && !String.IsNullOrWhiteSpace(uxNumber.Text) && !String.IsNullOrWhiteSpace(uxEmail.Text)) 
                {
                    if (userId == -1)
                        viewModel.usersRepository.CreateUser(uxName.Text, uxAddress.Text, uxNumber.Text, uxEmail.Text);
                    else
                        viewModel.usersRepository.EditUserById(userId, uxName.Text, uxAddress.Text, uxNumber.Text, uxEmail.Text);

                    if (NavigationService.CanGoBack)
                    {
                        NavigationService.GoBack();
                    }
                    else
                    {
                        NavigationService.Navigate(new UserDatabase());
                    }
                }                
            }
        }
    }
}
