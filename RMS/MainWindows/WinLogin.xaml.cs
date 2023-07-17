using BakeryManagementSystem.MainWindows;
using BAL;
using BAL.Classes;
using DAL;
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

namespace BakeryManagementSystem
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class WinLogin : Window
    {
        public WinLogin()
        {
            InitializeComponent();
        }

        public void RemoveText(object sender, EventArgs e)
        {
            
        }

        public void AddText(object sender, EventArgs e)
        {

        }
        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtlogin.Text.Trim() == "")
                {
                    MessageBox.Show("Please enter login Id", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }

                if (txtpassword.Password.Trim() == "")
                {
                    MessageBox.Show("Please enter Password", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }

                var user = clsUserBMSBAL.GetUser(txtlogin.Text, txtpassword.Password);
                if (user == null || user.userid == 0)
                {
                    MessageBox.Show("Invalid Login Id and Password", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }

                if (user.loginId != txtlogin.Text || user.password != txtpassword.Password)
                {
                    MessageBox.Show("Invalid Login Id and Password", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                clsAppObject.LoginUser = user;
                ShowWindow();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ShowWindow()
        {
            MainWindows.winMainManu main = new winMainManu();
            this.Hide();
            main.Show();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                txtlogin.Text = "Login Id";

                txtlogin.GotFocus += Txtlogin_GotFocus;
                txtlogin.LostFocus += Txtlogin_LostFocus;

                txtlogin.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Txtlogin_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtlogin.Text))
            {
                txtlogin.Text = "Login Id";
                txtlogin.Tag = "";
            }
            else
                txtlogin.Tag = txtlogin.Text;
            
        }

        private void Txtlogin_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtlogin.Tag == null || string.IsNullOrWhiteSpace(txtlogin.Tag.ToString()))
                txtlogin.Text = "";
        }

      
    }

    public class PasswordBoxMonitor : DependencyObject
    {
        public static bool GetIsMonitoring(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsMonitoringProperty);
        }

        public static void SetIsMonitoring(DependencyObject obj, bool value)
        {
            obj.SetValue(IsMonitoringProperty, value);
        }

        public static readonly DependencyProperty IsMonitoringProperty =
            DependencyProperty.RegisterAttached("IsMonitoring", typeof(bool), typeof(PasswordBoxMonitor), new UIPropertyMetadata(false, OnIsMonitoringChanged));



        public static int GetPasswordLength(DependencyObject obj)
        {
            return (int)obj.GetValue(PasswordLengthProperty);
        }

        public static void SetPasswordLength(DependencyObject obj, int value)
        {
            obj.SetValue(PasswordLengthProperty, value);
        }

        public static readonly DependencyProperty PasswordLengthProperty =
            DependencyProperty.RegisterAttached("PasswordLength", typeof(int), typeof(PasswordBoxMonitor), new UIPropertyMetadata(0));

        private static void OnIsMonitoringChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var pb = d as PasswordBox;
            if (pb == null)
            {
                return;
            }
            if ((bool)e.NewValue)
            {
                pb.PasswordChanged += PasswordChanged;
            }
            else
            {
                pb.PasswordChanged -= PasswordChanged;
            }
        }

        static void PasswordChanged(object sender, RoutedEventArgs e)
        {
            var pb = sender as PasswordBox;
            if (pb == null)
            {
                
                return;
            }
            SetPasswordLength(pb, pb.Password.Length);
        }
    }
}
