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
    public partial class Form3 : Form
    {
        private Form1 frm1;

        public Form3()
        {
            InitializeComponent();
        }


        public Form3(Form1 frm1)
        {
            InitializeComponent();
            this.frm1 = frm1;
        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            StringBuilder cardeFormat = new StringBuilder();

            cardeFormat.Append("MECARD:"); // 카드형태 선언   
            cardeFormat.AppendFormat("N:{0};", textBox1.Text.Trim()); // 이름   
            cardeFormat.AppendFormat("TEL:{0};", textBox2.Text.Trim()); // 전화번호   
            cardeFormat.AppendFormat("URL:{0};", textBox3.Text.Trim()); // URL   
            cardeFormat.AppendFormat("EMAIL:{0};", textBox4.Text.Trim()); // 이메일   
            cardeFormat.AppendFormat("ADR:{0};", textBox5.Text.Trim()); // 주소   
            cardeFormat.AppendFormat("NOTE:{0};", textBox6.Text.Trim()); // 메모   
            cardeFormat.Append(";");
            WebRequest requestPic = WebRequest.Create("http://chart.apis.google.com/chart?cht=qr&chs=586x225&chl=" + cardeFormat);
            WebResponse responsePic = requestPic.GetResponse();
            frm1.pictureBox1.Image = Image.FromStream(responsePic.GetResponseStream());
        
        }
    }
}
