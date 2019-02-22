using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Monitor
{
    public partial class 网络监视器 : Form
    {
        public 网络监视器()
        {
            InitializeComponent();
        }

        public 网络监视器(string text)
        {
            InitializeComponent();
            textBox.Text = text;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private System.Timers.Timer timer1;//面向设备定时器
        private void 网络监视器_Load(object sender, EventArgs e)
        {
            timer1 = new System.Timers.Timer(2000);
            timer1.AutoReset = true;
            timer1.Elapsed += new System.Timers.ElapsedEventHandler(timer1_Elapsed);
        }
        void timer1_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            System.Drawing.SolidBrush myBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Red);//画刷
            this.CreateGraphics().FillEllipse(myBrush, 120, 20, 30, 30);
        }

    }
}
