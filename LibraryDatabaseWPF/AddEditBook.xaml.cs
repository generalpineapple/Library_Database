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
    /// Interaction logic for AddEditBook.xaml
    /// </summary>
    public partial class AddEditBook : Page
    {
        private Books book = null;
        public AddEditBook()
        {
            InitializeComponent();
        }

        public AddEditBook(Books book)
        {
            InitializeComponent();
            this.book = book;
            uxTitle.Text = book.Title;
            uxTitle.IsEnabled = false; 
            uxISBN.Text = book.ISBN;
            uxISBN.IsEnabled = false;
            uxAuthor.Text = book.AuthorName;
            uxAuthor.IsEnabled = false;
            
        }

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
            if (DataContext is ViewModel viewModel)
            {
                if (book.Equals(null))
                {
                    if (!String.IsNullOrWhiteSpace(uxTitle.Text) && !String.IsNullOrWhiteSpace(uxISBN.Text) && !String.IsNullOrWhiteSpace(uxAuthor.Text))
                    {
                        string genre = SelectedRadioValue("Arts & Photography", G1, G2, G3, G4, G5, G6, G7, G8, G9, G0, G10, G11, G12, G13, G14, G15, G16, G17, G18, G19, G20, G21, G22, G23, G24, G25, G26, G27, G28, G29, G30, G31);
                        viewModel.bookRepository.CreateBook(uxISBN.Text, uxAuthor.Text, uxTitle.Text, genre, uxCondition.SelectedItem.ToString());
                    }
                }
                else
                {
                    viewModel.bookRepository.EditBookQuality(book.BookId, uxCondition.SelectedIndex + 1);
                }

                if (NavigationService.CanGoBack)
                {
                    NavigationService.GoBack();
                }
                else
                {
                    NavigationService.Navigate(new LibraryDatabase());
                }
            }
        }

        private string SelectedRadioValue(string defaultValue, params RadioButton[] buttons)
        {
            foreach (RadioButton button in buttons)
            {
                if (button.IsChecked == true)
                {
                    return button.Tag.ToString();
                }
            }
            return defaultValue;
        }
    }
}
