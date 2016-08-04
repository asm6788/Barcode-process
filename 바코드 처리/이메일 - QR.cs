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
    public partial class 이메일___QR : Form
    {
        private Form1 frm;

        public 이메일___QR()
        {
            InitializeComponent();
        }

        public 이메일___QR(Form1 frm)
        {
            InitializeComponent();
            this.frm = frm;
        }

        private void 이메일___QR_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            StringBuilder cardeFormat = new StringBuilder();

            cardeFormat.Append("mailto:"); // 메세지 
            cardeFormat.AppendFormat("{0}", textBox1.Text.Trim()); // 번호
            WebRequest requestPic = WebRequest.Create("http://chart.apis.google.com/chart?cht=qr&chs=586x225&chl=" + cardeFormat);
            WebResponse responsePic = requestPic.GetResponse();
            frm.pictureBox1.Image = Image.FromStream(responsePic.GetResponseStream());
        }
    }
}
