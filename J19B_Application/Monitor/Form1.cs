using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Threading;
using System.IO.Ports;

using System.Net.Sockets;
using System.Net;
using System.Reflection;
using System.Collections.Specialized;


namespace Monitor
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            pictureBox1.BackColor = Color.Gray;
            timertemp1 = new System.Timers.Timer(2000);
            timertemp1.AutoReset = true;
            timertemp1.Elapsed += new System.Timers.ElapsedEventHandler(timertemp1_Elapsed);
        }
        private System.Timers.Timer timertemp1;//面向设备定时器

        void timertemp1_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            pictureBox1.BackColor = Color.Gray;
            string url = "http://api.heclouds.com/devices/505608937/datapoints?";//设备地址
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            SetHeaderValue(request.Headers, "api-key", "qBctplasdtyk3bImaCxjb3B3==w=");//设备API地址和 首部参数
            request.Host = "api.heclouds.com";
            request.ProtocolVersion = new Version(1, 1);
            request.ContentType = "text/html;charset=UTF-8";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream myResponseStream = response.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
            string retString = myStreamReader.ReadToEnd();
            myStreamReader.Close();
            myResponseStream.Close();
            textBox5.Text = retString;
  
            网络监视器 f2 = new 网络监视器(retString);
            f2.ShowDialog();
            string[] temp = new string[10];
            string[] hum = new string[10];
            Redata = retString.Split('"');

            //显示温度
            temp = Redata[284].Split(':');
            temp = temp[1].Split('}');
            textBox2.Text = temp[0] + "℃";
            //显示湿度
            hum = Redata[296].Split(':');
            hum = hum[1].Split('}');
            textBox3.Text = hum[0] + "%Rh";

            textBox4.Text = Redata[281];
            //if (Redata[45] == "on")
            //{ pictureBox1.BackColor = Color.Lime; }
            //else
            //{ pictureBox1.BackColor = Color.Red; }

        }

        public static void SetHeaderValue(WebHeaderCollection header, string name, string value)// HTTP协议报文头加入

        {
            var property = typeof(WebHeaderCollection).GetProperty("InnerCollection", BindingFlags.Instance | BindingFlags.NonPublic);
            if (property != null)
            {
                var collection = property.GetValue(header, null) as NameValueCollection;
                collection[name] = value;
            }
        }
        string[] Redata = new string[2048];
        private void button1_Click(object sender, EventArgs e)//连接服务器并获取数据
        {
            
            string url = "http://api.heclouds.com/devices/505608937/datapoints?";//设备地址
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            SetHeaderValue(request.Headers, "api-key", "qBctplasdtyk3bImaCxjb3B3==w=");//设备API地址和 首部参数
            request.Host = "api.heclouds.com";
            request.ProtocolVersion = new Version(1, 1);
            request.ContentType = "text/html;charset=UTF-8";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream myResponseStream = response.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
            string retString = myStreamReader.ReadToEnd();
            myStreamReader.Close();
            myResponseStream.Close();
            textBox5.Text = retString;
            string[] temp = new string[10];
            string[] hum = new string[10];
            Redata = retString.Split('"');

            //显示温度
            temp = Redata[284].Split(':');
            temp = temp[1].Split('}');
            textBox2.Text = temp[0] + "℃";
            //显示湿度
            hum = Redata[296].Split(':');
            hum = hum[1].Split('}');
            textBox3.Text = hum[0] + "%Rh";

           textBox4.Text = Redata[281];

        }

        private void button2_Click(object sender, EventArgs e)
        {

            if (button2.Text == "自动开启")
            {   timertemp1.Start();
                button2.Text = "自动关闭";
            }
            else
            {
                button2.Text = "自动开启";
                pictureBox1.BackColor = Color.Gray;
                timertemp1.Close();
            }
            
        }

        private void 串口监视器ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            网络监视器 mainForm = new 网络监视器();
            mainForm.Show();
        }

        private void 仪表盘ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HTML mainForm = new HTML();
            mainForm.Show();
        }
    }
}
