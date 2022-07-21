using UnityEngine;
using System.Collections;

namespace UnityUtil
{

    public abstract class CameraTexture : TextureGettable
    {
        public RenderTexture tex;
        public float scale = 1f;

        #region Override
        public override Texture GetTexture() { return tex; }
        #endregion

        void Start()
        {
            UpdateTexture();
        }


#if UNITY_EDITOR
        public void OnEnable()
        {
            StartCoroutine(CheckScreenSizeChange());
        }
        public void OnDisable()
        {
            StopAllCoroutines();
        }

#endif

        void UpdateTexture()
        {
            if (tex != null)
            {
                var cam = GetComponent<Camera>();
                if (cam.targetTexture == tex) cam.targetTexture = null;
                DestroyImmediate(tex);
            }
            tex = new RenderTexture(Mathf.FloorToInt(Screen.width * scale), Mathf.FloorToInt(Screen.height * scale), 1);

            OnTextureUpdate(tex);
        }

        protected virtual void OnTextureUpdate(RenderTexture t) { }

        static Vector2 ScreenSize => new(Screen.width, Screen.height);

        IEnumerator CheckScreenSizeChange()
        {
            var size = ScreenSize;
            while (true)
            {
                yield return new WaitForEndOfFrame();

                var newSize = ScreenSize;
                if (size != newSize)
                {
                    UpdateTexture();
                    size = newSize;
                }
            }
        }

        public void OnDestroy()
        {
            DestroyImmediate(tex);
        }
    }
}