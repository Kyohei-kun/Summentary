using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CS_InputPlayer : MonoBehaviour
{
    [SerializeField] GameObject prefabTurret;
    [SerializeField] GameObject prefabProdBuild;
    [SerializeField] GameObject prefabHouse;

    void Update()
    {
        Camera temp = Camera.main;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit) && Input.GetMouseButtonDown(1)) //Joueur clique sur le sol
        {
            if (SelectionContainType(typeof(CS_Ally)))
            {
                foreach (GameObject go in gameObject.GetComponent<CS_Selected_Dictionary>().SelectedTable.Values)
                {
                    if (go.GetComponent<CS_Ally>() != null)
                    {
                        go.GetComponent<CS_Ally>().MoveTo(hit.point);
                    }
                }
            }
            else if (SelectionContainType(typeof(CS_Building)))
            {
                foreach (GameObject go in gameObject.GetComponent<CS_Selected_Dictionary>().SelectedTable.Values)
                {
                    if (go.GetComponent<CS_Building>() != null)
                    {
                        go.GetComponent<CS_Building>().ChangeReallyPoint(hit.point);
                    }
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            foreach (GameObject go in gameObject.GetComponent<CS_Selected_Dictionary>().SelectedTable.Values)
            {
                go.GetComponent<CS_Ally>().Transformation(prefabTurret, 3);
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            foreach (GameObject go in gameObject.GetComponent<CS_Selected_Dictionary>().SelectedTable.Values)
            {
                go.GetComponent<CS_Ally>().Transformation(prefabProdBuild, 5);
            }
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            foreach (GameObject go in gameObject.GetComponent<CS_Selected_Dictionary>().SelectedTable.Values)
            {
                go.GetComponent<CS_Ally>().Transformation(prefabHouse, 5);
            }
        }
    }

    public bool SelectionContainType(Type type)
    {
        foreach (GameObject item in gameObject.GetComponent<CS_Selected_Dictionary>().SelectedTable.Values)
        {
            if (type.IsAssignableFrom(item.GetComponent<CS_Selectable>().GetType()))
            {
                return true;
            }
        }
        return false;
    }
}