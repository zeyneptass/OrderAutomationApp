using Business.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
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

namespace ConsoleUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private EfCategoryDal _categoryDal;
        public MainWindow()
        {
            InitializeComponent();

            OrdersDataGrid.ItemsSource = GetOrdersData();

            List<Masa> MasaListesi = new List<Masa>();

            // Örnek masa verilerini ekleyin (Bu kısmı projenize uygun şekilde güncelleyin)
            for (int i = 1; i <= 15; i++)
            {
                MasaListesi.Add(new Masa { MasaNumarasi = i, MasaResimYolu = @"C:\Users\zeyne\OneDrive\Masaüstü\OOPLectures\CafeAutomationApp\ConsoleUI\Icon\chair.png" });
            }

            // XAML içindeki ItemsControl'a MasaListesi'ni bağla
            masaItemsControl.ItemsSource = MasaListesi;

            // EfCategoryDal'ı oluştur
            _categoryDal = new EfCategoryDal();

            // Kategori ComboBox'ını doldur
            CategoryComboBox.ItemsSource = GetCategories();
            CategoryComboBox.DisplayMemberPath = "CategoryName"; // Kategori ismini göstermek için
        }

        private List<Category> GetCategories()
        {
            // Kategorileri çek
            return _categoryDal.GetAll();
        }


        private static List<Order> GetOrdersData()
        {
            // Burada gerçek bir veritabanı sorgusu veya başka bir yöntem kullanabilirsiniz
            // Bu örnekte basit bir şekilde birkaç Order oluşturduk
            List<Order> orders = new List<Order>
        {
            new Order { OrderId = 1, ProductId = 101, TableId = 1, Quantity = 2, TotalPrice = 25.00m, OrderDate = DateTime.Now },
            new Order { OrderId = 2, ProductId = 102, TableId = 2, Quantity = 1, TotalPrice = 12.50m, OrderDate = DateTime.Now },
            // Diğer Orders'ları burada ekleyebilirsiniz
        };

            return orders;
        }

        public class Masa
        {
            public int MasaNumarasi { get; set; }
            public string MasaResimYolu { get; set; }
        }

        // Masa butonlarına tıklandığında çalışacak olay işleyicisi
        private void MasaButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is Masa secilenMasa)
            {
                // Seçilen masa ile ilgili bilgileri göster
                MessageBox.Show($"Rezervasyon ID: {secilenMasa.MasaNumarasi}\nMasa ID: {secilenMasa.MasaNumarasi}\nMüşteri ID: {secilenMasa.MasaNumarasi}\nRezervasyon Tarihi: {DateTime.Now}");
            }
        }

        private void AddOrderButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AddProductButton_Click(object sender, RoutedEventArgs e)
        {
            var productService = new ProductManager(new EfProductDal());

            // Get values from UI controls
            string productName = ProductNameTextBox.Text;
            string description = DescriptionTextBox.Text;
            decimal unitPrice = Convert.ToDecimal(UnitPriceTextBox.Text);
            int unitsInStock = Convert.ToInt32(UnitsInStockTextBox.Text);

            // Assuming you have a Category class and bind a list of categories to the ComboBox
            Category selectedCategory = (Category)CategoryComboBox.SelectedItem;

            // Create a new Product instance
            var newProduct = new Product
            {
                ProductName = productName,
                Description = description,
                UnitPrice = unitPrice,
                UnitsInStock = unitsInStock,
                CategoryId = selectedCategory.CategoryId,
                CreatedAt = DateTime.Now
            };

            // Add the new product
            productService.Add(newProduct);

            // Refresh the data grid if necessary
            // OrdersDataGrid.ItemsSource = GetOrdersData(); // Update this line with the appropriate method

            // Clear input fields or perform any other necessary UI updates
            ClearProductInputFields();

        }
        private void ClearProductInputFields()
        {
            ProductNameTextBox.Clear();
            DescriptionTextBox.Clear();
            UnitPriceTextBox.Clear();
            UnitsInStockTextBox.Clear();
            CategoryComboBox.SelectedIndex = -1;
        }


        private void ListOfProductsButton_Click(object sender, RoutedEventArgs e)
        {
            var productService = new ProductManager(new EfProductDal());

            // Tüm ürünleri çek
            List<Product> productList = productService.GetAll();

            // Çekilen ürünleri DataGrid'e bağla
            listOfProduct.ItemsSource = productList;
        }

        private void MakeReservationButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ProcessPaymentButton_Click(object sender, RoutedEventArgs e)
        {

        }

    }
}
