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
    public partial class frmCurrency : Form
    {
        private DataAccess ds;
        public frmCurrency()
        {
            InitializeComponent();
            ds = new DataAccess();
        }

        private void frmCurrency_Load(object sender, EventArgs e)
        {
            this.BackColor = VariableSystem.BackGR;
            ds.UpdateExRate(true);
            dataGridView1.DataSource = ds.SelectSQL("select CurrencyCode,Buy,Transfer,Sell,DateTime from _Currency");

        }
    }
}
