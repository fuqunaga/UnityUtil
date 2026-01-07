using System.IO;
using UnityEngine;

namespace UnityUtil
{
    public static class TextureUtil
    {
        public static RenderTexture CheckSizeAndGenerateIfNeed(RenderTexture texture, Vector2Int size, int depth = 24, RenderTextureFormat format = RenderTextureFormat.Default, int mipCount = 0)
        {
            if (size.x <= 0 || size.y <= 0) return texture;
            
            if (texture != null
                && (texture.width != size.x || texture.height != size.y))
            {
                Object.Destroy(texture);
                texture = null;
            }

            if (texture == null)
            {
                texture = new RenderTexture(size.x, size.y, depth, format, mipCount);
            }

            return texture;
        }
        
        public static Texture2D LoadFromStreamingAssetsFolder(string path)
        {
            Texture2D ret = null;
            var fullPath = Application.streamingAssetsPath + "/" + path;
            if (!File.Exists(fullPath))
            {
                Debug.Log($"TextureUtil.LoadFromStreamingAssetsFolder() File Not Founds.[{fullPath}]");
            }
            else
            {
                using var reader = new BinaryReader(File.Open(fullPath, FileMode.Open));
                
                ret = new Texture2D(2, 2);
                ret.LoadImage(reader.ReadBytes((int)reader.BaseStream.Length));
            }

            return ret;
        }
    }
}