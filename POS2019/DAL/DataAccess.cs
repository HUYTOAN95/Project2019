using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS2019.DAL
{
    public class DataAccess
    {
        public DataSet ds = null;
        public SqlDataAdapter da = null;
        private SqlDataReader dr = null;
        public bool InsertDBToSQL(DataTable dt)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                if (dt != null)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {

                        sb.Append("INSERT INTO " + dt.TableName + "(");

                        for (int checkcol = 0; checkcol < dt.Columns.Count; checkcol++)
                        {
                            if (dt.Columns[checkcol].ColumnName.StartsWith("f_identity"))
                            {
                                MessageBox.Show("1");
                                sb.Append(dt.Columns[checkcol - 1].ColumnName).Append(",");
                            }
                            else
                            {
                                sb.Append(dt.Columns[checkcol].ColumnName).Append(",");
                            }
                        }
                        //for (int col = 0; col < dt.Columns.Count - 1; col++)
                        //{
                        //    sb.Append(dt.Columns[col].ColumnName).Append(",");
                        //}

                        sb.Remove(sb.Length - 1, 1);
                        sb.Append(") VALUES (");
                        for (int j = 0; j < dt.Columns.Count; j++)
                        {
                            if (dt.Columns[j].ColumnName.StartsWith("f_identity"))
                            {
                                System.Type ColType = dt.Rows[i][j - 1].GetType();
                                if (ColType == typeof(int) || ColType == typeof(decimal))
                                {

                                    sb.Append(dt.Rows[i][j - 1].ToString()).Append(",");
                                }
                                else
                                {
                                    sb.Append("'").Append(dt.Rows[i][j - 1].ToString()).Append("'").Append(",");
                                }
                            }
                            else
                            {
                                System.Type ColType = dt.Rows[i][j].GetType();
                                if (ColType == typeof(int) || ColType == typeof(decimal))
                                {

                                    sb.Append(dt.Rows[i][j].ToString()).Append(",");
                                }
                                else
                                {
                                    sb.Append("'").Append(dt.Rows[i][j].ToString()).Append("'").Append(",");
                                }
                            }

                        }
                        //for (int j = 0; j < dt.Columns.Count - 1; j++)
                        //{
                        //    System.Type ColType = dt.Rows[i][j].GetType();
                        //    if (ColType == typeof(int) || ColType == typeof(decimal))
                        //    {

                        //        sb.Append(dt.Rows[i][j].ToString()).Append(",");
                        //    }
                        //    else
                        //    {
                        //        sb.Append("'").Append(dt.Rows[i][j].ToString()).Append("'").Append(",");
                        //    }
                        //}
                        // }

                        sb.Remove(sb.Length - 1, 1);
                        sb.Append(")");
                    }
                    //MessageBox.Show(sb.ToString());
                    Ultis.WriteLog(sb.ToString());
                }
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = ConnectDataBase.Cnn;
                cmd.CommandText = sb.ToString();
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                Ultis.WriteLog(ex.ToString());
                return false;

            }

            return true;
        }
        public bool UpdateExRate(bool IsUpate)
        {
            //-------- Kiểm tra ngày đó tỷ giá được cập nhật hay chưa
            try
            {
                if (IsUpate)
                {

                    MessageBox.Show("1");
                    string sqlcmd = @"select count(*) from _Currency where DateTime ='" + DateTime.Now.ToString("MM/dd/yyyy") + "'";
                    Ultis.WriteLog(sqlcmd);
                    string isUpdate = SQLEXEC(sqlcmd).ToString();
                    if (Int32.Parse(isUpdate) > 0) { return false; }
                    else
                    {
                        System.Xml.XmlDocument xml = new System.Xml.XmlDocument();
                        xml.Load("http://www.vietcombank.com.vn/exchangerates/ExrateXML.aspx");
                        System.Xml.XmlNodeList noXml;
                        noXml = xml.SelectNodes("/ExrateList/Exrate");
                        int i = 0;
                        DataTable dt = new DataTable();
                        dt = SelectSQL("select top(0) * from _Currency");
                        for (i = 0; i <= noXml.Count - 1; i++)
                        {
                            DataRow dr = dt.NewRow();
                            //ListViewItem CurrencyCode = new ListViewItem(noXml.Item(i).Attributes["CurrencyCode"].InnerText);
                            //ListViewItem CurrencyCode = new ListViewItem(noXml.Item(i).Attributes["CurrencyCode"].InnerText);
                            //// item.SubItems.Add(noXml.Item(i).Attributes["ListViewItem"].InnerText);
                            ////item.SubItems.Add(noXml.Item(i).Attributes["Buy"].InnerText);
                            ////item.SubItems.Add(noXml.Item(i).Attributes["Transfer"].InnerText);
                            //// item.SubItems.Add(noXml.Item(i).Attributes["Sell"].InnerText);
                            ////MessageBox.Show(noXml.Item(i).Attributes["Sell"].InnerText);

                            //ListViewItem item2 = new ListViewItem(noXml.Item(i).Attributes["Sell"].InnerText);
                            //ListViewItem item3 = new ListViewItem(noXml.Item(i).Attributes["Transfer"].InnerText);
                            // item2.SubItems.Add(noXml.Item(i).Attributes["Sell"].InnerText);
                            dr["CurrencyCode"] = noXml.Item(i).Attributes["CurrencyCode"].InnerText;
                            dr["Buy"] = noXml.Item(i).Attributes["Buy"].InnerText;
                            dr["Transfer"] = noXml.Item(i).Attributes["Transfer"].InnerText;
                            dr["Sell"] = noXml.Item(i).Attributes["Sell"].InnerText;
                            dr["DateTime"] = System.DateTime.Now.ToString("MM/dd/yyyy");
                            dt.Rows.Add(dr);
                        }
                        dt.TableName = "_Currency";
                        InsertDBToSQL(dt);
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                Ultis.WriteLog(ex.ToString());
            }

            //----------------------------------------------------
            return false;
        }
        public DataTable SelectSQL(string sql)
        {
            SqlCommand cmd = new SqlCommand();
            DataTable dt = new DataTable();
            da = new SqlDataAdapter();
            dt = null;
            DataSet ds = new DataSet();
            try
            {
                cmd.Connection = ConnectDataBase.Cnn;
                cmd.CommandText = sql;
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
                da.SelectCommand = cmd;
                da.Fill(ds);
                dt = ds.Tables[0];
                Ultis.WriteLog(sql);
            }
            catch (SqlException ex)
            {
                Ultis.WriteLog(ex.ToString());
                return null;
            }
            finally
            {

            }
            return dt;


        }
        /// <summary>
        /// Hàm lấy dữ liệu trả về kiểu đối tượng với 1 đối tượng nên câu lệnh truyền vào được phép lấy 1 đối tượng - theo câu lệnh 
        /// truy vấn sql " select id from table where 1=1"
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public object SQLEXEC(string sql)
        {
            object msg = new object();

            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = ConnectDataBase.Cnn;
                cmd.CommandText = sql;
                cmd.CommandType = CommandType.Text;
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    msg = dr[0];

                }
                dr.Close();
                Ultis.WriteLog(sql);
            }
            catch (Exception ex)
            {
                Ultis.WriteLog(ex.ToString());
                return null;
            }
            return msg;


        }
        /// <summary>
        ///  Hàm lấy dữ liệu trả về kiểu đối tượng với 1 đối tượng nên câu lệnh truyền vào được phép lấy 1 đối tượng - theo thủ tục procedure 
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public object SQLEXEC0A(string sql)
        {
            object msg = new object();

            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = ConnectDataBase.Cnn;
                cmd.CommandText = sql;
                cmd.CommandType = CommandType.StoredProcedure;
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    msg = dr[0];

                }
                dr.Close();
                Ultis.WriteLog(sql);
            }
            catch (Exception ex)
            {
                Ultis.WriteLog(ex.ToString());
                return null;
            }
            return msg;


        }
        public bool SQLEXEC1(string sql)
        {

            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = ConnectDataBase.Cnn;
                cmd.CommandText = sql;
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
                Ultis.WriteLog(sql);
            }
            catch (SqlException ex)
            {
                Ultis.WriteLog(ex.ToString());
                return false;
            }
            finally
            {
            }
            return true;


        }
        public bool SQLEXEC1A(string sql)
        {

            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = ConnectDataBase.Cnn;
                cmd.CommandText = sql;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();
                Ultis.WriteLog(sql);
            }
            catch (SqlException ex)
            {
                Ultis.WriteLog(ex.ToString());
                return false;
            }
            finally
            {
            }
            return true;


        }
    }
}
