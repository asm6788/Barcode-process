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
    public partial class Form4 : Form
    {
        private Form1 frm;

        public Form4()
        {
            InitializeComponent();
        }

        public Form4(Form1 frm)
        {
            InitializeComponent();
            this.frm = frm;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            StringBuilder cardeFormat = new StringBuilder();

            cardeFormat.Append("WIFI:"); // 카드형태 선언   
            cardeFormat.AppendFormat("S:{0};", textBox1.Text.Trim()); // SSID
            switch (comboBox1.GetItemText(comboBox1.SelectedItem))
            {
                case "암호화 X":
                    break;
                case "WEP":
                    cardeFormat.AppendFormat("T:WEP;");   
                    break;
                case "WAP/WAP2":
                    cardeFormat.AppendFormat("T:WPA;");   
                    break;
            }
            cardeFormat.AppendFormat("P:{0};", textBox2.Text.Trim()); // 비번
            if (checkBox1.Checked)
            {
                cardeFormat.AppendFormat("H:true;");
            }
            cardeFormat.Append(";");
            WebRequest requestPic = WebRequest.Create("http://chart.apis.google.com/chart?cht=qr&chs=586x225&chl=" + cardeFormat);
            WebResponse responsePic = requestPic.GetResponse();
            frm.pictureBox1.Image = Image.FromStream(responsePic.GetResponseStream());
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
