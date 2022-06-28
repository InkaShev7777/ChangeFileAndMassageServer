using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ChangeFileAndMassageServer
{
    class Program
    {
        static void Main(string[] args)
        {
            const int PORT = 8008;
            IPEndPoint iPEnd = new IPEndPoint(IPAddress.Parse("127.0.0.1"), PORT);
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            Console.WriteLine("SERVER START");
            Socket clientSocket = null;
            try
            {
                socket.Bind(iPEnd);
                socket.Listen(10);


                clientSocket = socket.Accept();
                while(true)
                {
                    int byteCount = 0;
                    byte[] buffer = new byte[256];
                    StringBuilder stringBuilder = new StringBuilder();
                    do
                    {
                        byteCount = clientSocket.Receive(buffer);
                        stringBuilder.Append(Encoding.Unicode.GetString(buffer, 0, byteCount));
                    } while (clientSocket.Available > 0);
                    string msg = stringBuilder.ToString();
                    if (msg == "/end")
                    {
                        clientSocket.Shutdown(SocketShutdown.Send);
                        clientSocket.Close();
                        break;
                    }
                    else
                    {
                        Console.WriteLine($"New msg:\t{stringBuilder.ToString()}");
                    }

                    
                }
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                clientSocket.Shutdown(SocketShutdown.Both);
                clientSocket.Close();
            }
            Console.ReadKey();
        }
    }
}
