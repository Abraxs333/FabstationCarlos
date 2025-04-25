using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeState : MonoBehaviour
{
    [SerializeField] GameState NewState;
    
    public void SetNewState()
    {
        GameManager.Instance.ChangeState(NewState);
    }
}
