using POS2019.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS2019.GUI
{
    public partial class frmMainPOS : Form
    {
        public frmMainPOS()
        {
            InitializeComponent();
        }
        protected void Displaynotify()
        {
            try
            {
                notifyIcon1.Icon = VariableSystem.IconPath;
                notifyIcon1.Text = "POS";
                notifyIcon1.Visible = true;
                notifyIcon1.BalloonTipTitle = "Wellcome to POS Proffesional !";
                notifyIcon1.BalloonTipText = "Vui lòng bấm xem chi tiết!";
                notifyIcon1.ShowBalloonTip(100);
            }
            catch (Exception ex)
            {
                Ultis.WriteLog(ex.ToString());
            }
        }

        private void frmMainPOS_Load(object sender, EventArgs e)
        {
            Displaynotify();
            this.BackColor = VariableSystem.BackGR;
            foreach (Control Control in (Ultis.GetAllControls(this)))
            {
                Control.Font = VariableSystem.Font; ;
                
            }
            //foreach (Control Control in (Ultis.GetAllControls(this).OfType<Button>().ToList()))
            //{
               
            //    Control.BackColor = VariableSystem.BackGR;
            //}
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            MessageBox.Show(" Program is running !");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmChatClient frm = new frmChatClient();
            frm.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            frmServerChat frm = new frmServerChat();
            frm.ShowDialog();
        }

        private void generalFunctionToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        StringBuilder sb = new StringBuilder();
        private void button6_Click(object sender, EventArgs e)
        {

            sb.Append("1");
            listView2.Items.Add(sb.ToString());
        }

        private void button7_Click_1(object sender, EventArgs e)
        {
            sb.Append("2");
            listView2.Items.Add(sb.ToString());
        }

        private void button8_Click(object sender, EventArgs e)
        {
            sb.Append("3");
            listView2.Items.Add(sb.ToString());
        }

        private void button11_Click(object sender, EventArgs e)
        {
            sb.Append("4");
            listView2.Items.Add(sb.ToString());
        }

        private void button12_Click(object sender, EventArgs e)
        {
            sb.Append("5");
            listView2.Items.Add(sb.ToString());
        }

        private void button15_Click(object sender, EventArgs e)
        {
            sb.Append("6");
            listView2.Items.Add(sb.ToString());
        }

        private void button18_Click(object sender, EventArgs e)
        {
            sb.Append("7");
            listView2.Items.Add(sb.ToString());
        }

        private void button19_Click(object sender, EventArgs e)
        {
            sb.Append("8");
            listView2.Items.Add(sb.ToString());
        }

        private void button20_Click(object sender, EventArgs e)
        {
            sb.Append("9");
            listView2.Items.Add(sb.ToString());
        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            frmCountry frm = new frmCountry();
            frm.ShowDialog();
        }  
        
    }
}
