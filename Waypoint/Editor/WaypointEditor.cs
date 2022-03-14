using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Waypoint))]
//we are creating custom editor for control Waypoint class
public class WaypointEditor : Editor
{
    Waypoint Waypoint => target as Waypoint;
    // we casted the target of the editor to be a waypoint

    private void OnSceneGUI()
    {
        Handles.color = Color.green;
        //to looping through all waypoints
        for (int i = 0; i < Waypoint.Points.Length; i++)     
        {
            // Applaying a new position to each point
            EditorGUI.BeginChangeCheck();

            //create handles
            Vector3 currentWaypointPoint = Waypoint.CurrentPosition + Waypoint.Points[i];
            Vector3 newWaypointPoint = Handles.FreeMoveHandle(currentWaypointPoint, Quaternion.identity, 0.455f, new Vector3(0.3f, 0.3f, 0.3f), Handles.SphereHandleCap);

            // Create Text
            GUIStyle textStyle = new GUIStyle();
            textStyle.fontStyle = FontStyle.Bold;
            textStyle.fontSize = 15;
            textStyle.normal.textColor = Color.white;
            Vector3 textAlligment = Vector3.down * 0.18f + Vector3.right * 0.31f;
            Handles.Label(Waypoint.CurrentPosition + Waypoint.Points[i] + textAlligment, $"{i + 1}", textStyle);

            // Applaying a new position to each point
            EditorGUI.EndChangeCheck();

            // Updating each position of each point in our path
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(target, "Free Move Handle");
                Waypoint.Points[i] = newWaypointPoint - Waypoint.CurrentPosition;
            }
        }
    }
}
