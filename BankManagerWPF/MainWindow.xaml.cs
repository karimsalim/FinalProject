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


namespace BankManagerWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            //BankContext db = new BankContext();
            //var clients = from c in db.Clients
            //                select c;
            //TheDataGrid.ItemsSource = clients;
        }

        //private void Button_Click(object sender, RoutedEventArgs e)
        //{
        //        MainWindow window = new MainWindow();
        //        window.Show();
        //    }

        private void CboBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

    }
    }
    

