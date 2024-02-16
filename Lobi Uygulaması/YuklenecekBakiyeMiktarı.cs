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
    public partial class YuklenecekBakiyeMiktarı : Form
    {
        public YuklenecekBakiyeMiktarı()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                UserFundedBalance.Instance.yuklenenbakiye = Convert.ToInt32(textBox1.Text);
                DialogResult result = MessageBox.Show(UserFundedBalance.Instance.yuklenenbakiye.ToString() + "TL Yükleniyor", "Bakiye Yükle", MessageBoxButtons.OKCancel);
                if (result == DialogResult.Cancel)
                {
                    UserFundedBalance.Instance.yuklenenbakiye = 0;
                    return;
                }
                BakiyeYukle bakiyeyukle = new BakiyeYukle();
                bakiyeyukle.Show();
                this.Hide();
            }
            catch (FormatException)
            {
                if (!string.IsNullOrEmpty(textBox1.Text))
                {
                    MessageBox.Show("Lütfen Sayısal Bir Değer Giriniz");
                    return;
                }
                else
                {
                    MessageBox.Show("Lütfen Belirli Alanı Doldurunuz.");
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
            
            UserFundedBalance.Instance.yuklenenbakiye = 50;
            DialogResult result = MessageBox.Show(UserFundedBalance.Instance.yuklenenbakiye.ToString() + "TL Yükleniyor", "Bakiye Yükle", MessageBoxButtons.OKCancel);
            if (result == DialogResult.Cancel)
            {
                UserFundedBalance.Instance.yuklenenbakiye = 0;
                return;
            }
            BakiyeYukle bakiyeyukle = new BakiyeYukle();
            bakiyeyukle.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            UserFundedBalance.Instance.yuklenenbakiye = 100;
            DialogResult result = MessageBox.Show(UserFundedBalance.Instance.yuklenenbakiye.ToString() + "TL Yükleniyor", "Bakiye Yükle", MessageBoxButtons.OKCancel);
            if (result == DialogResult.Cancel)
            {
                UserFundedBalance.Instance.yuklenenbakiye = 0;
                return;
            }
            BakiyeYukle bakiyeyukle = new BakiyeYukle();
            bakiyeyukle.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            UserFundedBalance.Instance.yuklenenbakiye = 200;
            DialogResult result = MessageBox.Show(UserFundedBalance.Instance.yuklenenbakiye.ToString() + "TL Yükleniyor", "Bakiye Yükle", MessageBoxButtons.OKCancel);
            if (result == DialogResult.Cancel)
            {
                UserFundedBalance.Instance.yuklenenbakiye = 0;
                return;
            }
            BakiyeYukle bakiyeyukle = new BakiyeYukle();
            bakiyeyukle.Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            UserFundedBalance.Instance.yuklenenbakiye = 250;
            DialogResult result = MessageBox.Show(UserFundedBalance.Instance.yuklenenbakiye.ToString() + "TL Yükleniyor", "Bakiye Yükle", MessageBoxButtons.OKCancel);
            if (result == DialogResult.Cancel)
            {
                UserFundedBalance.Instance.yuklenenbakiye = 0;
                return;
            }
            BakiyeYukle bakiyeyukle = new BakiyeYukle();
            bakiyeyukle.Show();
            this.Hide();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            UserFundedBalance.Instance.yuklenenbakiye = 500;
            DialogResult result = MessageBox.Show(UserFundedBalance.Instance.yuklenenbakiye.ToString() + "TL Yükleniyor", "Bakiye Yükle", MessageBoxButtons.OKCancel);
            if (result == DialogResult.Cancel)
            {
                UserFundedBalance.Instance.yuklenenbakiye = 0;
                return;
            }
            BakiyeYukle bakiyeyukle = new BakiyeYukle();
            bakiyeyukle.Show();
            this.Hide();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            UserFundedBalance.Instance.yuklenenbakiye = 0;
            Form1 f1 = new Form1();
            f1.Show();
            this.Hide();
        }
    }
}
