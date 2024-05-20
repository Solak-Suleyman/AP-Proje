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
using System.Windows.Shapes;

namespace APProjeFrontend
{
    /// <summary>
    /// Interaction logic for HomeScreen.xaml
    /// </summary>
    public partial class HomeScreen : Window
    {
        public HomeScreen()
        {
            InitializeComponent();
            ContentControl.Content = new Lobby();
        }

        private void LobbyButton_Click(object sender, RoutedEventArgs e)
        {
            ContentControl.Content = new Lobby();
        }

        private void CreateGameButton_Click(object sender, RoutedEventArgs e)
        {
            ContentControl.Content = new CreateGame();
        }

        private void LeadershipTableButton_Click(object sender, RoutedEventArgs e)
        {
            ContentControl.Content = new LeadershipTable();
        }

        private void AboutButton_Click(object sender, RoutedEventArgs e)
        {
            ContentControl.Content = new About();
        }
    }
}

