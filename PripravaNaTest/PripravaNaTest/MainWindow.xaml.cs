using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using Microsoft.Win32;
using RestSharp;

namespace PripravaNaTest
{
    /// <summary>
    /// Interakční logika pro MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
       
        public MainWindow()
        {
            InitializeComponent();
        }
        public List<Comment> GetComments()
        {
            var client = new RestClient("http://jsonplaceholder.typicode.com/comments");
            var request = new RestRequest(Method.GET);
            request.AddHeader("location", "http://www.cpress.cz/");
            request.AddParameter("postId", IdGet.Text);
            var res = client.Execute<List<Comment>>(request);
            request.OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; };
            var queryResult = res.Data;
            if (res.ResponseStatus == ResponseStatus.Error)
            {
                throw new System.ArgumentException("Chyba na serveru, zkontroluj URL");
                //Error.Content= "Chyba na serveru, zkontroluj URL");
            }
                return queryResult;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int n;
            bool isNumeric = int.TryParse(IdGet.Text, out n);

            if (isNumeric)
            {
                var comments = GetComments();
                bool foundt = true;
                if (comments.Count == 0)
                {
                    Comments.ItemsSource = "";
                    Error.Content="Nebyl nalezen žádný komentář s tímto userId: " + IdGet.Text;
                }
                else
                {
                    Comments.ItemsSource = comments;
                    Error.Content = "Uspešně nalezeno, počet komentářů s userId: "+ IdGet.Text+" je: " + comments.Count;
                }
            }
            else
            {
                Comments.ItemsSource = "";
                Error.Content = "Musíš zadat pouze čislice";
                IdGet.Text = "";
            }   
        }
    }
}
