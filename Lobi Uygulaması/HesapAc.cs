using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lobi_Uygulaması
{
    public partial class HesapAc : Form
    {
        public HesapAc()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                AccDetailsRepo.Instance.ogrenciad = textBox1.Text;
                AccDetailsRepo.Instance.ogrencisoyad = textBox2.Text;
                AccDetailsRepo.Instance.ogrencino = Convert.ToInt32(textBox3.Text);
                KartıTanımla kartislem = new KartıTanımla();
                kartislem.Show();
                this.Hide();
            }
            catch (FormatException)
            {
                if (!string.IsNullOrEmpty(textBox1.Text) && !string.IsNullOrEmpty(textBox2.Text) && !string.IsNullOrEmpty(textBox3.Text))
                {
                    MessageBox.Show("Lütfen Sayısal Bir Değer Giriniz");
                    return;
                }
                else
                {
                    MessageBox.Show("Lütfen Alanları Doldurunuz.");
                    return;
                }
            }
            catch (OverflowException)
            {
                MessageBox.Show("Lütfen '2147483648' Sayısından Küçük Bir Rakam Giriniz");
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Beklenmeyen bir hata oluştu: " + ex.Message);
                Application.Exit();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AccDetailsRepo.Instance.ogrenciad = null;
            AccDetailsRepo.Instance.ogrencisoyad = null;
            AccDetailsRepo.Instance.ogrencino = 0;
            Form1 f1 = new Form1();
            f1.Show();
            this.Hide();
        }
    }
}
