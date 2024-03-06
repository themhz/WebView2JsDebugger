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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Jint;
using Esprima;
using WebViewJsDebugger.parsers;
using Newtonsoft.Json;
using Windows.UI.Xaml.Controls;


namespace WebViewJsDebugger
{
    public partial class Form1 : Form
    {
        private string currentFilePath = null;
        private SearchForm searchForm;
        private AddUrlItem urlItemForm;
        public bool downloadStringEventAttacked = false;

        public Form1()
        {
                        
            InitializeComponent();
            
            //txtUrl.Text = "https://services.tee.gr/adeia/faces/main";
            //txtUrl.Text = "https://portal.tee.gr/ypeka/auth/pages/app/dilosi.jspx";                         
            //txtUrl.Text = "https://apps.tee.gr/buildID/faces/appMain";
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

            webView21.NavigationCompleted += WebView_NavigationCompleted;

            lstlog.SelectedIndexChanged += lstlog_SelectedIndexChanged;
            //webView21.CoreWebView2.DownloadStarting += CoreWebView2_DownloadStarting;
            LoadUrlFile();


        }

        CoreWebView2DownloadOperation downloadOperation;
        private async void button1_Click(object sender, EventArgs e)
        {
                        
            string script = scintilla1.Text;
            try
            {
                
                var parser = new JavaScriptParser();
                parser.ParseScript(script);
            }
            catch (ParserException scripterror)
            {
                MessageBox.Show($"Syntax error: {scripterror.Message}");
            }
                        
            
            if (webView21.CoreWebView2 != null)            
            {                
                if (ScriptChecker.checkIfPostMessageExists(script))
                {
                    // Handling web message received
                    webView21.WebMessageReceived += WebView_WebMessageReceived;
                }

                if (downloadStringEventAttacked == false)
                {
                    // Handling download event
                    webView21.CoreWebView2.DownloadStarting += CoreWebView2_DownloadStarting;
                    downloadStringEventAttacked = true;
                }
                
                // Execute the script
                var result = await webView21.ExecuteScriptAsync(script);
            }
            else
            {
                notify("You need to browse to a url to upload js code"); 
            }                                               
        }
    

        private async void button2_Click(object sender, EventArgs e)
        {
            if ((UrlItem)cbxUrls.SelectedItem != null)
            {
                webView21.Source = new Uri(((UrlItem)cbxUrls.SelectedItem).Value, UriKind.Absolute);
            }
            
        }

        private void WebView_WebMessageReceived(object sender, CoreWebView2WebMessageReceivedEventArgs args)
        {
            notify("WebMessageReceived event fired");
            string message = args.TryGetWebMessageAsString();
            if (tcs.Task.IsCompleted)
            {
                tcs.SetResult(message); // Set the result of the task to the received message
            }

            webView21.WebMessageReceived -= WebView_WebMessageReceived;        

            if (lstlog.Columns.Count > 0)
            {
                lstlog.Columns[0].Width = -2;
            }
            

            lstlog.Items.Insert(0, message);
            


        }

        private async void WebView_NavigationCompleted(object sender, CoreWebView2NavigationCompletedEventArgs args)
        {
            if (chkPersistCode.Checked)
            {
                await webView21.ExecuteScriptAsync(scintilla1.Text);
            }
            
            notify("change page event fired");
        }

        private void CoreWebView2_DownloadStarting(object sender, CoreWebView2DownloadStartingEventArgs e)
        {
            downloadOperation = e.DownloadOperation; // Store the 'DownloadOperation' for later use in events
            
            webView21.CoreWebView2.Profile.DefaultDownloadFolderPath = "C:\\Users\\themis\\Documents\\testdownloads";

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

        private void OnWebResourceRequested(object sender, CoreWebView2WebResourceRequestedEventArgs e)
        {
            var request = e.Request;
            var response = e.Response;

            if (request.Uri.Contains("your_upload_endpoint") && response != null)
            {
                // Optionally, check the response status code or body to confirm success
                Console.WriteLine("Upload detected");
                // Perform your post-upload logic here
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
                openFileDialog.InitialDirectory = "C:\\Users\\themis\\Documents\\jscodes";
                openFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    currentFilePath = openFileDialog.FileName;

                    try
                    {
                        scintilla1.Text = File.ReadAllText(currentFilePath);
                        UpdateTitleBar(currentFilePath); // Update the title bar
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
                UpdateTitleBar(currentFilePath);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving file: {ex.Message}");
            }


        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
                saveFileDialog.FilterIndex = 1;
                saveFileDialog.RestoreDirectory = true;

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    currentFilePath = saveFileDialog.FileName;
                    try
                    {
                        File.WriteAllText(currentFilePath, scintilla1.Text);
                        MessageBox.Show("File saved successfully!");
                        UpdateTitleBar(currentFilePath); // Update the title bar
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error saving file: {ex.Message}");
                    }
                }
            }
        }

