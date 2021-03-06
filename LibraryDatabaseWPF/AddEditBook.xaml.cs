﻿using LibraryDatabaseWPF.Models;
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
        private ViewModel view = new ViewModel();
        public AddEditBook()
        {
            InitializeComponent();
            DataContext = view;
        }

        /// <summary>
        /// fills textboxes with current information and disables them.
        /// only allows for condition of a book to be changed
        /// </summary>
        /// <param name="book"></param>
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
            DataContext = view;

        }

        /// <summary>
        /// returns to previous page if possible
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
                NavigationService.Navigate(new LibraryDatabase());
            }
        }

        /// <summary>
        /// checks to see if you are creating or editing a book
        /// when creating a book calls on repo to create
        /// when edit does the same
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnConfirm_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is ViewModel viewModel)
            {
                if (book == null)
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

        /// <summary>
        /// does a aweful tedious process of searching through the long list of 31 genres, which I typed by hand, to find which of the buttons is selected
        /// I mean come on 31 genres, really. One of the genres is calander. Why would a library even have calenders for check out purposes.
        /// </summary>
        /// <param name="defaultValue"></param>
        /// <param name="buttons"></param>
        /// <returns></returns>
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
