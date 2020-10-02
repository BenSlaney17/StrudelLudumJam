using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GameEvent")]
public class GameEvent : ScriptableObject
{
    // The colour that the listener panel will appear in the Multi Game Event Listener inspector
    // By default assigned grey
    public Color inspectorColour = new Color(0.3f, 0.3f,0.3f);

    private List<EventListener> listeners = new List<EventListener>();

    public void Raise()
    {
        for (int i = listeners.Count - 1; i >= 0 ; i--)
        {
            listeners[i].OnEventRaised();
        }
    }

    public void RegisterListener(EventListener listener)
    {
        listeners.Add(listener);
    }

    public void UnregisterListener(EventListener listener)
    {
        listeners.Remove(listener);
    }
}
