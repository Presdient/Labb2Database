using Labb2Database.DataModels;
using Labb2Database.Handler;
using Microsoft.EntityFrameworkCore;
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
    public partial class AddBookRemoveBook : Window
    {
        private readonly BookstoreContext _dbContext;
        private readonly MainWindow mainWindowInstance;
        private bool changesMade = false;


        public AddBookRemoveBook(MainWindow mainWindow)
        {
            InitializeComponent();

            _dbContext = new BookstoreContext();
            mainWindowInstance = mainWindow;

            isbnComboBox.ItemsSource = _dbContext.Books.ToList();
            isbnComboBox.DisplayMemberPath = "ISBN"; 

            storeIdComboBox.ItemsSource = _dbContext.Stores.ToList();
            storeIdComboBox.DisplayMemberPath = "ID"; 


        }
        private void BtnAddBook_Click(object sender, RoutedEventArgs e)
        {
            string isbn = (isbnComboBox.SelectedItem as Books)?.ISBN;
            int storeId = (storeIdComboBox.SelectedItem as Stores)?.ID ?? 0;

            if (int.TryParse(quantityTextBox.Text, out int quantity))
            {
                var stockBalance = _dbContext.StockBalance
                    .SingleOrDefault(sb => sb.ISBN == isbn && sb.StoreID == storeId);

                if (stockBalance == null)
                {
                    stockBalance = new StockBalance
                    {
                        ISBN = isbn,
                        StoreID = storeId,
                        Quantity = quantity
                    };
                    _dbContext.StockBalance.Add(stockBalance);
                }
                else
                {
                    stockBalance.Quantity += quantity;
                }

                _dbContext.SaveChanges();
                MessageBox.Show("Book successfully added.");
                changesMade = true;
            }
            else
            {
                MessageBox.Show("Please enter a valid quantity.");
            }
        }

        private void BtnRemove_Click(object sender, RoutedEventArgs e)
        {
            string ISBN = (isbnComboBox.SelectedItem as Books)?.ISBN;
            int StoreID = (storeIdComboBox.SelectedItem as Stores)?.ID ?? 0;

            if (int.TryParse(quantityTextBox.Text, out int quantity))
            {
                var stockBalance = _dbContext.StockBalance
                    .SingleOrDefault(sb => sb.ISBN == ISBN && sb.StoreID == StoreID);

                if (stockBalance != null)
                {
                    if (stockBalance.Quantity >= quantity)
                    {
                        stockBalance.Quantity -= quantity;

                        if (stockBalance.Quantity == 0)
                        {
                            _dbContext.StockBalance.Remove(stockBalance);
                        }

                        _dbContext.SaveChanges();
                        MessageBox.Show("Book successfully removed.");
                        changesMade = true;
                    }
                    else
                    {
                        MessageBox.Show("Quantity to remove exceeds available stock.");
                    }
                }
                else
                {
                    MessageBox.Show("Selected book not found in stock.");
                }
            }
            else
            {
                MessageBox.Show("Please enter a valid quantity.");
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


        private void BackToMenu_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow(); 
            mainWindow.Show(); 
            this.Close(); 
        }
    }
}