using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Media;
using DRWColor = System.Drawing.Color;
using WMColor = System.Windows.Media.Color;

namespace WordleApp
{
    /// <summary>
    /// The Wordle engine uses a more generic data type to store colours compared to the one that WPF uses.
    /// This class converts .NET's generic colour data type from System.Drawing to the type
    /// used by WPF found in System.Windows.Media.
    /// </summary>
    public static class ColourConverter
    {
        public static SolidColorBrush GetColour(DRWColor colour)
        {
            var brushColour = WMColor.FromArgb(colour.A, colour.R, colour.G, colour.B);
            return new SolidColorBrush(brushColour);
        }
    }
}
