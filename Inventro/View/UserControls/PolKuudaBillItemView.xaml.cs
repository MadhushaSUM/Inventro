using Inventro.Utils;
using System.Windows;
using System.Windows.Controls;

namespace Inventro.View.UserControls
{
    public partial class PolKuudaBillItemView : UserControl
    {
        private readonly decimal billValue;
        private readonly Product product;
        private readonly decimal measuredWeight;
        private readonly List<decimal> measuredWeights;
        private readonly decimal containerWeight;
        private readonly decimal minusWeight;

        public PolKuudaBillItemView(Product product, List<decimal> measuredWeights, decimal containerWeight, decimal minusWeight, StackPanel billStack)
        {
            InitializeComponent();

            this.product = product;
            this.measuredWeights = measuredWeights;
            this.containerWeight = Math.Round(containerWeight, 3);
            this.minusWeight = Math.Round(minusWeight, 3);

            btnRemove.Click += (sender, e) =>
            {
                billStack.Children.Remove(this);
            };            

            lblItemName.Content = product.getProductName();
            lblDailyRate.Content = product.getDailyRate();

            for (int i = 0; i < measuredWeights.Count; i++)
            {
                measuredWeight += measuredWeights[i];

                Grid grid = new();
                ColumnDefinition col1 = new()
                {
                    Width = new GridLength(1, GridUnitType.Star)
                };
                ColumnDefinition col2 = new()
                {
                    Width = new GridLength(1, GridUnitType.Star)
                };
                ColumnDefinition col3 = new()
                {
                    Width = new GridLength(1, GridUnitType.Star)
                };
                grid.ColumnDefinitions.Add(col1);
                grid.ColumnDefinitions.Add(col2);
                grid.ColumnDefinitions.Add(col3);

                Label lblTurn = new()
                {
                    Content = $"{i + 1} වන වටය",
                    Style = (Style)Application.Current.Resources["billLabel"],
                    FontSize = 11
                };
                Grid.SetColumn(lblTurn, 0);
                grid.Children.Add(lblTurn);

                Label lblWeight = new()
                {
                    Content = $"{Math.Round(measuredWeights[i] ,3):F3} kg",
                    Style = (Style)Application.Current.Resources["billLabel"],
                    FontSize = 11,
                    HorizontalContentAlignment = HorizontalAlignment.Right
                };
                Grid.SetColumn(lblWeight, 1);
                grid.Children.Add(lblWeight);

                stackRounds.Children.Add(grid);
            }

            lblContainerWeight.Content = $"{containerWeight:F3} kg";
            lblMinusWeight.Content = $"{minusWeight:F3} kg";

            decimal billWeight = measuredWeight - (containerWeight + minusWeight);

            lblBillWeight.Content = $"{billWeight:F3} kg";

            billValue = Math.Round(billWeight * product.getDailyRate(), 2);
            lblBillValue.Content = "රු. " + billValue.ToString();
        }

        public decimal GetBillValue() { return billValue; }
        public Product GetProduct() { return product; }
        public decimal GetMeasuredWeight() { return measuredWeight; }
        public List<decimal> GetMeasuredWeightTurns() { return measuredWeights; }
        public decimal GetContainerWeight() { return containerWeight; }
        public decimal GetMinusWeight() { return minusWeight; }
    }
}
