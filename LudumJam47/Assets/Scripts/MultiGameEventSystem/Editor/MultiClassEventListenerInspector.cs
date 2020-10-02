using UnityEngine;
using UnityEditor;

// https://forum.unity.com/threads/display-a-list-class-with-a-custom-editor-script.227847/ used to help with this script
#if UNITY_EDITOR
[CustomEditor(typeof(MultiGameEventListener))]
public class MultiClassEventListenerInspector : Editor
{
    // the target of the editor
    MultiGameEventListener listener;
    // serialised object of the listener
    SerializedObject listenerSerialisedObject;
    // serialised property of the listeners List property
    SerializedProperty listeners;
    int ListSize;

    public void OnEnable()
    {
        listener = (MultiGameEventListener)target;
        listenerSerialisedObject = new SerializedObject(listener);
        listeners = listenerSerialisedObject.FindProperty("listeners");
    }

    public override void OnInspectorGUI()
    {
        //Update the listeners list
        listenerSerialisedObject.Update();

        // Resize our list
        EditorGUILayout.Space();
        ListSize = listeners.arraySize;
        ListSize = EditorGUILayout.IntField("List Size", ListSize);

        // resize listeners size if it has changed
        if(ListSize != listeners.arraySize)
        {
            ResizeList();
        }   

        EditorGUILayout.Space();
        EditorGUILayout.Space();
        
        //Display our list to the inspector window
        for (int i = 0; i < listeners.arraySize; i++)
        {
            DisplayListener(i);        
        }

        DisplayAddAndRemoveButtons();
       
        listenerSerialisedObject.ApplyModifiedProperties();
    }

    private void ResizeList()
    {
        while (ListSize > listeners.arraySize)
        {
            listeners.InsertArrayElementAtIndex(listeners.arraySize);
        }
        while (ListSize < listeners.arraySize)
        {
            listeners.DeleteArrayElementAtIndex(listeners.arraySize - 1);
        }
    }

    private void DisplayListener(int listenerNumber)
    {
        // get the current listener
        SerializedProperty currentListener = listeners.GetArrayElementAtIndex(listenerNumber);
        SerializedProperty gameEvent = currentListener.FindPropertyRelative("Event");
        SerializedProperty response = currentListener.FindPropertyRelative("Response");

        // set the background colour to the colour assigned in the game event asset
        Color newColour = Color.grey;

        // if there is a game event get the colour
        if (gameEvent.objectReferenceValue != null)
            newColour = ((GameEvent)gameEvent.objectReferenceValue).inspectorColour;

        // make sure alpha is 1
        newColour.a = 1.0f;

        // set the background colour
        GUI.backgroundColor = newColour;

        // Display the property fields in two ways
        GUILayout.BeginVertical(EditorStyles.helpBox);

        EditorGUILayout.LabelField($"{gameEvent.objectReferenceValue?.name} Listener");
        EditorGUILayout.PropertyField(gameEvent);
        EditorGUILayout.PropertyField(response);

        GUILayout.EndVertical();

        EditorGUILayout.Space();
    }

    private void DisplayAddAndRemoveButtons()
    {
        // add or remove an item in the list with buttons
        GUILayout.BeginHorizontal();
        GUI.backgroundColor = Color.green;
        if (GUILayout.Button("Add New Event"))
        {
            listener.listeners.Add(new EventListener());
        }
        GUI.backgroundColor = Color.red;
        if (GUILayout.Button("Remove Event"))
        {
            listener.listeners.RemoveAt(listener.listeners.Count - 1);
        }
        GUILayout.EndHorizontal();
    }
}
#endif