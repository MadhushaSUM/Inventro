using Inventro.Utils;
using System.Windows.Controls;

namespace Inventro.View.UserControls
{
    public partial class GeneralBillItemView : UserControl
    {
        private readonly decimal billValue;
        private readonly Product product;
        private readonly decimal measuredWeight;
        private readonly decimal containerWeight;
        private readonly decimal minusWeight;
        public GeneralBillItemView(Product product, decimal measuredWeight, decimal containerWeight, decimal minusWeight, StackPanel billStack)
        {
            InitializeComponent();

            this.product = product;
            this.measuredWeight = Math.Round(measuredWeight, 3);
            this.containerWeight = Math.Round(containerWeight, 3);
            this.minusWeight = Math.Round(minusWeight, 3);

            btnRemove.Click += (sender, e) =>
            {
                billStack.Children.Remove(this);
            };

            lblItemName.Content = product.getProductName();
            lblDailyRate.Content = product.getDailyRate();  
            lblMeasuredTotalWeight.Content = $"{measuredWeight:F3} kg";
            lblContainerWeight.Content = $"{containerWeight:F3} kg";
            lblMinusWeight.Content = $"{minusWeight:F3} kg";

            decimal billWeight = measuredWeight - (containerWeight + minusWeight);

            lblBillWeight.Content = $"{billWeight:F3} kg";


            billValue = Math.Round(billWeight * product.getDailyRate(), 2);
            lblBillValue.Content = "රු. " + billValue.ToString();
        }

        public decimal getBillValue() { return billValue; }
        public Product GetProduct() { return product; }
        public decimal getMeasuredWeight() { return measuredWeight; }
        public decimal getContainerWeight() { return containerWeight; }
        public decimal getMinusWeight() { return minusWeight; }
    }
}
