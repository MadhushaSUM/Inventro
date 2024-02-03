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

namespace Inventro.View.UserControls.ProductItems
{
    public partial class WeighingTurn : UserControl
    {
        readonly StackPanel stackPanel;
        public WeighingTurn(StackPanel weighingTurnsStack)
        {
            InitializeComponent();
            stackPanel = weighingTurnsStack;
        }

        public WeightSetter GetWeightSetter()
        {
            return turnWeight;
        }

        private void btnRemove_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (stackPanel.Children.Count > 2)
            {
                stackPanel.Children.Remove(this);
            }
        }
    }
}
