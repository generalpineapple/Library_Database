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
using System.Windows.Shapes;

namespace LibraryDatabaseWPF
{
    /// <summary>
    /// Interaction logic for UserReportWindow.xaml
    /// </summary>
    public partial class UserReportWindow : Window
    {       
        /// <summary>
        /// this is self-explanitory
        /// </summary>
        /// <param name="userReport"></param>
        public UserReportWindow(UserReport userReport)
        {
            InitializeComponent();
            uxID.Text = userReport.UserId.ToString();
            uxCurrent.Text = userReport.CurrentCheckouts.ToString();
            uxDays.Text = userReport.DaysLate.ToString();
            uxLate.Text = userReport.LateReturns.ToString();
            uxOnTime.Text = userReport.OnTimeReturns.ToString();
            uxOverdue.Text = userReport.OverDueBooks.ToString();
        }

        private void OnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
