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
using System.Diagnostics;
using System.Threading;
using System.Windows.Threading;
using System.IO;

namespace WpfApp1
{
   /// <summary>
   /// Логика взаимодействия для MainWindow.xaml
   /// </summary>
   
   public partial class MainWindow : Window
   {
     
      public MainWindow()
      {
         InitializeComponent();
      }


      BitmapImage fireLeft = new BitmapImage(new Uri("fireBoyLeft.png", UriKind.Relative));
      BitmapImage fireRight = new BitmapImage(new Uri("fireBoyRight.png", UriKind.Relative));
      BitmapImage waterRight = new BitmapImage(new Uri("waterGirlRight.png", UriKind.Relative));
      BitmapImage waterLeft = new BitmapImage(new Uri("waterGirlLeft.png", UriKind.Relative));
      BitmapImage water = new BitmapImage(new Uri("waterGirl.png", UriKind.Relative));
      BitmapImage fire = new BitmapImage(new Uri("fireBoy.png", UriKind.Relative));
      static MediaElement death = new MediaElement();
      static MediaElement collectCoin = new MediaElement();
      static int[,] map;

      public void SetMap(int[,] _map)
      {
         map = _map;
      }



      static Point startPositionBoy = new Point();
      static Point startPositionGirl = new Point();
      public void SetPositions(Point Girl, Point Boy)
      {
         startPositionBoy = Boy; startPositionGirl = Girl;
      }
      static Point pointGate = new Point();
      static Point point = new Point();
      static Image imgtmp;
      static bool buttonOn = false;
      DispatcherTimer timer;
      static Canvas canvas;
      class Player
      {
         
         public bool[] LRU = new[] { false, false, false };
         public double lastTop;
         public double top;
         public double left;
         public double dx, dy;
         public bool onGround;
         public Rect rect = new Rect(0,0,34,34);
         public Image UIElement;
         public bool playerOnDoor = false;
         public bool playeronButton = false;
         public uint countGems = 0;
  
         public Player(double top, double left)
         {
            this.top = top;
            this.left = left;
            dx = dy = 0;
            onGround = false;

            rect.X = left;
            rect.Y = top;



         }
         private  void CollisionX()
         {
               for (int i = (int)(top / 34); i < (int)(top + rect.Height) / 34; i++)
                  for (int j = (int)(left / 34); j <= (int)((left + rect.Width + 1) / 34); j++)
                     if (map[i, j] == 1)
                     {
                        if (dx > 0) left = j * 34 - rect.Width;
                        if (dx < 0) left = j * 34 + 34;

                     }
                     else if (map[i, j] == 13)
                     {
                        if (dx > 0) left = j * 34 - rect.Width; ;
                        if (dx < 0) left = j * 34 + 34;
                     }
                     else if (map[i, j] == 14)
                     {
                        if (dx > 0) left = j * 34 - rect.Width;
                        if (dx < 0) left = j * 34 + 34;
                     }
                     else if (map[i, j] == 12)
                     {
                        playerOnDoor = true;
                        dx = 0;
                     }
                     else if (map[i,j] == 15)
                     {
                        if(UIElement.Name =="Fireboy")
                        {
                           map[i,j] = 0;
                           imgtmp = canvas.InputHitTest(new Point(j * 34, (i + 1) * 34)) as Image;
                           string path = "backGroundBrick.png";
                           collectCoin.Play();
                           collectCoin.Position = TimeSpan.Zero;
                           Change(path, imgtmp);
                           countGems++;
                        }

                     }
                  else if (map[i, j] == 16)
                  {
                     if (UIElement.Name == "Watergirl")
                     {
                        map[i, j] = 0;
                        imgtmp = canvas.InputHitTest(new Point(j * 34, (i + 1) * 34)) as Image;
                        string path = "backGroundBrick.png";
                        collectCoin.Play();
                        collectCoin.Position = TimeSpan.Zero;
                        Change(path, imgtmp);
                        countGems++;

                     }

                  }
                  else
                        playerOnDoor = false;
         }

         private void Died()
         {
            dy = 0;
            onGround = true;
            if (UIElement.Name == "Fireboy")
            {
               top = startPositionBoy.Y;
               left = startPositionBoy.X;
            }
            else
            {
               top = startPositionGirl.Y;
               left = startPositionGirl.X;
            }
         
            death.Play();
            death.Position = TimeSpan.Zero;
         }
         private void RunningOnTheFloor(int i)
         {
            top = i * 34 - rect.Height;
            dy = 0;
            onGround = true;
         }
  
