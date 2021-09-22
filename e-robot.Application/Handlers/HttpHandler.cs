using e_robot.Application.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace e_robot.Application.Handlers
{
    public class HttpHandler
    {
        public static string Post(string url,dynamic obj, HttpHeader[] headers)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.Credentials = CredentialCache.DefaultCredentials;


            UTF8Encoding encoding = new UTF8Encoding();
            var bytes = encoding.GetBytes(JsonConvert.SerializeObject(obj));

            foreach (var item in headers)
            {
                request.Headers.Add(item.Name,item.Value);
            }

            request.ContentType = "application/json";
            request.ContentLength = bytes.Length;

            using (var newStream = request.GetRequestStream())
            {
                newStream.Write(bytes, 0, bytes.Length);
                newStream.Close();
            }

            var result = (HttpWebResponse)request.GetResponse();


            string jsonString = "";

            using (Stream stream = result.GetResponseStream())
            {
                StreamReader reader = new StreamReader(stream, System.Text.Encoding.UTF8);
                jsonString = reader.ReadToEnd();
            }

            return jsonString;
        }
    }
}
