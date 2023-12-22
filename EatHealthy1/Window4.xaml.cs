using System;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Windows;
using static EatHealthyWPF.ResepMakanan;

namespace EatHealthyWPF
{
    public partial class SavedRecipesWindow : Window
    {
        public ObservableCollection<Recipe> SavedRecipes { get; set; }

        public SavedRecipesWindow()
        {
            InitializeComponent();
            SavedRecipes = new ObservableCollection<Recipe>();
            LoadSavedRecipes();
            DataContext = this;
        }

        private void LoadSavedRecipes()
        {
            try
            {
                using (SqlConnection sqlCon = new SqlConnection(@"Data Source=LAPTOP-D34GQHM8; Initial Catalog=LoginDB; Integrated Security=True;"))
                {
                    sqlCon.Open();
                    string query = "SELECT Recipe FROM tblUser";
                    SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                    SqlDataReader reader = sqlCmd.ExecuteReader();

                    while (reader.Read())
                    {
                        string savedRecipe = reader["Recipe"].ToString();
                        SavedRecipes.Add(new Recipe { Title = "Saved Recipe", Ingredients = new System.Collections.Generic.List<string> { savedRecipe } });
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading saved recipes: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
