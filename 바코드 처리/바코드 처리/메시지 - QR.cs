﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 바코드_처리
{
    public partial class 메시지___QR : Form
    {
        private Form1 frm;

        public 메시지___QR()
        {
            InitializeComponent();
        }

        public 메시지___QR(Form1 frm)
        {
            InitializeComponent();
            this.frm = frm;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            StringBuilder cardeFormat = new StringBuilder();

            cardeFormat.Append("smsto:"); // 메세지 
            cardeFormat.AppendFormat("{0}:", textBox1.Text.Trim()); // 번호
            cardeFormat.AppendFormat("{0}", textBox2.Text.Trim()); // 내용
            WebRequest requestPic = WebRequest.Create("http://chart.apis.google.com/chart?cht=qr&chs=586x225&chl=" + cardeFormat);
            WebResponse responsePic = requestPic.GetResponse();
            frm.pictureBox1.Image = Image.FromStream(responsePic.GetResponseStream());
        }
    }
}
