using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(Gastrin), true), CanEditMultipleObjects]
public class GastrinEditor : Editor
{
    public override void OnInspectorGUI() {
        base.OnInspectorGUI();
        EditorGUILayout.HelpBox("위액의 각도가 범위 안에 있으면 덩어리 발사", MessageType.None);
    }
}
