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
    public partial class Shops : Window
    {
        private readonly BookstoreContext _context;

        public Shops()
        {
            InitializeComponent();
            _context = new BookstoreContext();
            this.Loaded += Shops_Loaded;
        }

        private void Shops_Loaded(object sender, RoutedEventArgs e)
        {
            LoadShopInformation();
        }

        private void LoadShopInformation()
        {
            try
            {
                var shopsInfo = _context.Stores.Select(store => new
                {
                    ID = store.ID,
                    StoreName = store.StoreName,
                    AddressLine = store.AddressLine,
                    City = store.City,
                    State = store.State,
                    PostalCode = store.PostalCode
                }).ToList();

                shopsListView.ItemsSource = shopsInfo;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading shop information: {ex.Message}");
            }
        }
    }
}
