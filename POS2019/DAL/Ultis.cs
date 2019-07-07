using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Management;
using System.Data;
using System.Data.OleDb;


namespace POS2019.DAL
{
    public class Ultis
    {
        /// <summary>
        /// Mèo đầu gấu -- hàm viết lỗi ra file log.text
        /// </summary>
        /// <returns></returns>
        public static string WriteLog(string MsgLog)
        {
            string basePath = Application.StartupPath;
            string txtPath = basePath + @"/log.txt";

            if (!File.Exists(txtPath)) File.Create(txtPath);
            try
            {
                using (StreamWriter sw = File.AppendText(txtPath))
                {
                    // AppendLog(MsgLog, sw);

                    sw.Write("\r\nLog Entry : ");
                    sw.WriteLine("{0} {1}", DateTime.Now.ToLongTimeString(), DateTime.Now.ToLongDateString());
                    sw.WriteLine("------Begin - Mèo Đầu Gấu-------------------------");
                    sw.WriteLine("Nội dung :{0} ", MsgLog);
                    sw.WriteLine("-------End -- Mèo Đầu Gấu ------------------------");
                }
            }

            catch (Exception ex)
            {

            }
            return MsgLog;

        }
        public static string WriteCode(string codetext)
        {
            string basePath = Application.StartupPath;
            string txtPath = basePath + @"/" + codetext + ".cs";

            if (!File.Exists(txtPath)) File.Create(txtPath);
            try
            {
                using (StreamWriter sw = File.AppendText(txtPath))
                {
                    // AppendLog(MsgLog, sw);

                    sw.Write("\r\nLog Entry : ");
                    sw.WriteLine("{0} {1}", DateTime.Now.ToLongTimeString(), DateTime.Now.ToLongDateString());
                    sw.WriteLine("------Begin - Mèo Đầu Gấu-------------------------");
                    sw.WriteLine("Nội dung :{0} ", codetext);
                    sw.WriteLine("-------End -- Mèo Đầu Gấu ------------------------");
                }
            }

            catch (Exception ex)
            {

            }
            return codetext;

        }
        public static string WriteConnect(string cnnstr)
        {
            string basePath = Application.StartupPath;
            string txtPath = basePath + @"/config.txt";
            if (!File.Exists(txtPath)) File.Create(txtPath);
            try
            {

                using (StreamWriter sw = File.AppendText(txtPath))
                {
                    // MessageBox.Show("1");
                    // AppendLog(MsgLog, sw);

                    // sw.Write("\r\nLog Entry : ");
                    // sw.WriteLine("{0} {1}", DateTime.Now.ToLongTimeString(), DateTime.Now.ToLongDateString());
                    // sw.WriteLine("  :");

                    sw.WriteLine("{0}", cnnstr);
                    //sw.WriteLine("-------------------------------");
                }

            }

            catch (Exception ex)
            {
                WriteLog(ex.ToString());
            }
            return cnnstr;

        }
        public static string GetConnect()
        {
            string strcnn = "";
            string basePath = Application.StartupPath;
            string txtPath = basePath + @"/config.txt";
            try
            {
                if (File.Exists(txtPath))
                {
                    using (StreamReader str = new StreamReader(txtPath))
                    {
                        while (str.Peek() >= 0)
                        {
                            strcnn = str.ReadLine();
                            //string[] ss1 = ss.Split(';');
                            //server = ss1[0].ToString().Trim();
                            //db = ss1[1].ToString().Trim();
                            //user = ss1[2].ToString().Trim();
                            //pass = ss1[3].ToString().Trim();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                WriteLog(ex.ToString());
            }
            return strcnn;

        }
        public static bool IsNumeric(object Input)
        {
            if (Input == null || Input is DateTime) return false;
            if (Input is Int16 || Input is Int32 || Input is Int64 || Input is Decimal || Input is Single || Input is Double || Input is Boolean)
                return true;
            try
            {
                if (Input is string)
                    Double.Parse(Input as string);
                else
                    Double.Parse(Input.ToString());
                return true;
            }
            catch { }

            return false;
        }
        public static bool isEmail(string inputEmail)
        {
            inputEmail = inputEmail ?? string.Empty;
            string strRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                  @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                  @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
            Regex re = new Regex(strRegex);
            if (re.IsMatch(inputEmail))
                return (true);
            else
                return (false);
        }
        public static bool isPhone(string inputPhone)
        {
            inputPhone = inputPhone ?? string.Empty;
            string strRegex = @"\d{3}-\d{4}-\d{3}";
            Regex re = new Regex(strRegex);
            if (re.IsMatch(inputPhone)) return true;
            else return false;
        }
        // hàm import excel to datatable 
        private static DataTable ReadExcel(string filePath)
        {
            DataTable table = new DataTable();
            string strConn = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=\"Excel 12.0 Xml;HDR=YES;IMEX=1;TypeGuessRows=0;ImportMixedTypes=Text\"", filePath);
            using (OleDbConnection dbConnection = new OleDbConnection(strConn))
            {
                using (OleDbDataAdapter dbAdapter = new OleDbDataAdapter("SELECT * FROM [Sheet1$]", dbConnection)) //rename sheet if required!
                    dbAdapter.Fill(table);
                int rows = table.Rows.Count;
            }
            return table;
        }
        public static bool SendMail(string ToMail, string FromMail, string FromPW, string Content, string Subject, System.Net.Mail.Attachment FileAttach)
        {
            try
            {
                MailMessage msg = new MailMessage();
                msg.From = new MailAddress(FromMail);
                msg.To.Add(ToMail);
                msg.Subject = Subject;
                msg.Body = Content;
                if (FileAttach != null)
                {
                    FileAttach = new System.Net.Mail.Attachment("File Đính Kèm");
                    msg.Attachments.Add(FileAttach);
                }
                msg.IsBodyHtml = false;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.UseDefaultCredentials = false;
                NetworkCredential NetworkCred = new NetworkCredential(FromMail, FromPW);
                smtp.Credentials = NetworkCred;
                smtp.EnableSsl = true;
                smtp.Port = 587;
                smtp.Send(msg);
                return true;

            }
            catch (Exception ex)
            {

                Ultis.WriteLog(ex.ToString());

            }
            return false;
        }
        public static IEnumerable<Control> GetAllControls(Control aControl)
        {
            Stack<Control> stack = new Stack<Control>();

            stack.Push(aControl);

            while (stack.Any())
            {
                var nextControl = stack.Pop();

                foreach (Control childControl in nextControl.Controls)
                {
                    stack.Push(childControl);
                }

                yield return nextControl;
            }
        }
        public static string GetLocalIPAddress()
        {
            string IP = "";
            try
            {

                var host = Dns.GetHostEntry(Dns.GetHostName());
                foreach (var ip in host.AddressList)
                {
                    if (ip.AddressFamily == AddressFamily.InterNetwork)
                    {
                        IP = ip.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                Ultis.WriteLog(ex.ToString());
            }
            return IP;

        }
      

    }
}
