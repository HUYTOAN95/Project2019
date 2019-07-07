using DevComponents.DotNetBar;
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
    public partial class frmMainProject : Form
    {
        public frmMainProject()
        {
            InitializeComponent();
        }

        private void buttonItem1_Click(object sender, EventArgs e)
        {

        }

        private void ribbonPanel2_Click(object sender, EventArgs e)
        {

        }

        private void buttonItem1_Click_1(object sender, EventArgs e)
        {

        }

        private void expandablePanel1_Click(object sender, EventArgs e)
        {

        }

        private void ribbonControl1_Click(object sender, EventArgs e)
        {

        }

        private void frmMainProject_Load(object sender, EventArgs e)
        {
            this.BackColor = VariableSystem.BackGR;
            foreach (Control Control in (Ultis.GetAllControls(this)))
            {
                Control.Font = VariableSystem.Font; ;

            }
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void buttonItem16_Click(object sender, EventArgs e)
        {
           

        }
        private bool CheckExistTab(string Table)
        {

            for (int i = 0; i < tabControl1.Tabs.Count; i++)
            {
                if (tabControl1.Tabs[i].Text == Table)
                {
                    tabControl1.SelectedTabIndex = i;
                    tabControl1.SelectedTabIndex = tabControl1.Tabs.Count - 1;
                    return true;
                }
            }
            return false;

        }
        private void addTab(string table, Form form )
        {
            if (CheckExistTab(table) == false)
            {
                TabItem tab = tabControl1.CreateTab(table);
                form = new Form();
                form.Dock = DockStyle.Fill;
                form.FormBorderStyle = FormBorderStyle.None;
                form.TopLevel = false;
                tab.AttachedControl.Controls.Add(form);
                form.Show();
            }
        }
    }
}
