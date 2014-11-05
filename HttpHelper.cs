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

            req.DefaultRequestHeaders.Add("DNT", "1");
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

        public static async Task<string> TradingViewSignup(string username, string password, string email)
        {
            const string FetchURL = "https://www.tradingview.com/accounts/signup/";

            var handler = new HttpClientHandler();
            handler.UseCookies = true;
            AddCookie(handler);

            var req = new HttpClient(handler);

            // Headers
            AddBasicTradingViewHeader(req);

            // Post
            string postContent = string.Format("signup_trial=0&email={2}&username={0}&password={1}",
                WebUtility.UrlEncode(username), WebUtility.UrlEncode(password), WebUtility.UrlEncode(email));
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

        public static async Task<bool> TradingViewCommentVote(int commentid, bool agree)
        {
            if (UserLoginCookie == null)
            {
                return false;
            }

            const string FetchURL = "https://www.tradingview.com/vote-for-comment/";

            var handler = new HttpClientHandler();
            handler.UseCookies = true;
            AddCookie(handler);

            var req = new HttpClient(handler);

            // Headers
            AddBasicTradingViewHeader(req);

            // Post
            string postContent = string.Format("id={0}&vote={1}", commentid, agree ? "agree":"disagree");

            HttpContent hcontent = new StringContent(postContent);
            hcontent.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");

            try
            {
                HttpResponseMessage data = await req.PostAsync(FetchURL, hcontent);
                HttpContent content = data.Content;
                data.EnsureSuccessStatusCode();

                string ReturnData = await content.ReadAsStringAsync();
                Debug.WriteLine(ReturnData);

                // {"status":"success","rating":-8}
                return true;
            }
            catch (Exception eex)
            {
                Debug.WriteLine(eex.ToString());
            }
            return false;
        }

        public static async Task<bool> TradingView_AddToFavourites(int commentid)
        {
            if (UserLoginCookie == null)
            {
                return false;
            }

            const string FetchURL = "https://www.tradingview.com/add-to-favorites/";

            var handler = new HttpClientHandler();
            handler.UseCookies = true;
            AddCookie(handler);

            var req = new HttpClient(handler);

            // Headers
            AddBasicTradingViewHeader(req);

            // Post
            string postContent = string.Format("id={0}", commentid);

            HttpContent hcontent = new StringContent(postContent);
            hcontent.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");

            try
            {
                HttpResponseMessage data = await req.PostAsync(FetchURL, hcontent);
                HttpContent content = data.Content;
                data.EnsureSuccessStatusCode();

                string ReturnData = await content.ReadAsStringAsync();
                Debug.WriteLine(ReturnData);

                //{"followers":1,"error":""}
                return true;
            }
            catch (Exception eex)
            {
                Debug.WriteLine(eex.ToString());
            }
            return false;
        }

        public static async Task<bool> TradingView_AddToFavourites(int chartid)
        {
            if (UserLoginCookie == null)
            {
                return false;
            }

            const string FetchURL = "https://www.tradingview.com/add-to-favorites/";

            var handler = new HttpClientHandler();
            handler.UseCookies = true;
            AddCookie(handler);

            var req = new HttpClient(handler);

            // Headers
            AddBasicTradingViewHeader(req);

            // Post
            string postContent = string.Format("id={0}", chartid);

            HttpContent hcontent = new StringContent(postContent);
            hcontent.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");

            try
            {
                HttpResponseMessage data = await req.PostAsync(FetchURL, hcontent);
                HttpContent content = data.Content;
                data.EnsureSuccessStatusCode();

                string ReturnData = await content.ReadAsStringAsync();
                Debug.WriteLine(ReturnData);

                //{"followers":1,"error":""}
                return true;
            }
            catch (Exception eex)
            {
                Debug.WriteLine(eex.ToString());
            }
            return false;
        }

        public static async Task<bool> TradingView_VoteForChart(int chartid, bool vote)
        {
            if (UserLoginCookie == null)
            {
                return false;
            }

            const string FetchURL = "https://www.tradingview.com/vote-for-chart/";

            var handler = new HttpClientHandler();
            handler.UseCookies = true;
            AddCookie(handler);

            var req = new HttpClient(handler);

            // Headers
            AddBasicTradingViewHeader(req);

            // Post
            string postContent = string.Format("id={0}&vote={1}", chartid, vote? "true":"false");

            HttpContent hcontent = new StringContent(postContent);
            hcontent.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");

            try
            {
                HttpResponseMessage data = await req.PostAsync(FetchURL, hcontent);
                HttpContent content = data.Content;
                data.EnsureSuccessStatusCode();

                string ReturnData = await content.ReadAsStringAsync();
                Debug.WriteLine(ReturnData);

                //{"like_score":1,"dislike_score":0,"result_score":1,"error":""}
                return true;
            }
            catch (Exception eex)
            {
                Debug.WriteLine(eex.ToString());
            }
            return false;
        }

        public static async Task<bool> TradingView_VoteForChart(int chartid, string comment, int chartparent)
        {
            if (UserLoginCookie == null)
            {
                return false;
            }

            const string FetchURL = "https://www.tradingview.com/postcomment/";

            var handler = new HttpClientHandler();
            handler.UseCookies = true;
            AddCookie(handler);

            var req = new HttpClient(handler);

            // Headers
            AddBasicTradingViewHeader(req);

            // Post
            string csrfmiddlewaretoken = "";
            Cookie cookie = handler.CookieContainer.GetCookies(new Uri("https://www.tradingview.com/"))["csrftoken"];
            if (cookie != null)
            {
                csrfmiddlewaretoken = cookie.Value;
            }
            else
                return false;

            string postContent = string.Format("csrfmiddlewaretoken={0}&object_id={1}&comment={2}&parent={3}",
                csrfmiddlewaretoken, chartid, WebUtility.UrlEncode(comment), chartparent);

            HttpContent hcontent = new StringContent(postContent);
            hcontent.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");

            try
            {
                HttpResponseMessage data = await req.PostAsync(FetchURL, hcontent);
                HttpContent content = data.Content;
                data.EnsureSuccessStatusCode();

                string ReturnData = await content.ReadAsStringAsync();
                Debug.WriteLine(ReturnData);

                //{"like_score":1,"dislike_score":0,"result_score":1,"error":""}
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
