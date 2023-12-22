using OpenAI_API;
using System;
using System.Threading.Tasks;
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

        private async void CatatAsupanCairan_Click(object sender, RoutedEventArgs e)
        {
            string jumlahAirMinum = TxtJumlahAirMinum.Text;

            ComboBoxItem selectedJenisMinuman = (ComboBoxItem)CmbJenisMinuman.SelectedItem;
            string jenisMinuman = selectedJenisMinuman.Content.ToString();

            try
            {
                var openAiApiKey = "Isi API Key di sini";
                var openAiApi = new OpenAIAPI(openAiApiKey);

                var prompt = $"Saran mentor kesehatan untuk Asupan Cairan sehari {jumlahAirMinum} dengan jenis {jenisMinuman}";
                var response = await openAiApi.Completions.CreateCompletionAsync(
                    model: "gpt-3.5-turbo-instruct",
                    prompt: prompt,
                    top_p: 0.1,
                    max_tokens: 1000
                );

                MessageBox.Show($"Saran: {response}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            // Create an instance of MainWindow and show it
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();

            // Close the current window
            this.Close();
        }

    }
}
