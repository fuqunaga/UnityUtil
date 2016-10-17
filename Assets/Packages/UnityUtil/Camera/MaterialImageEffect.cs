using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
public class MaterialImageEffect : MonoBehaviour
{
    public Material _material;
    public int _lod = 0;
    public bool _destoryMaterial;

    void Start()
    {
        if (_material == null)
            Destroy(this);
    }

    void OnRenderImage(RenderTexture src, RenderTexture dst)
    {
        var div = 1 << _lod;

        var tmp = RenderTexture.GetTemporary(src.width / div, src.height / div);
        Graphics.Blit(src, tmp, _material);
        Graphics.Blit(tmp, dst);

        RenderTexture.ReleaseTemporary(tmp);
    }

    public void OnDestroy()
    {
        if (_destoryMaterial && (_material != null))
        {
            Destroy(_material);
        }
    }
}
