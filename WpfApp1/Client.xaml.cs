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
using System.Net;
using System.Net.Sockets;

namespace WpfApp1
{
   /// <summary>
   /// Логика взаимодействия для Client.xaml
   /// </summary>
   public partial class Client : Window
   {
      public Client()
      {
         InitializeComponent();
      }

      private void Canvas_Loaded(object sender, RoutedEventArgs e)
      {
         // Создание сокета для подключения к серверу
        Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

         // IP-адрес и порт сервера, к которому нужно подключиться
         IPAddress ipAddress = IPAddress.Parse("127.0.0.1"); // замените на реальный IP-адрес сервера
         int port = 8080; // замените на реальный порт сервера

         try
         {
            // Подключение к серверу
            socket.Connect(new IPEndPoint(ipAddress, port));

            // Отправка данных на сервер
            string dataToSend = "Hello, server!";
            byte[] data = Encoding.UTF8.GetBytes(dataToSend);
            socket.Send(data);

            // Получение ответа от сервера
            byte[] buffer = new byte[1024];
            int receivedBytes = socket.Receive(buffer);
            string receivedData = Encoding.UTF8.GetString(buffer, 0, receivedBytes);

            // Обработка полученных данных
            Console.WriteLine("Сервер ответил: " + receivedData);
         }
         catch (SocketException ex)
         {
            Console.WriteLine("Ошибка подключения к серверу: " + ex.Message);
         }
         finally
         {
            // Закрытие соединения
            socket.Close();
         }
      }
   }
    
}
