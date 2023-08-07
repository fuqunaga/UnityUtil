using System.Runtime.InteropServices;
using Unity.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

namespace UnityUtil.Example
{
    // [ExecuteAlways]
    public class LineSegmentGpuTest : MonoBehaviour
    {
        private static class CsParam
        {
            public static readonly int Point = Shader.PropertyToID("_Point");
            public static readonly int LineSegmentBuffer = Shader.PropertyToID("_LineSegmentBuffer");
            public static readonly int OutputBuffer = Shader.PropertyToID("_OutputBuffer");
        }

        private struct Output
        {
            public int hasHit;
            public Vector2 hitPoint;
            public Vector2 closestPoint0;
            public Vector2 closestPoint1;
            public float distance0;
            public float distance1;
            public int isLeft0;
            public int isLeft1;
        }
        
        
        [FormerlySerializedAs("_computeShader")] public ComputeShader computeShader;
        
        [FormerlySerializedAs("line0_start")] public Transform line0Start;
        [FormerlySerializedAs("line0_end")] public Transform line0End;
        [FormerlySerializedAs("line1_start")] public Transform line1Start;
        [FormerlySerializedAs("line1_end")] public Transform line1End;

        [FormerlySerializedAs("point")] public Transform targetPoint;

        private int _kernelIndex = -1;
        private GraphicsBuffer _lineSegmentBuffer;
        private GraphicsBuffer _outputBuffer;
        

        private void OnDrawGizmos()
        {
            var point = targetPoint.position;

            var line0 = new LineSegment() { start = line0Start.position, end = line0End.position };
            var line1 = new LineSegment() { start = line1Start.position, end = line1End.position };

            DispatchComputeShader(point, line0, line1);
            
            var outputArray = new Output[1];
            _outputBuffer.GetData(outputArray);
            var output = outputArray[0];
            

            // line0
            Gizmos.color = Color.red;
            Gizmos.DrawLine(line0.start, line0.end);

            // line1
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(line1.start, line1.end);

            // intersection line0,line1
            if (output.hasHit > 0)
            {
                Gizmos.color = Color.yellow;
                Gizmos.DrawWireSphere(output.hitPoint, 0.3f);
            }


            // closet point
            Gizmos.color = Color.gray;
            Gizmos.DrawLine(output.closestPoint0, point);
            Gizmos.DrawLine(output.closestPoint1, point);


#if UNITY_EDITOR
            Handles.Label(point, $"Distance: {output.distance0},{output.distance1}");
            Handles.Label(point - Vector3.up* 0.2f, $"IsLeft: {output.isLeft0},{output.isLeft1}");
#endif
        }

        private void DispatchComputeShader(Vector2 point, LineSegment line0, LineSegment line1)
        {
            if (_kernelIndex < 0)
            {
                _kernelIndex = computeShader.FindKernel("CSMain");
            }

            _lineSegmentBuffer ??= new GraphicsBuffer(GraphicsBuffer.Target.Structured, 2, Marshal.SizeOf<LineSegment>());
            _outputBuffer ??= new GraphicsBuffer(GraphicsBuffer.Target.Structured, 1, Marshal.SizeOf<Output>());

            
            var array = new NativeArray<LineSegment>(2, Allocator.Temp);
            array[0] = line0;
            array[1] = line1;
            _lineSegmentBuffer.SetData(array);
            array.Dispose();
            
            computeShader.SetFloats(CsParam.Point, point.x, point.y);
            computeShader.SetBuffer(_kernelIndex, CsParam.LineSegmentBuffer, _lineSegmentBuffer);
            computeShader.SetBuffer(_kernelIndex, CsParam.OutputBuffer, _outputBuffer);
            
            computeShader.Dispatch(_kernelIndex, 1, 1, 1);
        }
    }
}