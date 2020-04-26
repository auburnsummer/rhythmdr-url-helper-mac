using System;
using System.Collections;
using MonoMod;
using UnityEngine;

#pragma warning disable CS0626
namespace Assembly_CSharp
{
    [MonoModPatch("global::scnCLS")]
    public class patch_scnCLS : scnCLS
    {
        [MonoModAdded]
        public bool GetReadyToReload;

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
            GetReadyToReload = false;
            RPCHTTPServer.StartServer();
            RPCHTTPServer.Listen();
            orig_Start();
        }


        [MonoModAdded]
        public IEnumerator ReloadAfterFastImport()
        {
            Debug.Log("Waiting 2 seconds for things to settle...");
            ((patch_scnCLS)scnCLS.instance).canSelectLevel = false;
            yield return new WaitUntil(() => (scnCLS.instance.levelImporter.gameObject.activeInHierarchy == false));

            // yield return new WaitForSeconds(2.0f);

            Debug.Log("Okay, let's try it now!");

            yield return scnCLS.instance.LoadLevelsData(-1);

        }

        [MonoModPublic]
        [MonoModIgnore]
        public extern void ToggleLevels(bool showLevels);

        [MonoModPublic]
        [MonoModIgnore]
        public bool canSelectLevel;

    }
}
