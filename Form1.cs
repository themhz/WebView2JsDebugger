using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.WinForms;
using Microsoft.Web.WebView2.Wpf;
using ScintillaNET;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
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

        CoreWebView2DownloadOperation downloadOperation;
        private async void button1_Click(object sender, EventArgs e)
        {
                        
            string script = scintilla1.Text;
            webView21.WebMessageReceived += WebView_WebMessageReceived;
            webView21.CoreWebView2.DownloadStarting += CoreWebView2_DownloadStarting;

            // Execute the script
            var result = await webView21.ExecuteScriptAsync(script);            
        }
    

        private async void button2_Click(object sender, EventArgs e)
        {
            webView21.Source = new Uri(txtUrl.Text, UriKind.Absolute);
        }

        private void WebView_WebMessageReceived(object sender, CoreWebView2WebMessageReceivedEventArgs args)
        {
            string message = args.TryGetWebMessageAsString();
            webView21.WebMessageReceived -= WebView_WebMessageReceived;
            
        }

        private void CoreWebView2_DownloadStarting(object sender, CoreWebView2DownloadStartingEventArgs e)
        {
            downloadOperation = e.DownloadOperation; // Store the 'DownloadOperation' for later use in events
            downloadOperation.BytesReceivedChanged += DownloadOperation_BytesReceivedChanged; // Subscribe to BytesReceivedChanged event
            downloadOperation.EstimatedEndTimeChanged += DownloadOperation_EstimatedEndTimeChanged; // Subsribe to EstimatedEndTimeChanged event
            downloadOperation.StateChanged += DownloadOperation_StateChanged;
        }

        private void DownloadOperation_StateChanged(object sender, object e)
        {
            Debug.WriteLine("State " + downloadOperation.State.ToString());

            if (downloadOperation.State == CoreWebView2DownloadState.Completed)
            {
                Debug.WriteLine($"Download of {downloadOperation.ResultFilePath} completed.");

                // Unsubscribe from the events when download is completed
                downloadOperation.BytesReceivedChanged -= DownloadOperation_BytesReceivedChanged;
                downloadOperation.EstimatedEndTimeChanged -= DownloadOperation_EstimatedEndTimeChanged;
                downloadOperation.StateChanged -= DownloadOperation_StateChanged;
            }
        }

        private void DownloadOperation_EstimatedEndTimeChanged(object sender, object e)
        {
            Debug.WriteLine("EstimatedEndTimeChanged " +downloadOperation.EstimatedEndTime.ToString()); // Show the progress
        }

        private void DownloadOperation_BytesReceivedChanged(object sender, object e)
        {
            Debug.WriteLine("BytesReceivedChanged " +downloadOperation.BytesReceived.ToString()); // Show the progress
        }
    }
}
