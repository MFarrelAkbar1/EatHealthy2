using System.Windows;
using System.Windows.Controls;

namespace EatHealthyWPF
{
    public partial class PelacakanCairan : Window
    {
        public PelacakanCairan()
        {
            InitializeComponent();
        }

        private void CatatAsupanCairan_Click(object sender, RoutedEventArgs e)
        {
            string jumlahAirMinum = TxtJumlahAirMinum.Text;

            ComboBoxItem selectedJenisMinuman = (ComboBoxItem)CmbJenisMinuman.SelectedItem;
            string jenisMinuman = selectedJenisMinuman.Content.ToString();

            MessageBox.Show($"Catatan Asupan Cairan:\nJumlah: {jumlahAirMinum} ml\nJenis: {jenisMinuman}");
        }
    }
}
