using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CS_Unit : MonoBehaviour
{
    GameObject selectedGameObject;
    NavMeshAgent unitAgent;

    private void Awake()
    {
        selectedGameObject = transform.Find("Selected").gameObject;
        SetSelectedVisible(false);
        unitAgent = gameObject.GetComponent<NavMeshAgent>();
    }

    public void MoveTo(Vector3 position)
    {
        unitAgent.SetDestination(position);
    }

    public void SetSelectedVisible (bool visible)
    {
        selectedGameObject.SetActive(visible);
    }
}
