using UnityEngine;
using UnityEngine.Serialization;

namespace UnityUtil
{
    [ExecuteInEditMode]
    [RequireComponent(typeof(Camera))]
    public class MaterialImageEffect : MonoBehaviour
    {
        [FormerlySerializedAs("_material")] public Material material;
        [FormerlySerializedAs("_lod")] public int lod = 0;
        [FormerlySerializedAs("_destoryMaterial")] public bool destroyMaterial;

        void Start()
        {
            if (material == null)
                Destroy(this);
        }

        void OnRenderImage(RenderTexture src, RenderTexture dst)
        {
            var div = 1 << lod;

            var tmp = RenderTexture.GetTemporary(src.width / div, src.height / div);
            Graphics.Blit(src, tmp, material);
            Graphics.Blit(tmp, dst);

            RenderTexture.ReleaseTemporary(tmp);
        }

        public void OnDestroy()
        {
            if (destroyMaterial)
            {
                Destroy(material);
            }
        }
    }
}