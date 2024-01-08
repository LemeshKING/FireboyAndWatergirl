using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;

namespace Server
{
   internal class Program
   {
      static void Main(string[] args)
      {
         // Устанавливаем IP-адрес и порт, на котором будет прослушиваться входящие соединения
         string ipAddress = "127.0.0.1";
         int port = 8080;

         // Создаем объект сокета
         Socket listenerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

         // Создаем объект IP-адреса и конечной точки
         IPAddress ipAddr = IPAddress.Parse(ipAddress);
         IPEndPoint ipEndPoint = new IPEndPoint(ipAddr, port);

         // Назначаем сокету конечную точку
         listenerSocket.Bind(ipEndPoint);

         // Начинаем прослушивать входящие соединения
         listenerSocket.Listen(10);

         Console.WriteLine("Сервер запущен. Ожидание входящих соединений...");

         while (true)
         {
            // Принимаем входящее соединение
            Socket clientSocket = listenerSocket.Accept();
            byte[] buffer = new byte[1024];
            int receivedBytes = clientSocket.Receive(buffer);
            string receivedData = Encoding.UTF8.GetString(buffer, 0, receivedBytes);

            // Обработка полученных данных
            Console.WriteLine("Получено сообщение от клиента: " + receivedData);

            // Отправка ответа клиенту
            string response = "Сообщение успешно получено на сервере!";
            byte[] responseData = Encoding.UTF8.GetBytes(response);
            clientSocket.Send(responseData);

            // Закрытие подключения с клиентом
            clientSocket.Shutdown(SocketShutdown.Both);
            clientSocket.Close();
         }
         // Здесь можно обработать входящее соединение

         // Закрываем сокет клиента
  
         
      }

   }
   
}
