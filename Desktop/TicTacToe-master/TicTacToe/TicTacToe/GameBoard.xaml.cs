using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
    /// Interaction logic for GameBoard.xaml
    /// </summary>
    public partial class GameBoard : Page
    {
        bool player1 = true;
        bool first = false;
        TicTacToe.MainWindow mw = (TicTacToe.MainWindow)Application.Current.MainWindow;
        Board b;
        Game game;
        List<List<Button>> board_buttons;
        public GameBoard(Player p1, Player p2)
        {
            InitializeComponent();
            b = new Board();
            game = new Game(p1, p2);
            board_buttons = new List<List<Button>>();
            name_player_one.Content = p1.name;
            name_player_two.Content = p2.name;
        }
        public void b_Click(object sender, RoutedEventArgs e)
        {
            if (!first)
                return;
            int x = (int)(sender as Button).GetValue(Grid.RowProperty);
            int y = (int)(sender as Button).GetValue(Grid.ColumnProperty);
            x = x - 2; // remove 2 upper rows from calculation.
            // If the space is already occupied, do nothing.
            if (game.gameBoard.getBoard()[x, y] != 0)
            {
                return;
            }
            // Get current player and apply move for them at the button pressed here.
            int player = game.getCurrentPlayer();
            Move move = new Move(x, y);
            do_move(move, player);
        }
        private void clearBoard()
        {
           
            for (int x = 0; x < 6; x++)
            {
                for (int y = 0; y < 6; y++)
                {
                    board_buttons[x][y].Content = "";
                }
            }
        }
        public bool validSquare(int x, int y)
        {
            return (x >= 0 && x <= 5) && (y >= 0 && y <= 5);
        }
        private void update_butons(Move m, int player)
        {
            board_buttons[m.x][m.y].Foreground = (player == 1) ? Brushes.Red : Brushes.Blue;
            board_buttons[m.x][m.y].Content = (player == 1) ? "X" : "O";
        }
        private void addScorePlayerTwo(int s)
        {
            int score;
            score_player_two.Content = (Int32.TryParse(score_player_two.Content.ToString(), out score)) ? score + s : s;
        }

        private void addScorePlayerOne(int s)
        {
            int score;
            score_player_one.Content = (Int32.TryParse(score_player_one.Content.ToString(), out score)) ? score + s : s;
        }
        private void update_score(Move p2_move, int p)
        {
            int x = p2_move.x;
            int y = p2_move.y;
            int[,] b = game.gameBoard.getBoard();

            int score = 0;
            score += checkHorizontal(x, y, p, b);
            score += checkVertical(x, y, p, b);
            score += checkDiagnal(x, y, p, b);
            if (p == 1)
            {
                if(player1)
                    addScorePlayerOne(score);
                else
                    addScorePlayerTwo(score);
            }
            else
            {
                if (player1)
                    addScorePlayerTwo(score);
                else
                    addScorePlayerOne(score);
                
            }


        }
        private int checkDiagnal(int x, int y, int p, int[,] b)
        {
            //Diagonal check.
            int top_x = x - 3;
            int top_y = y - 3;
            int score = 0;
            for (int i = 0; i < 4; i++)
            {
                if (
                    (validSquare(top_x, top_y) && b[top_x, top_y] == p) &&
                    (validSquare(top_x + 1, top_y + 1) && b[top_x + 1, top_y + 1] == p) &&
                    (validSquare(top_x + 2, top_y + 2) && b[top_x + 2, top_y + 2] == p) &&
                    (validSquare(top_x + 3, top_y + 3) && b[top_x + 3, top_y + 3] == p)
                    )
                {
                    score += 1;
                }
                top_x++;
                top_y++;
            }
            //Anti-diagonal check
            top_x = x - 3;
            top_y = y + 3;
            for (int i = 0; i < 4; i++)
            {
                if (
                    (validSquare(top_x, top_y) && b[top_x, top_y] == p) &&
                    (validSquare(top_x + 1, top_y - 1) && b[top_x + 1, top_y - 1] == p) &&
                    (validSquare(top_x + 2, top_y - 2) && b[top_x + 2, top_y - 2] == p) &&
                    (validSquare(top_x + 3, top_y - 3) && b[top_x + 3, top_y - 3] == p)
                    )
                {
                    score += 1;
                }
                top_x++;
                top_y--;
            }

            return score;

        }

        private int checkVertical(int x, int y, int p, int[,] b)
        {
            //check vertical
            // **a* *a** a*** ***a
            int top_x = x - 3;
            int top_y = y;
            int score = 0;
            for (int i = 0; i < 4; i++)
            {
                if (
                    (validSquare(top_x, top_y) && b[top_x, top_y] == p) &&
                    (validSquare(top_x + 1, top_y) && b[top_x + 1, top_y] == p) &&
                    (validSquare(top_x + 2, top_y) && b[top_x + 2, top_y] == p) &&
                    (validSquare(top_x + 3, top_y) && b[top_x + 3, top_y] == p)
                    )
                {
                    score += 1;
                }
                top_x++;
            }

            return score;
            #region
            /*
            int score = 0;
            if (
               (validSquare(x, y - 2) && b[x, y - 2] == p) &&
               (validSquare(x, y - 1) && b[x, y - 1] == p) &&
               (validSquare(x, y) && b[x, y] == p) &&
               (validSquare(x, y + 1) && b[x, y + 1] == p)
               )
            {
                score += 1;
            }
            if (
               (validSquare(x, y - 1) && b[x, y - 1] == p) &&
               (validSquare(x, y) && b[x, y] == p) &&
               (validSquare(x, y + 1) && b[x, y + 1] == p) &&
               (validSquare(x, y + 2) && b[x, y + 2] == p)
               )
            {
                score += 1;
            }
            if (
               (validSquare(x, y) && b[x, y] == p) &&
               (validSquare(x, y + 1) && b[x, y + 1] == p) &&
               (validSquare(x, y + 2) && b[x, y + 2] == p) &&
               (validSquare(x, y + 3) && b[x, y + 3] == p)
               )
            {
                score += 1;
            }
            if (
               (validSquare(x, y - 3) && b[x, y - 3] == p) &&
               (validSquare(x, y - 2) && b[x, y - 2] == p) &&
               (validSquare(x, y - 1) && b[x, y - 1] == p) &&
               (validSquare(x, y) && b[x, y] == p)
               )
            {
                score += 1;
            }

            return score;*/
            #endregion

        }

        private int checkHorizontal(int x, int y, int p, int[,] b)
        {
            int top_x = x;
            int top_y = y - 3;
            int score = 0;
            for (int i = 0; i < 4; i++)
            {
                if (
                   (validSquare(top_x, top_y) && b[top_x, top_y] == p) &&
                   (validSquare(top_x, top_y + 1) && b[top_x, top_y + 1] == p) &&
                   (validSquare(top_x, top_y + 2) && b[top_x, top_y + 2] == p) &&
                   (validSquare(top_x, top_y + 3) && b[top_x, top_y + 3] == p)
                   )
                {
                    score += 1;
                }
                top_y++;
            }

            return score;
            #region old
            /*
            //check horizontal
            // **a* *a** a*** ***a
            int score = 0;
            if ((validSquare(x - 2, y) && b[x - 2, y] == p) &&
               (validSquare(x - 1, y) && b[x - 1, y] == p) &&
               (validSquare(x, y) && b[x, y] == p) &&
               (validSquare(x + 1, y) && b[x + 1, y] == p))
            {
                score+=1;
            }
            if ((validSquare(x - 1, y)&& b[x - 1, y] == p) &&
               (validSquare(x , y)    && b[x , y] == p) &&
               (validSquare(x + 1, y) && b[x + 1, y] == p) &&
               (validSquare(x + 2, y) && b[x +2, y] == p))
            {
                score+=1;
            }
            if ((validSquare(x , y)   && b[x, y] == p) &&
               (validSquare(x + 1, y) && b[x + 1, y] == p) &&
               (validSquare(x + 2, y) && b[x + 2, y] == p) &&
               (validSquare(x + 3, y) && b[x + 3, y] == p))
            {
                score += 1;
            }
            if ((validSquare(x - 3, y) && b[x - 3, y] == p) &&
               (validSquare(x-2, y) && b[x-2, y] == p) &&
               (validSquare(x-1, y) && b[x-1 , y] == p) &&
               (validSquare(x , y) && b[x , y] == p))
            {
                score += 1;
            }

            return score;
             * */
            #endregion
        }
        // This abstraction allows AI to play against itself only requiring
        // the initial move to be kicked off.
        public void do_move(Move m, int player)
        {
            update_butons(m, player);
            game.gameBoard.applyMove(m, player);
            update_score(m, player);
           
            game.nextPlayer();
            while (game.current_player.is_computer && !game.gameOver)
            {
                player = game.getCurrentPlayer();
                int p = game.current_player == game.player_one ? 1 : 2;
                Move move = game.current_player.get_move(game.gameBoard, p);
                update_butons(move, player);
                game.gameBoard.applyMove(move, player);
                update_score(move, player);
                game.nextPlayer();
            }

            if (game.gameOver)
            {
                gameOver();
                return;
            }
        }

        private void gameOver()
        {
            int p1_score = Int32.Parse(score_player_one.Content.ToString());
            int p2_score = Int32.Parse(score_player_two.Content.ToString());


            if(p1_score > p2_score)
            {
                MessageBox.Show(name_player_one.Content + " wins!");
            }
            else if (p2_score > p1_score)
            {
                MessageBox.Show(name_player_two.Content + " wins!");
            }
            else
            {
                MessageBox.Show("It's a draw!");
            }
            saveGame(p1_score, p2_score);
            //Replay.Visibility = Visibility.Visible;
        }

        private void saveGame(int one, int two)
        {
            //C:\Users\roseboro\Documents\GitHub\TicTacToe\TicTacToe\TicTacToe\ticTacToeabase.mdf

            String connection_String = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=D:\Downloads\TicTacToe\TicTacToe\TicTacToe\ticTacToeabase.mdf;Integrated Security=True";
             System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(connection_String);
            // get id's if possible.
            int p1_id, p2_id;

            if(!player1)
            {
                Player p = game.player_one;
                game.player_one = game.player_two;
                game.player_two = p;
            }

            if(game.player_two.loggedIn || game.player_one.loggedIn)
            {
               
                try
                {
                    conn.Open();
                    if(!game.player_one.is_computer && game.player_one.name.ToLower() != "guest"){
                        String getId = "Select top 1 Id from players where name = '" + game.player_one.name+"';";
                        SqlCommand Id = new SqlCommand(getId, conn);
                        p1_id = (int)Id.ExecuteScalar();
                    }else if(game.player_one.is_computer)
                    {
                        p1_id = 1;
                    }
                    else
                    {
                        p1_id = 2;
                    }

                    if(!game.player_two.is_computer && game.player_two.name.ToLower() != "guest"){
                        String getId = "Select top 1 Id from players where name = '" + game.player_two.name+"';";
                        SqlCommand Id = new SqlCommand(getId, conn);
                        p2_id = (int)Id.ExecuteScalar();
                    }
                    else if(game.player_two.is_computer)
                    {
                        p2_id = 1;
                    }
                    else
                    {
                        p2_id = 2;
                    }


                    String command = "Insert into games values (" + p1_id + ", " + one + ", " + two + ", " + p2_id + ");";
                    System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = "Insert into games (PlayerId, Score, OppScore, OpponentId, Time) values (@PlayerId, @Score, @OppScore, @OpponentId, @Time);";
                    cmd.Parameters.AddWithValue("@PlayerId", p1_id);
                    cmd.Parameters.AddWithValue("@Score", one);
                    cmd.Parameters.AddWithValue("@OppScore", two);
                    cmd.Parameters.AddWithValue("@OpponentId", p2_id);
                    cmd.Parameters.AddWithValue("@Time", DateTime.Now);


                    int affected = cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@PlayerId", p2_id);
                    cmd.Parameters.AddWithValue("@Score", two);
                    cmd.Parameters.AddWithValue("@OppScore", one);
                    cmd.Parameters.AddWithValue("@OpponentId", p1_id);
                    cmd.Parameters.AddWithValue("@Time", DateTime.Now);
                    affected = cmd.ExecuteNonQuery();
                   
                }
                catch (Exception e)
                {
                    MessageBox.Show("Error with datastore: " + e.Message);
                }
                finally
                {
                    conn.Close();
                }
              

            }
        }

        private void frame_loaded(object sender, RoutedEventArgs e)
        {
            first = false;
            game.current_player = game.player_one;
            for (int x = 2; x <= 7; x++)
            {
                List<Button> t = new List<Button>();
                for (int y = 0; y < 6; y++)
                {
                    Button b = new Button();
                    b.Click += b_Click;
                    b.SetValue(Grid.RowProperty, x);
                    b.SetValue(Grid.ColumnProperty, y);
                    b.BorderBrush = Brushes.Green;
                    b.BorderThickness = new Thickness(1);
                    b.Background = Brushes.Transparent;
                    b.Focusable = false;
                    b.FontSize = 50;
                    Board.Children.Add(b);
                    t.Add(b);
                }
                board_buttons.Add(t);
            }

           
        }

        private void newGame_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService ns = NavigationService.GetNavigationService(this);
            mw.mainFrame.GoBack();
        }

        private void Player1First_Click(object sender, RoutedEventArgs e)
        {
            first = true;
            player1 = true;
            Player1First.Visibility = Visibility.Hidden;
            Player2First.Visibility = Visibility.Hidden;
            score_player_one.Foreground = Brushes.Red;
            score_player_two.Foreground = Brushes.Blue;
        }

        private void Player2First_Click(object sender, RoutedEventArgs e)
        {
            Player p1 = game.player_one;
            Player p2 = game.player_two;
            game = new Game(p2, p1);
            first = true;
            player1 = false;
            Player1First.Visibility = Visibility.Hidden;
            Player2First.Visibility = Visibility.Hidden;
            score_player_two.Foreground = Brushes.Red;
            score_player_one.Foreground = Brushes.Blue;
            if(p2.is_computer)
            {
                Move m = p2.get_move(game.gameBoard, 1);
                do_move(m, 1);
            }
        }

        private void Replay_Click(object sender, RoutedEventArgs e)
        {
            player1= true;
            first = false;
            clearBoard();
            score_player_one.Content = "0";
            score_player_two.Content = "0";
            
            Player1First.Visibility = Visibility.Visible;
            Player2First.Visibility = Visibility.Visible;
        }
    }
}
