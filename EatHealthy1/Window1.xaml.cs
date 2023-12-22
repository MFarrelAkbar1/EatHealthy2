using System;
using System.Windows;

namespace EatHealthyWPF
{
    public partial class KalkulatorKesehatan : Window
    {
        public KalkulatorKesehatan()
        {
            InitializeComponent();
        }

        private void HitungButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                double beratBadan = double.Parse(BeratBadanTextBox.Text);
                double tinggiBadanCm = double.Parse(TinggiBadanTextBox.Text);
                double tinggiBadanM = tinggiBadanCm / 100;
                double hasil = HitungIndeksMassaTubuh(beratBadan, tinggiBadanM);
                string keterangan = BerikanKeteranganIMT(hasil);

                MessageBox.Show($"Indeks Massa Tubuh Anda adalah {hasil}\n{keterangan}");
            }
            catch (FormatException)
            {
                MessageBox.Show("Pastikan input berupa angka.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Terjadi kesalahan: {ex.Message}");
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private double HitungIndeksMassaTubuh(double beratBadan, double tinggiBadan)
        {
            return beratBadan / (tinggiBadan * tinggiBadan);
        }

        private string BerikanKeteranganIMT(double hasilIMT)
        {
            if (hasilIMT < 18.5)
                return "Anda mungkin mengalami kekurangan berat badan.";
            else if (hasilIMT >= 18.5 && hasilIMT < 25)
                return "Selamat! Berat badan Anda normal.";
            else if (hasilIMT >= 25 && hasilIMT < 30)
                return "Anda mungkin mengalami overweight.";
            else
                return "Anda mungkin mengalami obesitas. Segera konsultasikan dengan dokter.";
        }
    }
}
