using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_InputPlayer : MonoBehaviour
{
    void Update()
    {
        Camera temp = Camera.main;
        if(true)
        {

        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit) && Input.GetMouseButtonDown(1))
        {
            foreach (GameObject go in gameObject.GetComponent<CS_Selected_Dictionary>().SelectedTable.Values)
            {
                go.GetComponent<CS_Ally>().MoveTo(hit.point);
            }
        }
    }
}
