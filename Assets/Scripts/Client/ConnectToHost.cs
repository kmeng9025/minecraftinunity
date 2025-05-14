using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System;
using System.Text;
using System.Runtime.CompilerServices;
namespace Client
{
    public class ConnectToHost
    {
        TcpClient client;
        NetworkStream stream;
        Thread thread;
        public static ConnectToHost instance;

        public ConnectToHost()
        {
            thread = new Thread(new ThreadStart(SetupClient));
            thread.Start();
        }

        private void SetupClient()
        {
            try{
                client = new TcpClient();
                Debug.Log("Connecting to server...");
                client.Connect(IPAddress.Parse("127.0.0.1"), 4567);
                stream = client.GetStream();
                Debug.Log("Connected to server");
                Thread listenerThread = new Thread(new ThreadStart(ListenToData));
                listenerThread.Start();
                SendMessageToHost("cw 12455879");
            } catch (Exception e){
                Debug.Log("Error at client- " + e.ToString());
            }
        }

        private void ListenToData()
        {
            int i;
            byte[] buffer = new byte[512*1024];
            while ((i = stream.Read(buffer, 0, buffer.Length)) > 0)
            {
                string data = Encoding.UTF8.GetString(buffer, 0, i);
                Debug.Log("client received: " + data);
                ReceivedMessage(data);
            }
        }
        private void ReceivedMessage(string message)
        {
            String[] messages = message.Split('@');
            for (int i = 0; i < messages.Length-1; i++)
            {
                string messageHeader = messages[i].Substring(0, 2);
                switch (messageHeader)
                {
                    case "cd":
                        BlockManager.LoadChunks(messages[i]);
                        break;
                    case "ub":
                        Debug.Log("Update Block: " + messages[i].Substring(2));
                        break;
                    case "mp":
                        Debug.Log("Mob Position");
                        break;
                    case "pp":
                        Debug.Log("Player Position: " + messages[i].Substring(2));
                        break;
                    case "bn":
                        Debug.Log("Banned");
                        break;
                    case "kk":
                        Debug.Log("Kick");
                        break;
                    case "cg":
                        Debug.Log("Chunk Generated On Server");
                        break;
                    case "wg":
                        BlockManager.LoadChunks("wg");
                        Variables.worldGenerated = true;
                        break;
                    case "gm":
                        Debug.Log("Game Mode: " + messages[i].Substring(2));
                        break;
                    case "pd":
                        Debug.Log("Player Direction: " + messages[i].Substring(2));
                        break;
                    default:
                        StopClient();
                        break;
                }
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void SendMessageToHost(string message)
        {
            byte[] msg = Encoding.UTF8.GetBytes(message);
            stream.Write(msg, 0, msg.Length);
            stream.FlushAsync();
            Debug.Log("client sent: " + message);
        }

        public void StopClient()
        {
            if (client != null)
            {
                client.Close();
                client = null;
                Debug.Log("Client stopped");
            }
            if (stream != null)
            {
                stream.Close();
                stream = null;
                Debug.Log("Stream stopped");
            }
            if (thread != null)
            {
                thread.Abort();
                thread = null;
                Debug.Log("Thread stopped");
            }
        }
        public static ConnectToHost getInstance()
        {
            if (instance == null)
            {
                instance = new ConnectToHost();
            }
            return instance;
        }
    }
}