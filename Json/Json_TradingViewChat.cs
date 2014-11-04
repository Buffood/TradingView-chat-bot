using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingView_Chat_Bot.Json
{
    public class Json_TradingViewChat
    {
        public class Meta
        {
            public string url { get; set; }
            public string text { get; set; }
            public string interval { get; set; }
            public string type { get; set; }
            public string preview_url { get; set; }
        }

        public class Data
        {
            public string username { get; set; }
            public string text { get; set; }
            public string symbol { get; set; }
            public bool is_moderator { get; set; }
            public Meta meta { get; set; }
            public string id { get; set; }
            public int user_id { get; set; }
            public string room { get; set; }
            public string pro_plan { get; set; }
            public string user_pic { get; set; }
            public string time { get; set; }
            public string type { get; set; }
            public bool is_pro { get; set; }
        }

        public class Content
        {
            public string action { get; set; }
            public Data data { get; set; }
        }

        public class Text
        {
            public Content content { get; set; }
            public string channel { get; set; }
        }

        public class Json_TradingViewChatRootObject
        {
            public int id { get; set; }
            public string channel { get; set; }
            public Text text { get; set; }
        }
    }
}
