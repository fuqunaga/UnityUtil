using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace UnityUtil
{


    [RequireComponent(typeof(Camera))]
    public class ApplyTexToCamera : MonoBehaviour
    {
        #region TypeDefine
        public enum BlendOp
        {
            None = -1,
            Add,
            Blend,
            AddWithA
        };

        [System.Serializable]
        public class Data
        {
            public BlendOp blend;
            public TextureGettable texGetttable;
        }
        #endregion

        public Shader _shader;
        Material material;

        public List<Data> datas;

        void Start()
        {
            material = new Material(_shader);
        }

        public void OnDestroy()
        {
            DestroyImmediate(material);
        }

        void OnRenderImage(RenderTexture src, RenderTexture dest)
        {
            var list = datas.Where((d) => (d.texGetttable != null) && (d.blend != BlendOp.None)).ToList();

            if (list.Any())
            {
                var t0 = RenderTexture.GetTemporary(src.width, src.height);
                var t1 = RenderTexture.GetTemporary(src.width, src.height);

                Graphics.Blit(src, t0);
                list.ForEach(d =>
                {
                    var tex = d.texGetttable.GetTexture();
                    if (tex != null)
                    {
                        material.SetTexture("_BlendTex", tex);
                        Graphics.Blit(t0, t1, material, (int)d.blend);

                        var swap = t0;
                        t0 = t1;
                        t1 = swap;
                    }
                });

                Graphics.Blit(t0, dest);

                RenderTexture.ReleaseTemporary(t1);
                RenderTexture.ReleaseTemporary(t0);
            }
            else
            {
                Graphics.Blit(src, dest);
            }
        }
    }

    // インスペクタで設定したいのでinterfaceじゃなくてclassになってる
    public abstract class TextureGettable : MonoBehaviour
    {
        public abstract Texture GetTexture();
    }
}