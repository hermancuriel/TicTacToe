using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    public class Game
    {
        public Board gameBoard;
        public Player player_one;
        public Player player_two;
        public Player current_player;
        public int player_one_score;
        public int player_two_score;
        public bool gameOver
        {
            get { return gameBoard.emptySpaces.Count == 0; }
        }


        public Game() 
        {
            gameBoard = new Board();
            player_one = new Player(false);
            player_two = new Player(true);
            player_one_score = player_two_score = 0;
            current_player = player_one;
        }

        public Game(Player p1, Player p2)
        {
            gameBoard = new Board();
            player_one = p1;
            player_two = p2;
            player_one_score = player_two_score = 0;
            current_player = player_one;
        }

        public void nextPlayer()
        {
            if (current_player == player_one)
            {
                current_player = player_two;
            }
            else
            {
                current_player = player_one;
            }
        }

        public int getCurrentPlayer()
        {
            if (current_player == player_one)
                return 1;
            else
                return 2;
        }



        


    }
}
