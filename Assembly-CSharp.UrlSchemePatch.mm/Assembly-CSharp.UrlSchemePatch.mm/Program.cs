using System;
using System.Net;
using MonoMod;

using Assembly_CSharp;
using UnityEngine;


#pragma warning disable CS0626
namespace Assembly_CSharp
{

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

        public HttpListener myListener;

        public extern void orig_set_Showing(bool showing);

        public void set_Showing(bool showing)
        {
            Debug.Log("The showing has been hit!");
            Debug.Log(showing);

            if (showing == true)
            {
                myListener = new HttpListener();
                myListener.Prefixes.Add("http://localhost:49812/");
                myListener.Start();
                Debug.Log("Waiting for HTTP...");
                HttpListenerContext context = myListener.GetContext();
                HttpListenerRequest request = context.Request;
                // Obtain a response object.
                HttpListenerResponse response = context.Response;
                string responseString = "<HTML><BODY> Hello world!</BODY></HTML>";
                byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseString);
                response.ContentLength64 = buffer.Length;
                System.IO.Stream output = response.OutputStream;
                output.Write(buffer, 0, buffer.Length);
                output.Close();
            }
            else
            {
                myListener.Stop();
            }

            orig_set_Showing(showing);
        }
    }
}
