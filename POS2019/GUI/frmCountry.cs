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
    public partial class frmCountry : Form
    {
        private DataAccess ds;
        public frmCountry()
        {
            InitializeComponent();
            ds = new DataAccess();
        }

        private void frmCountry_Load(object sender, EventArgs e)
        {
            dataGridView1.ColumnCount = 6;
            dataGridView1.Columns[0].Name = "DistrictID";
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[0].DataPropertyName = "DistrictID";
            dataGridView1.Columns[1].Name = "DistrictCode";
            dataGridView1.Columns[1].DataPropertyName = "DistrictCode";
            dataGridView1.Columns[2].Name = "DistrictName";
            dataGridView1.Columns[2].DataPropertyName = "DistrictName";
            dataGridView1.Columns[3].Name = "EnglishName";
            dataGridView1.Columns[3].DataPropertyName = "EnglishName";
            dataGridView1.Columns[4].Name = "Grate";
            dataGridView1.Columns[4].DataPropertyName = "Grate";
            dataGridView1.Columns[5].Name = "ProvinceName";
            dataGridView1.Columns[5].DataPropertyName = "ProvinceName";
            dataGridView1.Columns[0].HeaderText = "ID";
            dataGridView1.Columns[1].HeaderText = "Mã";
            dataGridView1.Columns[2].HeaderText = "Tên";
            dataGridView1.Columns[3].HeaderText = "Tên E";
            dataGridView1.Columns[4].HeaderText = "Cấp";
            dataGridView1.Columns[5].HeaderText = "Tỉnh";
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.AllowUserToResizeColumns = false;
            dataGridView1.AllowUserToResizeRows = false;
            LoadData();
        }
        public void LoadData()
        {
            string sqlcmd = @"Select a.DistrictID,a.DistrictCode,a.DistrictName,a.EnglishName,a.Grate, b.ProvinceName from _District a inner join _Province b on a.ProvinceCode = b.ProvinceCode";
            dataGridView1.DataSource = ds.SelectSQL(sqlcmd);
        }
        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
           
        }
    }
}
