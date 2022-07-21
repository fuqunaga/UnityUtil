using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Serialization;

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
            [FormerlySerializedAs("texGetttable")] public TextureGettable texGettable;
        }
        #endregion

        [FormerlySerializedAs("_shader")] public Shader shader;
        Material _material;

        [FormerlySerializedAs("datas")] public List<Data> dataList;
        private static readonly int BlendTex = Shader.PropertyToID("_BlendTex");

        void Start()
        {
            _material = new Material(shader);
        }

        public void OnDestroy()
        {
            DestroyImmediate(_material);
        }

        void OnRenderImage(RenderTexture src, RenderTexture dest)
        {
            var list = dataList.Where((d) => (d.texGettable != null) && (d.blend != BlendOp.None)).ToList();

            if (list.Any())
            {
                var t0 = RenderTexture.GetTemporary(src.width, src.height);
                var t1 = RenderTexture.GetTemporary(src.width, src.height);

                Graphics.Blit(src, t0);
                list.ForEach(d =>
                {
                    var tex = d.texGettable.GetTexture();
                    if (tex != null)
                    {
                        _material.SetTexture(BlendTex, tex);
                        Graphics.Blit(t0, t1, _material, (int)d.blend);

                        (t0, t1) = (t1, t0);
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