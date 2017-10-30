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
using System.Web;
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

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Midget dwarf = new Midget();
            dwarf.name = "Gimly";
            dwarf.beardLength = 30;
            dwarf.height = 45;
            dwarf.weight = 69;
            var jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(dwarf);
            var client = new RestClient("https://student.sps-prosek.cz/~bastlma14/Api/");
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-type", "application/json");
            request.AddParameter("application/json; charset=utf-8", jsonString, ParameterType.RequestBody);
            var res = client.Execute(request);
            var queryResult = res.Content;
            Error.Content = queryResult;
            Error1.Content = jsonString;

        }
    }
}
