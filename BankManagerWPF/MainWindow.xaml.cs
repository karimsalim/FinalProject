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
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using BankManagerWPF.Classes;


namespace BankManagerWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        HttpClient client = new HttpClient();
        HttpResponseMessage managerResponse;
        HttpResponseMessage clientResponse;
        HttpResponseMessage response;
        List<Manager> Managers;
        
        public MainWindow()
        {
            InitializeComponent();

            client.BaseAddress = new Uri("http://localhost:61759");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
        /// <summary>
        /// Act when the Main Window is loaded.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            managerResponse = await client.GetAsync("/api/Manager/GetManager");
            
            if (managerResponse.IsSuccessStatusCode)
            {
                var content = await managerResponse.Content.ReadAsStringAsync();
                Managers = JsonConvert.DeserializeObject<List<Manager>>(content);
                foreach (Manager manager in Managers)
                {
                    manager.FullName = manager.FirstName + " " + manager.LastName;
                }
                ManagerBox.ItemsSource = Managers;
            }
        }

        private async void ManagerBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selected = ManagerBox.SelectedValue.ToString();
            clientResponse = await client.GetAsync("/api/clients/GetClients/" + selected);
            if (clientResponse.IsSuccessStatusCode)
            {
                var content = await clientResponse.Content.ReadAsStringAsync();
                List<Client> Clients = JsonConvert.DeserializeObject<List<Client>>(content);
                ClientGrid.ItemsSource = Clients;
            }
        }
    }
    }
    

