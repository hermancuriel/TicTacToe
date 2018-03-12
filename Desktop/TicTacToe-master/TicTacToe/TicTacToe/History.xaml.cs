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
    /// Interaction logic for History.xaml
    /// </summary>
    public partial class History : Page
    {

        TicTacToe.MainWindow mw = (TicTacToe.MainWindow)Application.Current.MainWindow;
        public History()
        {
            InitializeComponent();
        }

        private void ButtonHistory_Click(object sender, RoutedEventArgs e)
        {
            //C:\Users\roseboro\Documents\GitHub\TicTacToe\TicTacToe\TicTacToe\ticTacToeabase.mdf
            String connection_String = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=D:\Downloads\TicTacToe\TicTacToe\TicTacToe\ticTacToeabase.mdf;Integrated Security=True";
             System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(connection_String);
             gameshistory.Rows.Clear();
               
                try
                {
                     conn.Open();
                     String getId = "Select top 1 Id from players where name = '" + Playername.Text+"';";
                     SqlCommand Id = new SqlCommand(getId, conn);
                     int p1_id = (int)Id.ExecuteScalar();
                     String getGames = "Select * from games where PlayerId = "+p1_id + " order by Time desc;";
                     SqlCommand getHistory = new SqlCommand(getGames, conn);
                     SqlDataReader reader = getHistory.ExecuteReader();
                     if(reader.HasRows)
                     {
                         var IndexPlayerId = reader.GetOrdinal("PlayerId");
                         var IndexScore = reader.GetOrdinal("Score");
                         var IndexoppScore = reader.GetOrdinal("OppScore");
                         var IndexOppId = reader.GetOrdinal("OpponentId");
                         var IndexTime = reader.GetOrdinal("Time");

                         while(reader.Read())
                         {
                             StackPanel s = new StackPanel();
                             s.Orientation = Orientation.Horizontal;
                             var PlayerId = reader.GetValue(IndexPlayerId);
                             var Score = reader.GetValue(IndexScore);
                             var OppScore = reader.GetValue(IndexoppScore);
                             var OpponentId = reader.GetValue(IndexOppId);
                             var Time = reader.GetValue(IndexTime);

                             String PlayerOne = getNameFromId(PlayerId, conn);
                             String PlayerTwo = getNameFromId(OpponentId, conn);

                             Table t = new Table();
                             TableRow r = new TableRow();
                             
                             TableCell p1 = new TableCell(new Paragraph(new Run(PlayerOne.ToString())));

                             TableCell p1_score = new TableCell(new Paragraph(new Run(Score.ToString())));

                             TableCell p2 = new TableCell(new Paragraph(new Run(PlayerTwo.ToString())));

                             TableCell p2_score = new TableCell(new Paragraph(new Run(OppScore.ToString())));

                             TableCell time = new TableCell(new Paragraph(new Run(Time.ToString())));
                             TableCell vs = new TableCell(new Paragraph(new Run("VS")));
                             TableCell on = new TableCell(new Paragraph(new Run("On")));
                             
                             vs.Foreground = Brushes.Red;
                             on.Foreground = Brushes.Red;
                             r.Cells.Add(p1_score);
                             r.Cells.Add(p1);
                             r.Cells.Add(vs);
                             r.Cells.Add(p2);
                             r.Cells.Add(p2_score);
                             r.Cells.Add(on);
                             r.Cells.Add(time);
                             gameshistory.Rows.Add(r);
                           
                         }
                     }
                   

                    

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error with datastore: " + ex.Message);
                }
                finally
                {
                    conn.Close();
                }
        }

        private string getNameFromId(Object PlayerId, SqlConnection conn)
        {
             String connection_String = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=D:\Downloads\TicTacToe\TicTacToe\TicTacToe\ticTacToeabase.mdf;Integrated Security=True";
             System.Data.SqlClient.SqlConnection c = new System.Data.SqlClient.SqlConnection(connection_String);

            c.Open();
            String command = "Select top 1 name from players where Id = " + PlayerId + ";";
            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(command, c);
            String result = (String)cmd.ExecuteScalar();
            c.Close();
            return result;
             
        }

       

        private void MainMenu_Click(object sender, RoutedEventArgs e)
        {
            mw.mainFrame.Navigate(new StartPage());
        }
    }
}
