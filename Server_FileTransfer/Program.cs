using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace Server_FileTransfer
{
    class Program
    {
        static void Main(string[] args)
        {
            IPEndPoint iep = new IPEndPoint(IPAddress.Any, 6000);
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Bind(iep);
            socket.Listen(5);

            Socket clientSocket = socket.Accept();

            byte[] Byte = new byte[1024];

            string filename = "file.txt";
            string filedata = "abcdefg";
            int msgSize = Encoding.ASCII.GetByteCount(filename);
            byte[] Byte1 = BitConverter.GetBytes(msgSize);
            byte[] Byte2 = Encoding.ASCII.GetBytes(filename);
            byte[] Byte3 = Encoding.ASCII.GetBytes(filedata);
            int j = 0;
            for (int i = 0; i < 1024; i++)
            {
                if (i < 4)
                {
                    Byte[i] = Byte1[i];
                }
                else if (i < Byte2.Length)
                {
                    Byte[i] = Byte2[i - 4];
                }
                else if (i < Byte3.Length)
                {
                    Byte[i] = Byte3[j];
                    j++;
                }
            }
            int length=
            clientSocket.Send(Byte);
            Console.WriteLine(msgSize);
            Console.WriteLine(Byte1);
            Console.WriteLine(Byte2);
            Console.WriteLine(Byte3);
            Console.ReadLine();
            clientSocket.Shutdown(SocketShutdown.Both);
            clientSocket.Close();
        }
    }
}
