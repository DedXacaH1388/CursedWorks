using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Newtonsoft.Json;
using System.IO;

namespace Warehouse.Classes
{
    public class Palette
    {
        public Color text;
        public Color background;
        public Color backgroundAccent;
        public Color themeColor;
        public Color themeColorAccent;
        public Color active;
        public Color selected;
        public Color selectedAccent;
        public Color selectedText;
        public Color selectedTextAccent;
        public Color negativeValue;
        public Color positiveValue;

        public Palette(
            Color text, Color background,
            Color backgroundAccent, Color themeColor, 
            Color themeColorAccent, Color active, 
            Color selected, Color selectedAccent,
            Color selectedText, Color selectedTextAccent,
            Color negativeValue, Color positiveValue
            )
        {
            this.text = text;
            this.background = background;
            this.backgroundAccent = backgroundAccent;
            this.themeColor = themeColor;
            this.themeColorAccent = themeColorAccent;
            this.active = active;
            this.selected = selected;
            this.selectedAccent = selectedAccent;
            this.selectedText = selectedText;
            this.selectedTextAccent = selectedTextAccent;
            this.negativeValue = negativeValue;
            this.positiveValue = positiveValue;
        }

        public Palette()
        {
            this.text = Color.FromArgb(245, 246, 250);
            this.background = Color.FromArgb(47, 54, 64);
            this.backgroundAccent = Color.FromArgb(53, 59, 72);
            this.themeColor = Color.FromArgb(140, 122, 230);
            this.themeColorAccent = Color.FromArgb(156, 136, 255);
            this.active = Color.FromArgb(113, 128, 147);
            this.selected = Color.FromArgb(113, 128, 147);
            this.selectedAccent = Color.FromArgb(127, 143, 166);
            this.selectedText = Color.FromArgb(245, 246, 250);
            this.selectedTextAccent = Color.FromArgb(245, 246, 250);
            this.negativeValue = Color.FromArgb(232, 65, 24);
            this.positiveValue = Color.FromArgb(68, 189, 50);
        }

        public void save()
        {
            var jsonToWrite = JsonConvert.SerializeObject(this, Formatting.Indented);
            using (StreamWriter sw = new StreamWriter(Info.paletteFilePath))
                sw.Write(jsonToWrite);
        }
    }
}
