using UnityEngine;
using System.Linq;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace UnityUtil
{

    [System.Serializable]
public class ScaledCurve {

    [SerializeField]
    protected AnimationCurve curve;
    public float scaleT = 1f;
    public float scaleY = 1f;

    public float Evaluate(float time)
    {
        return curve.Evaluate(time / scaleT) * scaleY;
    }
}

#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(ScaledCurve))]
public class ScaledCurveDrawer : PropertyDrawer
{
    float _Height{ get { return EditorGUIUtility.singleLineHeight;}}

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return _Height * 2f;
    }

    public override void OnGUI(Rect position, SerializedProperty prop, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, prop);

        position.height = _Height;
        EditorGUI.PropertyField(position, prop.FindPropertyRelative("curve"), label);


        position.y += position.height;
        position = LikePrefixLabelReturn(position);

        var fields = new[]{"scaleY","scaleT"};
        var props = fields.Select(f => prop.FindPropertyRelative(f)).ToArray();
        var floats = props.Select(p => p.floatValue).ToArray();
                     
        EditorGUI.MultiFloatField(position,
            fields.Select(f => new GUIContent(f.Replace("scale",""))).ToArray(),
            floats
            );

        for (var i = 0; i < floats.Length; ++i )
        {
            props[i].floatValue = floats[i];
        }

        EditorGUI.EndProperty();
    }

    Rect LikePrefixLabelReturn(Rect totalPosition)
    {

        var ret = new Rect(totalPosition.x + EditorGUIUtility.labelWidth, totalPosition.y, totalPosition.width - EditorGUIUtility.labelWidth, _Height);

        return ret;
    }
}
#endif
}