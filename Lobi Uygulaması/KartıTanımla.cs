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
    public partial class KartıTanımla : Form
    {
        public int gecensure;
        public int gecensure2;
        public OleDbConnection baglanti = new OleDbConnection("Provider = Microsoft.ACE.OLEDB.12.0; Data Source = C:\\Users\\furki\\OneDrive\\Masaüstü\\siparis.accdb");

        public KartıTanımla()
        {
            InitializeComponent();
            serialPort1 = new SerialPort("COM3", 9600);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            gecensure++;
            if (gecensure == 20)
            {
                timer1.Stop();
                label2.Text = ("20 saniyedir işlem yapılmadı. İşlem iptal ediliyor.");
                timer2.Start();
                return;
            }
            if (label2.Text == "Kart bekleniyor.....")
            {
                label2.Text = "Kart bekleniyor";
            }
            label2.Text = label2.Text + ".";
        }

        private void KartıTanımla_Shown(object sender, EventArgs e)
        {
            gecensure = 0;
            timer1.Start();
            label2.Visible = true;
            label1.Visible = false;
            label3.Visible = false;
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

        private void timer2_Tick(object sender, EventArgs e)
        {
            gecensure2++;
            if (gecensure2 == 3)
            {
                AccDetailsRepo.Instance.ogrenciad = null;
                AccDetailsRepo.Instance.ogrencisoyad = null;
                AccDetailsRepo.Instance.ogrencino = 0;
                AccDetailsRepo.Instance.kartno = null;
                timer2.Stop();
                if (serialPort1.IsOpen)
                {
                    serialPort1.Close();
                }
                Form1 f1 = new Form1();
                f1.Show();
                this.Hide();
            }

        }

        private void KartıTanımla_KeyDown(object sender, KeyEventArgs e)
        {
            /*if (e.KeyCode == Keys.CapsLock)
            {
                timer1.Stop();
                label1.Visible = true;
                label2.Visible = false;
                label3.Visible = true;
                label3.Text = "Hesap oluşturuldu";
                label1.Text = "Hoşgeldin " + AccDetailsRepo.Instance.ogrenciad + " iyi günler";
                timer2.Start();
            }*/
        }

        private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;
            this.Invoke((MethodInvoker)delegate
            {
            AccDetailsRepo.Instance.kartno = sp.ReadLine();
            try
            {
                baglanti.Open();
                OleDbCommand komut = new OleDbCommand("INSERT INTO Profiles (RFID, Isim, Soyisim, Okulnumarasi, Bakiye) VALUES (@rfid, @isim, @soyisim, @okulnumarası, @bakiye)", baglanti);
                komut.Parameters.AddWithValue("@rfid", AccDetailsRepo.Instance.kartno);
                komut.Parameters.AddWithValue("@isim", AccDetailsRepo.Instance.ogrenciad);
                komut.Parameters.AddWithValue("@soyisim", AccDetailsRepo.Instance.ogrencisoyad);
                komut.Parameters.AddWithValue("@okulnumarası", AccDetailsRepo.Instance.ogrencino);
                komut.Parameters.AddWithValue("@bakiye", 0);
                komut.ExecuteNonQuery();
                baglanti.Close();
                timer1.Stop();
                label1.Visible = true;
                label2.Visible = false;
                label3.Visible = true;
                label3.Text = "Hesap oluşturuldu";
                label1.Text = "Hoşgeldin " + AccDetailsRepo.Instance.ogrenciad + " iyi günler";
                timer2.Start();
                }
                catch (Exception ex)
                {
                    if (ex.Message == "The changes you requested to the table were not successful because they would create duplicate values in the index, primary key, or relationship. Change the data in the field or fields that contain duplicate data, remove the index, or redefine the index to permit duplicate entries and try again.")
                    {
                        label2.Text = ("Bu veri zaten veritabanında mevcut.");
                        timer1.Stop();
                        baglanti.Close();
                        timer2.Start();
                    }
                    else
                    {
                        timer1.Stop();
                        label2.Text = ("Veritabanına kaydedilirken hata oluştu: " + ex.Message);
                        baglanti.Close();
                        timer2.Start();
                    }
                }
            });
        }
    }
}