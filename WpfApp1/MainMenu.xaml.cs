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

namespace WpfApp1
{
   /// <summary>
   /// Логика взаимодействия для MainMenu.xaml
   /// </summary>
   public partial class MainMenu : Window
   {
      private LocationMenu locationMenu;
      private ClientServerMenu clientServerMenu;
      public MainMenu()
      {
         InitializeComponent();
         Intro.Play();
      }

      private void Button_Click(object sender, RoutedEventArgs e)
      {

         if (locationMenu == null)
         {
            locationMenu = new LocationMenu();
            locationMenu.Closed += locationMenu_Closed;
            locationMenu.Show();
            Intro.Close();
            this.Hide();
         }
      }
      private void locationMenu_Closed(object sender, EventArgs e)
      {
         locationMenu = null;
         this.Show();
         Intro.Play();
      }

      private void Button_Click_1(object sender, RoutedEventArgs e)
      {
         if (clientServerMenu == null)
         {
            clientServerMenu = new ClientServerMenu();
            clientServerMenu.Closed += ClientServerMenu_Closed;
            clientServerMenu.Show();
            Intro.Close();
            this.Hide();
         }
      }

      private void ClientServerMenu_Closed(object sender, EventArgs e)
      {
         clientServerMenu = null;
         this.Show();
         Intro.Play();
      }
   }
}
