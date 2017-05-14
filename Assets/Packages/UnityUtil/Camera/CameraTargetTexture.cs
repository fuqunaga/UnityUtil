using UnityEngine;
using System.Collections;

namespace UnityUtil
{

    [ExecuteInEditMode]
    [RequireComponent(typeof(Camera))]
    public class CameraTargetTexture : CameraTexture
    {
        protected override void OnTextureUpdate(RenderTexture t)
        {
            t.hideFlags = HideFlags.HideAndDontSave;
            GetComponent<Camera>().targetTexture = t;
        }
    }

}