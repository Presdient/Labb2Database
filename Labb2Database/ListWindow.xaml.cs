using Labb2Database;
using Labb2Database.DataModels;
using Labb2Database.Handler;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
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

namespace Labb2Database
{
    public partial class ListWindow : Window
    {
        private readonly BookstoreContext _context;

        public ListWindow()
        {
            InitializeComponent();

            this.Loaded += ListWindow_Loaded; 
            this.Closed += ListWindow_Closed;
            _context = new BookstoreContext(); 
        }

        private void ListWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadStockBalance(); 
        }

        private void LoadStockBalance()
        {
            try
            {
                var stockBalanceInfo = _context.StockBalance
                    .Include(sb => sb.Books)
                    .Select(sb => new
                    {
                        StoreID = sb.StoreID,
                        ISBN = sb.ISBN,
                        Quantity = sb.Quantity
                    })
                    .ToList();

                stockBalanceListView.ItemsSource = stockBalanceInfo;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading stock balance: {ex.Message}");
            }
        }

        private void ListWindow_Closed(object sender, EventArgs e)
        {
            MainWindow mainWindow = Application.Current.MainWindow as MainWindow;

            if (mainWindow != null)
            {
                mainWindow.Show();
            }
        }

        public void RefreshStockBalance()
        {
            try
            {
                var stockBalanceInfo = _context.StockBalance
                    .Include(sb => sb.Books)
                    .Select(sb => new
                    {
                        StoreID = sb.StoreID,
                        ISBN = sb.ISBN,
                        Quantity = sb.Quantity
                    })
                    .ToList();

                stockBalanceListView.ItemsSource = null; 
                stockBalanceListView.ItemsSource = stockBalanceInfo; 
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading stock balance: {ex.Message}");
            }
        }
    }
}
