using UnityEngine;

namespace UnityUtil
{
    public static class ComputeShaderExtension
    {
        public static void DispatchThreads(this ComputeShader cs, int kernelIndex, int threadNumX, int threadNumY, int threadNumZ)
        {
            uint x, y, z;
            cs.GetKernelThreadGroupSizes(kernelIndex, out x, out y, out z);

            cs.Dispatch(kernelIndex, Mathf.CeilToInt((float)threadNumX / x), Mathf.CeilToInt((float)threadNumY / y), Mathf.CeilToInt((float)threadNumZ / z));
        }
    }

}