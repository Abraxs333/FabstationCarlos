using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class REsetButton : MonoBehaviour
{

    [SerializeField] private UnityEvent onAngleMeasureStateBehavior;
    [SerializeField] private UnityEvent DefaultBehavior;
    public void ResetState()
    {
        if (GameManager.Instance.CurrentState == GameState.MeasureAngle) onAngleMeasureStateBehavior?.Invoke();
        else DefaultBehavior?.Invoke();
    }
}
