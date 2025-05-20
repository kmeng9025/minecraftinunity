using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Text;
using System;
using System.Runtime.CompilerServices;
using UnityEditor.PackageManager;
namespace Server
{
    public class ConnectToClient
    {
        static ConnectToClient instance;
        TcpListener server;
        TcpClient client;
        NetworkStream stream;
        Thread thread;
        Anticheat antiCheat;
        public ConnectToClient()
        {
            thread = new Thread(new ThreadStart(SetupServer));
            thread.Start();
        }
        private void SetupServer()
        {
            antiCheat = Anticheat.getInstance();
            while (true)
            {
                try
                {
                    server = new TcpListener(IPAddress.Parse("127.0.0.1"), 4567);
                    server.Start();
                    while (true)
                    {
                        Debug.Log("Waiting for a connection...");
                        client = server.AcceptTcpClient();
                        Debug.Log("Connected to client");
                        stream = client.GetStream();
                        Thread listenerThread = new Thread(new ThreadStart(ListenToData));
                        listenerThread.Start();
                    }
                }
                catch (Exception e)
                {
                    Debug.Log("Error at server: " + e.ToString());
                    server.Stop();
                    server = null;
                }
            }
        }
        private void ListenToData()
        {
            int i;
            string data = "";
            byte[] buffer = new byte[1024];
            while ((i = stream.Read(buffer, 0, buffer.Length)) > 0)
            {
                data = Encoding.UTF8.GetString(buffer, 0, i);
                Debug.Log("server received: " + data);
                ReceivedMessage(data);
                buffer = new byte[1024];
                data = "";
            }
        }
        private void ReceivedMessage(string message)
        {
            string[] messages = message.Split('@');
            for (int i = 0; i < messages.Length; i++)
            {
                string messageHeader = messages[i].Substring(0, 2);
                switch (messageHeader)
                {
                    case "pp":
                        antiCheat.gotData(messages[i].Substring(2));
                        break;
                    case "pn":
                        Debug.Log("Player Name: " + messages[i].Substring(2));
                        break;
                    case "cd":
                        Debug.Log("Camera Direction");
                        break;
                    case "cl":
                        Debug.Log("Click Registered");
                        break;
                    case "pl":
                        Debug.Log("Player Location");
                        break;
                    case "cw":
                        Variables.serverGenerator = new ServerGenerator(long.Parse(messages[i].Substring(3)));
                        Debug.Log("Create World");
                        break;
                    case "lw":
                        Debug.Log("Load World");
                        break;
                    case "sw":
                        Debug.Log("Save World");
                        break;
                    case "cm":
                        Debug.Log("Command");
                        break;
                    default:
                        StopServer();
                        break;
                }
            }
        }
        
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void SendMessageToClient(string message)
        {
            byte[] msg = Encoding.UTF8.GetBytes(message+"@");
            stream.Write(msg, 0, msg.Length);
            stream.FlushAsync();
            Debug.Log("server sent: " + message + "@");
        }

        public void StopServer()
        {
            if (client != null)
            {
                client.Close();
            }
            if (server != null)
            {
                server.Stop();
            }
            if (thread != null && thread.IsAlive)
            {
                thread.Abort();
            }
        }
        public static ConnectToClient getInstance()
        {
            if (instance == null)
            {
                instance = new ConnectToClient();
            }
            return instance;
        }
    }
}
