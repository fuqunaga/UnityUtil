using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class ApplyTexToCamera : MonoBehaviour {

	public RenderTexture filterTex;
	public Material material;

	void OnRenderImage(RenderTexture src, RenderTexture dest)
	{

        if (filterTex)
        {
            material.SetTexture("_BlendTex", filterTex);
            Graphics.Blit(src, dest, material);
        }
        else
        {
            Graphics.Blit(src, dest);
        }
	}
}
