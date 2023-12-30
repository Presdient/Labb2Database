using Labb2Database;
using Labb2Database.DataModels;
using Labb2Database.Handler;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
    public partial class MainWindow : Window
    {
        private readonly MainWindow mainWindowInstance;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnListStockBalance_Click(object sender, RoutedEventArgs e)
        {
            ListWindow listWindow = new ListWindow();
            listWindow.Show();
            this.Hide();
        }


        private void AddRemoveBook_Click(object sender, RoutedEventArgs e)
        {
            AddBookRemoveBook addRemoveBookWindow = new AddBookRemoveBook(this); 
            addRemoveBookWindow.Show();
            this.Close();
        }


        private void Shops_Click(object sender, RoutedEventArgs e)
        {
            Shops shops = new Shops();
            shops.Show();
        }

        private void EditShops_Click(object sender, RoutedEventArgs e)
        {
            EditShops editShops = new EditShops(this);
            editShops.Show();
            this.Close();
        }

        private void BtnQuitWindow_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
        
