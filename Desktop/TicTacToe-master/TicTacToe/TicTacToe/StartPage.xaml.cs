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
    /// Interaction logic for StartPage.xaml
    /// </summary>
    public partial class StartPage : Page
    {
        Player LoggedInOne, LoggedInTwo;
        TicTacToe.MainWindow mw = (TicTacToe.MainWindow)Application.Current.MainWindow;
        
        public StartPage()
        {
            InitializeComponent();
           
        }
        private void ButtonLoginVsComputer_Click(object sender, RoutedEventArgs e)
        {
            Login l = new Login();
            l.ShowDialog();
            if (l.DialogResult.HasValue && l.DialogResult.Value)
            {
                //login
                LoggedInOne = new Player(l.Username.Text, l.Password.Password, false);
            }
            else
            {
                //Cancelled

            }
            if (LoggedInOne.Login())
            {
                int p = getDifficulty();
                if (p > 0 && p < 4)
                {
                    Player p2 = new Player("Computer", true, p);
                    mw.mainFrame.Navigate(new GameBoard(LoggedInOne, p2));
                }
            }
            else
            {
                MessageBox.Show("Invalid Name or password");
                LoggedInOne = null;
            }
        }

        private void ButtonLoginVsGuest_Click(object sender, RoutedEventArgs e)
        {
            Login l = new Login();
            l.ShowDialog();
            if (l.DialogResult.HasValue && l.DialogResult.Value)
            {
                //login
                LoggedInOne = new Player(l.Username.Text, l.Password.Password, false);
            }
            else
            {
                //Cancelled

            }
            if (LoggedInOne != null && LoggedInOne.Login())
            {
                int p = getDifficulty();
                if (p > 0 && p < 4)
                {
                    Player p2 = new Player("Guest", false);
                    mw.mainFrame.Navigate(new GameBoard(LoggedInOne, p2));
                }
            }
            else
            {
                if (LoggedInOne != null)
                {
                    MessageBox.Show("Invalid Name or password");
                }
                LoggedInOne = null;
            }
        }

        private void ButtonLoginVsPlayer_Click(object sender, RoutedEventArgs e)
        {
           
            while (LoggedInOne == null || !LoggedInOne.loggedIn)
            {
                Login l = new Login();
                l.ShowDialog();
                if (l.DialogResult.HasValue && l.DialogResult.Value)
                {
                    //login
                    LoggedInOne = new Player(l.Username.Text, l.Password.Password, false);
                    if(LoggedInOne.Login())
                    {
                        break;
                    }
                    else
                    {
                        MessageBox.Show("Invalid name/password combination!");
                    }

                }
                else
                {
                    LoggedInOne = null;
                    return;
                    break;
                }
            }

            while ( LoggedInTwo == null  || !LoggedInTwo.loggedIn)
            {
                Login l = new Login();
                l.ShowDialog();
                if (l.DialogResult.HasValue && l.DialogResult.Value)
                {
                    //login
                    LoggedInTwo = new Player(l.Username.Text, l.Password.Password, false);
                    if (LoggedInTwo.Login())
                    {
                        break;
                    }
                    else
                    {
                        MessageBox.Show("Invalid name/password combination!");
                    }

                }
                else
                {
                    LoggedInTwo = null;
                    return;
                    break;
                }
            }

            mw.mainFrame.Navigate(new GameBoard(LoggedInOne, LoggedInTwo));
          

        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.Close();
        }
       
        private int getDifficulty()
        {
            int diff = -1;
            AiDifficulty d = new AiDifficulty();
            d.ShowDialog();
            if (d.DialogResult.HasValue && d.DialogResult.Value)
            {
                diff = d.difficulty;
                return diff;
            }
            return diff;
        }

       

        private void ButtonGuest_Click(object sender, RoutedEventArgs e)
        {
            int difficulty = getDifficulty();
            if (difficulty > 0 && difficulty < 4)
            {
                startGuest(difficulty);
            }
            else
            {
                MessageBox.Show("Must select a difficulty!");
            }
        }
              
        private void startGuest(int p)
        {
            Player p1 = new Player("Guest", false);
            Player p2 = new Player("Computer", true, p);
            
            mw.mainFrame.Navigate(new GameBoard(p1, p2));
        }

        private void ButtonHistory_Click(object sender, RoutedEventArgs e)
        {
            mw.mainFrame.Navigate(new History());
        }

        private void ButtonRegister_Click(object sender, RoutedEventArgs e)
        {
            Login l = new Login(true);
            l.ShowDialog();
            if (l.DialogResult.HasValue && l.DialogResult.Value)
            {
                
                String connection_String = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=D:\Downloads\TicTacToe\TicTacToe\TicTacToe\ticTacToeabase.mdf;Integrated Security=True";
                System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(connection_String);
             
                try
                {
                    conn.Open();
                    System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = "Insert into players (Name, Password) values (@Name, @Password);";
                    cmd.Parameters.AddWithValue("@Name", l.Username.Text);
                    cmd.Parameters.AddWithValue("@Password", l.Password.Password);
                    int affected = cmd.ExecuteNonQuery();
                }
                catch(Exception ex)
                {
                 //Problem with datastore!!!!!
                 MessageBox.Show("Problem registering: " + ex.Message);
                }
             
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            LoggedInOne = null;
            LoggedInTwo = null;
        }
        

    }
}
