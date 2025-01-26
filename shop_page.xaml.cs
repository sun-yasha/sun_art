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
using System.Windows.Threading;

namespace KR_SunArt_shusharina
{
    /// <summary>
    /// Логика взаимодействия для shop_page.xaml
    /// </summary>
    public partial class shop_page : Page
    {
        private int currentProduct = 1;
        private int itemsPage = 20;
        private int totalItems;
        private List<Product> allProduct;
        private int totalPages;
        private DispatcherTimer timer;
        public int CurrentPage { get => currentProduct; set => currentProduct = value; }
        public int ItemsPage { get => itemsPage; set => itemsPage = value; }
        public int TotalItems { get => totalItems; set => totalItems = value; }
        public List<Product> AllProduct { get => allProduct; set => allProduct = value; }
        public int TotalPages { get => totalItems; set => totalItems = value; }
        private DispatcherTimer Timer { get => timer; set => timer = value; }


        public shop_page()
        {
            InitializeComponent();
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

            //var allSort = SunArt_shusharina_KREntities.GetContext().Product.ToList();
            //allSort.Insert(0, new Product
            //{
            //    Title = "Не выбрано"
            //});
            //filter.ItemsSource = allSort;
            //filter.SelectedIndex = 0;
            UpdateProducts();
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            var item = (Product)DGrid.SelectedItem;
            Mananger.MainFrame.Navigate(new AddEdit_page(item));
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            Mananger.MainFrame.Navigate(new AddEdit_page(null));
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            var productForRemoving = DGrid.SelectedItems.Cast<Product>().ToList();
            if (MessageBox.Show($"Вы уверены, что хотите удалить {productForRemoving.Count()} элементов?", "Внимание!", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    SunArt_ShusharinaEntities.GetContext().Product.RemoveRange(productForRemoving);
                    SunArt_ShusharinaEntities.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены!");
                    DGrid.ItemsSource = SunArt_ShusharinaEntities.GetContext().Product.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                SunArt_ShusharinaEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                DGrid.ItemsSource = SunArt_ShusharinaEntities.GetContext().Product.ToList();
            }
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


        private void poisk_TextChanged(object sender, TextChangedEventArgs e)
        {
            {
                var Pro = SunArt_ShusharinaEntities.GetContext().Product.ToList();

                Pro = Pro.Where(p => p.Title.ToLower().Contains(poisk.Text.ToLower())).ToList();
                DGrid.ItemsSource = Pro.OrderBy(p => p.Title).ToList();
            }
        }

        private void filter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateProducts();
        }

        private void sort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateProducts();
        }

        private void color_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateProducts();
        }
    }
}