         private void CollisionY()
         {
            int tmp = (int)(top + rect.Height) / 34;
            
            
            for (int i = (int)(top / 34); i <= tmp; i++)
               for (int j = (int)(left / 34); j <= (int)(left + rect.Width) / 34; j++)
               {
                  if (map[i, j] == 1)
                  {
                     if (top - dy == i * 34)
                        top -= dy;

                     else if (dy > 0)
                     {
                        if (top + 34 == point.Y - 34)
                           if (left + 34 < point.X || left - 34 > point.X)
                              playeronButton = false;
                           else
                              playeronButton = true;
                        else
                           playeronButton = false;
                        RunningOnTheFloor(i);
                     }

                     else if (dy < 0) { top = i * 34 + 34; dy = 0; }
                  }
                  else if ((map[i, j] == 13))
                  { if (dy < 0) { top = i * 34 + 34; dy = 0; } }
                  else if (map[i, j] == 11)
                  {
                     if (dy > 0)
                     {
                        RunningOnTheFloor(i);
                        playeronButton = true;
                        point.X = j * 34; point.Y = (i + 1) * 34;
                        imgtmp = canvas.InputHitTest(point) as Image;
                        string pathtmp = "btnon.png";
                        Change(pathtmp, imgtmp);

                     }
                     else if (dy < 0)
                     { top = i * 34 + 34; dy = 0; }
                  }
                  else if (map[i, j] == 8)
                  {
                     if (dy > 0)
                     {
                        if (left + 10 >= j * 34)
                           Died();
                     }
                     else if (dy < 0)
                     { top = i * 34 + 34; dy = 0; }
                  }
                  else if (map[i, j] == 10)
                  {
                     if (left - 10 <= j * 34)
                        Died();
                  }
                  else if (map[i, j] == 9)
                     Died();
                  else if (map[i, j] == 7)
                  {
                     if (UIElement.Name == "Fireboy")
                     {
                        if (dy > 0)
                        {
                           if (left - 10 <= j * 34)
                              Died();
                        }
                        else if (dy < 0)
                        { top = i * 34 + 34; dy = 0; }
                     }
                     else if (dy > 0)
                        RunningOnTheFloor(i);
                     else if (dy < 0)
                     { top = i * 34 + 34; dy = 0; }
                  }
                  else if (map[i, j] == 6)
                  {
                     if (UIElement.Name == "Fireboy")
                     {
                        if (dy > 0)
                           Died();
                        else if (dy < 0)
                        { top = i * 34 + 34; dy = 0; }
                     }
                     else if (dy > 0)
                        RunningOnTheFloor(i);
                     else if (dy < 0)
                     { top = i * 34 + 34; dy = 0; }
                  }
                  else if (map[i, j] == 5)
                  {
                     if (UIElement.Name == "Fireboy")
                     {
                        if (dy > 0)
                        {
                           if (left + 10 >= i * 34)
                              Died();
                        }
                        else if (dy < 0)
                        { top = i * 34 + 34; dy = 0; }

                     }
                     else if (dy > 0)
                        RunningOnTheFloor(i);
                     else if (dy < 0)
                     { top = i * 34 + 34; dy = 0; }
                  }
                  else if (map[i, j] == 2)
                  {
                     if (UIElement.Name == "Watergirl")
                     {
                        if (dy > 0)
                        {
                           if (left + 10 >= j * 34)
                              Died();
                        }
                        else if (dy < 0)
                        {
                           top = i * 34 + 34;
                           dy = 0;
                        }
                     }
                     else if (dy > 0)
                        RunningOnTheFloor(i);
                     else if (dy < 0)
                     {
                        top = i * 34 + 34;
                        dy = 0;
                     }
                  }
                  else if (map[i, j] == 3)
                  {
                     if (UIElement.Name == "Watergirl")
                     {
                        if (dy > 0)
                           Died();
                        else if (dy < 0)
                        { top = i * 34 + 34; dy = 0; }
                     }
                     else if (dy > 0)
                        RunningOnTheFloor(i);
                     else if (dy < 0)
                     { top = i * 34 + 34; dy = 0; }
                  }
                  else if (map[i, j] == 15)
                  {
                     if (UIElement.Name == "Fireboy")
                     {
                        map[i, j] = 0;
                        imgtmp = canvas.InputHitTest(new Point(j * 34, (i + 1) * 34)) as Image;
                        string path = "backGroundBrick.png";
                        collectCoin.Play();
                        collectCoin.Position = TimeSpan.Zero;
                        Change(path, imgtmp);
                        countGems++;
                     }

                  }
                  else if (map[i, j] == 16)
                  {
                     if (UIElement.Name == "Watergirl")
                     {
                        map[i, j] = 0;
                        imgtmp = canvas.InputHitTest(new Point(j * 34, (i + 1) * 34)) as Image;
                        collectCoin.Play();
                        collectCoin.Position = TimeSpan.Zero;
                        string path = "backGroundBrick.png";
                        Change(path, imgtmp);
                        countGems++;
                     }

                  }
                  else if (map[i, j] == 4)
                  {
                     if (UIElement.Name == "Watergirl")
                     {
                        if (dy > 0)
                        {
                           if (left - 10 <= j * 34)
                              Died();
                        }
                        else if (dy < 0)
                        { top = i * 34 + 34; dy = 0; }
                     }
                     else if (dy > 0)
                        RunningOnTheFloor(i);
                     else if (dy < 0)
                     { top = i * 34 + 34; dy = 0; }
                  }
                 
               }
            
         }
         public void Update(double time)
         {

       
               left += dx;
               rect.X = left;


               CollisionX();
               if (!onGround)
                  dy += 0.2;
               top += dy;
               rect.Y = top;

               onGround = false;
               CollisionY();
               dx = 0;
           
            
            Canvas.SetTop(UIElement, top);
            Canvas.SetLeft(UIElement, left);


         }
         public void SetPosition(double top, double left)
         {
            this.top = top; this.left = left;
         }
         public void SetUI(Image Ui)
         {
            UIElement = Ui;
         }
      }
      Player girl;
      Player boy;
      private async void Watergirl_KeyDown(object sender, KeyEventArgs e)
      {
         await Task.Run(() =>
         { 
            switch (e.Key)
            {
               case Key.D: girl.LRU[0] = true; break;
               case Key.A: girl.LRU[1] = true; break;
               case Key.W:
                  if (girl.onGround)
                     girl.LRU[2] = true;
                  break;
            }
         });
         
      }
      Stopwatch stopwatch = new Stopwatch();

