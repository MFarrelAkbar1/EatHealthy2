using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EatHealthyWPF
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void KalkulatorButton_Click(object sender, RoutedEventArgs e)
        {

            KalkulatorKesehatan kalkulatorWindow = new KalkulatorKesehatan();
            kalkulatorWindow.ShowDialog();
        }

        private void ResepButton_Click(object sender, RoutedEventArgs e)
        {
            ResepMakanan resepWindow = new ResepMakanan();
            resepWindow.ShowDialog();
        }

        private void PelacakanCairanButton_Click(object sender, RoutedEventArgs e)
        {
            PelacakanCairan pelacakanCairanWindow = new PelacakanCairan();
            pelacakanCairanWindow.Show();
        }

        private void KeluarButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
