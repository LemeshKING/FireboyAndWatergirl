using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace WpfApp1
{
   /// <summary>
   /// Логика взаимодействия для LocationMenu.xaml
   /// </summary>
   public partial class LocationMenu : Window
   {
      MainWindow game;
      public LocationMenu()
      {
         InitializeComponent();
         Intro.Play();
      }

      private void Button_Click(object sender, RoutedEventArgs e)
      {
         int[,] map = new int[,] { 
             { 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13 },
             { 13, 0, 0, 0 ,0, 0, 16, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  13 },
             { 13, 0, 0, 0 ,5, 6, 6, 6, 6, 6, 7, 0, 0, 0, 0, 0, 0, 0, 0, 12, 0, 0,  13 },
             { 13, 0, 0, 0 ,0, 0, 0, 0, 0, 0, 0, 0, 15, 1, 1, 1, 1, 1, 1, 1, 1, 1,  13 },
             { 13, 5, 6, 7 ,0, 0, 0, 0, 0, 15, 0, 2, 4, 0, 0, 0, 0, 0, 0, 0, 0, 0,  13 },
             { 13, 0, 16, 0 ,0, 0, 1, 2, 3, 3, 4, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  13 },
             { 13, 11, 1, 0 ,0, 0, 0, 14, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  13 },
             { 13, 0, 1, 5 ,7, 1, 1, 1, 1, 11, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0,  13 },
             { 13, 0, 0, 0 ,0, 16, 16, 16, 0, 0, 0, 0, 0, 15, 15, 15, 0, 0, 0, 0, 0, 1,  13 },
             { 13, 0, 0, 0 ,5, 6, 6, 6, 7, 0, 0, 0, 2, 3, 3, 3, 4, 0, 0, 0, 1, 1,  13 },
             { 13, 0, 0, 0 ,0, 15, 15, 15, 0, 0, 0, 0, 0, 16, 16, 16, 0, 0, 0, 1, 1, 1,  13 },
             { 1, 1, 1, 1 ,2, 3, 3, 3, 4, 1, 1, 1, 5, 6, 6, 6, 7, 1, 1, 1, 1, 1,  1 },
         };
         if (game == null)
         {
            game = new MainWindow();
            game.SetMap(map);
            game.SetPositions(new Point(80, 340), new Point(34, 340));
            game.Closed += game_Closed;
            game.Show();
            Intro.Close();
            this.Hide();
         }

      }

      private void Button_Click_1(object sender, RoutedEventArgs e)
      {
         int[,] map = new int[,] {
            { 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13 },
            { 13, 0, 0, 0 ,0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 13 },
            { 13, 0, 0, 0 ,0, 0, 0, 0, 0, 0, 0, 0, 0, 15, 0, 0, 0, 0, 12, 0, 0, 0, 13 },
            { 13, 0, 0, 0 ,0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 13 },
            { 13, 0, 0, 0 ,0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 13 },
            { 13, 0, 0, 0 ,0, 0, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 13 },
            { 13, 1, 11, 1 ,0, 0, 0, 14, 0, 0, 0, 0, 0, 0, 0, 16, 15, 0, 0, 0, 0, 0, 13 },
            { 13, 0, 0, 0 ,1, 1, 1, 1, 1, 1, 11, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 13 },
            { 13, 15, 0, 0 ,0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 13 },
            { 13, 0, 0, 0 ,0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 13 },
            { 13, 0, 0, 0 ,15, 15, 15, 0, 0, 16, 16, 16, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 13 },
             { 1, 1, 1, 1 ,2, 3, 4, 1, 1, 5, 6, 7, 1, 1, 8, 9, 10, 1, 1, 1, 1, 1, 1 },
         };
         if (game == null)
         {
            game = new MainWindow();
            game.SetMap(map);
            game.SetPositions(new Point(80, 340), new Point(34, 340));
            game.Closed += game_Closed;
            game.Show();
            Intro.Close();
            this.Hide();
         }
      }

      private void Button_Click_2(object sender, RoutedEventArgs e)
      {
         int[,] map = new int[,] {
            { 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13 },
            { 13, 0, 0, 0 ,0, 16, 14, 0, 0, 0, 0, 0, 0, 0, 0, 0, 15, 0, 0, 0, 0, 0, 13 },
            { 13, 16, 0, 0 ,0, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 1, 11, 1, 0, 0, 0, 0, 13 },
            { 13, 0, 0, 0 ,1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 1, 1, 0, 1, 1, 0, 0, 0, 13 },
            { 13, 0, 0, 1 ,1, 0, 0, 0, 13, 0, 0, 12, 0, 0, 13, 0, 0, 0, 0, 1, 0, 0, 13 },
            { 13, 0, 0, 0 ,0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 13 },
            { 13, 1, 0, 0 ,0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 13 },
            { 13, 1, 1, 1 ,0, 16, 16, 0, 0, 0, 0, 0, 0, 0, 0, 15, 15, 0, 0, 1, 1, 1, 13 },
            { 13, 0, 0, 0 ,5, 6, 6, 6, 7, 0, 0, 0, 0, 0, 2, 3, 3, 3, 4, 0, 0, 0, 13 },
            { 13, 0, 0, 0 ,0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 13 },
            { 13, 0, 0, 15 ,15, 15, 15, 0, 0, 0, 1, 1, 1, 0, 0, 16, 16, 16, 16, 0, 0, 0, 13 },
            { 1, 1, 2, 3 ,3, 3, 3, 3, 4, 1, 1, 1, 1, 1, 5, 6, 6, 6, 6, 6, 7, 1, 1 },
         };
         if (game == null)
         {
            game = new MainWindow();
            game.SetMap(map);
            game.SetPositions(new Point(714, 340), new Point(34, 340));
            game.Closed += game_Closed;
            game.Show();
            Intro.Close();
            this.Hide();
         }
      }
      private void game_Closed(object sender, EventArgs e)
      {
         game = null;
         this.Show();
         Intro.Play();
      }

      private void Intro_Loaded(object sender, RoutedEventArgs e)
      {
         this.Closed += LocationMenu_Closed;
      } 

      private void LocationMenu_Closed(object sender, EventArgs e)
      {
         Intro.Close();
      }
   }
}
