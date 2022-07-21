using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Serialization;

namespace UnityUtil
{


    [ExecuteInEditMode]
    [RequireComponent(typeof(Camera))]
    [System.Obsolete("use ApplyTexToCamera and CameraTexture.")]
    public class MultiCameraToCamera : MonoBehaviour
    {
        public enum BlendOp
        {
            None = -1,
            Add,
            Blend
        };

        [System.Serializable]
        public class Data
        {
            public Camera camera;
            public BlendOp blend;
        }

        public Material material;
        [FormerlySerializedAs("datas")] public List<Data> dataList;
        private static readonly int BlendTex = Shader.PropertyToID("_BlendTex");


        void OnRenderImage(RenderTexture src, RenderTexture dest)
        {
            var list = (from d in dataList
                        where d.camera != null && d.camera.targetTexture != null
                        where d.blend != BlendOp.None
                        select d
                        ).ToList();

            if (list.Any())
            {
                var tmp = RenderTexture.GetTemporary(src.width, src.height, src.depth, src.format);
                var tmpDest = tmp;

                list.ForEach(d =>
                {
                    material.SetTexture(BlendTex, d.camera.targetTexture);
                    Graphics.Blit(src, tmpDest, material, (int)d.blend);
                    (src, tmpDest) = (tmpDest, src);
                });

                Graphics.Blit(src, dest);

                RenderTexture.ReleaseTemporary(tmp);
            }
            else
            {
                Graphics.Blit(src, dest);
            }
        }
    }
}