using System;
using System.Collections.Generic;
using System.Windows;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using OpenAI_API;
using System.Linq;
using System.Data.SqlClient;
using System.Data;

namespace EatHealthyWPF
{
    public partial class ResepMakanan : Window
    {
        public ObservableCollection<Recipe?> Recipes { get; set; }

        public ResepMakanan()
        {
            InitializeComponent();
            Recipes = new ObservableCollection<Recipe?>();
            DataContext = this; // Set the DataContext to this instance to enable data binding
        }
        public class Data
        {
            public string ImageUrl { get; set; } = string.Empty;
        }
        public class Recipe
        {
            public string Title { get; set; } = string.Empty;
            public List<string> Ingredients { get; set; } = new List<string>();
            public List<Data> Images { get; set; } = new List<Data>();
        }
        private void btnSaveRecipe_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Assuming you have a variable to hold the current recipe
                Recipe currentRecipe = Recipes.FirstOrDefault();

                if (currentRecipe != null)
                {
                    using (SqlConnection sqlCon = new SqlConnection(@"Data Source=LAPTOP-D34GQHM8; Initial Catalog=LoginDB; Integrated Security=True;"))
                    {
                        sqlCon.Open();
                        string query = "INSERT INTO tblUser (Recipe) VALUES (@Recipe)";
                        SqlCommand sqlCmd = new SqlCommand(query, sqlCon);

                        // Assuming Ingredients is a single string, you might need to adjust this part
                        sqlCmd.Parameters.AddWithValue("@Recipe", string.Join(", ", currentRecipe.Ingredients));

                        sqlCmd.ExecuteNonQuery();
                    }
                    MessageBox.Show("Recipe saved successfully!");
                }
                else
                {
                    MessageBox.Show("No recipe available to save.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        public static async Task<Tuple<string, List<string>>> GenerateRecipeFromChatGPT(string userQuery)
        {
            try
            {
                var openAiApiKey = "Masukkan API key di sini";
                var openAiApi = new OpenAIAPI(openAiApiKey);

                var prompt = $"Buat satu resep sehat ntuk {userQuery} dengan bahan lalu Langkah-langkah dalam bahasa Indonesia";
                var response = await openAiApi.Completions.CreateCompletionAsync(
                    model: "gpt-3.5-turbo-instruct",
                    prompt: prompt,
                    top_p: 0.1,
                    max_tokens: 1000
                );
                // Retrieve image URL using the response from image generation API
                var imageResponse = await openAiApi.ImageGenerations.CreateImageAsync(
                    model: "dall-e-3",
                    input: userQuery
                );

                var imageUrls = imageResponse.Data?.Select(data => data.Url).ToList() ?? new List<string>();

                return Tuple.Create(response.ToString(), imageUrls);
            }
            catch (Exception ex)
            {
                // Handle the exception (log, display an error message, etc.)
                Console.WriteLine($"Error: {ex.Message}");
                return Tuple.Create(string.Empty, new List<string>());
            }
        }

        private void ShowLoading(bool show)
        {
            loadingProgressBar.Visibility = show ? Visibility.Visible : Visibility.Collapsed;
        }

        private async void GenerateRecipeButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var userQuery = userQueryTextBox.Text;

                if (!string.IsNullOrEmpty(userQuery))
                {
                    ShowLoading(true); // Show loading bar before starting the generation process

                    var (generatedRecipe, images) = await GenerateRecipeFromChatGPT(userQuery);

                    // Update UI with the generated recipe and images on the UI thread
                    Dispatcher.Invoke(() =>
                    {
                        UpdateRecipes(userQuery, generatedRecipe, images);
                        ShowLoading(false); // Hide loading bar after the generation process is complete
                    });
                }
            }
            catch (Exception ex)
            {
                ShowLoading(false); // Make sure to hide loading bar in case of an error
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void UpdateRecipes(string userQuery, string generatedRecipe, List<string> imageUrls)
        {
            Recipes.Clear();
            // Create Data instances for each image URL
            var images = imageUrls.Select(url => new Data { ImageUrl = url }).ToList();

            // Add the generated recipe and images to the Recipes collection
            var recipe = new Recipe()
            {
                Title = userQuery,
                Ingredients = new List<string> { generatedRecipe },
                Images = images
            };
            Recipes.Add(recipe);
        }
        private void ViewSavedRecipesButton_Click(object sender, RoutedEventArgs e)
        {
            SavedRecipesWindow savedRecipesWindow = new SavedRecipesWindow();
            savedRecipesWindow.Show();
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