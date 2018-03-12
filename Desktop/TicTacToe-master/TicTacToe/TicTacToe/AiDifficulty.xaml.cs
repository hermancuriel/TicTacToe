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

namespace TicTacToe
{
    /// <summary>
    /// Interaction logic for AiDifficulty.xaml
    /// </summary>
    public partial class AiDifficulty : Window
    {
        public int difficulty = 0;
        public AiDifficulty()
        {
            InitializeComponent();
        }
        private void Hard_Click(object sender, RoutedEventArgs e)
        {
            difficulty = 3;
            this.DialogResult = true;
        }
        private void Medium_Click(object sender, RoutedEventArgs e)
        {
            difficulty = 2;
            this.DialogResult = true;
        }

        private void Easy_Click(object sender, RoutedEventArgs e)
        {
            difficulty = 1;
            this.DialogResult = true;
        }
    }
}
