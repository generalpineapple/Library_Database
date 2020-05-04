﻿using System;
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
        private ViewModel view = new ViewModel();
        public LibraryDatabase()
        {
            InitializeComponent();
            DataContext = view;
        }

        private void OnAddBook_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddEditBook());
        }

        private void OnEditBook_Click(object sender, RoutedEventArgs e)
        {
            if (uxListBox.SelectedItem is Books book)
                NavigationService.Navigate(new AddEditBook(book));
        }

        private void OnSearch_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is ViewModel viewModel)
            {
                if (String.IsNullOrWhiteSpace(uxSearchText.Text))
                {

                    viewModel.BookList = viewModel.bookRepository.FetchAllBooks().ToList();
                    UpdateList();
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
                    UpdateList();
                }
            }
        }

        private void OnCheckIn_Click(object sender, RoutedEventArgs e)
        {
            //TODO: Update Selected book to have the Checked in status.
        }

        private void OnCheckOut_Click(object sender, RoutedEventArgs e)
        {
            if(uxListBox.SelectedItem is Books book)
            {
                NavigationService.Navigate(new UserDatabase(book));
            }

        }

        private void OnGetTopBooksByGenre_Click(object sender, RoutedEventArgs e)
        {
            if(DataContext is ViewModel viewModel)
            {
                viewModel.BookList = viewModel.bookRepository.GetTopBooksByGenre().ToList();
                UpdateList();
                
            }
        }

        private void OnGetBooksToReplace_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is ViewModel viewModel)
            {
                viewModel.BookList = viewModel.bookRepository.FetchBooksToReplace().ToList();
            }
        }
        
        private void UpdateList()
        {
            if (DataContext is ViewModel viewModel)
            {
                uxListBox.ItemsSource = viewModel.BookList;
            }
        }
    }
}
