using Org.Mentalis.Network.ProxySocket;
using SuperSocket.ClientEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TradingView_Chat_Bot.Proxy
{
    public class Socks5ProxyStateObject
    {
        public Socket worker { get; set; }
        public StringBuilder sb = new StringBuilder();
        public const int BufferSize = 256;
        public byte[] buffer = new byte[BufferSize];
    }

    public class DataEventArgs : EventArgs
    {
        public string Data { get; set; }

        public DataEventArgs(string data)
        {
            Data = data;
        }
    }


    public class Socks5ProxyClient
    {
        private ProxySocket client;
        private bool useTOR = false;

        ManualResetEvent resetConnect = new ManualResetEvent(false);
        ManualResetEvent resetDisconnect = new ManualResetEvent(false);
        ManualResetEvent resetRecieve = new ManualResetEvent(false);
        ManualResetEvent resetSend = new ManualResetEvent(false);

        //On Client Connected
        public delegate void OnClientConnectHandler(object sender, EventArgs e);
        public event OnClientConnectHandler onClientConnect;

        //On Client Disconnected
        public delegate void OnClientDisConnectHandler(object sender, EventArgs e);
        public event OnClientDisConnectHandler onClientDisConnect;

        //On Data received
        public delegate void OnDataReceivedHandler(object sender, DataEventArgs e);
        public event OnDataReceivedHandler onDataReceived;

        //On Error received
        public delegate void OnErrorHandler(object sender, DataEventArgs e);
        public event OnErrorHandler onError;

        public Socks5ProxyClient(bool useTOR = false)
        {
            try
            {
                client = new ProxySocket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                this.useTOR = useTOR;
            }
            catch (Exception err)
            {
                onError(this, new DataEventArgs(err.ToString()));
            }
        }

        public void disconnect()
        {
            try
            {
                client.BeginDisconnect(false, new AsyncCallback(onDisconnectCallback), client);
                resetDisconnect.WaitOne();
            }
            catch (Exception err)
            {
                onError(this, new DataEventArgs(err.ToString()));
            }
        }

        public void connect()
        {
            try
            {
                if (this.useTOR)
                {
                    client.ProxyEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 9050);
                    client.ProxyType = ProxyTypes.Socks5;
                }
                resetConnect.Reset();
                client.BeginConnect("www.yourhostname.com", 994, new AsyncCallback(onConnectCallback), client);
                resetConnect.WaitOne();
            }
            catch (Exception err)
            {
                onError(this, new DataEventArgs(err.ToString()));
            }
        }

        private void onConnectCallback(IAsyncResult ar)
        {
            try
            {
                ProxySocket worker = (ProxySocket)ar.AsyncState;
                if (worker == null)
                {
                    worker = this.client;
                }

                if (worker.Connected == true)
                {
                    worker.EndConnect(ar);
                    worker.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.KeepAlive, 1);
                }
                else
                {
                    resetConnect.Reset();
                    //Not connected
                }
                resetConnect.Set();
                onClientConnect(this, new EventArgs());
            }
            catch (Exception err)
            {
                onError(this, new DataEventArgs(err.ToString()));
            }
        }

        private void ReceiveCallback(IAsyncResult ar)
        {
            try
            {
                Socks5ProxyStateObject state = (Socks5ProxyStateObject)ar.AsyncState;
                Socket handler = state.worker;
                int read = handler.EndReceive(ar);

                if (read > 0)
                {
                    //Store what we have now and receive the rest
                    state.sb.Append(Encoding.ASCII.GetString(state.buffer, 0, read));

                    //There might be more data to receive
                    client.BeginReceive(state.buffer, 0, 256, 0, new AsyncCallback(ReceiveCallback), state);
                    resetRecieve.Set();

                    onDataReceived(this, new DataEventArgs(state.sb.ToString()));
                    state.sb.Clear(); //Clear object else we get responses 2 times
                }
            }
            catch (Exception err)
            {
                onError(this, new DataEventArgs(err.ToString()));
            }
        }

        private void onDisconnectCallback(IAsyncResult ar)
        {
            try
            {
                resetDisconnect.Set();
                ProxySocket worker = (ProxySocket)ar.AsyncState;
                worker.EndDisconnect(ar);
                client.Shutdown(SocketShutdown.Both);
                onClientConnect(this, new EventArgs());
            }
            catch (Exception err)
            {
                //client.Shutdown(SocketShutdown.Both);
                onError(this, new DataEventArgs(err.ToString()));
            }
        }

        public void Send(string data)
        {
            try
            {

                if (client.Connected == false) { connect(); }
                if (client.Connected == true)
                {

                    //Get reciepients
                    string encodedMessage = data;
                    byte[] buffer = ASCIIEncoding.UTF8.GetBytes(encodedMessage + "\n");

                    Socks5ProxyStateObject state = new Socks5ProxyStateObject();
                    state.worker = client;
                    state.buffer = buffer;

                    client.BeginSend(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(onSendCallback), state);
                    resetSend.WaitOne();
                }
                else
                {
                    onError(this, new DataEventArgs("Sorry we can't send your message cause you are not connected to the server."));
                }
            }
            catch (Exception err)
            {
                onError(this, new DataEventArgs(err.ToString()));
            }
        }

        private void onSendCallback(IAsyncResult ar)
        {
            try
            {
                Socks5ProxyStateObject state = (Socks5ProxyStateObject)ar.AsyncState;
                int bytesSend = state.worker.EndSend(ar);
                resetSend.Set();
            }
            catch (Exception err)
            {
                onError(this, new DataEventArgs(err.ToString()));
            }
        }

        public void StartReceive()
        {
            try
            {
                Socks5ProxyStateObject state = new Socks5ProxyStateObject();
                state.worker = client;
                if (client.Connected)
                {
                    client.BeginReceive(state.buffer, 0, Socks5ProxyStateObject.BufferSize, 0,
                        new AsyncCallback(ReceiveCallback), state);
                    resetRecieve.Set();
                }
            }
            catch (Exception err)
            {
                onError(this, new DataEventArgs(err.ToString()));
            }
        }
    }
}
