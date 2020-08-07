using UnityEngine;

namespace UnityUtil
{

    [ExecuteInEditMode]
    public class CaptureFrameBuffer : CameraTexture
    {
        public void OnRenderImage(RenderTexture source, RenderTexture destination)
        {
            Graphics.Blit(source, destination);
            Graphics.Blit(source, tex);
        }
    }
}