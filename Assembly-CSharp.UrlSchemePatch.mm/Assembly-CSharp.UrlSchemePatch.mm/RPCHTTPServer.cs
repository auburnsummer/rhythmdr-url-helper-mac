using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using GDMiniJSON;
using UnityEngine;

namespace Assembly_CSharp
{
    static class RPCHTTPServer
    {
        public static HttpListener myListener;

        public static void StartServer()
        {
            myListener = new HttpListener();
            myListener.Prefixes.Add("http://localhost:49812/");
            myListener.Start();
        }

        public static void StopServer()
        {
            myListener.Stop();
        }

        public async static void Listen()
        {
            while (myListener.IsListening)
            {
                var context = await myListener.GetContextAsync();
                Console.WriteLine("Client connected");
                await Task.Factory.StartNew(() => ProcessRequest(context));
            }
        }

        public static void OutputString(HttpListenerResponse resp, String s, int code)
        {
            resp.StatusCode = code;
            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(s);
            resp.ContentLength64 = buffer.Length;
            System.IO.Stream output = resp.OutputStream;
            output.Write(buffer, 0, buffer.Length);
            output.Close();
        }

        public static string RequestBody(HttpListenerRequest request)
        {
            System.IO.Stream body = request.InputStream;
            System.Text.Encoding encoding = request.ContentEncoding;
            System.IO.StreamReader reader = new System.IO.StreamReader(body, encoding);

            // Convert the data to a string and display it on the console.
            string s = reader.ReadToEnd();
            body.Close();
            reader.Close();

            return s;
        }

        public static string DoAction(Dictionary<string, object> dict)
        {

            string command = (string)dict["command"];

            if (command == "AddLevel")
            {
                string zp = (string)dict["ZipPath"];
                string filename = (string)dict["filename"];
                scnCLS.instance.levelImporter.Showing = true;
                scnCLS.instance.levelImporter.ToggleInsertUrlContainer(false);
                ((patch_LevelImporter)scnCLS.instance.levelImporter).AddLevel(zp, filename);
            }

            return command;

        }

        public static void ProcessRequest(HttpListenerContext context)
        {
            HttpListenerRequest request = context.Request;

            string method = request.HttpMethod;

            if (method.ToUpperInvariant() == "POST")
            {
                // Obtain a response object.
                HttpListenerResponse response = context.Response;
                string input = RequestBody(request);
                // Use GD Mini Json
                Dictionary<string, object> dictionary = Json.Deserialize(input) as Dictionary<string, object>;

                string output;
                try
                {
                    output = DoAction(dictionary);
                    OutputString(response, output, 200);
                }
                catch (Exception e)
                {
                    OutputString(response, e.ToString(), 400);
                }

            }
            else
            {
                // Obtain a response object.
                HttpListenerResponse response = context.Response;
                OutputString(response, "RPC only supports POST.", 405);
            }
        }
    }
}