      private void LoadImage(string imagePath,int i, int j)
      {
         
         BitmapImage imgFile = new BitmapImage(new Uri(imagePath, UriKind.Relative));
         Image img = new Image();
         img.Source = imgFile;

         img.Height = 34; img.Width = 34;
         img.Stretch = Stretch.Fill;
         img.HorizontalAlignment = HorizontalAlignment.Left; img.VerticalAlignment = VerticalAlignment.Top;
         Canvas.SetLeft(img, j * 34);
         Canvas.SetTop(img, 34 * i);
         Map.Children.Add(img);
        
      }
     

      private void Map_Loaded(object sender, RoutedEventArgs e)
      {
         for (int i = 11; i >= 0; i--)
         {
            string path = "";
            for (int j = 0; j < 23; j++)
            {
               if (map[i, j] == 1)
                  path = "\\stoneMid.png";
               else if (map[i, j] == 0)
                  path = "\\backGroundBrick.png";
               else if (map[i, j] == 2)
                  path = "\\lavaLeft.png";
               else if (map[i, j] == 3)
                  path = "\\lavaMid.png";
               else if (map[i, j] == 4)
                  path = "\\lavaRight.png";
               else if (map[i, j] == 5)
                  path = "\\waterLeft.png";
               else if (map[i, j] == 6)
                  path = "\\waterMid.png";
               else if (map[i, j] == 7)
                  path = "\\waterRight.png";
               else if (map[i, j] == 8)
                  path = "\\acidLeft.png";
               else if (map[i, j] == 9)
                  path = "\\acidMid.png";
               else if (map[i, j] == 10)
                  path = "/acidRight.png";
               else if (map[i, j] == 11)
               {
                  path = "\\backGroundBrick.png";
                  LoadImage(path, i, j);
                  path = "/btnoff.png";
               }
               else if (map[i, j] == 12)
               {
                  path = "\\backGroundBrick.png";
                  LoadImage(path, i, j);
                  path = "/door.png";
               }
               else if (map[i, j] == 13)
                  path = "/side.png";
               else if (map[i, j] == 14)
               {
                  path = "\\backGroundBrick.png";
                  LoadImage(path, i, j);
                  path = "gateMiddle.png";
                  pointGate.X = j * 34;
                  pointGate.Y = (i + 1) * 34;
               }
               else if (map[i, j] == 15)
               {
                  path = "\\backGroundBrick.png";
                  LoadImage(path, i, j);
                  path = "\\redGem.png";
               }
               else if (map[i, j] == 16)
               {
                  path = "\\backGroundBrick.png";
                  LoadImage(path, i, j);
                  path = "\\blueGem.png";
               }

               LoadImage(path, i, j);

            }
         }
         LevelAudio.Play();
         canvas = Map;
         death = Death;
         collectCoin = CoinCollect;
         stopwatch.Start();
         girl = new Player(startPositionGirl.Y, startPositionGirl.X);
         boy = new Player(startPositionBoy.Y,startPositionBoy.X);
         girl.SetUI(Watergirl);
         boy.SetUI(Fireboy);
         timer = new DispatcherTimer();
         timer.Tick += Timer_Tick;
         timer.Start();
         timer.Interval = TimeSpan.FromMilliseconds(10);
         timer.IsEnabled = true;
         KeyDown += Fireboy_KeyDown;
         KeyUp += Fireboy_KeyUp;
         KeyDown += Watergirl_KeyDown;
         KeyUp += Watergirl_KeyUp;
         this.Closed += MainWindow_Closed;
         
      }

