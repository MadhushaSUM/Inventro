using System.Windows.Controls;
using System.Text.Json;
using System.Windows;
using System.IO;

namespace Inventro.Utils
{
    public class SavePage
    {
        public static void SavePageComponent(List<Label> labels, string folderName)
        {
            foreach (Label label in labels)
            {
                LabelSavingFormat format = new()
                {
                    Name = label.Name,
                    Content = label.Content.ToString(),
                    FontSize = label.FontSize,
                    FontFamily = label.FontFamily.ToString(),
                    ContentVerticalyCentered = label.VerticalContentAlignment == VerticalAlignment.Center,
                    ContentHorizontalyCentered = label.HorizontalContentAlignment == HorizontalAlignment.Center,
                    TopMargin = Canvas.GetTop(label),
                    LeftMargin = Canvas.GetLeft(label),
                    Width = label.Width,
                    Height = label.Height,
                    FontWeight = label.FontWeight == FontWeights.Bold,
                    FontStyle = label.FontStyle == FontStyles.Italic,
                };

                string labelProperties = JsonSerializer.Serialize(format);
                string filePath = "BillLayout/" + folderName + "/" + label.Name + ".json";
                
                File.WriteAllText(filePath, labelProperties);
            }

            MessageBox.Show("Layout Saved", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
