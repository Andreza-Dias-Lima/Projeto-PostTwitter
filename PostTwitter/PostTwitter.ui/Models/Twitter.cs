using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using PostTwitter.api.Domain.Models;
using PostTwitter.api.Services;

namespace PostTwitter.ui.Models
{
    public class Twitter
    {
        public string url = "https://api.twitter.com/1.1/search/tweets.json";

        public string ConnectTwitterApi(string resource_url, string q)
        {
            // oauth application keys
            var oauth_token = "1197941538950668288-XsBxiGlxWY9CcptCsUpgh2bJ90R7td"; 
            var oauth_token_secret = "rZu7Zy4gilnal8t4lxcn7OCo3AYX97U729SGGpmK8xVbe"; 
            var oauth_consumer_key = "R73YCN834esqqI3Y2siuUeqUt";
            var oauth_consumer_secret = "DCo4kjYbGIg9khbBEbxo4OSztUc1YzFGxIUSQvhzdrg2iij0iX";

            // oauth implementation details
            var oauth_version = "1.0";
            var oauth_signature_method = "HMAC-SHA1";

            // unique request details
            var oauth_nonce = Convert.ToBase64String(new ASCIIEncoding().GetBytes(DateTime.Now.Ticks.ToString()));
            var timeSpan = DateTime.UtcNow
                - new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            var oauth_timestamp = Convert.ToInt64(timeSpan.TotalSeconds).ToString();


            // create oauth signature
            var baseFormat = "oauth_consumer_key={0}&oauth_nonce={1}&oauth_signature_method={2}" +
                            "&oauth_timestamp={3}&oauth_token={4}&oauth_version={5}&q={6}";

            var baseString = string.Format(baseFormat,
                                        oauth_consumer_key,
                                        oauth_nonce,
                                        oauth_signature_method,
                                        oauth_timestamp,
                                        oauth_token,
                                        oauth_version,
                                        Uri.EscapeDataString(q)
                                        );

            baseString = string.Concat("GET&", Uri.EscapeDataString(resource_url), "&", Uri.EscapeDataString(baseString));

            var compositeKey = string.Concat(Uri.EscapeDataString(oauth_consumer_secret),
                                    "&", Uri.EscapeDataString(oauth_token_secret));

            string oauth_signature;
            using (HMACSHA1 hasher = new HMACSHA1(ASCIIEncoding.ASCII.GetBytes(compositeKey)))
            {
                oauth_signature = Convert.ToBase64String(
                    hasher.ComputeHash(ASCIIEncoding.ASCII.GetBytes(baseString)));
            }

            // create the request header
            var headerFormat = "OAuth oauth_nonce=\"{0}\", oauth_signature_method=\"{1}\", " +
                               "oauth_timestamp=\"{2}\", oauth_consumer_key=\"{3}\", " +
                               "oauth_token=\"{4}\", oauth_signature=\"{5}\", " +
                               "oauth_version=\"{6}\"";

            var authHeader = string.Format(headerFormat,
                                    Uri.EscapeDataString(oauth_nonce),
                                    Uri.EscapeDataString(oauth_signature_method),
                                    Uri.EscapeDataString(oauth_timestamp),
                                    Uri.EscapeDataString(oauth_consumer_key),
                                    Uri.EscapeDataString(oauth_token),
                                    Uri.EscapeDataString(oauth_signature),
                                    Uri.EscapeDataString(oauth_version)
                            );

            ServicePointManager.Expect100Continue = false;

            // make the request
            var postBody = "q=" + Uri.EscapeDataString(q); //+ "&result_type=popular&count=2";//
            resource_url += "?" + postBody;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(resource_url);
            request.Headers.Add("Authorization", authHeader);

            request.Method = "GET";
            request.ContentType = "application/x-www-form-urlencoded";
            var response = (HttpWebResponse)request.GetResponse();
            var reader = new StreamReader(response.GetResponseStream());
            var objText = reader.ReadToEnd();

            return objText;
        }
        public List<Post> returnPostTwitter(string q)
        {
            string objText = ConnectTwitterApi(url, q);

            List<Post> lstPost = new List<Post>();
            
            try
            {
                JObject rss = JObject.Parse(objText);
               
                JArray jsonDat = JArray.Parse(rss["statuses"].ToString());

                foreach (var postData in jsonDat)
                {
                    int qtdeSeguidores;
                    Int32.TryParse(postData["user"]["friends_count"].ToString(), out qtdeSeguidores);

                    string format = "ddd MMM dd HH:mm:ss zzzzz yyyy";
                    DateTime dateValue;
                    DateTime.TryParseExact(postData["created_at"].ToString(), format, CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal, out dateValue);

                    Usuario usuario = new Usuario();
                    usuario.Nome = postData["user"]["name"].ToString();
                    usuario.Login = postData["user"]["screen_name"].ToString();
                    usuario.QtdeSeguidores = qtdeSeguidores;
                    usuario.Cd_Twitter = postData["user"]["id_str"].ToString();

                    Post post = new Post();
                    post.Descricao = postData["text"].ToString();
                    post.Data = dateValue;
                    post.Usuario = usuario;
                    post.Local = postData["user"]["location"].ToString();
                    post.Tag = q;

                    lstPost.Add(post);
                }
            }
            catch (Exception ex) {
            }

            return lstPost;
        }
    }


}
