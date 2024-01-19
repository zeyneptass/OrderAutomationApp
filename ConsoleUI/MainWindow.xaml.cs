using Business;
using Business.Abstract;
using Business.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;

using Entities.Concrete;
using Microsoft.VisualBasic;
using Serilog;
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
        // Reszervasyon bilgilerini ViewReservation'da görüntülemek için
        private IReservationService _reservationService;
        public MainWindow()
        {
            InitializeComponent();

            GetCategoriesForComboBox();

            List<TableInfo> MasaListesi = new List<TableInfo>();

            // Örnek masa verilerini ekleyin (Bu kısmı projenize uygun şekilde güncelleyin)
            for (int i = 1; i <= 15; i++)
            {
                MasaListesi.Add(new TableInfo { TableNumber = i, TableUrl = @"C:\Users\zeyne\OneDrive\Masaüstü\OOPLectures\CafeAutomationApp\ConsoleUI\Icon\chair.png" });
            }

            //XAML içindeki ItemsControl'a MasaListesi'ni bağla
            masaItemsControl.ItemsSource = MasaListesi;

          
            // ComboBox'a değerleri ekleyin
            string[] tableIdValues = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10","11","12","13","14","15" };
            TableIdComboBox.ItemsSource = tableIdValues;

            _reservationService = new ReservationManager(new EfReservationDal());
        }
        public class TableInfo
        {
            public int TableNumber { get; set; }
            public string TableUrl { get; set; }
        }

        private void GetCategoriesForComboBox()
        {
            // ProductCategoryManager'ı oluştur
            IProductCategoryService productCategoryService = new ProductCategoryManager(new EfCategoryDal());

            // Tüm kategorileri çek
            List<Category> categories = productCategoryService.GetAllProductCategories();

            // Kategori ComboBox'ını doldur
            CategoryComboBox.ItemsSource = categories;
            CategoryComboBox.DisplayMemberPath = "CategoryName"; // Kategori ismini göstermek için

            AllCatgoriesForListOfProduct.ItemsSource = categories;
            AllCatgoriesForListOfProduct.DisplayMemberPath = "CategoryName";
        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                int masaNumarasi = 0;

                if (sender is FrameworkElement element)
                {
                    if (element.Tag is int tagNumarasi)
                    {
                        masaNumarasi = tagNumarasi;
                    }
                    else if (element.DataContext is Entities.Concrete.Table secilenMasa)
                    {
                        masaNumarasi = secilenMasa.TableNumber;
                    }
                }

                if (masaNumarasi != 0)
                {
                    ITableService tableService = new TableManager(new EfTableDal());
                    IReservationService reservationService = new ReservationManager(new EfReservationDal());

                    // Masa verilerini çekme
                    List<Entities.Concrete.Table> tables = tableService.GetByTableId(masaNumarasi);

                    // Rezervasyon verilerini çekme
                    List<Reservation> reservations = reservationService.GetByTableId(masaNumarasi);

                    // MessageBox ile kullanıcıya gösterme
                    StringBuilder message = new StringBuilder();
                    message.AppendLine($"Masa Bilgileri (Masa {masaNumarasi}):");
                    foreach (var table in tables)
                    {
                        message.AppendLine($"Masa numarası: {table.TableNumber}, Kapasite: {table.Capacity}");
                    }

                    message.AppendLine();
                    message.AppendLine($"Rezervasyon Bilgileri (Masa {masaNumarasi}):");
                    foreach (var reservation in reservations)
                    {
                        message.AppendLine($"Rezervasyon ID: {reservation.ReservationId}, Müşteri Adı: {reservation.CustomerName}, Tarih: {reservation.ReservationDate}");
                    }

                    MessageBox.Show(message.ToString(), "Masa ve Rezervasyon Bilgileri", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    // Eğer masa numarası alınamazsa kullanıcıya bir mesaj göster
                    MessageBox.Show("Masa numarası alınamadı.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}");
            }
        }


        // TabItem'e tıklandığında gerçekleşen olay handler'ı
        private void AddOrderTabItem_Selected(object sender, RoutedEventArgs e)
        {
            try
            {
                var productService = new ProductManager(new EfProductDal());

                var allProducts = productService.GetAll();
                allProductsForOrder.ItemsSource = allProducts;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}");
            }
        }
        private void AllProductsForOrder_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedProduct = allProductsForOrder.SelectedItem as Product;

            if (selectedProduct != null)
            {
                ProductName_TextBox.Text = selectedProduct.ProductName;
            }
        }
        private void AddOrderButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string productName = ProductName_TextBox.Text;
                int tableId = Convert.ToInt16(TableIdTextBox.Text);
                int quantity = Convert.ToInt16(QuantityTextBox.Text);

                DateTime? selectedDate = OrderDatePicker.SelectedDate;

                // Eğer bir tarih seçilmişse, DateTime türüne çevir, aksi halde varsayılan bir değer kullan
                DateTime orderDate = selectedDate ?? DateTime.MinValue;

                var productService = new ProductManager(new EfProductDal());
                var orderService = new OrderManager(new EfOrderDal());

                var product = productService.GetProductByName(productName);

                if (product != null)
                {
                    decimal unitPrice = product.UnitPrice;

                    decimal totalPrice = quantity * unitPrice;

                    var newOrder = new Order
                    {
                        TableId = tableId,
                        Quantity = quantity,
                        TotalPrice = totalPrice,
                        OrderDate = orderDate,
                        ProductId = product.ProductId 
                    };
                    orderService.Add(newOrder);
                    listOfOrder.Items.Add(newOrder);
                    
                    MessageBox.Show("Order is added successfully.");
                }
                else
                {
                    MessageBox.Show("Product not found.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
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
            productService.Add(newProduct);

            ClearProductInputFields();
            RefreshProductList();
        }
        public void RefreshProductList()
        {
            var productService = new ProductManager(new EfProductDal());

            // Tüm ürünleri çek ve DataGrid'i güncelle
            var allProducts = productService.GetAll();
            listOfProduct.ItemsSource = allProducts;
            listOfProduct.Items.Refresh();
        }

        private void ClearProductInputFields()
        {
            ProductNameTextBox.Clear();
            DescriptionTextBox.Clear();
            UnitPriceTextBox.Clear();
            UnitsInStockTextBox.Clear();
            CategoryComboBox.SelectedIndex = -1;
        }
        private void Delete_Product_Button_Click(object sender, RoutedEventArgs e)
        {
            var productService = new ProductManager(new EfProductDal());

            string productNameToDelete = deletingProductName.Text;
            var productToDelete = productService.GetByName(productNameToDelete).FirstOrDefault();

            if (productToDelete != null)
            {
                // Delete the product
                productService.Delete(productToDelete);

                // Refresh the product list
                RefreshProductList();

                // Optional: Show a message indicating success
                MessageBox.Show($"Product : '{productNameToDelete}' is deleted successfully");
                deletingProductName.Clear();
            }
            else
            {
                // Optional: Show a message indicating that the product was not found
                MessageBox.Show($"Product :  '{productNameToDelete}' not found.");
            }
        }
        private void ListOfProductsButton_Click(object sender, RoutedEventArgs e)
        {
            var productService = new ProductManager(new EfProductDal());

            // Get the product name from UI control
            string productName = listProductName.Text;

            // Use the GetByName method to get products by name
            var products = productService.GetByName(productName);
            listOfProduct.ItemsSource = products;

            // Refresh the DataGrid
            listOfProduct.Items.Refresh();
            listProductName.Clear();

        }


        private void MakeReservationButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Kullanıcının girdiği bilgileri al
                if (TableIdComboBox.SelectedValue != null)
                {
                    int selectedTableId = Convert.ToInt32(TableIdComboBox.SelectedValue);
                    string customerName = CustomerNameTextBox.Text;
                    DateTime reservationDate = ReservationDatePicker.SelectedDate ?? DateTime.Now;

                    // Gerekli doğrulamaları yapabilirsin
                    if (selectedTableId <= 0 || string.IsNullOrEmpty(customerName) || reservationDate < DateTime.Now)
                    {
                        MessageBox.Show("Please enter valid information.");
                        return;
                    }

                    // Reservation nesnesini oluştur
                    Reservation newReservation = new Reservation
                    {
                        TableId = selectedTableId,
                        CustomerName = customerName,
                        ReservationDate = reservationDate
                    };

                    // Reservation'ı eklemek için ReservationManager kullan
                    ReservationManager reservationManager = new ReservationManager(new EfReservationDal());
                    reservationManager.Add(newReservation);

                    // Başarılı bir şekilde eklediyse bilgi ver
                    MessageBox.Show("The reservation has been created successfully.");

                    // Liste güncellenmeli
                    RefreshReservationList();
                }
                else
                {
                    MessageBox.Show("Please choose a table.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while creating the reservation: {ex.Message}");
                if (ex.InnerException != null)
                {
                    MessageBox.Show($"İç Hata: {ex.InnerException.Message}");
                }
            }

        }

        private void RefreshReservationList()
        {
            ReservationManager reservationManager = new ReservationManager(new EfReservationDal());
            listOfReservation.ItemsSource = reservationManager.GetAll();
        }

        // Diğer yardımcı metotları ekleyebilirsiniz
        private void ClearReservationInputFields()
        {
            // Giriş alanlarını temizle
            TableIdComboBox.SelectedIndex = -1;
            CustomerNameTextBox.Clear();
            ReservationDatePicker.SelectedDate = DateTime.Now;
        }
    }
}
