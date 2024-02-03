using Inventro.View.UserControls;
using System.Windows.Controls;

namespace Inventro.Utils
{
    public class PrintBill
    {
        private static List<Label> headerLabels = [];
        private static List<Label> billItemLabels = [];
        private static List<Label> footerLabels = [];

        public static void Print(List<UserControl> billItems, int transactionID)
        {
            CreatePage page = new();

            StackPanel bill = new()
            {
                Width = 300,
            };

            Canvas header = page.CreateHeader(headerLabels, transactionID);
            headerLabels[2].Content = DateTime.Now.ToString();
            bill.Children.Add(header);

            
            decimal total = 0;
            foreach (var billItem in billItems)
            {
                if (billItem is GeneralBillItemView generalBillItem)
                {
                    bool containerFlag = generalBillItem.getContainerWeight() == 0;
                    bool minusFlag = generalBillItem.getMinusWeight() == 0;
                    Canvas item = page.CreateBillItem(billItemLabels, containerFlag, minusFlag);

                    billItemLabels[1].Content = generalBillItem.GetProduct().getProductName();
                    billItemLabels[2].Content = $"( 1kg = රු.{generalBillItem.GetProduct().getDailyRate()})";
                    billItemLabels[4].Content = $"{Math.Round(generalBillItem.getMeasuredWeight(), 3):F3} kg";

                    int i = 0;
                    if (!containerFlag)
                    {
                        billItemLabels[6].Content = $"{Math.Round(generalBillItem.getContainerWeight(), 3):F3} kg";
                    }else
                    {
                        i += 2;
                    }
                    if (!minusFlag)
                    {
                        billItemLabels[8 - i].Content = $"{Math.Round(generalBillItem.getMinusWeight(), 3):F3} kg";
                    }else
                    {
                        i += 2;
                    }

                    decimal totalWeight = Math.Round(generalBillItem.getMeasuredWeight() - (generalBillItem.getContainerWeight() + generalBillItem.getMinusWeight()), 3);
                    billItemLabels[9 - i].Content = $"{totalWeight:F3} kg";

                    decimal price = Math.Round(totalWeight * generalBillItem.GetProduct().getDailyRate(), 2);
                    billItemLabels[10 - i].Content = $"රු. {price:F2}";

                    total += price;

                    bill.Children.Add(item);
                }
                else if (billItem is PolKuudaBillItemView polkuudaBillItem)
                {
                    Canvas item = page.CreatePolKuudaBillItem(
                        polkuudaBillItem.GetProduct(),
                        polkuudaBillItem.GetMeasuredWeightTurns(),
                        polkuudaBillItem.GetContainerWeight(),
                        polkuudaBillItem.GetMinusWeight());

                    total += Math.Round(polkuudaBillItem.GetMeasuredWeight() * polkuudaBillItem.GetProduct().getDailyRate(), 2);

                    bill.Children.Add(item);
                }
            }

            Canvas footer = page.CreateFooter(footerLabels);

            footerLabels[1].Content = $"රු. {total:F2}";
            bill.Children.Add(footer);
            

            PrintDialog printDialog = new();

            printDialog.PrintVisual(bill, "Bill");     

        }
    }
}
