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
         

            People.ItemsSource = GetPeople();
        }
        public List<Person> GetPeople()
        {
            var client = new RestClient("http://jsonplaceholder.typicode.com/todos");
            var request = new RestRequest(Method.GET);
            var res = client.Execute<List<Person>>(request);
            request.OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; };
            var queryResult = res.Data;
            Save(queryResult);
            return queryResult;

        }
        //(1,2,3),
        public void Save(List<Person> osoba)
        {
            string test = null;
           
            foreach (var person in osoba)
            {
                test = "(" +person.userId + "," + person.id+ "," + "\"" + person.title + "\"" + "," + "\"" + person.completed + "\"" + ")";
                App.DatabasePersons.Add(test);
            }
        }
    }
}
