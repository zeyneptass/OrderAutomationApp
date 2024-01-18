using Business;
using Business.Abstract;
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




        //public class Table
        //{
        //    public int TableNumber { get; set; }
        //    public string TableUrl { get; set; }
        //}

        //// Masa butonlarına tıklandığında çalışacak olay işleyicisi
        //private void TableButtonClick(object sender, RoutedEventArgs e)
        //{
        //    if (sender is Button button && button.DataContext is Table secilenMasa)
        //    {
        //        // Seçilen masa ile ilgili bilgileri göster
        //        MessageBox.Show($"Rezervasyon ID: {secilenMasa.TableNumber}\nMasa ID: {secilenMasa.TableNumber}\nMüşteri ID: {secilenMasa.TableNumber}\nRezervasyon Tarihi: {DateTime.Now}");
        //    }
        //}

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                // Image'in Tag özelliğinden masa numarasını al
                int masaNumarasi = Convert.ToInt32((sender as Image)?.Tag);

                // TableManager'ı kullanarak masaları çek
                TableManager tableManager = new TableManager(new EfTableDal()); // Tablo verilerini çekmek için uygun bir TableDal ekleyin

                // İlgili masa numarasına ait masaları al
                List<Entities.Concrete.Table> masalar = tableManager.GetByTableId(masaNumarasi);

                // Eğer masa varsa işlemleri gerçekleştir
                if (masalar.Any())
                {
                    // İlgili masa numarasına ait bilgileri kullanarak MessageBox göster
                    Entities.Concrete.Table relatedTable = masalar.FirstOrDefault();
                    string message = $"Masa: {relatedTable?.TableNumber}, Resim: {relatedTable?.TableUrl}\n";

                    // İlgili masa numarasına ait rezervasyonları çek
                    ReservationManager reservationManager = new ReservationManager(new EfReservationDal());
                    List<Reservation> masaRezervasyonlari = reservationManager.GetDetails("", masaNumarasi, DateTime.Now);

                    foreach (var rezervasyon in masaRezervasyonlari)
                    {
                        message += $"Müşteri: {rezervasyon.CustomerName}, Tarih: {rezervasyon.ReservationDate}\n";
                    }

                    MessageBox.Show(message);
                }
                else
                {
                    // Eğer masa yoksa kullanıcıya bir mesaj göster
                    MessageBox.Show("Bu masa için bilgi bulunmamaktadır.");
                }
            }
            catch (Exception ex)
            {
                // Hata yönetimini gerçekleştirin
                MessageBox.Show($"Hata oluştu: {ex.Message}");
            }
        }

        private void AddOrderButton_Click(object sender, RoutedEventArgs e)
        {
            var productService = new ProductManager(new EfProductDal());


            string produtName = ProductName_TextBox.Text;
            int tableId = Convert.ToInt16(TableIdTextBox.Text);
            int quantity = Convert.ToInt16(QuantityTextBox.Text);
            decimal totalPrice = Convert.ToDecimal(TotalPriceTextBox.Text);
            DateTime OrderDatePicker = DateTime.Now;
            var newOrder = new Order { };

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
        

            // Refresh the data grid if necessary
            // OrdersDataGrid.ItemsSource = GetOrdersData(); // Update this line with the appropriate method

            // Clear input fields or perform any other necessary UI updates
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
                MessageBox.Show($"Ürün '{productNameToDelete}' başarıyla silindi.");
            }
            else
            {
                // Optional: Show a message indicating that the product was not found
                MessageBox.Show($"Ürün '{productNameToDelete}' bulunamadı.");
            }
        }

        private void ListOfProductsButton_Click(object sender, RoutedEventArgs e)
        {

            //var productService = new ProductManager(new EfProductDal());


            //// Get the product name and category name from UI controls
            //string productName = listProductName.Text;
            //string categoryName = AllCatgoriesForListOfProduct.Text;

            //// Use the GetByName method to get products by name
            //var products = productService.GetByName(productName);
            //listOfProduct.ItemsSource = products;

            //// Get category ID by name
            //int categoryId = GetAllByCategoryName(categoryName);

            //// Use GetAllByCategoryId method to get products by category ID
            //var categoryProducts = productService.GetAllByCategoryId(categoryId);
            //listOfProduct.ItemsSource = categoryProducts;

            //// Refresh the DataGrid
            //listOfProduct.Items.Refresh();
        }


        private void MakeReservationButton_Click(object sender, RoutedEventArgs e)
        {
            var reservationService = new ReservationManager(new EfReservationDal());

            // ComboBox'tan seçili değeri al
            string selectedTableId = TableIdComboBox.SelectedItem as string;

            // Seçili değer null değilse ve boş bir değer içermiyorsa işlemleri yap
            if (!string.IsNullOrEmpty(selectedTableId))
            {
                // Seçili değeri kullanarak TableId'yi al
                int tableId;
                if (int.TryParse(selectedTableId, out tableId))
                {
                    // Diğer girişleri al
                    string customerName = CustomerNameTextBox.Text;
                    DateTime reservationDate = ReservationDatePicker.SelectedDate ?? DateTime.Now; // Default olarak şu anki tarih

                    // Yeni rezervasyon oluştur
                    var newReservation = new Reservation
                    {
                        TableId = tableId,
                        CustomerName = customerName,
                        ReservationDate = reservationDate
                    };

                    // ListView'e ekle
                    listOfReservation.Items.Add(newReservation);
                    reservationService.Add(newReservation);

                    // İlgili alanları temizle
                    ClearReservationInputFields();
                }
                else
                {
                    MessageBox.Show("Geçersiz Table ID seçimi.");
                }
            }
            else
            {
                MessageBox.Show("Lütfen bir Table ID seçin.");
            }
        }

        // Diğer yardımcı metotları ekleyebilirsiniz
        private void ClearReservationInputFields()
        {
            // Giriş alanlarını temizle
            TableIdComboBox.SelectedIndex = -1;
            CustomerNameTextBox.Clear();
            ReservationDatePicker.SelectedDate = DateTime.Now;
        }






        private void ProcessPaymentButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

    
    }
}
