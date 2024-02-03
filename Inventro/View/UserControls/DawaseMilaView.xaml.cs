using Inventro.Utils;
using System.Windows.Controls;

namespace Inventro.View.UserControls
{
    public partial class DawaseMilaView : UserControl
    {
        private Product product;
        public DawaseMilaView(Product product)
        {
            InitializeComponent();

            this.product = product;
            lblItemName.Content = product.getProductName();
            txtItemPrice.Text = product.getDailyRate().ToString();
        }

        public Product GetProduct() { return product; }
        public decimal GetDailyRate()
        {
            try
            {
                return Math.Round(Convert.ToDecimal(txtItemPrice.Text), 2);
            } catch
            {
                return 0;
            }
        }

        private void txtItemPrice_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                Convert.ToDecimal(txtItemPrice.Text);
            } catch
            {
                txtItemPrice.Text = "0";
            }
        }
    }
}
