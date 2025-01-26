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
    /// Логика взаимодействия для basket_page.xaml
    /// </summary>
    public partial class basket_page : Page
    {
        public basket_page(string a, string b, string c, string d, string f, string g, string h, string j, string k, string l, int x1, int x2, int x3, int x4, int x5, int x6, int x7, int x8, int x9, int x10)
        {
            InitializeComponent();
            var currentProducts = SunArt_ShusharinaEntities.GetContext().Product.ToList();
            //basket_lb.ItemsSource = currentProducts.OrderBy(p => p.Title).ToList();
            double sum = 0;
            txtbl.Text += "Итого: ";
            if (a != null)
            {
                basket_lb.Items.Add(a);
                //txtbl.Text += a + Environment.NewLine;
                sum += 7000 * x1;
            }
            if (b != null)
            {
                basket_lb.Items.Add(b);
                //txtbl.Text += b + Environment.NewLine;
                sum +=10000 * x2;
            }
            if (c != null)
            {
                basket_lb.Items.Add(c);
                //txtbl.Text += c + Environment.NewLine;
                sum += 6000 * x3;
            }
            if (d != null)
            {
                basket_lb.Items.Add(d);
                //txtbl.Text += d + Environment.NewLine;
                sum += 5000 * x4;
            }
            if (f != null)
            {
                basket_lb.Items.Add(f);
                //txtbl.Text += f + Environment.NewLine;
                sum += 3230 * x5;
            }
            if (g != null)
            {
                basket_lb.Items.Add(g);
                //txtbl.Text += g + Environment.NewLine;
                sum += 7000 * x6;
            }
            if (h != null)
            {
                basket_lb.Items.Add(h);
                //txtbl.Text += h + Environment.NewLine;
                sum += 3000 * x7;
            }
            if (j != null)
            {
                basket_lb.Items.Add(j);
                //txtbl.Text += j + Environment.NewLine;
                sum += 2000 * x8;
            }
            if (k != null)
            {
                basket_lb.Items.Add(k);
                //txtbl.Text += k + Environment.NewLine;
                sum += 8000 * x9;
            }
            if (l != null)
            {
                basket_lb.Items.Add(l);
                //txtbl.Text += l + Environment.NewLine;
                sum += 2000 * x10;
            }
            txtbl.Text += sum.ToString();
        }

        private void oplata_Click(object sender, RoutedEventArgs e)
        {
            if (basket_lb.Items[0] != null)
            {
                MessageBox.Show("Спасибо за покупку!");
                Mananger.MainFrame.GoBack();
            }
        }
    }
}
