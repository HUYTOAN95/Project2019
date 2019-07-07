using POS2019.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS2019.GUI
{
    public partial class frmLogin : Form
    {
        private DataAccess ds;
        public frmLogin()
        {
            InitializeComponent();
            ds = new DataAccess();
        }
        public void LoadMenu()
        {
            ContextMenuStrip menu = new ContextMenuStrip();
            ToolStripMenuItem mnuback = new ToolStripMenuItem();
            ToolStripMenuItem mnucode = new ToolStripMenuItem();
            ToolStripMenuItem mnucopy = new ToolStripMenuItem();
            ToolStripMenuItem mnupaste = new ToolStripMenuItem();
            mnuback.Click += new EventHandler(mnuback_Click);
            mnucode.Click += new EventHandler(mnucode_Click);
            mnucopy.Click += new EventHandler(mnucopy_Click);
            mnupaste.Click += new EventHandler(mnupaste_Click);
            menu.Items.AddRange(new ToolStripItem[] { mnuback, mnucode, mnucopy, mnupaste });
            this.ContextMenuStrip = menu;
        }
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            this.Hide();
            frmmain frm = new frmmain();
            frm.ShowDialog();


        }

        private void button2_Click(object sender, EventArgs e)
        {
            //string filename = "code.cs";
            //string filepath = Path.GetDirectoryName(Application.ExecutablePath);
            //MessageBox.Show(filepath);



            frmMainProject frm = new frmMainProject();
           frm.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            //foreach (Control theControl in (Ultis.GetAllControls(this)).OfType<TextBox>().ToList())
            //{
            //    theControl.Text = "";
            //}
            //return;
            if (string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrWhiteSpace(textBox1.Text))
            {
                errorProvider1.SetError(textBox1, "Vui lòng nhập Người dùng ");
                MessageBox.Show("Người dùng không được bỏ trống !");
                textBox1.Focus();
                return;
            }
            else
                if (string.IsNullOrEmpty(textBox2.Text) || string.IsNullOrWhiteSpace(textBox2.Text))
                {
                    errorProvider1.SetError(textBox2, "Vui lòng nhập Mật khẩu");
                    MessageBox.Show("Mật khẩu không được bỏ trống !");
                    textBox1.Focus();
                    return;
                }
            errorProvider1.Clear();
            int CheckUser = Int32.Parse(ds.SQLEXEC("select count(*) from _Users where UserName='" + textBox1.Text.Trim() + "'").ToString());
            if (CheckUser == 0)
            {
                if (MessageBox.Show("Người dùng chưa có trong hệ thống, Bạn có muốn tạo tài khoản", "Thông Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    frmUsers frm = new frmUsers();
                    frm.ShowDialog();
                    return;
                }
                else return;
            }
            string sqlcmd = "Select count(*) from _Users where UserName='" + textBox1.Text.Trim() + "' and Password ='" + textBox2.Text.Trim() + "' and Status = 1";
            string sqlUser = "Select UserCode from _Users where UserName='" + textBox1.Text.Trim() + "' and Password ='" + textBox2.Text.Trim() + "' and Status = 1";
            string MachineName1 = Environment.MachineName;
            string MachineName2 = System.Net.Dns.GetHostName();
            string MachineName3 = System.Environment.GetEnvironmentVariable("COMPUTERNAME");
            string sqlServer = "select Server from _Server where MachineName1 ='" + MachineName1 + "' or MachineName2='" + MachineName2 + "' or MachineName3='" + MachineName3 + "'";
            int IsUser = Int32.Parse(ds.SQLEXEC(sqlcmd).ToString());
            {
                if (IsUser > 0)
                {
                    int UserCode = 0;
                    string Server = "";
                    Int32.TryParse(ds.SQLEXEC(sqlUser).ToString(), out UserCode);
                    VariableSystem.UserCode = UserCode;
                    VariableSystem.Userlogin = textBox1.Text.Trim();
                    Server = ds.SQLEXEC(sqlServer).ToString();
                    VariableSystem.IPServer = Server.Substring(0, Server.IndexOf(','));
                    //MessageBox.Show(VariableSystem.UserCode.ToString() + VariableSystem.IPServer);
                    this.Hide();
                    frmMainProject frm = new frmMainProject();
                    frm.ShowDialog();

                }
                else if (IsUser == 0)
                {
                    MessageBox.Show("Sai mật khẩu hoặc tên người dùng !");
                }
            }

            //this.Hide();
            //frmMainPOS frm = new frmMainPOS();
            // frm.ShowDialog();
            //frmCurrency frm = new frmCurrency();
            //frm.ShowDialog();
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            this.BackColor = VariableSystem.BackGR;
            this.Font = VariableSystem.Font;
        }

        private void frmLogin_Load_1(object sender, EventArgs e)
        {
            this.BackColor = VariableSystem.BackGR;
            foreach (Control Control in (Ultis.GetAllControls(this)))
            {
                Control.Font = VariableSystem.Font; ;
            }
            this.Icon = VariableSystem.IconPath;
            this.ShowIcon = true;


        }
        private long RandomNumber(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }
        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //----------- thực hiện insert mã random vào trong danh sách người dùng ---------
            if (string.IsNullOrEmpty(textBox1.Text.Trim()) || string.IsNullOrWhiteSpace(textBox1.Text.Trim())) { MessageBox.Show("Vui lòng nhập Tên người dùng lấy lại mật khẩu"); return; }
            long NumberRandom = RandomNumber(1, 100000000);
            ds.SQLEXEC1("update _Users set CodeUnlock =" + NumberRandom + " where UserName ='" + textBox1.Text.Trim() + "'");
            //------------end --------------------------------

            using (frmForgetPW frm = new frmForgetPW())
            {
                frm.UserName = textBox1.Text.Trim();
                frm.ShowDialog();
            }

        }

        private void frmLogin_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                MessageBox.Show("LEFT");
            }
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                //LoadMenu();
                // MessageBox.Show("RIGHT");
            }
        }

        private void textBox1_MouseClick(object sender, MouseEventArgs e)
        {



            //switch (e.Button)
            //{
            //    case MouseButtons.Right:
            //        //Open file code của login 
            //        ListBox list = new ListBox();
            //        list.Items.Add("0.Quay lại");
            //        list.Items.Add("1.Mở Code");
            //        MessageBox.Show(list.ToString());
            //        break;
            //}
        }

        private void quayToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void mnuback_Click(object sender, EventArgs e)
        {

        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        private void mnucopy_Click(object sender, EventArgs e)
        {

        }

        private void mnupaste_Click(object sender, EventArgs e)
        {

        }

        private void mnucode_Click(object sender, EventArgs e)
        {

        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }
    }
}
