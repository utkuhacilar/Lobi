using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.IO;
using System.IO.Ports;
using System.Windows.Forms;

namespace Lobi_Uygulaması
{
    public partial class BakiyeYukle : Form
    {
        public int gecensure;
        public int gecensure2;
        public OleDbConnection baglanti = new OleDbConnection("Provider = Microsoft.ACE.OLEDB.12.0; Data Source = C:\\Users\\furki\\OneDrive\\Masaüstü\\siparis.accdb");

        public BakiyeYukle()
        {
            InitializeComponent();
            serialPort1 = new SerialPort("COM3", 9600);
        }

        private void BakiyeYukle_Shown(object sender, EventArgs e)
        {
            gecensure = 0;
            timer1.Start();
            label2.Visible = true;
            label1.Visible = false;
            label3.Visible = false;
            label4.Visible = false;
            try
            {
                serialPort1.Open();
                serialPort1.DataReceived += new SerialDataReceivedEventHandler(serialPort1_DataReceived);
            }
            catch (Exception ex)
            {
                timer1.Stop();
                MessageBox.Show("Seri port bağlantısı başarısız: " + ex.Message);
                Application.Exit();
                return;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            gecensure++;
            if (gecensure == 20)
            {
                timer1.Stop();
                MessageBox.Show("20 saniyedir işlem yapılmadı işlem iptal ediliyor.");
                timer2.Start();
                return;
            }
            if (label2.Text == "Kart bekleniyor.....")
            {
                label2.Text = "Kart bekleniyor";
            }
            label2.Text = label2.Text + ".";
        }

        private void BakiyeYukle_KeyDown(object sender, KeyEventArgs e)
        {
            /*if (e.KeyCode == Keys.CapsLock)
            {
                timer1.Stop();
                label1.Visible = true;
                label2.Visible = false;
                label3.Visible = true;
                label4.Visible = true;
                label3.Text = UserFundedBalance.Instance.yuklenenbakiye + "TL Başarıyla Yüklendi";
                label4.Text = "Toplam " + "NoN" + "TL Kaldı";
                label1.Text = "Hoşgeldin " + "Null" + " iyi günler";
                timer2.Start();
            }*/
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            gecensure2++;
            if (gecensure2 == 3)
            {
                UserFundedBalance.Instance.yuklenenbakiye = 0;
                timer2.Stop();
                serialPort1.Close();
                if (serialPort1.IsOpen)
                {
                    serialPort1.Close();
                }
                Form1 f1 = new Form1();
                f1.Show();
                this.Hide();
            }
        }

        private void serialPort1_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;
            this.Invoke((MethodInvoker)delegate
            {
                string rfid = sp.ReadLine();
                OleDbCommand komut = new OleDbCommand("SELECT * FROM Profiles WHERE RFID = @RFID", baglanti);
                komut.Parameters.AddWithValue("@RFID", rfid);
                try
                {
                    baglanti.Open();
                    OleDbDataReader okuyucu = komut.ExecuteReader();
                    if (okuyucu.Read())
                    {
                        int suankibakiye = Convert.ToInt32(okuyucu["Bakiye"]);
                        int yuklenenbakiye = UserFundedBalance.Instance.yuklenenbakiye;
                        int guncelbakiye = suankibakiye + yuklenenbakiye;
                        komut = new OleDbCommand("UPDATE Profiles SET Bakiye = @GuncelBakiye WHERE RFID = @RFID", baglanti);
                        komut.Parameters.AddWithValue("@GuncelBakiye", guncelbakiye);
                        komut.Parameters.AddWithValue("@RFID", rfid);
                        komut.ExecuteNonQuery();
                        timer1.Stop();
                        label1.Visible = true;
                        label2.Visible = false;
                        label3.Visible = true;
                        label4.Visible = true;
                        label3.Text = yuklenenbakiye + "TL Başarıyla Yüklendi";
                        label4.Text = "Toplam " + guncelbakiye + "TL Kaldı";
                        label1.Text = "Hoşgeldin " + okuyucu["Isim"].ToString() + " " + okuyucu["Soyisim"].ToString() + " iyi günler";
                        baglanti.Close();
                        timer2.Start();
                    }
                    else
                    {
                        timer1.Stop();
                        label2.Text = ("RFID veritabanında bulunamadı!");
                        baglanti.Close();
                        timer2.Start();
                    }
                }
                catch (Exception ex)
                {
                    timer1.Stop();
                    label2.Text = ("Veritabanına kaydedilirken hata oluştu. Hata kodu: " + ex.Message);
                    baglanti.Close();
                    timer2.Start();
                }
            });
        }

            private async void BakiyeYukle_FormClosing(object sender, FormClosingEventArgs e)
            {
                if (serialPort1.IsOpen)
                {
                    try
                    {
                        // Seri portu asenkron olarak kapat
                        await Task.Run(() => serialPort1.Close());
                    }
                    catch (Exception ex)
                    {
                        // Seri portu kapatırken hata oluşursa logla veya bildirim göster
                        MessageBox.Show("Seri portu kapatırken hata oluştu: " + ex.Message);
                    }
                }
            }
        }
    }
