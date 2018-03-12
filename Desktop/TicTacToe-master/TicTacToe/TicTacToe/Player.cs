using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace TicTacToe
{
    public class Player
    {
        public bool is_computer;
        public int level;
        public String name;
        private String password;
        private bool p;
        public bool loggedIn = false;
        /// <summary>
        /// Constructs a player
        /// </summary>
        /// <param name="computer">Is this player a computer player? true/false</param>
        /// <param name="level">What level is this computer player at? 1-3. Defaults to 1.</param>
        public Player(bool computer, int _level = 1) 
        {
            is_computer = computer;
            level = _level;
        }

        public Player(String pname, bool computer, int _level = 1 )
        {
            is_computer = computer;
            level = _level;
            name = pname;
        }

        public Player(String pname, String pass, bool computer, int _level = 1)
        {
            is_computer = computer;
            level = _level;
            name = pname;
            password = pass;
        }

      

        public bool Login()
        {
            //C:\Users\roseboro\Documents\GitHub\TicTacToe\TicTacToe\TicTacToe\ticTacToeabase.mdf
            String connection_String = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=D:\Downloads\TicTacToe\TicTacToe\TicTacToe\ticTacToeabase.mdf;Integrated Security=True";
            System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(connection_String);
            try
            {
                conn.Open();
                String command = "Select Count(*) from Players where name = '" + name + "' and password = '" + password + "';";
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(command, conn);

                int rows = (int)cmd.ExecuteScalar();
                if (rows == 1)
                {
                    loggedIn = true;
                    conn.Close();
                    return true;
                }
                else
                {
                    loggedIn = false;
                    conn.Close();
                    return false;
                }
                

            }
            catch(Exception e)
            {
                MessageBox.Show("Error with datastore: " + e.Message);
            }
            return false;
        }
        


        public Move get_move(Board b, int player = 1)
        {

            switch (level)
            {
                case 1: return get_move_levelOne(b);
                    
                case 2: return get_move_levelTwo(b, player);
                    
                case 3: return get_move_levelThree(b, player);
                    
                default: return get_move_levelOne(b);
            }
            
        }

        private Move get_move_levelThree(Board b, int player)
        {
            foreach(Move to_try in b.emptySpaces)
            {
                Board copy = new Board(b);
                if (check_block(copy, player, to_try))
                    return to_try;
                // check if we can score: Secondary preference
                copy = new Board(b);
                copy.applyMove(to_try, player);
                if (check_any_score(to_try, player, copy))
                {
                    return to_try;
                }

            }
            // No score and no block -- Try to find sets of 3 then sets of 2 to setup blocks/scores
            Dictionary<Move, int> move_scores = new Dictionary<Move, int>();
            
            foreach(Move to_try in b.emptySpaces)
            {
                Board copy = new Board(b);
                int amt = 0;
                int score = 0;
                if ( ( amt = check_threes(copy, player, to_try) ) > 0 )
                {
                    score += amt * 3;
                }
                if ((amt = check_twos(copy, player, to_try)) > 0)
                {
                    score += amt * 2;
                }
                if (score > 0)
                    move_scores.Add(to_try, score);
            }

            if(move_scores.Count > 0)
            {
                Move res = move_scores.Aggregate((l, r) => l.Value > r.Value ? l : r).Key;
                return res;
            }

            return get_move_levelOne(b);

        }

        private int check_twos(Board copy, int player, Move to_try)
        {
            int x = to_try.x;
            int y = to_try.y;
            int[,] b = copy.getBoard();
            return checkTwosDiagnol(x, y, player, b) +
                checkTwosHorizontal(x, y, player, b) +
                checkTwosVertical(x, y, player, b);

        }

        private int check_threes(Board copy, int player, Move to_try)
        {
            int x = to_try.x;
            int y = to_try.y;
            int[,] b = copy.getBoard();
            return checkThreesDiagnol(x, y, player, b) +
                checkThreesHorizontal(x, y, player, b) +
                checkThreesVertical(x, y, player, b);
        }

        private bool check_block(Board copy, int player, Move to_try)
        {
            if (player == 1)
            {
                copy.applyMove(to_try, 2);
                //check if we can block a score. always prefer this.
                if (check_any_score(to_try, 2, copy))
                {
                    return true;
                }
            }
            else
            {
                copy.applyMove(to_try, 1);
                //check if we can block a score. always prefer this.
                if (check_any_score(to_try, 1, copy))
                {
                    return true;
                }
            }
            return false;
        }

        private Move get_move_levelTwo(Board b, int player)
        {
            
            foreach(Move to_try in b.emptySpaces)
            {
                Board copy = new Board(b);
                if (check_block(copy, player, to_try))
                    return to_try;

                // check if we can score: Secondary preference
                copy = new Board(b);
                copy.applyMove(to_try, player);
                if(check_any_score(to_try, player, copy))
                {
                    return to_try;
                }
               
            
            }


            // if no scores or blocks return random move;
            return get_move_levelOne(b);
        }
        
        private bool check_any_score(Move to_try, int Player, Board copy)
        {
            if (checkDiagnal(to_try.x, to_try.y, Player, copy.getBoard()) > 0 ||
               checkVertical(to_try.x, to_try.y, Player, copy.getBoard()) > 0 ||
               checkHorizontal(to_try.x, to_try.y, Player, copy.getBoard()) > 0)
            {
                return true;
            }
            return false;

        }

        private Move get_move_levelOne(Board b)
        {
            Random rnd = new Random();
            int r1 = rnd.Next(0, b.getEmptySpaces().Count);

            return b.getEmptySpaces()[r1];
        }

        public bool validSquare(int x, int y)
        {
            return (x >= 0 && x <= 5) && (y >= 0 && y <= 5);
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
                    (validSquare(top_x, top_y)         && b[top_x, top_y] == p) &&
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
                    (validSquare(top_x, top_y)         && b[top_x, top_y] == p) &&
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

        private int checkTwosDiagnol(int x, int y, int p, int[,] b)
        {
            int amt = 0;
            if (validSquare(x - 1, y-1) && b[x - 1, y-1] == p)
                amt++;
            if (validSquare(x + 1, y-1) && b[x + 1, y-1] == p)
                amt++;
            if (validSquare(x + 1, y + 1) && b[x + 1, y +1] == p)
                amt++;
            if (validSquare(x - 1, y + 1) && b[x - 1, y + 1] == p)
                amt++;

            return amt;
        }

        private int checkThreesDiagnol(int x, int y, int p, int[,] b)
        {
            int amt = 0;
            if (validSquare(x - 1, y - 1) && b[x - 1, y - 1] == p &&
                validSquare(x - 2, y - 2) && b[x - 2, y - 2] == p)
            {
                amt++;
            }

            if (validSquare(x + 1, y - 1) && b[x + 1, y - 1] == p &&
                validSquare(x + 2, y - 2) && b[x + 2, y - 2] == p)
            {
                amt++;
            }

            if (validSquare(x + 1, y + 1) && b[x + 1, y + 1] == p &&
                validSquare(x + 2, y + 2) && b[x + 2, y + 2] == p)
            {
                amt++;
            }

            if (validSquare(x - 1, y + 1) && b[x - 1, y + 1] == p &&
                validSquare(x - 2, y + 2) && b[x - 2, y + 2] == p)
            {
                amt++;
            }

            if (validSquare(x - 1, y - 1) && b[x - 1, y - 1] == p &&
                validSquare(x +1, y +1) && b[x +1, y + 1] == p)
            {
                amt++;
            }

            if (validSquare(x + 1, y - 1) && b[x + 1, y - 1] == p &&
                validSquare(x - 1, y + 1) && b[x - 1, y + 1] == p)
            {
                amt++;
            }

            return amt;
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
                    (validSquare(top_x, top_y)     && b[top_x, top_y] == p) &&
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
         

        }

        private int checkThreesVertical(int x, int y, int p, int [,] b)
        {
            int amt = 0;
            if (validSquare(x - 1, y) && b[x-1, y] == p &&
                validSquare(x + 1, y) && b[x+1, y] == p)
            {
                amt++;
            }

            if (validSquare(x - 1, y) && b[x - 1, y] == p &&
               validSquare(x - 2, y) && b[x -2, y] == p)
            {
                amt++;
            }

            if (validSquare(x + 1, y) && b[x + 1, y] == p &&
               validSquare(x + 2, y) && b[x + 2, y] == p)
            {
                amt++;
            }
            return amt;
        }

        private int checkTwosVertical(int x, int y, int p, int[,] b)
        {
            int amt = 0;
            if (validSquare( x - 1, y) && b[x-1, y] == p)
                amt++;
            if (validSquare( x + 1, y) && b[x+1, y] == p)
                amt++;

            return amt;
        }

        private int checkHorizontal(int x, int y, int p, int[,] b)
        {
            int top_x = x;
            int top_y = y - 3;
            int score = 0;
            for (int i = 0; i < 4; i++)
            {
                if (
                   (validSquare(top_x, top_y)     && b[top_x, top_y] == p) &&
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
        }

        private int checkThreesHorizontal(int x, int y, int p, int[,] b)
        {
         
            int amt = 0;
            if (validSquare(x, y-1) && b[x, y-1] == p &&
                validSquare(x, y+1) && b[x, y+1] == p)
            {
                amt++;
            }

            if (validSquare(x, y-1) && b[x, y-1] == p &&
               validSquare(x, y-2) && b[x, y-2] == p)
            {
                amt++;
            }

            if (validSquare(x, y+1) && b[x, y+1] == p &&
               validSquare(x, y+2) && b[x, y+2] == p)
            {
                amt++;
            }
            return amt;
        
        }
        private int checkTwosHorizontal(int x, int y, int p, int[,] b)
        {
            int amt = 0;
            if (validSquare(x, y - 1) && b[x, y - 1] == p)
                amt++;
            if (validSquare(x, y + 1) && b[x, y + 1] == p)
                amt++;

            return amt;
        }
    }
}
