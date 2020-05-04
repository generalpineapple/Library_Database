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
using System.Configuration;
using System.Collections.Specialized;

namespace LibraryDatabaseWPF
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Updates the username and password in the app config
        /// </summary>
        private void buttonSubmit_Click(object sender, RoutedEventArgs e)
        {
            string strUsername = textboxUsername.Text;
            string strPassword = textboxPassword.Password;
            try
            {
                var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                var settings = configFile.AppSettings.Settings;

                // update username in the config
                if (settings["username"] == null)
                {
                    settings.Add("username", strUsername);
                }
                else
                {
                    settings["username"].Value = strUsername;
                }

                // update username in the config
                if (settings["password"] == null)
                {
                    settings.Add("password", strPassword);
                }
                else
                {
                    settings["password"].Value = strPassword;
                }

                configFile.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);
            }
            catch (ConfigurationErrorsException)
            {
                Console.WriteLine("Error writing app settings");
            }
        }
    }
}
