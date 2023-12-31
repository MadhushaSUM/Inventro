using System.Windows;
using System.Windows.Controls;

namespace Inventro.View.UserControls
{
    public delegate void SaveBtnClick(object sender, RoutedEventArgs e);
    public partial class DawaseMilaView : UserControl
    {
        public DawaseMilaView(string itemName, SaveBtnClick func)
        {
            InitializeComponent();

            lblItemName.Content = itemName;
            btnItemPriceSave.Click += new RoutedEventHandler(func);
        }
    }
}
