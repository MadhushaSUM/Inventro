using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventro.Utils
{
    public class LabelSavingFormat
    {
        public string Name { get; set; }
        public string Content { get; set; }
        public double FontSize { get; set; }
        public string FontFamily { get; set; }
        public bool ContentVerticalyCentered { get; set; }
        public bool ContentHorizontalyCentered { get; set; }
        public double TopMargin { get; set; }
        public double LeftMargin { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public bool FontWeight { get; set; }
        public bool FontStyle { get; set; }
    }
}
