using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CS_Unit : MonoBehaviour
{
    GameObject selectedGameObject;
    NavMeshAgent unitAgent;
    bool isSelected;

    private void Awake()
    {
        selectedGameObject = transform.Find("Selected").gameObject;
        SetSelectedVisible(false);
        isSelected = false;
    }

    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit) && Input.GetMouseButtonDown(1) && isSelected == true)
        {
            unitAgent = gameObject.GetComponent<NavMeshAgent>();
            unitAgent.SetDestination(hit.point);
        }
    }

    public void SetSelectedVisible (bool visible)
    {
        selectedGameObject.SetActive(visible);

        if (visible == true)
        {
            isSelected = true;
        }
        else
        {
            isSelected = false;
        }
    }
}