        private void UpdateTitleBar(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                this.Text = "WebView Js Debugger";
            }
            else
            {
                this.Text = $"WebView Js Debugger - {Path.GetFileName(filePath)}";
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
            if (e.Control && e.KeyCode == Keys.F)
            {
                if (searchForm == null || searchForm.IsDisposed)
                {
                    searchForm = new SearchForm();
                    searchForm.FindNextClicked += SearchForm_FindNextClicked;
                }
                searchForm.Show();
                e.Handled = true;
                e.SuppressKeyPress = true;  // Add this line

            }
            
        }
        private void SearchForm_FindNextClicked(object sender, EventArgs e)
        {
            string searchText = searchForm.SearchText;
            int startPosition = scintilla1.SelectionStart + scintilla1.SelectionEnd;

            // Set the search range
            scintilla1.TargetStart = startPosition;
            scintilla1.TargetEnd = scintilla1.TextLength;

            // Search for the text
            int searchResult = scintilla1.SearchInTarget(searchText);
            if (searchResult != -1)
            {
                // Select the found text
                scintilla1.SetSelection(scintilla1.TargetEnd, scintilla1.TargetStart);
                scintilla1.ScrollCaret();
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
      

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Check for unsaved changes
            if (!string.IsNullOrEmpty(scintilla1.Text)) // or any other condition you'd use to detect changes
            {
                var result = MessageBox.Show("There are unsaved changes. Do you want to save them?", "Unsaved Changes", MessageBoxButtons.YesNoCancel);

                switch (result)
                {
                    case DialogResult.Yes:
                        menuSave_Click(this, EventArgs.Empty); // Call the save method
                        break;
                    case DialogResult.Cancel:
                        return; // Cancel the new file operation
                }
            }
            UpdateTitleBar("");
            // Clear the Scintilla control and reset the file path
            scintilla1.Text = "";
            currentFilePath = null; // Assuming you have this variable from previous interactions
        }


        private void notify(string message)
        {
            notifyIcon1.Icon = SystemIcons.Application;
            notifyIcon1.Visible = true;

            // Set the content of the balloon tip
            notifyIcon1.BalloonTipTitle = "Javascript Notfication";
            notifyIcon1.BalloonTipText = message;
            notifyIcon1.BalloonTipIcon = ToolTipIcon.Info; // You can choose between None, Info, Warning, and Error

            // Display the balloon tip
            notifyIcon1.ShowBalloonTip(3000);  // 3000 is the duration in milliseconds  
        }

        private void lstlog_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstlog.SelectedItems.Count > 0)
            {
                string text = lstlog.SelectedItems[0].Text;
                txtlog.Text = FormatJson(text);
            }
            else
            {
                txtlog.Text = ""; // Clear the TextBox if nothing is selected
            }
        }

        private string FormatJson(string json)
        {
            try
            {
                // Parse the JSON string
                var parsedJson = Newtonsoft.Json.Linq.JToken.Parse(json);

                // Re-serialize it with indentation
                return parsedJson.ToString(Newtonsoft.Json.Formatting.Indented);
            }
            catch (Newtonsoft.Json.JsonReaderException)
            {
                // If the text is not a valid JSON string, just return it as is
                return json;
            }
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            lstlog.Items.Clear();
        }

        private void postMessage_Click(object sender, EventArgs e)
        {            
            int currentPosition = scintilla1.CurrentPosition;
            scintilla1.InsertText(currentPosition, FormatJson("var data = { 'taskStatus': 'success', 'message': 'some data' }; \r\nwindow.chrome.webview.postMessage(JSON.stringify(data));"));
        }

        private void QuerySelectorByIdLike_Click(object sender, EventArgs e)
        {
            int currentPosition = scintilla1.CurrentPosition;
            scintilla1.InsertText(currentPosition, FormatJson("document.querySelector('[id*=\"\"{part of id}\"\"]');"));
        }

