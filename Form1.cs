using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WebSocket4Net;

namespace TradingView_Chat_Bot
{
    public partial class Form1 : Form
    {
        private LoginStatus loginStatus = LoginStatus.NOT_LOGGED_IN;

        // socket
        private SocketHelper socket_TradingView;

        public Form1()
        {
            InitializeComponent();

            Load += Form1_Load;
        }

        void Form1_Load(object sender, EventArgs e)
        {
            // Socket
            socket_TradingView = new SocketHelper();
            socket_TradingView.Changed += socket_TradingView_Changed;
            socket_TradingView.InitializeTradingViewWebSocket(); // todo: more error checking if this fails
        }

        async void socket_TradingView_Changed(object sender, MessageReceivedEventArgs e)
        {
            try
            {
                TradingView_Chat_Bot.Json.Json_TradingViewChat.Json_TradingViewChatRootObject classObj = 
                    JsonConvert.DeserializeObject<TradingView_Chat_Bot.Json.Json_TradingViewChat.Json_TradingViewChatRootObject>(e.Message);

                if (loginStatus == LoginStatus.LOGGED_IN)
                {
                    string posterUsername = classObj.text.content.data.username;
                    string posterRoom = classObj.text.content.data.room;
                    string posterText = classObj.text.content.data.text;

                    if (posterUsername != "UncleiSheep" && posterRoom == "bitcoin")
                    {
                        if (posterText.StartsWith("!"))
                        {
                            string[] commands = posterText.Split(' ');

                            switch (commands[0]) {
                                case "!help":
                                    {
                                        await HttpHelper.TradingViewChat("I'm a noob bot, I do not have any functionality yet :D", "ATYOURCOMMAND, IAMASHEEP",
                                            null, null);
                                        break;
                                    }
                                case "!stamp":
                                    {
                                        await HttpHelper.TradingViewChat("STAMPED!", "ATYOURCOMMAND, IAMASHEEP",
                                            null, null);
                                        break;
                                    }
                            }
                        }
                        else
                        {
                            await HttpHelper.TradingViewChat("Echo: " + posterText, posterUsername,
                                null, null);
                        }
                    }
                }
            }
            catch { }
        }

        private async void button_login_Click(object sender, EventArgs e)
        {
            label_status.Text = "Connecting....";

            if (loginStatus == LoginStatus.NOT_LOGGED_IN)
            {
                string ret = await HttpHelper.TradingViewLogin(textBox_user.Text, textBox_pass.Text);

                string retResult = null;
                if (ret != null)
                {
                    if (ret.Contains("Invalid username or password"))
                    {
                        retResult = "Invalid username or password";
                    }
                    else
                    {
                        retResult = "Login successfully. You may start chatting now.";

                        textBox_user.Enabled = false;
                        textBox_pass.Enabled = false;

                        button_login.Text = "Logoff";
                        loginStatus = LoginStatus.LOGGED_IN;
                    }
                }
                else
                {
                    retResult = "Error connecting to TV";
                }
                label_status.Text = retResult;
            }
            else if (loginStatus == LoginStatus.LOGGED_IN)
            {
                textBox_user.Enabled = true;
                textBox_pass.Enabled = true;

                button_login.Text = "Not Logged in";
                loginStatus = LoginStatus.NOT_LOGGED_IN;

                // clear cookies
                HttpHelper.UserLoginCookie = null;
            }
        }

        /// <summary>
        /// Post conversation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void button1_Click(object sender, EventArgs e)
        {
            if (loginStatus == LoginStatus.NOT_LOGGED_IN)
            {
                MessageBox.Show("You are not logged in :(", "Error");
                return;
            }

            button1.Enabled = false;

            string[] lines = textBox_lyrics.Text.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None);
            string symbol = textBox_Symbol.Text;
            short chatdelay = 500;
            Int16.TryParse(textBox_delay.Text, out chatdelay);

 
                // run in other thread
                await Task.Run(async () =>
                {
                    for (int i = 0; i < lines.Length; i++)
                    {
                        await HttpHelper.TradingViewChat(lines[i], symbol,
                            null, null);//"https://www.tradingview.com/x/QxoU5bTT/s/", "https://www.tradingview.com/x/QxoU5bTT/");

                        await Task.Delay(chatdelay);
                    }
                });

            button1.Enabled = true;
        }

        private void button_bitbotSocket_Click(object sender, EventArgs e)
        {

        }

        void BitbotSocketHelper_Changed(string msg)
        {
        }
    }
}
