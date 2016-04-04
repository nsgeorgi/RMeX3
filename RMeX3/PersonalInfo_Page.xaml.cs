using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text.RegularExpressions;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace RMeX3
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PersonalInfo_Page : Page
    {
        public PersonalInfo_Page()
        {
            this.InitializeComponent();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Settings_page));
        }

        public const string MatchEmailPattern =
  @"^(([\w-]+\.)+[\w-]+|([a-zA-Z]{1}|[\w-]{2,}))@"
+ @"((([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?
				[0-9]{1,2}|25[0-5]|2[0-4][0-9])\."
+ @"([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?
				[0-9]{1,2}|25[0-5]|2[0-4][0-9])){1}|"
+ @"([a-zA-Z0-9]+[\w-]+\.)+[a-zA-Z]{1}[a-zA-Z0-9-]{1,23})$";


        public static bool IsValidEmail(string email)
        {
            return Regex.IsMatch(email, MatchEmailPattern);
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            // Check empty fields
            if (FName.Text == string.Empty || LName.Text == string.Empty || Email.Text == string.Empty || Pass.Password == string.Empty || ConfirmPass.Password == string.Empty || Phone_Number.Text == string.Empty || Organization.Text == string.Empty)
                Error_Message.Text = "Empty fields";
            else if (!IsValidEmail(Email.Text))
                Error_Message.Text = "Invalid email";
            else if (Pass.Password != ConfirmPass.Password)
                Error_Message.Text = "Unmatched password";
            else Frame.Navigate(typeof(Settings_page));
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Settings_page));
        }
    }
}
