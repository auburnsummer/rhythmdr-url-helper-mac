using System;
using System.Net;
using MonoMod;

using Assembly_CSharp;
using UnityEngine;
using System.Threading.Tasks;


#pragma warning disable CS0626
namespace Assembly_CSharp
{

    static class RPCHTTPServer
    {
        public static HttpListener myListener;
        public static Task httpTask;

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

        public static void ProcessRequest(HttpListenerContext context)
        {
            HttpListenerRequest request = context.Request;
            // Obtain a response object.
            HttpListenerResponse response = context.Response;
            // Construct a response.
            string responseString = "<HTML><BODY> Hello world!</BODY></HTML>";
            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseString);
            // Get a response stream and write the response to it.
            response.ContentLength64 = buffer.Length;
            System.IO.Stream output = response.OutputStream;
            output.Write(buffer, 0, buffer.Length);
            // You must close the output stream.
            output.Close();
        }
    }

    [MonoModPatch("global::scnLogo")]
    class patch_scnLogo : scnLogo
    {
        private extern void orig_Exit();
        private void Exit()
        {
            Debug.Log("Hi from MonoMod! (URL Scheme Patch 0.0.1)");
            orig_Exit();
        }
    }

    [MonoModPatch("global::LevelImporter")]
    class patch_LevelImporter : LevelImporter
    {

        public extern void orig_set_Showing(bool showing);

        public void set_Showing(bool showing)
        {
            Debug.Log("The showing has been hit!");
            Debug.Log(showing);

            if (showing == true)
            {
                RPCHTTPServer.StartServer();
                RPCHTTPServer.Listen();
            }
            else
            {
                RPCHTTPServer.StopServer();
            }

            orig_set_Showing(showing);
        }
    }
}
