using UnityEngine;
#if USE_URP
using UnityEngine.Rendering.Universal;
#endif

namespace UnityUtil
{
    public static class CameraExtension
    {
#if USE_URP
        public static ScriptableRendererData GetScriptableRendererData(this Camera camera)
        {
            var cameraData = camera.GetUniversalAdditionalCameraData();
            var scriptableRenderer = cameraData.scriptableRenderer;
            
            var pipelineAsset = UniversalRenderPipeline.asset;
        
            var renderers = pipelineAsset.renderers;
            
            var index = 0;
            for(; index< renderers.Length; index++)
            {
                if (renderers[index] == scriptableRenderer)
                {
                    break;
                }
            }
        
            return pipelineAsset.rendererDataList[index];
        }
#endif
    }
}