using POS2019.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS2019.GUI
{
    public partial class frmChatClient : Form
    {


        private Socket client;
        byte[] buff = new byte[1024];
        byte[] buff2 = new byte[1024];

        private delegate void updateUI(string massage);
        private updateUI updateUi;
        public frmChatClient()
        {
            InitializeComponent();
            updateUi = new updateUI(update);
            CheckForIllegalCrossThreadCalls = false;
        }
        private void update(string m)
        {
            listBox1.Items.Add(m);
        }
        private void startClient()
        {
            EndPoint ep = new IPEndPoint(IPAddress.Parse(textBox1.Text), Int32.Parse(textBox2.Text));
            client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            updateUi("Đang kết nối tới server...");
            client.BeginConnect(ep, new AsyncCallback(beginConnect), client);
        }
        private void beginConnect(IAsyncResult ar)
        {
            Socket s = (Socket)ar.AsyncState;
            s.EndConnect(ar);
            updateUi("Đã nhận kết nối từ server " + s.RemoteEndPoint.ToString());
            string wc = "Xin chao server!...";
            buff2 = Encoding.ASCII.GetBytes(wc);
            client.BeginSend(buff2, 0, buff2.Length, SocketFlags.None, new AsyncCallback(sendata), client);

            client.BeginReceive(buff, 0, buff.Length, SocketFlags.None, new AsyncCallback(beginReceive), client);
        }
        private void sendata(IAsyncResult ia)
        {
            client.EndSend(ia);
        }
        private void beginReceive(IAsyncResult ia)
        {
            Socket s = (Socket)ia.AsyncState;
            int recv = 0;
            recv = s.EndReceive(ia);
            button2.Enabled = true;
            string recvei = Encoding.UTF8.GetString(buff, 0, recv).TrimEnd();
            
            updateUi("Server: " + recvei);
            if (recvei.ToUpper() == "")
            {
               
                updateUi("Ngắt kết nối với server ");
                client.Shutdown(SocketShutdown.Both);
                client.Close();
            }
            buff = new byte[1024];
            client.BeginReceive(buff, 0, buff.Length, SocketFlags.None, new AsyncCallback(beginReceive), client);
        }
        private void frmChatClient_Load(object sender, EventArgs e)
        {
            this.BackColor = VariableSystem.BackGR;
            foreach (Control Control in (Ultis.GetAllControls(this)))
            {
                Control.Font = VariableSystem.Font; ;
            }
            this.Icon = VariableSystem.IconPath;
            this.ShowIcon = true;
            textBox1.Text = VariableSystem.IPServer;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            startClient();
            textBox3.Focus();
        }
        private void send()
        {
            string wc = "["+VariableSystem.Userlogin+"] "+ textBox3.Text;
            byte[] gui = new byte[1024];
            gui = Encoding.ASCII.GetBytes(wc);
            textBox3.Clear();
            updateUi("Client:"+ wc);
            client.BeginSend(gui, 0, gui.Length, SocketFlags.None, new AsyncCallback(sendata), client);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            send();
            textBox3.Focus();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            updateUi("Ngắt kết nối với server");
            client.Shutdown(SocketShutdown.Both);
            client.Close();
            this.Close();
        }
    }
}
