using System;
using MonoMod;
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
            Debug.Log("Hi from MonoMod! (URL Scheme Patch 0.0.1a)");
            RPCHTTPServer.MakeServer();
            orig_Exit();
        }
    }
}
