using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace TradingView_Chat_Bot
{
    public class HttpHelper
    {
        public static CookieContainer UserLoginCookie = null;

        private static void AddCookie(HttpClientHandler handler)
        {
            if (UserLoginCookie != null)
                handler.CookieContainer = UserLoginCookie;
            else
            {
                Uri InstaURI = new Uri("https://www.tradingview.com/");

                handler.CookieContainer = new System.Net.CookieContainer();
                UserLoginCookie = handler.CookieContainer;
            }
        }

        private static void AddBasicTradingViewHeader(HttpClient req)
        {
            req.DefaultRequestHeaders.Accept.ParseAdd("application/json, application/xml, text/json, text/x-json, text/javascript, text/xml");
            req.DefaultRequestHeaders.Referrer = new Uri("https://www.tradingview.com/");
            req.DefaultRequestHeaders.Host = "www.tradingview.com";
            req.DefaultRequestHeaders.Add("Origin", "https://www.tradingview.com");
            req.DefaultRequestHeaders.Add("User-Agent", Constants.UserAgent);
            req.DefaultRequestHeaders.Accept.ParseAdd("application/json, text/javascript, */*; q=0.01");
            req.DefaultRequestHeaders.Add("X-Requested-With", "XMLHttpRequest");
            req.DefaultRequestHeaders.Add("sessionid", "null");
            req.DefaultRequestHeaders.Add("X-CSRFToken", "null");
            req.DefaultRequestHeaders.AcceptEncoding.ParseAdd("gzip,deflate");
            req.DefaultRequestHeaders.AcceptLanguage.ParseAdd("en-US,en;q=0.8");
        }

        public static async Task<string> TradingViewLogin(string username, string password)
        {
            const string FetchURL = "https://www.tradingview.com/accounts/signin/";

            var handler = new HttpClientHandler();
            handler.UseCookies = true;
            AddCookie(handler);

            var req = new HttpClient(handler);

            // Headers
            AddBasicTradingViewHeader(req);

            // Post
            string postContent = string.Format("username={0}&password={1}&remember=on", username, password);
            HttpContent hcontent = new StringContent(postContent);
            hcontent.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");

            try
            {
                HttpResponseMessage data = await req.PostAsync(FetchURL, hcontent);
                HttpContent content = data.Content;
                data.EnsureSuccessStatusCode();

                string ReturnData = await content.ReadAsStringAsync();
                //Debug.WriteLine(ReturnData);

                return ReturnData;
            }
            catch (Exception eex)
            {
                Debug.WriteLine(eex.ToString());
            }
            return null;
        }

        public static async Task<bool> TradingViewChat(string postText, string symbol, string imgPreviewUri = null, string imgUri = null)
        {
            if (UserLoginCookie == null)
            {
                return false;
            }

            const string FetchURL = "https://www.tradingview.com/conversation-post/";

            var handler = new HttpClientHandler();
            handler.UseCookies = true;
            AddCookie(handler);

            var req = new HttpClient(handler);

            // Headers
            AddBasicTradingViewHeader(req);

            // Post
            string postContent;

            if (imgPreviewUri == null || imgUri == null)
            {
                postContent = string.Format("text={0}&room=bitcoin&symbol={1}&meta=%7B%22text%22%3A%22%22%2C%22interval%22%3A%22%22%7D&is_private=", 
                    WebUtility.UrlEncode(postText), WebUtility.UrlEncode(symbol));
            }
            else
            {
                postContent = string.Format("text={0}&room=bitcoin&symbol={1}&meta=%7B%22text%22%3A%22Chart+snapshot%22%2C%22url%22%3A%22{2}%22%2C%22preview_url%22%3A%22{3}%22%2C%22type%22%3A%22snapshot%22%2C%22interval%22%3A%22%22%7D&is_private=",
                    WebUtility.UrlEncode(postText), WebUtility.UrlEncode(symbol), WebUtility.UrlEncode(imgPreviewUri), WebUtility.UrlEncode(imgUri));
            }
                
                HttpContent hcontent = new StringContent(postContent);
            hcontent.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");

            try
            {
                HttpResponseMessage data = await req.PostAsync(FetchURL, hcontent);
                HttpContent content = data.Content;
                data.EnsureSuccessStatusCode();

                string ReturnData = await content.ReadAsStringAsync();
                Debug.WriteLine(ReturnData);

                return true;
            }
            catch (Exception eex)
            {
                Debug.WriteLine(eex.ToString());
            }
            return false;
        }
    }
}
