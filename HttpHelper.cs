using Newtonsoft.Json.Linq;
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
        public static bool EnableProxyRouting = false;
        public static string ProxyIP = "127.0.0.1";
        public static int ProxyPort = 9050;

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
            if (EnableProxyRouting)
            {
                handler.Proxy = new WebProxy(ProxyIP, ProxyPort);
                handler.UseProxy = true;
            }

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
            if (EnableProxyRouting)
            {
                handler.Proxy = new WebProxy(ProxyIP, ProxyPort);
                handler.UseProxy = true;
            }

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

        #region chat
        public static async Task<bool> TradingViewChat(string postText, string symbol, string imgPreviewUri = null, string imgUri = null)
        {
            if (UserLoginCookie == null)
            {
                return false;
            }

            const string FetchURL = "https://www.tradingview.com/conversation-post/";

            var handler = new HttpClientHandler();
            if (EnableProxyRouting)
            {
                handler.Proxy = new WebProxy(ProxyIP, ProxyPort);
                handler.UseProxy = true;
            }

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

        public static async Task<bool> TradingCreateChatRoom(string title, string desc)
        {
            if (UserLoginCookie == null)
            {
                return false;
            }

            const string FetchURL = "https://www.tradingview.com/chats/public/create/";

            var handler = new HttpClientHandler();
            if (EnableProxyRouting)
            {
                handler.Proxy = new WebProxy(ProxyIP, ProxyPort);
                handler.UseProxy = true;
            }

            handler.UseCookies = true;
            AddCookie(handler);

            var req = new HttpClient(handler);

            // Headers
            AddBasicTradingViewHeader(req);

            // Post
            string postContent = string.Format("title={0}&desc={1}&room_id=",
                    WebUtility.UrlEncode(title), WebUtility.UrlEncode(desc));


            HttpContent hcontent = new StringContent(postContent);
            hcontent.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");

            try
            {
                HttpResponseMessage data = await req.PostAsync(FetchURL, hcontent);
                HttpContent content = data.Content;
                data.EnsureSuccessStatusCode();

                string ReturnData = await content.ReadAsStringAsync();
                Debug.WriteLine(ReturnData);

                // {"error":"Room purpose is required."}
                return true;
            }
            catch (Exception eex)
            {
                Debug.WriteLine(eex.ToString());
            }
            return false;
        }

        public static async Task<JArray> TradingCreate_GetChatRoomList()
        {
            const string FetchURL = "https://www.tradingview.com/chats/public/get/";

            var handler = new HttpClientHandler();
            if (EnableProxyRouting)
            {
                handler.Proxy = new WebProxy(ProxyIP, ProxyPort);
                handler.UseProxy = true;
            }

            handler.UseCookies = true;
            AddCookie(handler);

            var req = new HttpClient(handler);

            // Headers
            AddBasicTradingViewHeader(req);

            try
            {
                HttpResponseMessage data = await req.GetAsync(FetchURL);
                HttpContent content = data.Content;
                data.EnsureSuccessStatusCode();

                string ReturnData = await content.ReadAsStringAsync();
                Debug.WriteLine(ReturnData);

                return JArray.Parse(ReturnData);
                //[{"description":"Discuss all currency pairs","shortTitle":"Forex","title":"Forex","users_online":3027,"pinned":true,"room_id":"general"},{"description":"Debate public stocks and indexes","shortTitle":"Stock","title":"Stocks & Indexes","users_online":270,"pinned":true,"room_id":"stock"},{"description":"Talk about all cryptocurrencies","shortTitle":"Bitcoin","title":"Bitcoin","users_online":567,"pinned":true,"room_id":"bitcoin"},{"user_id":127134,"description":"12","title":"231231231","users_online":0,"created":1415176710,"room_id":"fteer8YJTlmNicte","id":188},{"user_id":24867,"description":"talk about how high it is going to go","title":"6E","users_online":0,"created":1402360498,"room_id":"KxOgFpBDXA0ZslNG","id":46},{"user_id":94287,"description":"796Futures","title":"796Futures","users_online":0,"created":1411392106,"room_id":"HazwTQLSaXF3s2N6","id":142},{"user_id":94287,"description":"796\u671f\u8d27","title":"796\u671f\u8d27","users_online":1,"created":1414732826,"room_id":"SlUPS7oQhSwg42za","id":175},{"user_id":121953,"description":"DKCentre.com","title":"Acox Fx","users_online":0,"created":1413696401,"room_id":"3Vwoi3CEBBx8lvxp","id":166},{"user_id":116087,"description":"Daily options trades. Share your  equities or Index option positions.","title":"Actionable Options Trades","users_online":0,"created":1412431278,"room_id":"p4H0kvthqMWfycJR","id":145},{"user_id":70121,"description":"advanced options strategies","title":"advanced options strategies","users_online":3,"created":1402765714,"room_id":"0SHMaBXg6P8t0GFP","id":51},{"user_id":105438,"description":"Discussing about world biggest IPO: Alibaba","title":"ALIBABA IPO","users_online":2,"created":1408448693,"room_id":"m8kOMEvUDt3KdwPL","id":107},{"user_id":104763,"description":"Guys, what is wrong with all those crappy charts that refresh like 5 minutes late, even 10 sometime.\n\nOnly stocks with > 5 millions volume seems to work 'Ok' ( could be better ).\n\nAt least it's just a Pro trial..","title":"Almost 5\/6 charts doesn't refresh","users_online":1,"created":1413833300,"room_id":"ewLd3GF3DgX604mt","id":169},{"user_id":30583,"description":"Discuss trading of xxx\/BTC pairs","title":"Altcoin","users_online":0,"created":1410767895,"room_id":"eY376L8Bt8Uf4zpx","id":136},{"user_id":85962,"description":"To share ideas and learn how to use Andrews' Pitchfork Tool","title":"Andrews' Pitchfork Traders","users_online":3,"created":1398977314,"room_id":"Hek0gmnZRpR5tClu","id":29},{"user_id":51972,"description":"Discussing astrological trading knowledge","title":"Astrological Trading","users_online":3,"created":1404800871,"room_id":"Ga75HipiShF7utLE","id":71},{"user_id":118446,"description":"to get rich","title":"AUD\/JPY","users_online":0,"created":1415062602,"room_id":"begPwEIEf7kqUzh1","id":182},{"user_id":49739,"description":"knowlrdge share","title":"Awais","users_online":0,"created":1415244201,"room_id":"CxbnFXItByhCeGYG","id":190},{"user_id":114357,"description":"Make money","title":"Beginner tips are welcome !! please thanks;)","users_online":0,"created":1411239249,"room_id":"TlEc17GGQPxZ4crE","id":139},{"user_id":45354,"description":"Binary Options discussion and signals","title":"Binary Options","users_online":0,"created":1414832186,"room_id":"49znDlChOIeSnaUe","id":177},{"user_id":27216,"description":"A room for binary option traders to share their charts and trading ideas.","title":"Binary Option Trading","users_online":1,"created":1409605044,"room_id":"farV5KTpCxT7xxaf","id":128},{"user_id":50127,"description":"Posting and sharing ideas on small biotech plays focused mainly on Simple Technical Analysis and market timing for those highly volatile \/ hard to play stocks","title":"Biotechs - Technical Analysis \/ Price swings","users_online":0,"created":1405822152,"room_id":"2mPMDXYQnOSMMK7b","id":80},{"user_id":112300,"description":"Technical Analysis without all the noise","title":"Bitcoin Pro Charts","users_online":0,"created":1411245368,"room_id":"lEoNliCeHJ1tQwKd","id":140},{"user_id":33766,"description":"discussion over the exchange","title":"BitVC - BTC and LTC levergaed trading","users_online":0,"created":1408865085,"room_id":"KCf1iDmSTMs0rdYN","id":114},{"user_id":84465,"description":"bot tester","title":"botrampage","users_online":0,"created":1415113873,"room_id":"JKsG7aX1EGudKvR3","id":183},{"user_id":118184,"description":"ISKAO :) CAJUE","title":"BRSA","users_online":0,"created":1412586844,"room_id":"Bzg0ASdBY7IEykhG","id":147},{"user_id":94215,"description":"Next generation of value preservation","title":"BTC - GOLD","users_online":0,"created":1414030830,"room_id":"zcWLHJnYl4nVPeeA","id":170},{"user_id":48921,"description":"Discuss Bitcoin trading on Bitfinex (BFX)","title":"BTCUSD - Bitfinex","users_online":1,"created":1397725865,"room_id":"521yt8koa6o6XeUd","id":1},{"user_id":22229,"description":"Share","title":"Charts","users_online":0,"created":1414797397,"room_id":"S9BxnSuVq2TEde8S","id":176},{"user_id":1233,"description":"cm_williams_vix_fix","title":"cm_williams_vix_fix","users_online":0,"created":1412788637,"room_id":"48zr4hCQi3qvmBPS","id":152},{"user_id":84425,"description":"Discussions about our favorite commodity","title":"Coffee","users_online":0,"created":1415126626,"room_id":"X8F2aX3V1hIp7gwi","id":184},{"user_id":61110,"description":"FAQs, suggestions, new feature guidance, general help","title":"Community Powered Technical Help & Discussion","users_online":16,"created":1403185752,"room_id":"DjPnZNEzTJELAFiR","id":5},{"user_id":127672,"description":"discuss trading opportunities for crude oil","title":"Crude oil","users_online":0,"created":1415248937,"room_id":"WI2euZQLsniSvfMy","id":191},{"user_id":10703,"description":"TA of price the only arbitrar","title":"DanV Charting The Waves","users_online":2,"created":1403185471,"room_id":"E4bnOJSWcO1zDjBG","id":24},{"user_id":83450,"description":"Chat for anyone who is scalping, short term, day trading DAX Index. Share your trades, your views, your lessons.Let's trade DAX together!","title":"DAX - Daytrading","users_online":4,"created":1407730653,"room_id":"ILzeCXbKcxKjUVVm","id":103},{"user_id":114670,"description":"dkcentre","title":"dkcentre","users_online":2,"created":1413797147,"room_id":"Q0MpwUf5Sx8uKZl1","id":167},{"user_id":61478,"description":"charting","title":"drawing tools","users_online":0,"created":1415014288,"room_id":"QU5mQjDrQBTUbpYv","id":181},{"user_id":108771,"description":"Trading Binary Options!","title":"EasyBinarySignals.com","users_online":0,"created":1409830350,"room_id":"fiUYMtnDR1SCUzrI","id":130},{"user_id":21669,"description":"Charts and ideas based on the Elliott Wave principle","title":"Elliott Wave Charting","users_online":6,"created":1406149215,"room_id":"6N3ooQ8L1g9B535D","id":88},{"user_id":43658,"description":"Predictive analysis and forecasting announcement using proprietary model - Advanced traders, please - David Alcindor","title":"e-MINI S&P 500","users_online":2,"created":1408986071,"room_id":"Fu3tMkKy660WGNhU","id":117},{"user_id":120695,"description":"Buy","title":"EUR\/usd","users_online":0,"created":1414548991,"room_id":"TSTYXo7cYXscUDOz","id":174},{"user_id":113290,"description":"Sadece EUR\/ USD","title":"EUR\/ USD","users_online":0,"created":1412961004,"room_id":"l3rl0nISOFvH3KIw","id":155},{"user_id":107387,"description":"To discuss and answer Exante related questions and features. To receive feedback and suggestions.","title":"Exante Support","users_online":1,"created":1410143119,"room_id":"hZyV0AqELQUv3UVA","id":131},{"user_id":43658,"description":"The purpose of this room is to help the trader regain a losing trade. This will be done on a case-by-case basis only. Proprietary tools will be used but not shared. Only directions will be provided - Feel free to submit request. Please, indicate the following: Entry price\/date, INITIAL and current Stop-Loss; Initial and current Take-Profit - Original trading plan and current plan. You are welcome to add the total loss and expected re-gain (negative, positive, full, partial, in units or specific dollar amount) - 4xForecaster","title":"Forex Re-Pair Clinic","users_online":0,"created":1411040465,"room_id":"zjESqSJ1S9LVZNrB","id":137},{"user_id":126033,"description":"sharing ideas","title":"Forex USDJPY","users_online":0,"created":1414989270,"room_id":"ahzqQy83iJgYZnD5","id":178},{"user_id":102877,"description":"This room is for full time FX traders. It is not the right group for demo traders or ppl that want to lecture others on how right they are and how unware others might be. We share ideas and discuss daily trades -  that it.","title":"Full Time FX trading","users_online":0,"created":1411126919,"room_id":"j9f0kO0blJc05F65","id":138},{"user_id":70038,"description":"Fulltime traders only","title":"Fulltime traders","users_online":6,"created":1409138663,"room_id":"gRQVkX37ku4n6fxl","id":120},{"user_id":119969,"description":"This is made for: Predictions, Analysis, Trends, Advice, Forecasts and Signals of GBP:USD currency pair.","title":"GBP:USD Traders ONLY","users_online":0,"created":1413122548,"room_id":"HUXLqokb8hvgiv8M","id":157},{"user_id":95330,"description":"share ideas of which direction its heading","title":"GER30 DAX","users_online":1,"created":1404784503,"room_id":"HQSb9LtO5bZY3Zha","id":70},{"user_id":53844,"description":"Gold","title":"Gold","users_online":1,"created":1415172507,"room_id":"3KRjmWdHiKoOKzzG","id":187},{"user_id":19223,"description":"DISCUSSIONS ESCLUSIVELY ON GOLD MINERS AND GOLD","title":"GOLD AND  MINERS 3XETFs","users_online":1,"created":1414322466,"room_id":"or56EhS7ZJAmn6vm","id":173},{"user_id":65671,"description":"How to trade Gold","title":"GOLD discussion","users_online":133,"created":1398404877,"room_id":"spKNkcfKuFVnvOKV","id":18},{"user_id":103200,"description":"so brasileiro doido kkk","title":"GRUPO BRASIL","users_online":0,"created":1406706021,"room_id":"e4VCmafnLm22iigm","id":93},{"user_id":98939,"description":"Discussing Hong Kong Market","title":"Hang Seng Index Discussion","users_online":0,"created":1404861443,"room_id":"gx0PT4hmWVhyA1Rg","id":75},{"user_id":89179,"description":"This room is for Harmonic pattern traders","title":"Harmonic Traders","users_online":1,"created":1401074055,"room_id":"oR7zBH4zt6twFZ8x","id":38},{"user_id":12644,"description":"Post trading setups based on highs lows and pure price action and candles formations no fibonachi no indicators  nice and clean plain charts","title":"Highs lows false breakes and breakes  (Price trading)","users_online":0,"created":1414040369,"room_id":"q5EhE6O8OnDQv09P","id":171},{"user_id":191,"description":"To highlight stocks at Key Hidden Levels, and trades setting up as they approach or move away from Key Hidden Level.","title":"Key Hidden Levels","users_online":7,"created":1409044265,"room_id":"c8BzrhGRvXxGXWnJ","id":118},{"user_id":57840,"description":"Talk about the BTC kraken exchange","title":"Kraken","users_online":1,"created":1401156623,"room_id":"xjF65TB2VkQbG1Bi","id":39},{"user_id":94287,"description":"Litecoin","title":"Litecoin","users_online":0,"created":1403782229,"room_id":"bKC5D4C8z2Pr0VN8","id":61},{"user_id":32675,"description":"live trading, google hangout, youtube videos","title":"LIVE TRADING & HANGOUTS","users_online":33,"created":1398538289,"room_id":"FENOYMVZiiLaxZq5","id":20},{"user_id":5636,"description":"Discuss long term ideas for value investors, yield\/income seekers, or long term trades. Focus is on Weekly or Monthly charts. Some longer term option strategies may be discussed as well.","title":"Long Term, Yield, and Value Seekers","users_online":0,"created":1405690076,"room_id":"IinvK3UjCR5wmzHa","id":79},{"user_id":59054,"description":"discuss about all major pair","title":"Major Pair Only","users_online":0,"created":1413818927,"room_id":"9ac2cYLZL2AUAme1","id":168},{"user_id":94380,"description":"Trade together, Profit together","title":"Midasfx Live Trading Room","users_online":0,"created":1409159968,"room_id":"rZi7etYbAsb1G4p4","id":125},{"user_id":36520,"description":"Discuss anticipated B\/O or partnership & up coming ER for the week ahead.","title":"MNKD (MannKind)","users_online":0,"created":1407139233,"room_id":"ec2HD72PXRxxrzSn","id":96},{"user_id":52819,"description":"Tips to trade NAS100","title":"NAS100 Discussion","users_online":0,"created":1415179903,"room_id":"9rch3IeEjul88vTe","id":189},{"user_id":93769,"description":"ideas, thoughts, chat","title":"NATGAS","users_online":4,"created":1402497628,"room_id":"UajYkAfATwDxwyFp","id":49},{"user_id":60056,"description":"Any topic is allowed, but keep it civil.","title":"Off-Topic","users_online":0,"created":1406132354,"room_id":"bRLgqxmNlBhxvqTb","id":10},{"user_id":119252,"description":"Talk about where oil is heading. Predictions, news, strategies.","title":"OIL (WTI\/Brent)","users_online":2,"created":1412939221,"room_id":"ZaJvQy7PDvTR4irM","id":154},{"user_id":33766,"description":"discuss this exchange","title":"OkCoin (cryptoexchange)","users_online":0,"created":1408867442,"room_id":"ggxeOGkUI0zacqtt","id":116},{"user_id":81062,"description":"only the forex pair EUR\/USD is discussed.","title":"Only EUR\/USD","users_online":27,"created":1398677214,"room_id":"Sj9VbiDu7w7CAVnv","id":26},{"user_id":75820,"description":"talks about penny stocks that might go up","title":"penny stocks","users_online":0,"created":1414993045,"room_id":"pY4BV00oLItS5LVq","id":179},{"user_id":62121,"description":"The purpose of this room is to talk about penny stocks trading possibilities and benefit the high percentages.","title":"PennyStocks","users_online":7,"created":1402996875,"room_id":"TyuWQ41YMrnIl2Od","id":56},{"user_id":121889,"description":"Discussing the new companies that are helping against the Ebola outbreak","title":"Pharmaceuticals","users_online":0,"created":1413665923,"room_id":"wwheDZcsDYR2BqSS","id":163},{"user_id":16936,"description":"Have any interesting ideas for new indicators or chart overlays? \n\nIn this chat room you can work with a pine script programmer to get your indicator up and running.","title":"Pine Script Editor","users_online":17,"created":1397870173,"room_id":"BfmVowG1TZkKO235","id":9},{"user_id":43658,"description":"This space is carved out for predictive analyses, forecasts and signals shared with the public. All contents are for educational purpose. I cannot guarantee being able to reply to all requests or comments. Due your due diligence, always - Cheers, David Alcindor, Predictive Analysis & Forecasting, TV Mod.","title":"Predictive Analysis\/Forecasting","users_online":4,"created":1399727282,"room_id":"5eHLst6YxeVqGlaO","id":32},{"user_id":19637,"description":"Price Action Trading room is for traders who look at Open, High, Low & Close in relation to previous OHLC's and Support and Resistance. (No need for indicators or abstract theorem)","title":"Price Action Trading","users_online":73,"created":1397749456,"room_id":"uV37CvFoudfzZ9EN","id":6},{"user_id":106584,"description":"stock trading","title":"rahulmsk99@riskfreetrading","users_online":1,"created":1408624000,"room_id":"J2xXiWwQIpk12aUR","id":112},{"user_id":69316,"description":"consulting btc","title":"RISBTD","users_online":1,"created":1411253251,"room_id":"wPesbAIAcdJRYBg2","id":141},{"user_id":76024,"description":"The Rational Investor's Trading School","title":"RISBTD Lounge","users_online":7,"created":1414928156,"room_id":"p6LOWJrf8WwsK02G","id":124},{"user_id":123798,"description":"What will happen next week??","title":"S&P 500","users_online":0,"created":1414232451,"room_id":"IOs1VDd0UXZp8PRY","id":172},{"user_id":65671,"description":"Chat room to discuss S&P 500 trading","title":"S&P 500 discussion","users_online":73,"created":1398338330,"room_id":"HcZG96OdOqHqMqqh","id":19},{"user_id":16936,"description":"Discuss stock options, positions and strategies.","title":"Stock Options","users_online":16,"created":1398330217,"room_id":"QrHg9VOq9YECfKxd","id":21},{"user_id":125369,"description":"Instant trading","title":"STOCK_TRADING","users_online":1,"created":1414994584,"room_id":"lu5cc1derbeh8nB6","id":180},{"user_id":28985,"description":"To discuss anything related to market's supply and demand tradable levels","title":"Supply and Demand","users_online":2,"created":1404614557,"room_id":"gJ5G4tGbmZc5goY2","id":68},{"user_id":62896,"description":"predicting turning points in price by locating order imbalances on a price chart.","title":"Supply and Demand Trading","users_online":5,"created":1398718105,"room_id":"GLTODfGzJGbU1YVv","id":27},{"user_id":111014,"description":"What is the problem with this website today. It is not working good today.","title":"Support","users_online":1,"created":1410402387,"room_id":"ML8dsajpZDyjhdVV","id":133},{"user_id":44655,"description":"This room is dedicated to users willing to discuss mainly Forex Trading Setups. Also I will be happy to receive your feedback and suggestions about tradingview community and charts. I will dedicate one hour a day to this chat room, starting September 1st 2014.","title":"Technician Chart Art","users_online":5,"created":1408100509,"room_id":"uHTgFAuHayxdy4kb","id":104},{"user_id":61110,"description":"For testing purposes","title":"Test room","users_online":0,"created":1415169631,"room_id":"qHo9VLefohcj3DnI","id":186},{"user_id":86379,"description":"The purpose is to help the traders who wanna learn how to trade in the right way","title":"The Analysis 9 Talk","users_online":0,"created":1407551356,"room_id":"uAev56PTLnyKldG4","id":102},{"user_id":70121,"description":"Discuss Various Benefits of Pro and Pro Plus Plan Upgrades","title":"Trading View *Pro* Features","users_online":5,"created":1403428444,"room_id":"uivRsAJcrI1Aso4b","id":59},{"user_id":31447,"description":"compartir con todo el mundo mas que una idea","title":"Treding espa\u00f1ol","users_online":2,"created":1398263471,"room_id":"2CFD4YQeH8hMgXGE","id":16},{"user_id":25143,"description":"PROFIT","title":"USDJPY","users_online":1,"created":1413363488,"room_id":"TlvaWKfrAEVbSSrr","id":162},{"user_id":16936,"description":"Search and Discover positions that have the ability to move quickly. Using simple and complex volatility indicators + strategies we can all share in the fun.","title":"Volatility Indications","users_online":1,"created":1410464356,"room_id":"yTiZSUwniXxsfgQu","id":134},{"user_id":51972,"description":"WhaleClub","title":"WhaleClub","users_online":0,"created":1415161432,"room_id":"Ki9wug39zUw4DLeI","id":185},{"user_id":50932,"description":"WSTI (Windstream) is in the news as the #1 Energy Stock Pick.  What are your thoughts?","title":"WSTI","users_online":0,"created":1403902026,"room_id":"dscdCiljv7ZnZeS4","id":62}]
            }
            catch (Exception eex)
            {
                Debug.WriteLine(eex.ToString());
            }
            return null;
        }
        #endregion

        public static async Task<bool> TradingViewCommentVote(int commentid, bool agree)
        {
            if (UserLoginCookie == null)
            {
                return false;
            }

            const string FetchURL = "https://www.tradingview.com/vote-for-comment/";

            var handler = new HttpClientHandler();
            if (EnableProxyRouting)
            {
                handler.Proxy = new WebProxy(ProxyIP, ProxyPort);
                handler.UseProxy = true;
            }

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
            if (EnableProxyRouting)
            {
                handler.Proxy = new WebProxy(ProxyIP, ProxyPort);
                handler.UseProxy = true;
            }

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

        public static async Task<bool> TradingView_VoteForChart(int chartid, bool vote)
        {
            if (UserLoginCookie == null)
            {
                return false;
            }

            const string FetchURL = "https://www.tradingview.com/vote-for-chart/";

            var handler = new HttpClientHandler();
            if (EnableProxyRouting)
            {
                handler.Proxy = new WebProxy(ProxyIP, ProxyPort);
                handler.UseProxy = true;
            }

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
            if (EnableProxyRouting)
            {
                handler.Proxy = new WebProxy(ProxyIP, ProxyPort);
                handler.UseProxy = true;
            }

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
