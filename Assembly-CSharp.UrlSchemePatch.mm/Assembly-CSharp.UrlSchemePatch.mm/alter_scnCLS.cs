using System;
using MonoMod;
using UnityEngine;

#pragma warning disable CS0626
namespace Assembly_CSharp
{
    [MonoModPatch("global::scnCLS")]
    public class patch_scnCLS : scnCLS
    {
        public extern void orig_GoBackToMainMenu();

        public new void GoBackToMainMenu()
        {
            Debug.Log("Back to main menu!!!");
            RPCHTTPServer.StopServer();
            orig_GoBackToMainMenu();
        }

        protected extern new void orig_Start();

        protected new void Start()
        {
            Debug.Log("START!!!!!!!");
            RPCHTTPServer.StartServer();
            RPCHTTPServer.Listen();
            orig_Start();
        }
    }
}
