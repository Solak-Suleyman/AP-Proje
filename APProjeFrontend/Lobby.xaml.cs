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

namespace APProjeFrontend
{
    /// <summary>
    /// Interaction logic for Lobby.xaml
    /// </summary>
    public partial class Lobby : UserControl
    {
        public Lobby()
        {
            InitializeComponent();
        }
        private void GameButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow Game = new MainWindow();
            Game.Show();
        }
    }
}
