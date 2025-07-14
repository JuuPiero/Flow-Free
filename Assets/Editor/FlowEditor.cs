
using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(FlowManager))]
public class FlowEditor : Editor {
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        // Lấy reference tới component
        FlowManager flowManager = (FlowManager)target;

        // Tạo nút
        if (GUILayout.Button("Generate Flow"))
        {
            flowManager.GenerateFlow();
        }
    }
}