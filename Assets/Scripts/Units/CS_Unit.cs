using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Unit : CS_Selectable
{
    Transform _transform;
    protected UnityEngine.AI.NavMeshAgent unitAgent;

    protected override void Start()
    {
        base.Start();
        _transform = GetComponent<Transform>();
        SetSelectedVisible(false);
        unitAgent = gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    public virtual void MoveTo(Vector3 position)
    {
        unitAgent.SetDestination(position);        
    }
}
