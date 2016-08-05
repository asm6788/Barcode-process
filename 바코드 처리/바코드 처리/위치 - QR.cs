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

namespace 바코드_처리
{
    public partial class 위치___QR : Form
    {
        private Form1 frm;

        public 위치___QR()
        {
            InitializeComponent();
        }

        public 위치___QR(Form1 frm)
        {
            InitializeComponent();
            this.frm = frm;
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            webBrowser1.Navigate("http://maps.google.com/maps?z=12&t=m&q=loc:"+textBox1.Text.Trim() +"+"+textBox2.Text.Trim());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            StringBuilder cardeFormat = new StringBuilder();

            cardeFormat.Append("geo:"); // 메세지 
            cardeFormat.AppendFormat("{0},{1}", textBox1.Text.Trim(), textBox2.Text.Trim()); // 번호
            WebRequest requestPic = WebRequest.Create("http://chart.apis.google.com/chart?cht=qr&chs=586x225&chl=" +cardeFormat);
            WebResponse responsePic = requestPic.GetResponse();
            frm.pictureBox1.Image = Image.FromStream(responsePic.GetResponseStream());
        }
    }
}
