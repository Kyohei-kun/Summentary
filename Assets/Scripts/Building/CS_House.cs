using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_House : MonoBehaviour
{
    CS_WaterPopulation popManager;

    void Start()
    {
        popManager = Camera.main.GetComponent<CS_WaterPopulation>();

        popManager.AddMaxPop();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            popManager.SubMaxPop();
            Destroy(gameObject);
        }
    }
}
