using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityUtil.Example
{
    [ExecuteAlways]
    public class LineSegmentTest : MonoBehaviour
    {
        public Transform line0_start;
        public Transform line0_end;
        public Transform line1_start;
        public Transform line1_end;

        public Transform point;

        private void OnDrawGizmos()
        {
            var pos = point.position;

            var line0 = new LineSegment() { start = line0_start.position, end = line0_end.position };
            var line1 = new LineSegment() { start = line1_start.position, end = line1_end.position };


            // line0
            Gizmos.color = Color.red;
            Gizmos.DrawLine(line0.start, line0.end);

            // line1
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(line1.start, line1.end);

            // intersection line0,line1
            var intersection = LineSegment.CalcIntersection(line0, line1);
            if ( intersection != null)
            {
                Gizmos.color = Color.yellow;
                Gizmos.DrawWireSphere(intersection.Value, 0.3f);
            }

            // closet point
            Gizmos.color = Color.gray;
            var cpos0 = line0.ClosestPoint(pos);
            var cpos1 = line1.ClosestPoint(pos);
            Gizmos.DrawLine(cpos0, pos);
            Gizmos.DrawLine(cpos1, pos);


            // normal from pos
            Gizmos.color = Color.green;
            var normal0 = line0.CalcNormal(pos);
            var normal1 = line1.CalcNormal(pos);

            Gizmos.DrawRay(pos, -normal0);
            Gizmos.DrawRay(pos, -normal1);


#if UNITY_EDITOR
            var dist0 = line0.Distance(pos);
            var dist1 = line1.Distance(pos);
            UnityEditor.Handles.Label(pos, $"Distance: {dist0},{dist1}");

            var left0 = line0.IsLeft(pos);
            var left1 = line1.IsLeft(pos);
            UnityEditor.Handles.Label(pos - Vector3.up* 0.2f, $"IsLeft: {left0},{left1}");
#endif
        }
    }

}