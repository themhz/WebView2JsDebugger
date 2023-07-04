using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.WinForms;
using ScintillaNET;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WebViewJsDebugger
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            scintilla1.Lexer = Lexer.Cpp;

            scintilla1.Styles[Style.Cpp.Default].ForeColor = Color.Silver;
            scintilla1.Styles[Style.Cpp.Comment].ForeColor = Color.FromArgb(0, 128, 0); // Green
            scintilla1.Styles[Style.Cpp.CommentLine].ForeColor = Color.FromArgb(0, 128, 0); // Green
            scintilla1.Styles[Style.Cpp.CommentDoc].ForeColor = Color.FromArgb(128, 128, 128); // Gray
            scintilla1.Styles[Style.Cpp.Number].ForeColor = Color.Olive;
            scintilla1.Styles[Style.Cpp.Word].ForeColor = Color.Blue;
            scintilla1.Styles[Style.Cpp.Word2].ForeColor = Color.Blue;
            scintilla1.Styles[Style.Cpp.String].ForeColor = Color.FromArgb(163, 21, 21); // Red
            scintilla1.Styles[Style.Cpp.Character].ForeColor = Color.FromArgb(163, 21, 21); // Red
            scintilla1.Styles[Style.Cpp.Operator].ForeColor = Color.Purple;
            scintilla1.Styles[Style.Cpp.Identifier].ForeColor = Color.Black;


        }

        private async void button1_Click(object sender, EventArgs e)
        {
                        
            string script = scintilla1.Text;
            webView21.WebMessageReceived += WebView_WebMessageReceived;
            // Execute the script
            var result = await webView21.ExecuteScriptAsync(script);            
        }
    

        private async void button2_Click(object sender, EventArgs e)
        {
            webView21.Source = new Uri("https://apps.e-efka.gov.gr/ePrivateConstructions/secure/Constructions.xhtml", UriKind.Absolute);
        }

        private void WebView_WebMessageReceived(object sender, CoreWebView2WebMessageReceivedEventArgs args)
        {
            string message = args.TryGetWebMessageAsString();

            webView21.WebMessageReceived -= WebView_WebMessageReceived;
        }
    }
}