      private void MainWindow_Closed(object sender, EventArgs e)
      {
         Map.Children.Clear();
         GC.Collect();
         Death.Close();
         timer.Stop();
         timer.IsEnabled = false;
         point.X = 0; point.Y = 0;
         LevelAudio.Close();
         this.Close();
      }

      static private void Change(string path, Image image)
      {
         BitmapImage imgFile = new BitmapImage(new Uri(path, UriKind.Relative));
         if(image != null)
            image.Source = imgFile;
         
      }

      private void Timer_Tick(object sender, EventArgs e)
      {
         Blue.Content = girl.countGems.ToString();
         Red.Content = boy.countGems.ToString();
         if (boy.LRU[0]) 
         {
            boy.dx = -2.5;
            Fireboy.Source = fireLeft;
         }
         else if (boy.LRU[1])
         {
            boy.dx = 2.5;
            Fireboy.Source = fireRight;
         }
         if (boy.LRU[2])
            if (boy.onGround)
            {
               boy.dy = -5;
               boy.lastTop = boy.top;
               boy.onGround = false;
               FireJump.Play();
               FireJump.Position = TimeSpan.Zero;
            }
         if (girl.LRU[0])
         {
            girl.dx = 2.5;
            Watergirl.Source = waterRight;
         }
         else if (girl.LRU[1])
         {
            girl.dx = -2.5;
            Watergirl.Source = waterLeft;
         }
         if (girl.LRU[2])
            if (girl.onGround)
            {
               girl.dy = -5;
               girl.lastTop = girl.top;
               girl.onGround = false;
               WaterJump.Play();
               WaterJump.Position = TimeSpan.Zero;
            }

         if (girl.dx == 0)
            Watergirl.Source = water;


         if (boy.dx == 0)
            Fireboy.Source = fire;
       

         if (boy.playeronButton || girl.playeronButton)
         {
            buttonOn = true;
            map[(int)(pointGate.Y - 34) / 34, (int)pointGate.X / 34] = 0;
            imgtmp = canvas.InputHitTest(pointGate) as Image;
            string path = "backGroundBrick.png";
            if (imgtmp.Name == "")
               Change(path, imgtmp);

         }
         else
            buttonOn = false;


         if (buttonOn == false)
            if (point.X > 0) 
            {
               imgtmp = Map.InputHitTest(point) as Image;
               string path = "btnoff.png";
               Change(path, imgtmp);
               map[(int)(pointGate.Y - 34) / 34, (int)pointGate.X / 34] = 14;
               imgtmp = Map.InputHitTest(pointGate) as Image;
               path = "gateMiddle.png";
               if (imgtmp.Name == "")
                  Change(path, imgtmp);
            }



         stopwatch.Restart();
         if (!girl.playerOnDoor || !boy.playerOnDoor) 
         {
            boy.Update(stopwatch.ElapsedMilliseconds);
            girl.Update(stopwatch.ElapsedMilliseconds);
         }
         else
         {
            Complete.Play();
            Complete.Position = TimeSpan.Zero;
            this.Close();
         }
         
      }

      private async void Fireboy_KeyDown(object sender, KeyEventArgs e)
      {
         await Task.Run(() =>
         {
            if (e.Key == Key.Left)
               boy.LRU[0] = true;
            else if (e.Key == Key.Right)
               boy.LRU[1] = true;
            if (e.Key == Key.Up)
               if (boy.onGround)
                  boy.LRU[2] = true;
         });
         
      }

      private async void Fireboy_KeyUp(object sender, KeyEventArgs e)
      {
         await Task.Run(() =>
         {
            if (e.Key == Key.Left)
            boy.LRU[0] = false;
         else if (e.Key == Key.Right)
            boy.LRU[1] = false;
         if (e.Key == Key.Up)
               boy.LRU[2] = false;
         });
      }

      private async void Watergirl_KeyUp(object sender, KeyEventArgs e)
      {
         await Task.Run(() =>
         {
            switch (e.Key)
            {
               case Key.D: girl.LRU[0] = false; break;
               case Key.A: girl.LRU[1] = false; break;
               case Key.W: girl.LRU[2] = false; break;
            }
         });
      }
   }
}
