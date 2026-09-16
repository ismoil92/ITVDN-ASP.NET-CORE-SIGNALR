using Microsoft.AspNetCore.SignalR.Client;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfClientApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private HubConnection connection;
        public MainWindow()
        {
            InitializeComponent();

            //connection = new HubConnectionBuilder()
            //    .WithUrl("https://localhost:7204/chat")
            //    .Build();

            //connection.On<string, string, string>("Receive", (message, username) =>
            //{

            //});
        }
    }
}