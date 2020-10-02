using UnityEngine;
using UnityEditor;
#if UNITY_EDITOR
[CustomEditor(typeof(GameEvent))]
public class GameEventInspector : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        GameEvent myEvent = (GameEvent)target;

        if(GUILayout.Button("Raise Event"))
        {
            myEvent.Raise();
        }
    }
}
#endif