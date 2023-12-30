using Labb2Database.DataModels;
using Labb2Database.Handler;
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

namespace Labb2Database
{
    public partial class EditShops : Window
    {
        private readonly BookstoreContext _dbContext;
        private bool changesMade = false;
        private readonly MainWindow mainWindowInstance;
        private bool updatingTextBox = false;
        public EditShops(MainWindow mainWindowInstance)
        {
            InitializeComponent();
            this.mainWindowInstance = mainWindowInstance;

            _dbContext = new BookstoreContext(); 

            storesComboBox.ItemsSource = _dbContext.Stores.ToList();
            storesComboBox.DisplayMemberPath = "StoreName";
            postalCodeTextBox.TextChanged += postalCodeTextBox_TextChanged;
        }

        private void BtnUpdateStore_Click(object sender, RoutedEventArgs e)
        {

            string addressLine = addressTextBox.Text;
            string postalCode = postalCodeTextBox.Text;

            int storeId = (storesComboBox.SelectedItem as Stores)?.ID ?? 0;

            if (!string.IsNullOrEmpty(addressLine) && !string.IsNullOrEmpty(postalCode))
            {
                var storeToUpdate = _dbContext.Stores.FirstOrDefault(store => store.ID == storeId);

                if (storeToUpdate != null)
                {
                    storeToUpdate.AddressLine = addressLine;
                    storeToUpdate.PostalCode = postalCode;

                    _dbContext.SaveChanges();
                    MessageBox.Show("Store information updated successfully.");
                    changesMade = true;
                }
                else
                {
                    MessageBox.Show("Selected store not found.");
                }
            }
            else
            {
                MessageBox.Show("Please enter the address and postal code.");
            }
        }


        private void BtnReviewChanges_Click(object sender, RoutedEventArgs e)
        {
            if (changesMade)
            {
                Shops shops = new Shops(); 
                shops.Show();
            }
            else
            {
                MessageBox.Show("No changes made yet.");
            }
        }


        private void ReviewChanges_Click(object sender, RoutedEventArgs e)
        {
            if (changesMade)
            {
                ListWindow listWindow = new ListWindow();
                listWindow.Show();
            }
            else
            {
                MessageBox.Show("Please add or remove a book before reviewing!");
            }
        }

        private void postalCodeTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!updatingTextBox)
            {
                updatingTextBox = true;

                string postalCode = postalCodeTextBox.Text;

                if (postalCode.Length > 6)
                {
                    MessageBox.Show("You can only have a maxium of 5 numbers in your postalcode.");
                    postalCode = postalCode.Substring(0, 6);
                    postalCodeTextBox.Text = postalCode;
                    postalCodeTextBox.SelectionStart = postalCode.Length;
                }
                else if (postalCode.Length == 3 && !postalCode.Contains(" "))
                {
                    postalCode = postalCode.Insert(3, " ");
                    postalCodeTextBox.Text = postalCode;
                    postalCodeTextBox.SelectionStart = postalCode.Length;
                }

                updatingTextBox = false;
            }
        }



        private void BtnBackToMenu_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }
}
