using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace WpfApp1
{
   /// <summary>
   /// Логика взаимодействия для ClientServerMenu.xaml
   /// </summary>
   public partial class ClientServerMenu : Window
   {
      private Client client;
      public ClientServerMenu()
      {
         InitializeComponent();
         Intro.Play();
      }

      private void Image_Loaded(object sender, RoutedEventArgs e)
      {
         this.Closed += ClientServerMenu_Closed;
      }

      private void ClientServerMenu_Closed(object sender, EventArgs e)
      {
         Intro.Stop();
         
      }

      private void Button_Click(object sender, RoutedEventArgs e)
      {
         ProcessStartInfo startInfo = new ProcessStartInfo();
         string tmp = System.IO.Directory.GetCurrentDirectory();
         startInfo.FileName = tmp + "\\Server.exe";

         // Запускать консольное приложение без отдельного окна
         startInfo.CreateNoWindow = false;

         // Запускать консольное приложение в фоновом режиме
         startInfo.WindowStyle = ProcessWindowStyle.Normal;

         // Запуск процесса
         Process.Start(startInfo);
      }

      private void Button_Click_1(object sender, RoutedEventArgs e)
      {
         client = new Client();
         client.Show();
      }
   }
   
}
