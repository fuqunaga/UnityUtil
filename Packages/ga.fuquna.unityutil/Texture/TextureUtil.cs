﻿using System.IO;
using UnityEngine;

namespace UnityUtil
{
    public static class TextureUtil
    {
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