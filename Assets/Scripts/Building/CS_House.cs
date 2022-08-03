using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_House : CS_Building
{
    CS_WaterPopulation popManager;

    protected override void Start()
    {
        base.Start();
        popManager = Camera.main.GetComponent<CS_WaterPopulation>();

        popManager.AddMaxPop();
    }

    private void OnDestroy()
    {
        popManager.SubMaxPop();
    }
}
