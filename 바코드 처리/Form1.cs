using AForge.Video;
using AForge.Video.DirectShow;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using ZXing;
using ZXing.Common;

namespace 바코드_처리
{

    public partial class Form1 : Form
    {
     
        private FilterInfoCollection VideoCaptureDevices;
        private VideoCaptureDevice FinalVideo;
        IBarcodeReader reader = new BarcodeReader();
        public bool QR = false;
        public BarcodeFormat type = 0;
        private  Thread decodingThread;
        public Form1()
        {
            InitializeComponent();
            VideoCaptureDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            foreach (FilterInfo VideoCaptureDevice in VideoCaptureDevices)
            {
                comboBox1.Items.Add(VideoCaptureDevice.Name);
            }
            comboBox1.SelectedIndex = 0;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void videoSourcePlayer2_Click(object sender, EventArgs e)
        {
           
        }

        void FinalVideo_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            Bitmap video = (Bitmap)eventArgs.Frame.Clone();
            pictureBox1.Image = video;
            pictureBox1.Invalidate();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            FinalVideo = new VideoCaptureDevice(VideoCaptureDevices[comboBox1.SelectedIndex].MonikerString);
            FinalVideo.NewFrame += new NewFrameEventHandler(FinalVideo_NewFrame);
            FinalVideo.Start();
            decodingThread = new Thread(DecodeBarcode);
            decodingThread.Start();
        }

        private void DecodeBarcode()
        {
        
            while (true)
            {
                if (pictureBox1.Image != null)
                {
                    var barcodeBitmap = (Bitmap)pictureBox1.Image;
                    Thread.Sleep(5);
                    var result = reader.Decode(barcodeBitmap);
                    if (result != null)
                    {
                        Invoke(new Action(
                            delegate ()
                            {
                                if (textBox1.Text != "")
                                {
                                    if (textBox1.Lines[textBox1.Lines.Length-1] != "바코드구격: " + result.BarcodeFormat + " 내용: " + result.Text)
                                    {
                                        textBox1.AppendText("\r\n바코드구격: " + result.BarcodeFormat + " 내용: " + result.Text);
                                    }
                                }
                                else
                                {
                                    textBox1.AppendText("\r\n바코드구격: " + result.BarcodeFormat + " 내용: " + result.Text);
                                }
                            }));
                    }
                }
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (FinalVideo != null)
            {
                FinalVideo.Stop();
            }
        }

       

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        

        private void button3_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "JPEG|*.jpg|PNG|*.png|AllFiles(*.*)|*.*";
            openFileDialog1.Title = "원하시는 파일을 선택해 주십시오";

            
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                var barcodeBitmap = (Bitmap)Image.FromFile(openFileDialog1.FileName);
                var result = reader.Decode(barcodeBitmap);
                if (result != null)
                {
                    textBox1.Text = "바코드구격: "+result.BarcodeFormat+" 내용: "+ result.Text;
                }
                else
                {
                    textBox1.Text = "\r\n인식실패";
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                foreach (string 바코드값 in 폴더자동탐색바코드값내놔(folderBrowserDialog1.SelectedPath))
                {
                    textBox1.AppendText("\r\n" + 바코드값.Split(':')[0] + " 바코드구격: " + 바코드값.Split(':')[2] + " 내용: " + 바코드값.Split(':')[1]);
                }
               
            }
        }

        public void 바코드생성(string text)
        {
            try
            {

                var writer = new BarcodeWriter
                {
                    Format = type,
                    Options = new EncodingOptions
                    {
                        PureBarcode = true,
                        Height = 225,
                        Width = 586
                    }
                };
              
                pictureBox1.Image = writer.Write(text);
            }
            catch (Exception e)
            {
                textBox1.AppendText("\r\n" + e.Message);
            }
        }

        public string[] 폴더자동탐색바코드값내놔(string file)
        {
            List<string> 바코드주소들 = new List<string>();
            List<string> 바코드값들 = new List<string>();
            foreach (string 주소 in Directory.GetFiles(file))
            {
                바코드주소들.Add(주소);

            }
            바코드주소들.RemoveAt(8);
            for (int i = 0; i != 바코드주소들.Count; i++)
            {

                var result = reader.Decode((Bitmap)Image.FromFile(바코드주소들[i]));
                if (result != null)
                {
                    바코드값들.Add(바코드주소들[i].Substring(바코드주소들[i].LastIndexOf("\\") + 1) + ":" + result.Text + ":" + result.BarcodeFormat);
                }
            }
            return 바코드값들.ToArray();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (textBox2.Text != "" && type != 0 && !QR)
            {
                button6.Enabled = true;
                if (FinalVideo != null)
                {
                    FinalVideo.Stop();
                }

                pictureBox1.InitialImage = null;
                pictureBox1.Invalidate();
                바코드생성(textBox2.Text);
                button6.Enabled = true;

            }
            else if (QR)
            {
            
                Form2 frm = new Form2(this);
                frm.Show();

            }
            else
            {
                if (textBox2.Text == "")
                {
                    textBox1.Text = "\r\n입력값이 없음";
                }

            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            pictureBox1.Image.Save(textBox1.Text + ".jpg", ImageFormat.Jpeg);
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox2.GetItemText(comboBox2.SelectedItem))
            {
                case "UPC-A": type = BarcodeFormat.UPC_A; break;
                case "UPC-E": type = BarcodeFormat.UPC_E; break;
                case "EAN-13": type = BarcodeFormat.EAN_13; break;
                case "EAN-8": type = BarcodeFormat.EAN_8; break;
                case "ITF": type = BarcodeFormat.ITF; break;
                case "Codabar": type = BarcodeFormat.CODABAR; break;
                case "Code 39": type = BarcodeFormat.CODE_39; break;
                case "Code 93": type = BarcodeFormat.CODE_93; break;
                case "MSI": type = BarcodeFormat.MSI; break;  
                case "Code 128": type = BarcodeFormat.CODE_128; break;
                case "QR Code": type = BarcodeFormat.QR_CODE; QR = true; break;
                    
                default:
                    MessageBox.Show("Please specify the encoding type.");
                    break;
            }
        }
    }


    
}
