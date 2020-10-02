using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class EventListener
{
    public GameEvent Event;
    public UnityEvent Response;

    public void OnEventRaised()
    {
        // invoke the reponse(s) if not null
        Response?.Invoke();
    }
}

public class MultiGameEventListener : MonoBehaviour
{
    public List<EventListener> listeners = new List<EventListener>();

    private void OnEnable()
    {
        // register all listeners
        for (int i = 0; i < listeners.Count; i++)
        {
            listeners[i].Event.RegisterListener(listeners[i]);
        }
    }

    private void OnDisable()
    {
        // unregister all listeners
        for (int i = 0; i < listeners.Count; i++)
        {
            listeners[i].Event.UnregisterListener(listeners[i]);
        }
    }

    
}
