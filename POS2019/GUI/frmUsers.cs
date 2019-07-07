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
    public partial class frmUsers : Form
    {
        private DataAccess ds;
        public frmUsers()
        {
            InitializeComponent();
            ds = new DataAccess();
        }

        private void frmUsers_Load(object sender, EventArgs e)
        {
            this.BackColor = VariableSystem.BackGR;
            foreach (Control Control in (Ultis.GetAllControls(this)))
            {
                Control.Font = VariableSystem.Font; ;
            }
            this.Icon = VariableSystem.IconPath;
            this.StartPosition = VariableSystem.Position;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string Sqlcheck = "Select count(*) from _Users where UserName ='" + textBox1.Text + "'";
            int CheckUser = 0;
            Int32.TryParse(ds.SQLEXEC(Sqlcheck).ToString(), out CheckUser);
            if (CheckUser > 0)
            {
                if (MessageBox.Show("Tên người dùng đã tồn tại, Bạn có thay đổi thông tin của TK này không ?", "Thông Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
                else
                {
                    string sqldele = "Delete from _Users where UserName ='" + textBox1.Text + "'";
                    ds.SQLEXEC1(sqldele);
                }
            }

            //------------ Thêm mới tài users---------------------
            {
                string sqlmax = "Select Max(UserCode) from _Users";
                int MaxID = 0;
                Int32.TryParse(ds.SQLEXEC(sqlmax).ToString(), out MaxID);
                MaxID += 1;
                DataTable dt = ds.SelectSQL("select top(0) * from _Users");
                DataRow dr = dt.NewRow();
                dr["UserCode"] = MaxID;
                dr["UserName"] = textBox1.Text.Trim();
                dr["Password"] = textBox3.Text.Trim();
                if (checkBox4.Checked) dr["Status"] = 1; else dr["Status"] = 0;
                dt.Rows.Add(dr);
                dt.TableName = "_Users";
                if (ds.InsertDBToSQL(dt)) MessageBox.Show("Thêm mới thành công !");
            }
        }
    }
}
