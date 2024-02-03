using Inventro.Utils;
using System.Windows;
using System.Windows.Controls;

namespace Inventro.View.UserControls.ProductItems
{
    public partial class PolKuudaItem : UserControl, WeightItem
    {
        private readonly Button btnNewTurn = new();
        private readonly Product product;
        public PolKuudaItem(Product product)
        {
            InitializeComponent();

            this.product = product;

            weighingTurnsStack.Children.Clear();
            weighingTurnsStack.Children.Add(new WeighingTurn(weighingTurnsStack));

            btnNewTurn.Content = "ඊලග වටය";
            btnNewTurn.FontSize = 20;
            btnNewTurn.Width = 120;
            btnNewTurn.Style = (Style)Application.Current.Resources["nextTurnButton"];
            btnNewTurn.Click += BtnNewTurn_click;


            weighingTurnsStack.Children.Add(btnNewTurn);
        }

        private void BtnNewTurn_click(object sender, RoutedEventArgs e)
        {
            weighingTurnsStack.Children.Remove(btnNewTurn);
            weighingTurnsStack.Children.Add(new WeighingTurn(weighingTurnsStack));
            weighingTurnsStack.Children.Add(btnNewTurn);
        }

        public decimal getWeights()
        {
            weighingTurnsStack.Children.Remove(btnNewTurn);

            decimal weights = 0;
            foreach (WeighingTurn turn in weighingTurnsStack.Children)
            {
                weights += turn.GetWeightSetter().getValue();
            }

            weighingTurnsStack.Children.Add(btnNewTurn);

            return weights;
        }
        public List<decimal> getWeightTurns()
        {
            weighingTurnsStack.Children.Remove(btnNewTurn);

            List<decimal> weights = [];
            foreach (WeighingTurn turn in weighingTurnsStack.Children)
            {
                weights.Add(turn.GetWeightSetter().getValue());
            }

            weighingTurnsStack.Children.Add(btnNewTurn);

            return weights;
        }
        public decimal getContainerWeight()
        {
            return wsContainer.getValue() * (weighingTurnsStack.Children.Count - 1);
        }
        public decimal getMinusWeight()
        {
            return wsMinus.getValue();
        }
        public Product getProduct()
        {
            return product;
        }
    }
}
