using Inventro.Utils;
using System.Windows.Controls;

namespace Inventro.View.UserControls.ProductItems
{
    public partial class GeneralItem : UserControl, WeightItem
    {
        private readonly Product product;
        public GeneralItem(Product product)
        {
            InitializeComponent();

            this.product = product;
        }

        public decimal getWeights()
        {
            return wsWeight.getValue();
        }
        public decimal getMinusWeight()
        {
            return wsMinus.getValue();
        }
        public decimal getContainerWeight()
        {
            return wsContainer.getValue();
        }
        public Product getProduct()
        {
            return product;
        }
        public List<decimal> getWeightTurns() 
        {
            return null;
        }
    }
}
