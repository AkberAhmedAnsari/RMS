using BAL.Classes;
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

namespace BakeryManagementSystem.Windows
{
    /// <summary>
    /// Interaction logic for winCustomerInformation.xaml
    /// </summary>
    public partial class winCustomerInformation : Window
    {
        public clsCustomerBAL objCustomerBAL = new clsCustomerBAL();
        public winCustomerInformation()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (MessageBox.Show("Do you want to save record?", "Confirmation",
                    MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    bool vbol = clsCustomerBAL.SaveLogic(objCustomerBAL);
                    if (vbol)
                    {
                        MessageBox.Show("Record save successfully", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                        clearRecord();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// All data get In controll
        /// </summary>
        private void fillDataInClassObject()
        {
            try
            {
                fillComboBox();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void fillComboBox()
        {
            var List = clsCustomerTypeBAL.getCoustomerType();
            CbClientType.ItemsSource = List;

            var CountryList = clsCountryBAL.GetAllCountry();
            CbCountry.ItemsSource = CountryList;

            var CityList = clsCityBAL.GetAllCity();
            CbCity.ItemsSource = CityList;

            var TitleList = clsTitleBAL.getTitle();
            Cbtitle.ItemsSource = TitleList;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                this.DataContext = objCustomerBAL;
                fillComboBox();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                clearRecord();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void clearRecord()
        {
            objCustomerBAL = new clsCustomerBAL();
            this.DataContext = objCustomerBAL;
        }
    }
}
