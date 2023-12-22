using EatHealthy;
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
            this.Hide();
            KalkulatorKesehatan kalkulatorWindow = new KalkulatorKesehatan();
            kalkulatorWindow.Show();
            this.Close();
        }

        private void ResepButton_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            ResepMakanan resepWindow = new ResepMakanan();
            resepWindow.ShowDialog();
            this.Close();
        }


        private void PelacakanCairanButton_Click(object sender, RoutedEventArgs e)
        {
            // Hide the current window
            this.Hide();

            // Create an instance of PelacakanCairan and show it
            PelacakanCairan pelacakanCairanWindow = new PelacakanCairan();
            pelacakanCairanWindow.ShowDialog();

            // Close the current window when PelacakanCairan is closed
            this.Close();
        }

        private void KeluarButton_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            Window4 loginPage = new Window4();
            loginPage.ShowDialog();
            this.Close();
        }
    }
}
