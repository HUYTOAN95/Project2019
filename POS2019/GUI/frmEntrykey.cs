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
    public partial class frmEntrykey : Form
    {
        public string Keyunlock { get; set; }
        private DataAccess ds;
        public frmEntrykey()
        {
            InitializeComponent();
            ds = new DataAccess();
        }

        private void frmEntrykey_Load(object sender, EventArgs e)
        {
            this.BackColor = VariableSystem.BackGR;
            foreach (Control Control in (Ultis.GetAllControls(this)))
            {
                Control.Font = VariableSystem.Font; ;
            }
            textBox1.Text = Keyunlock;
            textBox1.Enabled = false;

        }

        private void btnOpenLock_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox2.Text.Trim()) || string.IsNullOrEmpty(textBox2.Text.Trim())) { MessageBox.Show("Nhập chuỗi mở khóa"); errorProvider1.SetError(textBox2,"Key không được để trống !"); textBox2.Focus(); return; }
            string sqlcmd = @"Select count(*) from _Server where KeyUnlock=" + Keyunlock + @" and KeyUnlock2='" + textBox2.Text.Trim() + "'";
            string IsUnLock = ds.SQLEXEC(sqlcmd).ToString();
            if (string.IsNullOrWhiteSpace(textBox3.Text.Trim()) || string.IsNullOrEmpty(textBox3.Text.Trim())) { MessageBox.Show("Nhập số máy trạm"); errorProvider1.SetError(textBox3, "Số máy không được để trống !"); textBox3.Focus(); return; }
            if (Int32.Parse(IsUnLock) > 0)
            {

                string sqlUp = @"Update _Server Set NoWS =" + textBox3.Text.Trim() + @" Where KeyUnlock=" + Keyunlock + @" and KeyUnlock2='" + textBox2.Text.Trim() + "'";
                if (ds.SQLEXEC1(sqlUp))
                {
                    MessageBox.Show("Mở khóa thành công !");
                }
                this.Hide();

                frmLogin frm = new frmLogin();
                frm.ShowDialog();

            }
            else MessageBox.Show("Chuỗi mở khóa thất bại !");
            this.Hide();


        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            using (frmSendMail frm = new frmSendMail())
            {
                frm.KeyUnlock = textBox1.Text.Trim();
                frm.ShowDialog();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
