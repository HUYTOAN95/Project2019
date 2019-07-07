using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using POS2019.DAL;

namespace POS2019.GUI
{
    public partial class frmlogin2 : Form
    {
        private DataAccess ds;
        public frmlogin2()
        {
            InitializeComponent();
            ds = new DataAccess();
            this.BackColor = VariableSystem.BackGR;
            foreach (Control Control in (Ultis.GetAllControls(this)))
            {
                Control.Font = VariableSystem.Font; ;
                Control.ForeColor = VariableSystem.Forecolor;
            }
            this.Icon = VariableSystem.IconPath;
            this.ShowIcon = true;
            this.StartPosition = VariableSystem.Position;
        }

        private void frmlogin2_Load(object sender, EventArgs e)
        {

        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxX1.Text) || string.IsNullOrWhiteSpace(textBoxX1.Text))
            {
                errorProvider1.SetError(textBoxX1, "Vui lòng nhập Người dùng ");
                MessageBox.Show("Người dùng không được bỏ trống !");
                textBoxX1.Focus();
                return;
            }
            else
              if (string.IsNullOrEmpty(textBoxX2.Text) || string.IsNullOrWhiteSpace(textBoxX2.Text))
            {
                errorProvider1.SetError(textBoxX2, "Vui lòng nhập Mật khẩu");
                MessageBox.Show("Mật khẩu không được bỏ trống !");
                textBoxX2.Focus();
                return;
            }
            errorProvider1.Clear();
            int CheckUser = Int32.Parse(ds.SQLEXEC("select count(*) from _Users where UserName='" + textBoxX1.Text.Trim() + "'").ToString());
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
            string sqlcmd = "Select count(*) from _Users where UserName='" + textBoxX1.Text.Trim() + "' and Password ='" + textBoxX2.Text.Trim() + "' and Status = 1";
            string sqlUser = "Select UserCode from _Users where UserName='" + textBoxX1.Text.Trim() + "' and Password ='" + textBoxX2.Text.Trim() + "' and Status = 1";
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
                    VariableSystem.Userlogin = textBoxX1.Text.Trim();
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
        }
        /// <summary>
        /// hàm random chọn số ngẫu nhiên 
        private long RandomNumber(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //----------- thực hiện insert mã random vào trong danh sách người dùng ---------
            if (string.IsNullOrEmpty(textBoxX1.Text.Trim()) || string.IsNullOrWhiteSpace(textBoxX1.Text.Trim())) { MessageBox.Show("Vui lòng nhập Tên người dùng lấy lại mật khẩu"); return; }
            long NumberRandom = RandomNumber(1, 100000000);
            ds.SQLEXEC1("update _Users set CodeUnlock =" + NumberRandom + " where UserName ='" + textBoxX1.Text.Trim() + "'");
            //------------end --------------------------------

            using (frmForgetPW frm = new frmForgetPW())
            {
                frm.UserName = textBoxX1.Text.Trim();
                frm.ShowDialog();
            }
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            frmmain frm = new frmmain();
            frm.ShowDialog();
        }
    }
}
