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
            LoadGeneralStats();
        }

        /// <summary>
        /// Act when the ManagerBox combo box change it's selected element.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
            LoadManagedStats(selected);
        }

        /// <summary>
        /// load the general statistics from the API
        /// </summary>
        private async void LoadGeneralStats()
        {
            HttpResponseMessage responseClient = await client.GetAsync("/api/Clients/GetClientAmount");
            if (responseClient.IsSuccessStatusCode)
            {
                string clientAmount = await responseClient.Content.ReadAsStringAsync();
                ClientTotalText.Text = clientAmount;
            }
            HttpResponseMessage responseBalance = await client.GetAsync("/api/Accounts/GetTotalBalance");
            if (responseBalance.IsSuccessStatusCode)
            {
                string totalBalance = await responseBalance.Content.ReadAsStringAsync() + "€";
                BalanceTotalText.Text = totalBalance;
            }
            HttpResponseMessage responseSavingSum = await client.GetAsync("/api/Accounts/GetSavingSum");
            if (responseSavingSum.IsSuccessStatusCode)
            {
                string totalSavings = await responseSavingSum.Content.ReadAsStringAsync() + "€";
                SavingsTotalText.Text = totalSavings;
            }
            HttpResponseMessage responseCardP = await client.GetAsync("/api/Clients/GetCardsPercentages");
            if (responseCardP.IsSuccessStatusCode)
            {
                string cardPercentage = await responseCardP.Content.ReadAsStringAsync() + "%";
                CardPercentageText.Text = cardPercentage;
            }
            HttpResponseMessage responseSavingP = await client.GetAsync("/api/Clients/GetSavingsPercentages");
            if (responseSavingP.IsSuccessStatusCode)
            {
                string savingPercentage = await responseSavingP.Content.ReadAsStringAsync() + "%";
                SavingPercentageText.Text = savingPercentage;
            }
        }

        /// <summary>
        /// loads the statistics of the selected Manager from the API
        /// </summary>
        /// <param name="selected">The Selected manager's ID/param>
        private async void LoadManagedStats(string selected)
        {
            HttpResponseMessage responseClient = await client.GetAsync("/api/Clients/GetClientAmount/"+selected);
            if (responseClient.IsSuccessStatusCode)
            {
                string clientAmount = await responseClient.Content.ReadAsStringAsync();
                ClientTotalMText.Text = clientAmount;
            }
            HttpResponseMessage responseSavingSum = await client.GetAsync("/api/Accounts/GetSavingSum/"+selected);
            if (responseSavingSum.IsSuccessStatusCode)
            {
                string totalSavings = await responseSavingSum.Content.ReadAsStringAsync() + "€";
                SavingTotalMText.Text = totalSavings;
            }
            HttpResponseMessage responseCardP = await client.GetAsync("/api/Clients/GetCardsPercentages/"+selected);
            if (responseCardP.IsSuccessStatusCode)
            {
                string cardPercentage = await responseCardP.Content.ReadAsStringAsync() + "%";
                CardPercentageMText.Text = cardPercentage;
            }
            HttpResponseMessage responseSavingP = await client.GetAsync("/api/Clients/GetSavingsPercentages/"+selected);
            if (responseSavingP.IsSuccessStatusCode)
            {
                string savingPercentage = await responseSavingP.Content.ReadAsStringAsync() + "%";
                SavingPercentageMText.Text = savingPercentage;
            }
        }
    }
    }
    

