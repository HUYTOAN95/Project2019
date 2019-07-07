using POS2019.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS2019.GUI
{
    public partial class frmForgetPW : Form
    {
        private System.Windows.Forms.Timer aTimer;
        private int counter = 60;
        public string UserName { get; set; }
        private DataAccess ds;
        public frmForgetPW()
        {
            InitializeComponent();
            ds = new DataAccess();
        }

        private void frmForgetPW_Load(object sender, EventArgs e)
        {
            this.BackColor = VariableSystem.BackGR;
            //foreach (Control Control in (Ultis.GetAllControls(this)))
            //{
            //    Control.Font = VariableSystem.Font; ;
            //}
            this.Icon = VariableSystem.IconPath;
            this.ShowIcon = true;
        }

        private long RandomNumber(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }
        private void button1_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(textBox1.Text.Trim())) { MessageBox.Show("Vui lòng nhập Email"); return; }
            //--- lấy số ----------
            string sqlsele = "Select CodeUnlock from _Users where UserName ='" + UserName + "'";
            string CodeUnLock = ds.SQLEXEC(sqlsele).ToString();
            string body = "Chào Bạn! \nChúng tôi xin gửi bạn KEY lấy lại mật khẩu POS-Proffesional. \nMã lấy lại: " + CodeUnLock + " \nCảm ơn bạn đã sử dụng phần mềm của chúng tôi. \nTôi xin chân thành cảm ơn!";
            if (Ultis.SendMail(textBox1.Text.Trim(), "nguyenhuytoanbsh@gmail.com", "toankim1234", body, "KEY LẤY LẠI MẬT KHẨU", null))
            {
                MessageBox.Show("Mã lấy lại mật khẩu đã được gửi, vui lòng kiểm tra !");
                aTimer = new System.Windows.Forms.Timer();

                aTimer.Tick += new EventHandler(timer1_Tick);

                aTimer.Interval = 1000; // 1 second

                aTimer.Start();

                label3.Text = counter.ToString();
            }
            else MessageBox.Show("Email không hợp lệ !");


        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            counter--;

            if (counter == 0)
            {
                aTimer.Stop();
                MessageBox.Show("Mã bạn vừa lấy không còn hợp lệ, vui lòng lấy lại mã ?");
                long NumberRandom = RandomNumber(1, 100000000);
                ds.SQLEXEC1("update _Users set CodeUnlock =" + NumberRandom + " where UserName ='" + UserName + "'");
                counter = 60;
            }

            label3.Text = counter.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string sqlcheck = "select count(*) from _Users where UserName='" + UserName + "' and CodeUnlock=" + textBox2.Text.Trim() + "";
            int IsExit = 0;
            Int32.TryParse(ds.SQLEXEC(sqlcheck).ToString(), out IsExit);
            if(IsExit > 0)
            {
                this.Hide();
                frmUsers frm = new frmUsers();
                frm.ShowDialog();
            }
            else
            {
                MessageBox.Show("Mã không hợp lệ !");
            }
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (Ultis.isEmail(textBox1.Text.Trim()) == false)
                MessageBox.Show("Email không đúng ? ");
        }
    }
}
