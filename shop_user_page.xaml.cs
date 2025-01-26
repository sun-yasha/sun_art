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

namespace KR_SunArt_shusharina
{
    /// <summary>
    /// Логика взаимодействия для shop_user_page.xaml
    /// </summary>
    public partial class shop_user_page : Page
    {
        private List<Product> allProduct;
        public List<Product> AllProduct { get => allProduct; set => allProduct = value; }
        private int totalItems;
        public int TotalItems { get => totalItems; set => totalItems = value; }
        private int totalPages;
        public int TotalPages { get => totalItems; set => totalItems = value; }
        private int itemsPage = 20;
        public int ItemsPage { get => itemsPage; set => itemsPage = value; }
        public shop_user_page()
        {
            InitializeComponent();
            //var currentProducts = SunArt_shusharina_KREntities.GetContext().Product.ToList();
            //DGrid.ItemsSource = currentProducts.OrderBy(p => p.Title).ToList();

            AllProduct = SunArt_ShusharinaEntities.GetContext().Product.ToList();
            TotalItems = AllProduct.Count;
            TotalPages = (int)Math.Ceiling((double)TotalItems / ItemsPage);
            var allTypes = SunArt_ShusharinaEntities.GetContext().ProductType.ToList();
            allTypes.Insert(0, new ProductType
            {
                Title = "Все типы"
            });
            sort.ItemsSource = allTypes;
            sort.SelectedIndex = 0;

            var allColor = SunArt_ShusharinaEntities.GetContext().MaterialType.ToList();
            allColor.Insert(0, new MaterialType
            {
                Title = "Все цвета"
            });
            color.ItemsSource = allColor;
            color.SelectedIndex = 0;

        }

        private void poisk_TextChanged(object sender, TextChangedEventArgs e)
        {
            var Pro = SunArt_ShusharinaEntities.GetContext().Product.ToList();

            Pro = Pro.Where(p => p.Title.ToLower().Contains(poisk.Text.ToLower())).ToList();
            DGrid.ItemsSource = Pro.OrderBy(p => p.Title).ToList();
        }

        private void UpdateProducts()
        {
            var currentProducts = SunArt_ShusharinaEntities.GetContext().Product.ToList();
            DGrid.ItemsSource = currentProducts.OrderBy(p => p.Title).ToList();
            currentProducts = currentProducts.Where(p => p.Title.ToLower().Contains(poisk.Text.ToLower())).ToList();

            var selectedType = sort.SelectedItem as ProductType;
            var colorType = color.SelectedItem as MaterialType;
            var products = AllProduct;

            if (selectedType != null && selectedType.Title != "Все типы")
            {
                products = products.Where(p => p.ProductType.Title == selectedType.Title).ToList();
            }

            if (colorType != null && colorType.Title != "Все цвета")
            {
                products = products.Where(p => p.MaterialType.Title == colorType.Title).ToList();
            }

            //products = products.Where(p => p.Title.StartsWith(poisk.Text)).ToList();

            string sortOrder = "Не выбрано";
            if (filter.SelectedItem != null)
            {
                sortOrder = (filter.SelectedItem as ComboBoxItem).Content.ToString();
            }
            switch (sortOrder)
            {
                case "По убыванию":
                    products = products.OrderBy(p => p.Cost).ToList();
                    break;
                case "По возрастанию":
                    products = products.OrderByDescending(p => p.Cost).ToList();
                    break;
                default:
                    break;
            }

            DGrid.ItemsSource = products;
        }

        private void filter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateProducts();
        }

        private void sort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateProducts();
        }

        private void basket_Click(object sender, RoutedEventArgs e)
        {
            Mananger.MainFrame.Navigate(new basket_page(a, b, c, d, f, g, h, j, k, l, x1, x2, x3, x4, x5, x6, x7, x8, x9, x10));
        }

        Product P;
        string a; string b; string c; string d; string f; string g; string h; string j; string k; string l;
        int x1 = 0; int x2 = 0; int x3 = 0; int x4 = 0; int x5 = 0; int x6 = 0; int x7 = 0; int x8 = 0; int x9 = 0; int x10 = 0;
        private void DGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DGrid.SelectedIndex == 0)
            {
                x1++;
                P = (Product)DGrid.SelectedItem;
                a = P.Title + " " + P.Cost + " " + x1.ToString() + "шт";
                MessageBox.Show("Добавлено в корзину!");
            }

            if (DGrid.SelectedIndex == 1)
            {
                x2++;
                P = (Product)DGrid.SelectedItem;
                b = P.Title + " " + P.Cost + " " + x2.ToString() + "шт";
                MessageBox.Show("Добавлено в корзину!");
            }

            if (DGrid.SelectedIndex == 2)
            {
                x3++;
                P = (Product)DGrid.SelectedItem;
                c = P.Title + " " + P.Cost + " " + x3.ToString() + "шт";
                MessageBox.Show("Добавлено в корзину!");
            }

            if (DGrid.SelectedIndex == 3)
            {
                x4++;
                P = (Product)DGrid.SelectedItem;
                d = P.Title + " " + P.Cost + " " + x4.ToString() + "шт";
                MessageBox.Show("Добавлено в корзину!");
            }

            if (DGrid.SelectedIndex == 4)
            {
                x5++;
                P = (Product)DGrid.SelectedItem;
                f = P.Title + " " + P.Cost + " " + x5.ToString() + "шт";
                MessageBox.Show("Добавлено в корзину!");
            }

            if (DGrid.SelectedIndex == 5)
            {
                x6++;
                P = (Product)DGrid.SelectedItem;
                g = P.Title + " " + P.Cost + " " + x6.ToString() + "шт";
                MessageBox.Show("Добавлено в корзину!");
            }

            if (DGrid.SelectedIndex == 6)
            {
                x7++;
                P = (Product)DGrid.SelectedItem;
                h = P.Title + " " + P.Cost + " " + x7.ToString() + "шт";
                MessageBox.Show("Добавлено в корзину!");
            }

            if (DGrid.SelectedIndex == 7)
            {
                x8++;
                P = (Product)DGrid.SelectedItem;
                j = P.Title + " " + P.Cost + " " + x8.ToString() + "шт";
                MessageBox.Show("Добавлено в корзину!");
            }

            if (DGrid.SelectedIndex == 8)
            {
                x9++;
                P = (Product)DGrid.SelectedItem;
                k = P.Title + " " + P.Cost + " " + x9.ToString() + "шт";
                MessageBox.Show("Добавлено в корзину!");
            }

            if (DGrid.SelectedIndex == 9)
            {
                x10++;
                P = (Product)DGrid.SelectedItem;
                l = P.Title + " " + P.Cost + " " + x10.ToString() + "шт";
                MessageBox.Show("Добавлено в корзину!");
            }
        }
    }
}
