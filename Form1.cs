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
using System.IO;
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
        private string currentFilePath = null;

        public Form1()
        {
                        
            InitializeComponent();
            this.KeyPreview = true;
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

        private void menuOpen_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    currentFilePath = openFileDialog.FileName;

                    try
                    {
                        scintilla1.Text = File.ReadAllText(currentFilePath);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error reading file: {ex.Message}");
                    }
                }
            }
        }

        private void menuSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(currentFilePath))
            {
                using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
                    saveFileDialog.FilterIndex = 1;
                    saveFileDialog.RestoreDirectory = true;

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        currentFilePath = saveFileDialog.FileName;
                    }
                    else
                    {
                        return;
                    }
                }
            }

            try
            {
                File.WriteAllText(currentFilePath, scintilla1.Text);
                MessageBox.Show("File saved successfully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving file: {ex.Message}");
            }
        }

        private void scintilla1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.S)
            {
                menuSave_Click(this, EventArgs.Empty); // Call the save method
                e.Handled = true;  // Indicate that you've handled this key event                
                e.SuppressKeyPress = true;  // Prevent the key press from being sent to other controls
            }
        }

        private void scintilla1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 19) // Ctrl + S is represented by the ASCII value 19
            {
                e.Handled = true; // Prevent further processing of the character
            }
        }

        private void SetLineNumberMargin(Scintilla scintillaControl)
        {
            int maxLineNumberCharLength = scintillaControl.Lines.Count.ToString().Length;
            const int padding = 2;  // Give a little padding
            scintillaControl.Margins[0].Width = scintillaControl.TextWidth(Style.LineNumber, new string('9', maxLineNumberCharLength + 1)) + padding;
        }

        private void scintilla1_TextChanged(object sender, EventArgs e)
        {
            SetLineNumberMargin(sender as Scintilla);
        }

        private void SetupScintilla(Scintilla scintillaControl)
        {
            // Set the line numbers
            scintillaControl.Styles[Style.LineNumber].BackColor = Color.LightGray;
            scintillaControl.Styles[Style.LineNumber].ForeColor = Color.Black;
            scintillaControl.Styles[Style.LineNumber].Font = "Consolas";
            scintillaControl.Styles[Style.LineNumber].Size = 9;

            scintillaControl.Margins[0].Type = MarginType.Number;
            scintillaControl.Margins[0].Mask = 0;
            scintillaControl.Margins[0].Sensitive = false;
            scintillaControl.Margins[0].Width = 16;  // Initial width, will be adjusted by the SetLineNumberMargin method
        }

    }
}
