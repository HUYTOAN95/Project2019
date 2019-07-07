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
    public partial class frmServerChat : Form
    {

        private Socket server, client;
        byte[] buff = new byte[1024];
        byte[] buff2 = new byte[1024];
        private delegate void updateUI(string massage);
        private updateUI updateUi;
        public frmServerChat()
        {
            InitializeComponent();
            updateUi = new updateUI(update);
            CheckForIllegalCrossThreadCalls = false;
        }
        private void update(string m)
        {
            listBox1.Items.Add(m);
        }
        private void startServer()
        {
            EndPoint ep = new IPEndPoint(IPAddress.Any, 8000);
            server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            server.Bind(ep);
            server.Listen(10);
            server.BeginAccept(new AsyncCallback(beginAccept), server);
            updateUi("Đang lắng nghe các kết nối....");
        }
        private void beginAccept(IAsyncResult ar)
        {
            Socket s = (Socket)ar.AsyncState;
            client = s.EndAccept(ar);
            updateUi("Đã nhận kết nối từ client " + client.RemoteEndPoint.ToString());
            string wc = "Xin chao client!...";
            buff2 = Encoding.ASCII.GetBytes(wc);
            client.BeginSend(buff2, 0, buff2.Length, SocketFlags.None, new AsyncCallback(sendata), client);
            client.BeginReceive(buff, 0, buff.Length, SocketFlags.None, new AsyncCallback(beginReceive), client);
        }
        private void beginReceive(IAsyncResult ia)
        {
            Socket s = (Socket)ia.AsyncState;
            int recv = 0;
            recv = s.EndReceive(ia);
            //string recvei = Encoding.ASCII.GetString(buff, 0, recv);
            string recvei = Encoding.UTF8.GetString(buff, 0, recv).TrimEnd();
            updateUi("Client: " + recvei);
            if (recvei.ToUpper() == "")
            {
                updateUi("Ngắt kết nối với client ");
                server.Close();
                client.Close();
                this.Close();
            }
            buff = new byte[1024];
            client.BeginReceive(buff, 0, buff.Length, SocketFlags.None, new AsyncCallback(beginReceive), client);
        }
        private void sendata(IAsyncResult ia)
        {
            client.EndSend(ia);
        }
        private void frmServerChat_Load(object sender, EventArgs e)
        {
            this.BackColor = VariableSystem.BackGR;
            foreach (Control Control in (Ultis.GetAllControls(this)))
            {
                Control.Font = VariableSystem.Font; ;
            }
            this.Icon = VariableSystem.IconPath;
            this.ShowIcon = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            startServer();
            textBox1.Focus();
        }
        private void send()
        {
            string hl = "["+VariableSystem.Userlogin+"] "+ textBox1.Text;
            byte[] gui = new byte[1024];
            gui = Encoding.ASCII.GetBytes(hl);
            textBox1.Clear();
            updateUi("Server: " + hl);
            client.BeginSend(gui, 0, gui.Length, SocketFlags.None, new AsyncCallback(sendata), client);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            send();
            textBox1.Focus();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            updateUi("Ngắt kết nối với client");
            client.Shutdown(SocketShutdown.Both);
            client.Close();
            server.Close();
        }
    }
}
