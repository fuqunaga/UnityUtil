﻿using UnityEngine;
using System.Collections;

public abstract class GUISampleBase : MonoBehaviour
{
    public enum EnumSample
    {
        One,
        Two,
        Three
    }


    Rect _rect = new Rect();

    public void OnGUI()
    {
        _rect = GUILayout.Window(GetHashCode(), _rect, (id) =>
        {
            OnGUIInternal();
            GUI.DragWindow();
        },
        "GUISampleWindow",
        GUILayout.MinWidth(MinWidth));
    }

    protected abstract void OnGUIInternal();
    protected virtual float MinWidth { get { return 500f; } }
}
