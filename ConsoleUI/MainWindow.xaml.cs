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

        }

        private void MakeReservationButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ProcessPaymentButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
