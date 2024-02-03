using Inventro.Utils;

namespace Inventro
{
    internal interface WeightItem
    {
        public decimal getWeights();
        public Product getProduct();
        public decimal getContainerWeight();
        public decimal getMinusWeight();
        public List<decimal> getWeightTurns();
    }
}
