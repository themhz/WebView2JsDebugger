using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebViewJsDebugger
{
    public class UrlItem
    {
        public string Text { get; set; }
        public string Value { get; set; }

        public UrlItem(string text, string value)
        {
            Text = text;
            Value = value;
        }

        public override string ToString()
        {
            // This is what will be shown in the ComboBox
            return Text;
        }
    }
}
