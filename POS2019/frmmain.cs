using POS2019.DAL;
using POS2019.GUI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace POS2019
{
    public partial class frmmain : Form
    {
        public string server = "", database = "", user = "", password = "";
        private DataAccess ds;

        public frmmain()
        {
            InitializeComponent();
            ds = new DataAccess();
            
        }

      public delegate int KeyU() ;
     
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            GetInfoCnn();

            if (IsCheckEmpty())
            {
                errorProvider1.Clear();
                DAL.ConnectDataBase.InfoConnect(server, database, user, password);
                if (DAL.ConnectDataBase.ExIsOpen(DAL.ConnectDataBase.Cnn))
                {
                    MessageBox.Show(" Thông tin kết nối hợp lệ !");
                }
                else
                    MessageBox.Show("Kiểm tra lại thông tin kết nối !");

            }

        }
        public bool IsCheckEmpty()
        {
            if (string.IsNullOrEmpty(server) || string.IsNullOrWhiteSpace(server)) { MessageBox.Show("Chưa nhập tên Server hay địa chỉ IP!"); errorProvider1.SetError(textBox1, "Server không được để trống!"); textBox1.Focus(); return false; }
            if (string.IsNullOrEmpty(database) || string.IsNullOrWhiteSpace(database)) { MessageBox.Show("Chưa nhập tên Database! "); errorProvider1.SetError(textBox2, "Database không được để trống!"); textBox2.Focus(); return false; }
            if (string.IsNullOrEmpty(user) || string.IsNullOrWhiteSpace(user)) { MessageBox.Show("Chưa nhập tên user!"); errorProvider1.SetError(textBox3, "Server không được để trống!"); textBox3.Focus(); return false; }
            if (string.IsNullOrEmpty(password) || string.IsNullOrWhiteSpace(password)) { MessageBox.Show("Chưa nhập Password!"); errorProvider1.SetError(textBox4, "Server không được để trống!"); textBox4.Focus(); return false; }
            return true;
        }
        public void GetInfoCnn()
        {
            server = textBox1.Text.Trim();
            database = textBox2.Text.Trim();
            user = textBox3.Text.Trim();
            password = textBox4.Text.Trim();
        }

        private void btnconnect_Click(object sender, EventArgs e)
        {
            string MachineName1 = Environment.MachineName;
            string MachineName2 = System.Net.Dns.GetHostName();
            string MachineName3 = System.Environment.GetEnvironmentVariable("COMPUTERNAME");
            GetInfoCnn();
            string cnn = Ultis.GetConnect();
            if (!string.IsNullOrWhiteSpace(cnn) || !string.IsNullOrEmpty(cnn))
            {
                string[] ss1 = cnn.Split(';');
                string server1 = ss1[0].ToString().Trim().Substring(ss1[0].IndexOf('=') + 1);
                string db1 = ss1[1].ToString().Trim().Substring(ss1[1].IndexOf('=') + 1);
                string user1 = ss1[2].ToString().Trim().Substring(ss1[2].IndexOf('=') + 1);
                string pass1 = ss1[3].ToString().Trim().Substring(ss1[3].IndexOf('=') + 1);
                if (server != server1 || database != db1 && user != user1 && password != pass1)
                {

                    string CnnString = @"Data Source=" + server + ";Initial Catalog=" + database + ";User ID=" + user + ";Password=" + password + ";Integrated Security=True";
                    Ultis.WriteConnect(CnnString);
                }
            }
            else
            {
                string CnnString = @"Data Source=" + server + ";Initial Catalog=" + database + ";User ID=" + user + ";Password=" + password + ";Integrated Security=True";
                Ultis.WriteConnect(CnnString);
            }



            if (IsCheckEmpty())
            {
                DAL.ConnectDataBase.InfoConnect(server, database, user, password);
                if (DAL.ConnectDataBase.ExIsOpen(DAL.ConnectDataBase.Cnn))
                {

                    //---- Insert DL vào SQL 
                    DataTable dt = ds.SelectSQL("select top(0) * from _Server");
                    DataRow dr = dt.NewRow();
                    dr["Server"] = server;
                    dr["DBName"] = database;
                    dr["UserName"] = user;
                    dr["PassWord"] = password;
                    dr["KeyUnLock"] = RandomNumber(1, 100000000);
                    dr["KeyUnLock2"] = DateTime.Now.ToString("MM/dd/yyyyHH:mm:ss").Replace("/", "").Replace(":", "").Trim();
                    dr["MachineName1"] = MachineName1;
                    dr["MachineName2"] = MachineName2;
                    dr["MachineName3"] = MachineName3;
                   // dr["DateTime"] = DateTime.Today.ToString("MM/dd/yyyy");
                   // dr["DateTime"] = DateTime.Now.ToString("MM/dd/yyyy");
                    dt.Rows.Add(dr);
                    dt.TableName = "_Server";
                    ds.InsertDBToSQL(dt);
                    //----
                    using (frmEntrykey frm = new frmEntrykey())
                    {
                        frm.Keyunlock = dr["KeyUnLock"].ToString();
                        frm.ShowDialog();
                    }
                    this.Hide();
                }
                else
                    MessageBox.Show("Lỗi kết nối đến máy chủ!");

            }

        }
        private long RandomNumber(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }
        private void frmmain_Load(object sender, EventArgs e)
        {
            this.BackColor = VariableSystem.BackGR;
            foreach (Control Control in (Ultis.GetAllControls(this)))
            {
                Control.Font = VariableSystem.Font; ;
            }
            string cnn = Ultis.GetConnect();
            if (!string.IsNullOrWhiteSpace(cnn) || !string.IsNullOrEmpty(cnn))
            {
                string[] ss1 = cnn.Split(';');
                textBox1.Text = ss1[0].ToString().Trim().Substring(ss1[0].IndexOf('=') + 1);
                textBox2.Text = ss1[1].ToString().Trim().Substring(ss1[1].IndexOf('=') + 1);
                textBox3.Text = ss1[2].ToString().Trim().Substring(ss1[2].IndexOf('=') + 1);
                textBox4.Text = ss1[3].ToString().Trim().Substring(ss1[3].IndexOf('=') + 1);
            }
        }

        private void btncancel_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmLogin frm = new frmLogin();
            frm.ShowDialog();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
