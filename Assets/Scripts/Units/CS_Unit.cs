using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Unit : CS_Selectable
{
    Transform _transform;
    protected GameObject selectedGameObject;
    protected UnityEngine.AI.NavMeshAgent unitAgent;

    protected virtual void Start()
    {
        _transform = GetComponent<Transform>();
        try
        {
            selectedGameObject = transform.Find("Selected").gameObject;
        }
        catch (System.Exception)
        {
            if (this is CS_Ally)
            {
                Debug.LogError("No SelectedGameObject setup in parameter");
                throw;
            }
            selectedGameObject = new GameObject();

        }
        SetSelectedVisible(false);
        unitAgent = gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    public virtual void MoveTo(Vector3 position)
    {
        unitAgent.SetDestination(position);
    }

    public virtual void SetSelectedVisible(bool visible)
    {
        selectedGameObject.SetActive(visible);
    }
}
