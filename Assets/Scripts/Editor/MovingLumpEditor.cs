using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor;
[CustomEditor(typeof(MovingLump), true), CanEditMultipleObjects]
public class MovingLumpEditor : Editor
{
    private void OnSceneGUI() {
        MovingLump movingLump = (MovingLump)target;
        Vector2 point1 = movingLump.Point1;
        Vector2 point2 = movingLump.Point2;
        Handles.color = Color.red;
        float handleSize = movingLump.HandleSize;

        Vector2 pos1 = Handles.FreeMoveHandle(point1, Quaternion.identity, handleSize, Vector2.zero, Handles.CylinderHandleCap);
        movingLump.Point1 = pos1;

        Handles.color = Color.green;
        Vector2 pos2 = Handles.FreeMoveHandle(point2, Quaternion.identity, handleSize, Vector2.zero, Handles.CylinderHandleCap);
        movingLump.Point2 = pos2;

        Handles.color = Color.white;
        Handles.DrawLine(point1, point2);
        if (movingLump.MoveStartRed) {
            movingLump.transform.position = movingLump.Point1;
        } else
            movingLump.transform.position = movingLump.Point2;
    }
    public override void OnInspectorGUI() {
        base.OnInspectorGUI();
        MovingLump movingLump = (MovingLump)target;

        if (movingLump.MoveStartRed) {
            movingLump.transform.position = movingLump.Point1;
        } else
            movingLump.transform.position = movingLump.Point2;
        EditorGUILayout.HelpBox("이동 시작 하면 MoveDuration초 뒤에 다음 지점 도착 NextMoveTime 만큼 기다린후 다시 이동", MessageType.Info);
        EditorGUILayout.HelpBox("EaseType을 설정 함으로서 매끄럽게 이동하는 법 설정 가능\n\n 자세한 수치 알고 싶으시다면 아래 링크 직접 쓰시거나 혹은 프로그래머한테 연략하면 링크 보내드립니다.", MessageType.None);
        EditorGUILayout.HelpBox("https://blog.naver.com/hana100494/222084755392", MessageType.None);
        //https://blog.naver.com/hana100494/222084755392
    }
}
