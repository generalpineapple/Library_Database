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
using LibraryDatabaseWPF.Models;

namespace LibraryDatabaseWPF
{
    /// <summary>
    /// Interaction logic for LibraryDatabase.xaml
    /// </summary>
    public partial class LibraryDatabase : Page
    {
        public LibraryDatabase()
        {
            InitializeComponent();            
        }

        private void OnAddBook_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddEditBook());
        }

        private void OnEditBook_Click(object sender, RoutedEventArgs e)
        {
            //TODO: navigate to AddEditBook with selected book as a parameter
        }

        private void OnSearch_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is ViewModel viewModel)
            {
                if (String.IsNullOrWhiteSpace(uxSearchText.Text))
                {

                    viewModel.BookList = viewModel.bookRepository.FetchAllBooks().ToList();
                }
                else
                {    
                    var selectedTag = ((ComboBoxItem)uxSearchBy.SelectedItem).Tag.ToString();
                    viewModel.BookList.Clear();
                    switch (selectedTag)
                    {
                        case "title":
                            viewModel.BookList.Add(viewModel.bookRepository.FetchBookByTitle(uxSearchText.Text));
                            break;
                        case "author":
                            IReadOnlyList<Books> books = viewModel.bookRepository.FetchBookByAuthor(uxSearchText.Text);
                            foreach (Books book in books)
                                viewModel.BookList.Add(book);
                            break;
                        case "isbn":
                            viewModel.BookList.Add(viewModel.bookRepository.FetchBookFromISBN(uxSearchText.Text));
                            break;
                    }
                }
            }
        }

        private void OnCheckIn_Click(object sender, RoutedEventArgs e)
        {
            //TODO: Update Selected book to have the Checked in status.
        }

        private void OnCheckOut_Click(object sender, RoutedEventArgs e)
        {
            //TODO: Update Selected book to have the Checked out status.
        }

        private void OnDeleteBook_Click(object sender, RoutedEventArgs e)
        {
            //TODO: Delete Selected book from Database
        }

        private void OnGetTopBooksByGenre_Click(object sender, RoutedEventArgs e)
        {
            if(DataContext is ViewModel viewModel)
            {
                viewModel.BookList = viewModel.bookRepository.GetTopBooksByGenre().ToList();
            }
        }

        private void OnGetBooksToReplace_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is ViewModel viewModel)
            {
                viewModel.BookList = viewModel.bookRepository.FetchBooksToReplace().ToList();
            }
        }
    }
}
