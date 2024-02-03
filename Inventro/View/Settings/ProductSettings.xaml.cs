using Inventro.Utils;
using Microsoft.Win32;
using MySql.Data.MySqlClient;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace Inventro.View.Settings
{
    public partial class ProductSettings : UserControl
    {
        private OpenFileDialog ofd = new()
        {
            Filter = "Image files (*.png;)|*.png;"
        };
        private bool? isImageSelected = null;

        public ProductSettings()
        {
            InitializeComponent();

            try
            {
                dgProducts.AutoGenerateColumns = false;
                dgProducts.ItemsSource = Database.getProducts().DefaultView;
            }
            catch (MySqlException)
            {
                MessageBox.Show("Error loading products", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnSelectImage_Click(object sender, RoutedEventArgs e)
        {
            isImageSelected = ofd.ShowDialog();
        }

        private void btnAddProduct_Click(object sender, RoutedEventArgs e)
        {

            if (txtProductCode.Text == "")
            {
                MessageBox.Show("Enter a valid product code", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }
            if (txtProductName.Text == "")
            {
                MessageBox.Show("Enter a valid product name", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }
            if (isImageSelected != true)
            {
                MessageBox.Show("Select an image", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }


            try
            {
                string exeDirectory = AppDomain.CurrentDomain.BaseDirectory;
                string destination = System.IO.Path.Combine(exeDirectory, "Pictures", $"{txtProductCode.Text}.png");

                if (File.Exists(ofd.FileName))
                {
                    File.Copy(ofd.FileName, destination);
                }
                else
                {
                    MessageBox.Show("Error occured while fetching product image. Product will be added without a image. You can add a image to the Pictures directory later. Remember to rename it with the PRODUCT_CODE", "Error fetching the image", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                
                Database.addProduct(txtProductCode.Text, txtProductName.Text);
                MessageBox.Show("Product Added", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (MySqlException)
            {
                MessageBox.Show("Procedure Failed", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
