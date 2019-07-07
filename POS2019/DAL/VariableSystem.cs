using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS2019.DAL
{
    class VariableSystem
    {
        public static Color BackGR { get; set; }
        public static Font Font { get; set; }
        public static Icon IconPath { get; set; }
        public static int UserCode { get; set; }
        public static int NoWS { get; set; }
        public static int BranchID { get; set; }
        public static string IPServer { get; set; }
        public static string IPLocal { get; set; }
        public static int Port { get; set; }
        public static string Userlogin { get; set; }
        public static FormStartPosition Position { get; set; }
        public static Color Forecolor { get; set; } /// = Color.Black;

    }
}
