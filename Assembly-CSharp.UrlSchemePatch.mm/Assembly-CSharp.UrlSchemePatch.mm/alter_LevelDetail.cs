using System;
using MonoMod;
using UnityEngine;

#pragma warning disable CS0626
namespace AssemblyCSharp
{
    [MonoModPatch("global::LevelDetail")]
    public class alter_LevelDetail : LevelDetail
    {
        public extern void orig_AnimateDetailDrop();

        public new void AnimateDetailDrop()
        {
            Debug.Log("DROPPING!!!!");
            orig_AnimateDetailDrop();
        }
    }
}
