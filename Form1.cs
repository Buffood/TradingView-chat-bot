using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
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
            textBox_reg_usernamePool.Text = Constants.UsernamePool;

            // Socket
            socket_TradingView = new SocketHelper();
            socket_TradingView.Changed += socket_TradingView_Changed;
            socket_TradingView.InitializeTradingViewWebSocket(); // todo: more error checking if this fails

            BitbotSocketHelper.Changed += BitbotSocketHelper_Changed;
        }

        async void socket_TradingView_Changed(object sender, MessageReceivedEventArgs e)
        {
            try
            {
                TradingView_Chat_Bot.Json.Json_TradingViewChat.Json_TradingViewChatRootObject classObj =
                    JsonConvert.DeserializeObject<TradingView_Chat_Bot.Json.Json_TradingViewChat.Json_TradingViewChatRootObject>(e.Message);

                string posterUsername = classObj.text.content.data.username;
                string posterRoom = classObj.text.content.data.room;
                string posterText = classObj.text.content.data.text;

                if (posterRoom != "bitcoin")
                    return;

                // Output
                Action updateItems = () => listBox_chats.Items.Add(string.Format("{0}: {1}", posterUsername, posterText));
                listBox_chats.BeginInvoke(updateItems);

                // Commands processing
                if (loginStatus == LoginStatus.LOGGED_IN && posterUsername != "UncleiSheep")
                {
                    if (posterText.StartsWith("!"))
                    {
                        string[] commands = posterText.Split(' ');

                        switch (commands[0])
                        {
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
            catch { }
        }

        #region Login
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
        #endregion

        #region chat
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
        #endregion

        #region Bitbot
        private void button_bitbotSocket_Click(object sender, EventArgs e)
        {
            /*button_bitbotSocket.Enabled = false;

            if (!BitbotSocketHelper.IsConnected())
            {
                BitbotSocketHelper.StartClient();
                button_bitbotSocket.Text = "Disconnect";
            }
            else
            {
                BitbotSocketHelper.ShutdownSocket();
                button_bitbotSocket.Text = "Connect";
            }

            button_bitbotSocket.Enabled = true;*/

        }

        void BitbotSocketHelper_Changed(string msg)
        {
            /*try
            {
                JObject obj = JObject.Parse(msg);
                string type = (string)obj["type"];

                switch (type)
                {
                    case "ping":
                        {
                            JObject ret = new JObject();
                            ret.Add("type", "ping");

                            BitbotSocketHelper.Send(ret.ToString().Replace(Environment.NewLine, ""));
                            break;
                        }
                    default:
                        {
                            Action updateItems = () => listView_chats.Items.Add(msg);
                            listView_chats.BeginInvoke(updateItems);

                            break;
                        }
                }
            }
            catch (Exception exp)
            {
                Debug.WriteLine(exp.ToString());
                Debug.Write(msg);
            }*/
        }
        #endregion

        #region AccountGenerator
        private async void button_reg_generate_Click(object sender, EventArgs e)
        {
            bool useUsernamePool = checkBox1.Checked == true;

            string usernameStart = textBox_reg_usernameStart.Text;
            string[] usernamesPool = textBox_reg_usernamePool.Text.Split(' ');
            Random r = new Random();

            string password = textBox_reg_pass.Text;
            string emailStart = textBox_reg_emailStart.Text;
            string emailDomain = textBox_reg_emailDomain.Text;
            int numberofAccounts = (int)numericUpDown_reg_num.Value;

            if (numberofAccounts > 0 && password.Length >= 6 && emailDomain.Contains('@'))
            {
                button_reg_generate.Enabled = false;

                // run in other thread
                await Task.Run(async () =>
                {
                    int successfulRegistration = 0;

                    using (StreamWriter file = new StreamWriter("SavedAccounts.txt", true))
                    {
                        for (int i = 0; i < numberofAccounts; i++)
                        {
                            string registeringUsername;
                            string registeringEmail;
                            if (!useUsernamePool)
                            {
                                registeringUsername = usernameStart + i;
                                registeringEmail = emailStart + i + emailDomain;
                            }
                            else
                            {
                                registeringUsername =
                                    usernamesPool[r.Next(usernamesPool.Length)] +
                                    usernamesPool[r.Next(usernamesPool.Length)] +
                                    usernamesPool[r.Next(usernamesPool.Length)];
                                registeringEmail =
                                    usernamesPool[r.Next(usernamesPool.Length)] +
                                    usernamesPool[r.Next(usernamesPool.Length)] +
                                    usernamesPool[r.Next(usernamesPool.Length)] + emailDomain;
                            }

                            bool regSuccessful = false;
                            try
                            {
                                string ret = await HttpHelper.TradingViewSignup(registeringUsername, password,
                                    registeringEmail);

                                JObject obj = JObject.Parse(ret);
                                string errors = (string)obj["errors"];
                                if (errors == string.Empty)
                                {
                                    regSuccessful = true;
                                    successfulRegistration++;
                                }
                                // {"message":"Activation key has been sent to 3un21luckyrat@mailinator.com","errors":""}
                            }
                            catch { }

                            Action updateItems = () => label_reg_number.Text = i.ToString();
                            label_reg_number.BeginInvoke(updateItems);

                            if (regSuccessful)
                            {
                                file.WriteLine(string.Format("{0} {1}", registeringUsername, registeringEmail));
                            }
                        }
                        file.Close();
                    }
                });

                button_reg_generate.Enabled = true;
            }
            else
            {
                MessageBox.Show("Invalid input", "Error");
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            bool isChecked = checkBox1.Checked == true;

            if (isChecked)
            {
                textBox_reg_usernamePool.Enabled = true;

                textBox_reg_usernameStart.Enabled = false;
            }
            else
            {
                textBox_reg_usernamePool.Enabled = false;

                textBox_reg_usernameStart.Enabled = true;
            }
        }
        #endregion

        #region TorProxy
        private void checkBox_proxy_CheckedChanged(object sender, EventArgs e)
        {
            string address = textBox_proxy_addr.Text;
            int port = 9050;
            int.TryParse(textBox_proxy_port.Text, out port);

            if (IPAddressHelper.IsIPv4(address) && port > 1 && port < 65500)
            {
                HttpHelper.ProxyIP = address;
                HttpHelper.ProxyPort = port;

                HttpHelper.EnableProxyRouting = checkBox_proxy.Checked == true;
            }
        }
        #endregion
    }
}
