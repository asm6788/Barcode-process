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
    public partial class 스케줄___QR : Form
    {
        private Form1 frm;

        public 스케줄___QR()
        {
            InitializeComponent();
        }

        public 스케줄___QR(Form1 frm)
        {
            InitializeComponent();
            this.frm = frm;
        }

        private void 스케줄___QR_Load(object sender, EventArgs e)
        {

        }
        string 시간계산(int input)
        {
            int hour = Convert.ToInt32(input.ToString().Substring(0,2));
            int min = Convert.ToInt32(input.ToString().Substring(1, 2));
            if(hour <= 8)
            {
                hour += 15;
            }
            else if (hour >= 9)
            {
                hour -= 9;
            }
            return hour.ToString("D2") + min.ToString("D4");
        }
        private void button1_Click(object sender, EventArgs e)
        {
            StringBuilder cardeFormat = new StringBuilder();

            cardeFormat.Append("BEGIN:VEVENT"); // 카드형태 선언   
            cardeFormat.AppendFormat("\r\nSUMMARY:{0}", textBox1.Text.Trim()); //제목
            cardeFormat.AppendFormat("\r\nDTSTART:{0}", dateTimePicker1.Value.ToString("yyyyMMdd") +"T" +시간계산(Convert.ToInt32(textBox2.Text))+"Z"); // 시작날짜,시간  
            cardeFormat.AppendFormat("\r\nDTEND:{0}", dateTimePicker2.Value.ToString("yyyyMMdd") + "T" + 시간계산(Convert.ToInt32(textBox3.Text)) + "Z"); // 끝날짜,시간  
            cardeFormat.AppendFormat("\r\nLOCATION:{0}", textBox4.Text.Trim()); // 주소 
            cardeFormat.AppendFormat("\r\nDESCRIPTION:{0}", textBox5.Text.Trim()); // 주소   
            cardeFormat.AppendFormat("\r\nEND:VEVENT");  
            WebRequest requestPic = WebRequest.Create("http://chart.apis.google.com/chart?cht=qr&chs=586x225&chl=" + cardeFormat);
            WebResponse responsePic = requestPic.GetResponse();
            frm.pictureBox1.Image = Image.FromStream(responsePic.GetResponseStream());
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
