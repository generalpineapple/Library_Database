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
        private ViewModel view = new ViewModel();
        private int userId = -1;
        public AddEditUser()
        {
            InitializeComponent();
            DataContext = view;
        }

        public AddEditUser(Users users)
        {
            userId = users.UserId;
            uxEmail.Text = users.Email;
            uxNumber.Text = users.PhoneNumber;
            uxName.Text = users.Name;
            DataContext = view;
            //TODO: add Address
        }


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

        private void OnConfirm_Click(object sender, RoutedEventArgs e)
        {
            if(DataContext is ViewModel viewModel)
            {
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
