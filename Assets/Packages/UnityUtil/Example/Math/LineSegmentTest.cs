using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityUtil.Example
{
    [ExecuteAlways]
    public class LineSegmentTest : MonoBehaviour
    {
        public Transform pos0;
        public Transform pos1;
        public Transform pos2;
        public Transform pos3;

        public Transform posForNormal;

        private void OnDrawGizmos()
        {            
            var line0 = new LineSegment() { start = pos0.position, end = pos1.position };
            var line1 = new LineSegment() { start = pos2.position, end = pos3.position };

            Gizmos.color = Color.red;
            Gizmos.DrawLine(line0.start, line0.end);

            Gizmos.color = Color.blue;
            Gizmos.DrawLine(line1.start, line1.end);

            var intersection = LineSegment.CalcIntersection(line0, line1);
            if ( intersection != null)
            {
                Gizmos.color = Color.yellow;
                Gizmos.DrawWireSphere(intersection.Value, 0.3f);
            }

            Gizmos.color = Color.green;
            var npos = posForNormal.position;
            var normal0 = line0.CalcNormal(npos);
            var normal1 = line1.CalcNormal(npos);

            Gizmos.DrawRay(npos, -normal0);
            Gizmos.DrawRay(npos, -normal1);

#if UNITY_EDITOR
            var dist0 = line0.Distance(npos);
            var dist1 = line1.Distance(npos);
            UnityEditor.Handles.Label(npos, $"Distance: {dist0},{dist1}");

            var left0 = line0.IsLeft(npos);
            var left1 = line1.IsLeft(npos);
            UnityEditor.Handles.Label(npos - Vector3.up* 0.2f, $"IsLeft: {left0},{left1}");
#endif
        }
    }

}