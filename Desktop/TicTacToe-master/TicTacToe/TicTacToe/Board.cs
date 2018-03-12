using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    public class Board
    {
        private int[,] board = {
                                    {0,0,0,0,0,0}, // Board is initialized as 6x6 witha all 0's
                                    {0,0,0,0,0,0}, // 0 = empty
                                    {0,0,0,0,0,0}, // 1 = player 1
                                    {0,0,0,0,0,0}, // 2 = player 2
                                    {0,0,0,0,0,0},
                                    {0,0,0,0,0,0},
                                };

        public List<Move> emptySpaces;
        
        public Board(Board b)
        {
            emptySpaces = b.emptySpaces.ToList();
            board = (int[,])b.board.Clone();
            
        }

        public Board() {
            emptySpaces = new List<Move>();
            generate_moves();
        }

        private void generate_moves()
        {
            for (int i = 0; i < 6; i++)
            {
                for (int h = 0; h < 6; h++)
                {
                    Move t = new Move(i, h);
                    emptySpaces.Add(t);
                }
            }
        }
        public int[,] getBoard() { return board; }
        public List<Move> getEmptySpaces() { return emptySpaces; }
        public void applyMove(Move m, int player)
        {
            Move to_remove = emptySpaces.Single(r => r.x == m.x && r.y == m.y);
            if (player == 1)
            {
                
                board[m.x,m.y] = 1;
                emptySpaces.Remove(to_remove);
            }
            else
            {
                board[m.x,m.y] = 2;
                emptySpaces.Remove(to_remove);

            }
        }
        
    }
}
