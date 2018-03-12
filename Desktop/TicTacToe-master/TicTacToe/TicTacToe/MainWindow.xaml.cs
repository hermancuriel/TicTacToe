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

namespace TicTacToe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        Player LoggedInOne, LoggedInTwo;
        public MainWindow()
        {
            InitializeComponent();
            mainFrame.Navigate(new StartPage());
           
        }

        ///// <summary>
        /////  Initializes button assets and takes first move if first player is AI
        ///// </summary>
        //private void newGame_Click(object sender, RoutedEventArgs e)
        //{
            
        //    gameType.Visibility = Visibility.Visible;
          
        //   // game = new Game();
        //   // clearBoard();
        //   // Menu.Visibility = Visibility.Collapsed;

        //}

        

        //private void PvP_Click(object sender, RoutedEventArgs e)
        //{
        //    playerLogin.Visibility = Visibility.Visible;
        //    PlayerTwoLogin.Visibility = Visibility.Visible;
        //}

        //private void PvC_Click(object sender, RoutedEventArgs e)
        //{
        //    playerLogin.Visibility = Visibility.Visible;
        //    AIDifficulty.Visibility = Visibility.Visible;
        //}

        //private void startGame_Click(object sender, RoutedEventArgs e)
        //{
        //    Player p2;
        //    bool error = false;
        //    string player_one = playerOneLogin.Text;
        //    string player_two = PlayerTwoLogin.Text;
        //    bool player_two_pc = PlayerTwoLogin.Visibility == Visibility.Collapsed;
        //    Player p1 = new Player(player_one, false);
        //    if (player_two_pc)
        //    {
                
        //       p2 = new Player(false);
               
        //    }
        //    else
        //    {
        //        p2 = new Player(player_two, false);
        //    }

        //    mainFrame.Navigate(new GameBoard(p1, p2));
        //}

      

    
     
       

      

       

     

    }
}
