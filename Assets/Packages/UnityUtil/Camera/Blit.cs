using UnityEngine;
using System.Collections;
using UnityEngine.Assertions;

[ExecuteInEditMode]
public class Blit : MonoBehaviour {

    public Material _material;
    public int lod = 0;

    void Start()
    {
        if (_material == null)
            Destroy(this);
    }

    void OnRenderImage(RenderTexture src, RenderTexture dst)
    {
        UpdateMaterialParam();

        var div = 1 << lod;

        var tmp = RenderTexture.GetTemporary(src.width / div, src.height / div);
        Graphics.Blit(src, tmp, _material);
        Graphics.Blit(tmp, dst);

        RenderTexture.ReleaseTemporary(tmp);
    }

    protected virtual void UpdateMaterialParam() { }
}
