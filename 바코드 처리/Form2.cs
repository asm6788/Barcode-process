using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZXing;

namespace 바코드_처리
{
    public partial class Form2 : Form
    {
        private Form1 frm;
        public Form2()
        {
            InitializeComponent();
        }

        public Form2(Form1 form1)
        {
            InitializeComponent();
            this.frm = form1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (frm.textBox2.Text != "")
            {
                frm.QR = false;
                frm.button6.Enabled = true;
                QR코드생성(frm.textBox2.Text);

            }
            else
            {
                frm.textBox1.Text = "\r\n입력값이 없음";

            }
        }

        private void QR코드생성(string text)
        {
            WebRequest requestPic = WebRequest.Create("http://chart.apis.google.com/chart?cht=qr&chs=586x225&chl=" + text);
            WebResponse responsePic = requestPic.GetResponse();
            frm.pictureBox1.Image = Image.FromStream(responsePic.GetResponseStream());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form3 frm1 = new Form3(frm);
            frm1.Show();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form4 frm1 = new Form4(frm);
            frm1.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            메시지___QR frm1 = new 메시지___QR(frm);
            frm1.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            전화번호___QR frm1 = new 전화번호___QR(frm);
            frm1.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            이메일___QR frm1 = new 이메일___QR(frm);
            frm1.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            위치___QR frm1 = new 위치___QR(frm);
            frm1.Show();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            스케줄___QR frm1 = new 스케줄___QR(frm);
            frm1.Show();
        }
    }
}
