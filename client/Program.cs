using System;
using System.Threading;
using System.Net.Sockets;
namespace client
{
    class Program
    {
        static void Main(string[] args)
        {
            new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                Connect("127.0.0.1", 9000, "Hello I'm Device 1...");
            }).Start();

            new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                Connect("127.0.0.1", 9000, "Hello I'm Device 2...");
            }).Start();
            Console.ReadLine();
        }

        static private void Connect(String server, Int32 port, String message)
        {
            try
            {
                TcpClient client = new TcpClient(server, port);
                NetworkStream stream = client.GetStream();
                int count = 0;
                while (count++ < 3)
                {
                    Byte[] data = System.Text.Encoding.ASCII.GetBytes(message);

                    stream.Write(data, 0, data.Length);
                    Console.WriteLine("Sent: {0}", message);
                    
                    data = new Byte[256];
                    String response = String.Empty;
                    
                    Int32 bytes = stream.Read(data, 0, data.Length);
                    response = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
                    Console.WriteLine("Received: {0}", response);
                    Thread.Sleep(2000);
                }
                stream.Close();
                client.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: {0}", e);
            }
            Console.Read();
        }
    }
}
