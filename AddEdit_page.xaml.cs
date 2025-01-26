using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
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
using System.IO;
using Path = System.IO.Path;
using System.Data.Entity.Validation;

namespace KR_SunArt_shusharina
{
    /// <summary>
    /// Логика взаимодействия для AddEdit_page.xaml
    /// </summary>
    public partial class AddEdit_page : Page
    {
        private Product _currentProduct = new Product();
        private string _selectedImagePath;

        public string SelectedImagePath { get => _selectedImagePath; set => _selectedImagePath = value; }
        public AddEdit_page(Product selectedProduct)
        {
            InitializeComponent();

            if (selectedProduct != null) _currentProduct = selectedProduct;

            DataContext = _currentProduct;
            material.ItemsSource = SunArt_ShusharinaEntities.GetContext().ProductType.ToList();
        }

        private void uploadImage_Click(object sender, RoutedEventArgs e)
        {
            //try
            //{
            //    if (!fileDialog.ShowDialog().Value) return;
            //    if (new FileInfo(fileDialog.FileName).Lenght > 1024 * 1024 * 2)
            //    {
            //        MessageBox.Show("Размер изображения не должен превышать 2МБ");
            //        fileDialog.FileName = null;
            //        return;
            //    }
            //    MainImage.Source = new BitmapImage(new Uri(fileDialog.FileName));
            //}
            //catch
            //{
            //    MessageBox.Show("Ошибка");
            //}

            //OpenFileDialog openFileDialog = new OpenFileDialog();
            //openFileDialog.ShowDialog();
            //byte[] image_bytes = File.ReadAllBytes(openFileDialog.FileName); // получаем байты выбранного файла

            //string connectionString = "server=DESKTOP-AS1K7CN;Trusted_Connection=No;DataBase=SunArt_Shusharina;Integrated Security=True";
            //// строка подключения
            //using (SqlConnection connection = new SqlConnection(connectionString))
            //{
            //    connection.Open();
            //    SqlCommand command = new SqlCommand();
            //    command.Connection = connection;
            //    command.CommandText = @"INSERT INTO Product VALUES (@Image)";
            //    command.Parameters.Add("@Image", SqlDbType.Image, 1000000);
            //    command.Parameters["@Image"].Value = image_bytes;// скалярной переменной ImageData присвоем массив байтов
            //    command.ExecuteNonQuery();
            //}


            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.png;*.jpeg;*.jpg)|*.png;*.jpeg;*.jpg|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                SelectedImagePath = openFileDialog.FileName;
                MainImage.Source = new BitmapImage(new Uri(SelectedImagePath, UriKind.Absolute));
            }

        }

        private void deleteImage_Click(object sender, RoutedEventArgs e)
        {
            MainImage.Source = null;
            SelectedImagePath = null;
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            {
                StringBuilder errors = new StringBuilder();
                if (string.IsNullOrWhiteSpace(_currentProduct.Title))
                    errors.AppendLine("Укажите название");
                if (_currentProduct.Cost == null)
                    errors.AppendLine("Укажите цену");
                if (_currentProduct.Count == null)
                    errors.AppendLine("Укажите количество");

                if (errors.Length > 0)
                {
                    MessageBox.Show(errors.ToString());
                    return;
                }

                try
                {
                    SunArt_ShusharinaEntities.GetContext().Product.Add(_currentProduct);
                    if (!string.IsNullOrEmpty(_selectedImagePath))
                    {
                        string imagesDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "products");
                        if (!Directory.Exists(imagesDirectory))
                        {
                            Directory.CreateDirectory(imagesDirectory);
                        }
                        string fileName = Path.GetFileName(_selectedImagePath);
                        string destinationPath = Path.Combine(imagesDirectory, fileName);
                        File.Copy(_selectedImagePath, destinationPath, true);
                        _currentProduct.Image = destinationPath;
                    }
                    try
                {
                    SunArt_ShusharinaEntities.GetContext().SaveChanges();
                    MessageBox.Show("Информация сохранена");
                    Mananger.MainFrame.Navigate(new shop_page());
                    Mananger.MainFrame.Refresh();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
                    catch (Exception ex)
                {
                MessageBox.Show(ex.Message.ToString());
                }
            }
        }
    }
}
