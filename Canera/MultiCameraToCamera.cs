using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
public class MultiCameraToCamera : MonoBehaviour
{
    public List<Camera> cameras;
    public Material material;

    void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        var texs = (from c in cameras
                    where c.targetTexture != null
                    select c.targetTexture
                ).ToList();


        if (texs.Any())
        {
            texs.ForEach(t =>
            {
                material.SetTexture("_BlendTex", t);
                Graphics.Blit(src, dest, material);
                Graphics.Blit(dest, src);
            });
        }
        else
        {
            Graphics.Blit(src, dest);
        }
    }
}
