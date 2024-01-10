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
    public partial class AddUrlItem : Form
    {

        public AddUrlItem()
        {
            InitializeComponent();
            btnAdd.Click += (sender, e) => AddUrl?.Invoke(this, EventArgs.Empty);
        }
        
        public event EventHandler AddUrl;        
        public string name => txtName.Text;
        public string url => txtUrl.Text;

        
    }
}
