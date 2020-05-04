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

        /// <summary>
        /// navigates to the add book page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnAddBook_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddEditBook());
        }

        /// <summary>
        /// navigates to the edit book page with the selected book as its paramiter
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnEditBook_Click(object sender, RoutedEventArgs e)
        {
            if (uxListBox.SelectedItem is Books book)
                NavigationService.Navigate(new AddEditBook(book));
        }

        /// <summary>
        /// uses the combo box tag to determine which procedure to activate.
        /// uses the book repo to activate those procedures
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// navigates to the userdatabase so it can finish the check out procedure
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnCheckOut_Click(object sender, RoutedEventArgs e)
        {
            if(uxListBox.SelectedItem is Books book)
            {
                NavigationService.Navigate(new UserDatabase(book));
            }

        }

        /// <summary>
        /// generates a report quarry through user repo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnGetTopBooksByGenre_Click(object sender, RoutedEventArgs e)
        {
            if(DataContext is ViewModel viewModel)
            {
                viewModel.BookList = viewModel.bookRepository.GetTopBooksByGenre().ToList();
            }
        }

        /// <summary>
        /// generates a report quarry through user repo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnGetBooksToReplace_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is ViewModel viewModel)
            {
                viewModel.BookList = viewModel.bookRepository.FetchBooksToReplace().ToList();
            }
        }
    }
}
