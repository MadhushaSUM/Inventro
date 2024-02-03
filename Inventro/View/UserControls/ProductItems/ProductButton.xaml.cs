using Inventro.Utils;
using System.IO;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Inventro.View.UserControls.ProductItems
{
    public partial class ProductButton : UserControl
    {
        public ProductButton(Product product)
        {
            InitializeComponent();

            lblProductName.Content = product.getProductName();

            string exeDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string imagePath = Path.Combine(exeDirectory, "Pictures", $"{product.getProductCode()}.png");
            try
            {
                btnImage.Source = new BitmapImage(new Uri(imagePath));
            } catch (Exception) {
                // ignore if the button images directory or file is not found
            }
        }

        public Button GetButton()
        {
            return btnProduct;
        }
        public Border GetBorder() 
        {
            return productButtonBorder;
        }
    }
}
