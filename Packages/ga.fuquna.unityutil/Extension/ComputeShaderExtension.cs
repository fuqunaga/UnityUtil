using UnityEngine;

namespace UnityUtil
{
    public static class ComputeShaderExtension
    {
        public static void DispatchThreads(this ComputeShader cs, int kernelIndex, int threadNumX, int threadNumY, int threadNumZ)
        {
            cs.GetKernelThreadGroupSizes(kernelIndex, out var x, out var y, out var z);
            cs.Dispatch(kernelIndex, Mathf.CeilToInt((float)threadNumX / x), Mathf.CeilToInt((float)threadNumY / y), Mathf.CeilToInt((float)threadNumZ / z));
        }
    }
}