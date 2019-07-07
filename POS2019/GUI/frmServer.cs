using POS2019.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS2019.GUI
{
    public partial class frmServer : Form
    {
        private DataAccess db;
        public frmServer()
        {
            db = new DataAccess();
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string key = DateTime.Now.ToString("MM/dd/yyyyHH:mm:ss").Replace("/", "").Replace(":", "");
            key = key.Trim();
            MessageBox.Show(decimal.Parse(key).ToString());


        }
    }
}
