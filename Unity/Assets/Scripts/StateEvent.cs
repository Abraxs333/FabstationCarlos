using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName ="State Event", fileName = "New State Event")]
public class StateEvent : ScriptableObject
{

    HashSet<StateEventListener> listeners = new HashSet<StateEventListener>();

    public string Instructions;

    public void Invoke()
    {
        LogTools.Print(this, LogTools.LogType.StateEvent, "Raising Suscribed Events...");

        foreach (var listener in listeners)
        {
            listener.RaiseEvent();
        }
    }

    public void Register(StateEventListener stateEventListener)
    {
        listeners.Add(stateEventListener);
    }

    public void Deregister(StateEventListener stateEventListener)
    {
        listeners.Remove(stateEventListener);
    }

}
