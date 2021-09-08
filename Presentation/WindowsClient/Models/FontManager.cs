using System;
using System.IO;
using System.Linq;
using System.Drawing;
using System.Drawing.Text;
using System.Runtime.InteropServices;
using System.Collections.Generic;

namespace WindowsClient
{
    public static class FontManager
    {
        private static PrivateFontCollection collection = new();

        public static IEnumerable<string> AvailableFonts 
            => collection.Families.Select(f => f.Name);

        public static void LoadFonts()
        {
            collection.AddFontFile("Resources/Fonts/CascadiaMono.ttf");
            collection.AddFontFile("Resources/Fonts/CascadiaCode.ttf");
        }

        public static Font GetFont(string name, float size)
        {
            var family = collection.Families.First(f => f.Name == name);
            return new Font(family, size);
        }
    }
}
