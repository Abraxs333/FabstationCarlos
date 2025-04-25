using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StateEventListener : MonoBehaviour
{
    [SerializeField] StateEvent stateEvent;
    [SerializeField] UnityEvent unityEvent;

    private void Awake()
    {
        stateEvent.Register(this);       
    }

    public void OnDestroy()
    {
        stateEvent.Deregister(this);
    }

    public void RaiseEvent()
    {
        unityEvent?.Invoke();
    }

}
