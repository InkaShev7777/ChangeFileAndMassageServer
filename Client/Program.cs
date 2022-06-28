using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            const int PORT = 8008;
            const string IP_ADDR = "127.0.0.1";
            IPEndPoint iPEnd = new IPEndPoint(IPAddress.Parse(IP_ADDR), PORT);
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                socket.Connect(iPEnd);
                while(true)
                {
                    Console.WriteLine("Enter your massage:");
                    string msg = Console.ReadLine();
                    byte[] data = Encoding.Unicode.GetBytes(msg);
                    socket.Send(data);
                    if(msg.Equals("/end"))
                    {
                        socket.Shutdown(SocketShutdown.Both);
                        socket.Close();
                        Environment.Exit(0);
                    }
                    
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                socket.Shutdown(SocketShutdown.Both);
                socket.Close();
            }
            Console.ReadKey();
        }
    }
}
