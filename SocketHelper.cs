using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSocket4Net;

namespace TradingView_Chat_Bot
{
    public class SocketHelper
    {
        private WebSocket _socket = null;

        // A delegate type for hooking up change notifications.
        public delegate void ChatChangedEventHandler(object sender, WebSocket4Net.MessageReceivedEventArgs e);

        public event ChatChangedEventHandler Changed;

        public SocketHelper()
        {

        }

        #region TradingView
        private const string TRADINGVIEW_CHAT_URL = @"wss://www.tradingview.com/message-pipe-ws/public?_=1415095628209&tag=&time=&eventid=";

        public bool InitializeTradingViewWebSocket()
        {
            // Close any opened socket first
            Close();

            try
            {
                _socket = new WebSocket(TRADINGVIEW_CHAT_URL);
                _socket.NoDelay = true;

                _socket.Opened += websocket_Opened;
                _socket.Error += websocket_Error;
                _socket.Closed += websocket_Closed;
                _socket.MessageReceived += websocket_MessageReceived;
                _socket.DataReceived += _socket_DataReceived;
                _socket.Open();
            }
            catch (Exception exp)
            {
                Debug.WriteLine(exp.ToString());
                return false;
            }
            return true;
        }
        #endregion

        #region Common
        /// <summary>
        /// Closes the Socket connection and releases all associated resources
        /// </summary>
        public void Close()
        {
            if (_socket != null)
            {
                try
                {
                    _socket.Close(); // attempt to close... will throw exception if its not connected
                }
                catch { }
                _socket = null;
            }
        }

        void websocket_MessageReceived(object sender, MessageReceivedEventArgs e)
        {
            Debug.WriteLine("Received " + e.Message);

            if (Changed != null)
            {
                Changed(sender, e);
            }
        }

        void _socket_DataReceived(object sender, WebSocket4Net.DataReceivedEventArgs e)
        {
            //Debug.WriteLine("Received " + e.Data.ToString());
        }

        void websocket_Closed(object sender, EventArgs e)
        {

        }

        void websocket_Error(object sender, SuperSocket.ClientEngine.ErrorEventArgs e)
        {
            Close();
        }

        void websocket_Opened(object sender, EventArgs e)
        {
            Debug.WriteLine("[Socket] Socket opened");

            if (_socket == null)
                return;

        }
        #endregion
    }
}
