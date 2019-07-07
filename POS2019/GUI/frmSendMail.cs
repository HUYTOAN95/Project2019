using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using POS2019.DAL;




namespace POS2019.GUI
{
    public partial class frmSendMail : Form
    {
        public string KeyUnlock { get; set; }
        private DataAccess ds;
        public frmSendMail()
        {
            InitializeComponent();
            ds = new DataAccess();
        }

        private void textBox1_Validating(object sender, CancelEventArgs e)
        {


        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text) && string.IsNullOrEmpty(textBox2.Text))
            {
                errorProvider1.SetError(textBox1, "Vui lòng nhập Email hoặc SĐT");
                MessageBox.Show("Vui lòng nhập Email hoặc SĐT người nhận ");
                textBox1.Focus();
                return;
            }
            // gửi mail cho người nhận 
            if (!string.IsNullOrEmpty(textBox1.Text))
            {
                string MachineName1 = Environment.MachineName;
                string sqlcmd = "Select KeyUnlock2 from _Server where KeyUnLock=" + KeyUnlock + " and MachineName1 ='"+MachineName1+"'";
                string KeyUnlock2 = ds.SQLEXEC(sqlcmd).ToString();
                string body = "Chào Bạn! \nChúng tôi xin gửi bạn KEY kích hoạt phần mềm POS-Proffesional. \nChuỗi ngẫu nhiên: " + KeyUnlock + " \nKey kích hoạt : " + KeyUnlock2 + " \nCảm ơn bạn đã sử dụng phần mềm của chúng tôi. \nTôi xin chân thành cảm ơn!";
                if (Ultis.SendMail(textBox1.Text.Trim(), "nguyenhuytoanbsh@gmail.com", "toankim1234", body, "KEY KÍCH HOẠT POS-PRO", null))
                {
                    MessageBox.Show("Key đã được gửi tới Email của bạn, vui lòng kiểm tra lại !");
                    string sqlup = "Update _Server Set Email = '" + textBox1.Text.Trim() + "' where KeyUnLock=" + KeyUnlock + " and MachineName1 ='" + MachineName1 + "'";
                    ds.SQLEXEC1(sqlup);
                    this.Close();
                }

                else
                    MessageBox.Show("Email của bạn không đúng !");
            }

        }

        private void frmSendMail_Load(object sender, EventArgs e)
        {
            this.BackColor = VariableSystem.BackGR;
            foreach (Control Control in (Ultis.GetAllControls(this)))
            {
                Control.Font = VariableSystem.Font; ;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
