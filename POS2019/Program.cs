using POS2019.DAL;
using POS2019.GUI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS2019
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            VariableSystem.BackGR = Color.FromArgb(176, 196, 222);
            VariableSystem.Font = new Font("Times New Roman",10, FontStyle.Regular);
            VariableSystem.IconPath = new System.Drawing.Icon(Path.GetFullPath(@"IMG\logo.ico"));
            VariableSystem.Position = FormStartPosition.CenterScreen;
            VariableSystem.Forecolor = Color.Black;
            
            string cnn = Ultis.GetConnect();
            if (!string.IsNullOrWhiteSpace(cnn) || !string.IsNullOrEmpty(cnn))
            {
                string[] ss1 = cnn.Split(';');
                string server = ss1[0].ToString().Trim().Substring(ss1[0].IndexOf('=') + 1);
                string db = ss1[1].ToString().Trim().Substring(ss1[1].IndexOf('=') + 1);
                string user = ss1[2].ToString().Trim().Substring(ss1[2].IndexOf('=') + 1);
                string pass = ss1[3].ToString().Trim().Substring(ss1[3].IndexOf('=') + 1);

                if (ConnectDataBase.ExIsOpen(ConnectDataBase.InfoConnect(server, db, user, pass)) || ConnectDataBase.ExIsReady(ConnectDataBase.InfoConnect(server, db, user, pass)))
                {
                    //MessageBox.Show("1");
                    // cập nhật tỷ giá 

                    frmlogin2 frm = new frmlogin2();
                    frm.ShowDialog();
                }
                else
                {
                    Application.Run(new frmmain());
                }
            }
            else
            {
                Application.Run(new frmmain());
            }
        }
    }
}
