using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
using Newtonsoft.Json;

namespace Problem2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            txtBreed.Text = string.Empty;

        }

        private void btnGo_Click(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrWhiteSpace(txtBreed.Text)== true)
            {
                MessageBox.Show("You must enter a valid breed", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            //The trim is just in case the user decides to put a space at the end for the if statement
            string breed = txtBreed.Text.Trim();

            if(breed.Contains(' ') == true)
            {
                var dogInfo = breed.Split(' ');
                string subBreed = dogInfo[0];
                string mainBreed = dogInfo[1];
                breed = $"{mainBreed}/{subBreed}";
            }

            string url = "https://dog.ceo/api/breed/{breed}/images/random";

            BreedAPI dog = null;

            using(var client = new HttpClient())
            {
                var response = client.GetAsync(url).Result;

                if (response.IsSuccessStatusCode)
                {
                    var json = response.Content.ReadAsStringAsync().Result;

                    dog = JsonConvert.DeserializeObject<BreedAPI>(json);
                }
                else
                {
                    MessageBox.Show("Invalid response");
                    txtBreed.Clear();
                }
            }

            if(dog != null)
            {
                imgDog.Source = new BitmapImage(new Uri(dog.message));
            }
        }
    }
}
