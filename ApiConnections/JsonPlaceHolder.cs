using Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace ApiConnections
{
    public class JsonPlaceHolder
    {

        public List<PostsME> GetApiPosts()
        {
            List<PostsME> items = new List<PostsME>();
            string result = string.Empty;

            result = LlamarAPI("https://jsonplaceholder.typicode.com/posts");

            items = JsonConvert.DeserializeObject<List<PostsME>>(result);

            return items;
        }

        public List<CommentsME> GetApiComments()
        {
            List<CommentsME> items = new List<CommentsME>();
            string result = string.Empty;

            result = LlamarAPI("https://jsonplaceholder.typicode.com/comments");

            items = JsonConvert.DeserializeObject<List<CommentsME>>(result);

            return items;
        }



        private  string LlamarAPI(string url)
        {
            string responseBody = null;
            string json = string.Empty;

            try
            {
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.DefaultConnectionLimit = 9999;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;            

                var request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "GET";
                request.ContentType = "application/json";
                request.Accept = "application/json";

                /* if (!string.IsNullOrWhiteSpace(json))
                {
                    using (var sw = new StreamWriter(request.GetRequestStream()))
                    {
                        sw.Write(json);
                        sw.Flush();
                        sw.Close();
                    }
                } */

                using (WebResponse response = request.GetResponse())
                {
                    using (Stream strReader = response.GetResponseStream())
                    {
                        if (strReader != null)
                        {
                            using (StreamReader objReader = new StreamReader(strReader))
                            {
                                responseBody = objReader.ReadToEnd();
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                responseBody = null;
            }

            return responseBody;
        }

        

    }
}