        private TaskCompletionSource<string> tcs = new TaskCompletionSource<string>();
        private async void button4_Click(object sender, EventArgs e)
        {
            string[] args = { "C:\\Users\\themis\\Desktop\\compares\\first.txt", 
                              "C:\\Users\\themis\\Desktop\\compares\\second.txt",
                              "C:\\Users\\themis\\Desktop\\compares\\one.txt",
                              "C:\\Users\\themis\\Desktop\\compares\\two.txt",
                              "C:\\Users\\themis\\Desktop\\compares\\three.txt",
                              "C:\\Users\\themis\\Desktop\\compares\\four.txt",
                              "C:\\Users\\themis\\Desktop\\compares\\five.txt",
                              "C:\\Users\\themis\\Desktop\\compares\\six.txt",
            };
            var simulator = new WindowsInput.InputSimulator();

            
            foreach (string arg in args)
            {
                
                //Επιλογή τυπου αρχείου
                await webView21.ExecuteScriptAsync($@"var selectElement = document.querySelector('[id*="":soc6::content""]');
                                                    var desiredOptionText = ""Αρχείο κειμένου"";
                                                    Array.from(selectElement.options).forEach(function(optionElement) {{
                                                        if (optionElement.text === desiredOptionText) {{
                                                            optionElement.selected = 'selected';
                                                        }}
                                                    }});");

                await Task.Delay(2000);
                //Επιλογή κατηγορίας, δηλαδή click στον πίνακα
                await webView21.ExecuteScriptAsync($@"
                    var table = document.querySelector('[id*=""pc2:t1::db""]').querySelector(""table"");                    
                    for (var i = 0; i < table.rows.length; i++) {{
                        var row = table.rows[i];       
                        if(row.cells[1].innerText==""Υπεύθυνη Δήλωση μηχανικού ή βεβαίωση μηχανικού""){{
                            row.click();
                            break;
                        }}
                    }}
                ");

                await Task.Delay(2000);

                //Άνοιγμα του file dialog
                await webView21.ExecuteScriptAsync("document.querySelector('[id*=\":if1::content\"]').click();");
                await Task.Delay(2000);
                //Πληκτρολόγιση του τύπου αρχείου
                simulator.Keyboard.TextEntry(arg);

                await Task.Delay(2000);
                //Πάτημα του enter
                simulator.Keyboard.KeyPress(WindowsInput.Native.VirtualKeyCode.RETURN);
                await Task.Delay(2000);
                //Ανέβασμα αρχείουcompares\five.txt                
                
                await webView21.ExecuteScriptAsync("document.querySelector('[id*=\":b1\"]').querySelector('a').click();");
                await Task.Delay(2000);

                await webView21.ExecuteScriptAsync($@"
                    var table = document.querySelector('[id*=""pc2:t1::db""]').querySelector(""table"");
                    for (var i = 0; i < table.rows.length; i++) {{
                        var row = table.rows[i];
                        if(row.cells[1].innerText==""Υπεύθυνη Δήλωση μηχανικού ή βεβαίωση μηχανικού""){{
                            var data = {{ 'taskStatus': 'success', 'message': row.cells[3].innerText }}; 
                            window.chrome.webview.postMessage(JSON.stringify(data));
                            break;
                        }}
                    }}
                ");
                var response = await tcs.Task;

                await Task.Delay(2000);


            }       
           


        }

        private void button5_Click(object sender, EventArgs e)
        {
            
            
            if (urlItemForm == null || urlItemForm.IsDisposed)
            {
                urlItemForm = new AddUrlItem();
                urlItemForm.AddUrl += AddItemUrlForm_AddItemClicked;
            }
            urlItemForm.Show();
            
        }

        private void AddItemUrlForm_AddItemClicked(object sender, EventArgs e)
        {
            UrlItem urlItem = new UrlItem(urlItemForm.name, urlItemForm.url);
            cbxUrls.Items.Add(urlItem);
            cbxUrls.SelectedItem = urlItem;
            urlItemForm.Hide();
            SaveUrlFile();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (cbxUrls.SelectedIndex >= 0) // Check if an item is selected
            {
                cbxUrls.Items.RemoveAt(cbxUrls.SelectedIndex);

                if(cbxUrls.Items.Count > 0) { 
                    cbxUrls.SelectedIndex = 0;
                    RemoveUrlFile();
                }                    
            }
        }

        private void SaveUrlFile()
        {
            // Create a list to store all UrlItems
            List<UrlItem> allItems = cbxUrls.Items.Cast<UrlItem>().ToList();

            // Serialize the list to JSON
            string json = JsonConvert.SerializeObject(allItems, Formatting.Indented);

            // Define a file path
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "urlItems.json");

            // Write the JSON to the file
            File.WriteAllText(filePath, json);
        }

        private void RemoveUrlFile()
        {
            // Create a list to store all UrlItems left
            List<UrlItem> allItems = cbxUrls.Items.Cast<UrlItem>().ToList();

            // Serialize the list to JSON
            string json = JsonConvert.SerializeObject(allItems, Formatting.Indented);

            // Define a file path
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "urlItems.json");

            // Write the JSON to the file
            File.WriteAllText(filePath, json);
        }

        private void LoadUrlFile()
        {
            
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "urlItems.json");

            // Check if the file exists
            if (File.Exists(filePath))
            {
                // Read the JSON from the file
                string json = File.ReadAllText(filePath);

                // Deserialize the JSON to a list of UrlItems
                List<UrlItem> allItems = JsonConvert.DeserializeObject<List<UrlItem>>(json);

                // Add the items to the ComboBox
                foreach (var item in allItems)
                {
                    cbxUrls.Items.Add(item);
                }

                if (cbxUrls.Items.Count > 0)
                {
                    cbxUrls.SelectedIndex = 0;                    
                }
            }

        }

        private void openUrlsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Define a file path
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "urlItems.json");

            // Check if the file exists
            if (File.Exists(filePath))
            {
                // Open the file with the default application
                System.Diagnostics.Process.Start(filePath);
            }
            else
            {
                // If the file does not exist, inform the user
                MessageBox.Show("The file does not exist.");
            }
        }

        private void loadUrlsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cbxUrls.Items.Clear();
            LoadUrlFile();
        }

        private async void button7_Click(object sender, EventArgs e)
        {

            await webView21.ExecuteScriptAsync("document.querySelector('[id*=\":b1\"]').querySelector('a').click();");
            //var response = await tcs.Task;

        }
    }
}
