using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StoreManage
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }


        private void buttonConfirm_Click(object sender, EventArgs e)
        {
            StoreManage storeManage = new StoreManage(textBoxId.Text);
            storeManage.Show();
            this.Visible = false;
            
        }
    }
}
