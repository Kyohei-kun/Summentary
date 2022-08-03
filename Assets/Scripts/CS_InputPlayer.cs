using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CS_InputPlayer : MonoBehaviour
{
    [Header("Transform")]
    [SerializeField] GameObject prefabTurret;
    [SerializeField] int timeTurret;
    [SerializeField] GameObject prefabProdBuild;
    [SerializeField] int timeProdBuild;
    [SerializeField] GameObject prefabHouse;
    [SerializeField] int timeHouse;
    [SerializeField] GameObject prefabPuddle;
    [SerializeField] int timePuddle;
    [SerializeField] GameObject prefabRedirection;
    [SerializeField] int timeRedirection;

    [Space][Header("Feedback")]
    [SerializeField] GameObject prefabMovement;
    [SerializeField] LayerMask layerMaskMouse;

    void Update()
    {
        Camera temp = Camera.main;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, 10000f, layerMaskMouse) && Input.GetMouseButtonDown(1)) //Joueur clique sur le sol
        {
            if (SelectionContainType(typeof(CS_Ally))) //MOVE TO
            {
                foreach (GameObject go in gameObject.GetComponent<CS_Selected_Dictionary>().SelectedTable.Values)
                {
                    if (go.GetComponent<CS_Ally>() != null)
                    {
                        go.GetComponent<CS_Ally>().MoveTo(hit.point);
                    }
                }
                GameObject feedbackPin = Instantiate(prefabMovement);
                feedbackPin.transform.position = hit.point;
            }
            else if (SelectionContainType(typeof(CS_Building))) //SET RALLY POINT
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
                go.GetComponent<CS_Ally>().Transformation(prefabTurret, timeTurret);
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            foreach (GameObject go in gameObject.GetComponent<CS_Selected_Dictionary>().SelectedTable.Values)
            {
                go.GetComponent<CS_Ally>().Transformation(prefabProdBuild, timeProdBuild);
            }
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            foreach (GameObject go in gameObject.GetComponent<CS_Selected_Dictionary>().SelectedTable.Values)
            {
                go.GetComponent<CS_Ally>().Transformation(prefabHouse, timeHouse);
            }
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            foreach (GameObject go in gameObject.GetComponent<CS_Selected_Dictionary>().SelectedTable.Values)
            {
                go.GetComponent<CS_Ally>().Transformation(prefabPuddle, timePuddle);
            }
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            foreach (GameObject go in gameObject.GetComponent<CS_Selected_Dictionary>().SelectedTable.Values)
            {
                go.GetComponent<CS_Ally>().Transformation(prefabRedirection, timeRedirection);
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