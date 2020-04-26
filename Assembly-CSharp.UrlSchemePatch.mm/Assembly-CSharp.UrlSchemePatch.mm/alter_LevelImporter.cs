using System;
using System.Collections;
using System.IO;
using MonoMod;
using RDTools;
using UnityEngine;

#pragma warning disable CS0626
namespace Assembly_CSharp
{
    [MonoModPatch("global::LevelImporter")]
    class patch_LevelImporter : LevelImporter
    {
        private extern IEnumerator orig_AddContent(string zipPath, string filename = null);

        private IEnumerator AddContent(string zipPath, string filename=null)
        {
            Debug.Log("ADD CONTENT");
            Debug.Log(zipPath);
            Debug.Log(filename);

            return orig_AddContent(zipPath, filename);
        }

        [MonoModAdded]
        public void AddLevel(string zipPath, string filename = null)
        {
            base.StartCoroutine(this.AddContent(zipPath, filename));
        }



        public extern void orig_set_Showing(bool showing);
        public void set_Showing(bool showing)
        {

            orig_set_Showing(showing);
            // refresh if we came into level select directly
            if (!showing && !scnCLS.instance.ShowingWard)
            {
                scnBase.instance.StartCoroutine(((patch_scnCLS)scnCLS.instance).ReloadAfterFastImport());
                base.wrldCLS.infoPanel.UpdateShortcutsRow(CLSInfoPanel.LibraryDefaultShortcuts);
            }

        }
    }
}
