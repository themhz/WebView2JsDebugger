using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WebViewJsDebugger
{
    public partial class SearchForm : Form
    {
        public SearchForm()
        {
            InitializeComponent();
            this.KeyPreview = true;
            this.KeyDown += SearchForm_KeyDown;
            buttonFindNext.Click += (sender, e) => FindNextClicked?.Invoke(this, EventArgs.Empty);
            textBoxSearch.KeyDown += TextBoxSearch_KeyDown;

        }

        public string SearchText => textBoxSearch.Text;

        public event EventHandler FindNextClicked;

        private void buttonFindNext_Click(object sender, EventArgs e)
        {

        }

        private void textBoxSearch_TextChanged(object sender, EventArgs e)
        {

        }

        private void TextBoxSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                FindNextClicked?.Invoke(this, EventArgs.Empty);
                e.Handled = true;
                e.SuppressKeyPress = true;  // Prevent the ding sound on Windows
            }

            if (e.KeyCode == Keys.Escape) { 
                
            }
        }
        private void SearchForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
                e.Handled = true;
            }
        }
    }
}
