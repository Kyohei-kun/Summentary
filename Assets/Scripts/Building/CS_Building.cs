using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Building : CS_Selectable
{
    private Vector3 reallyPoint;
    int PV;

    public void ChangeReallyPoint(Vector3 pos)
    {
        reallyPoint = pos;
        Debug.Log("NewPoint raelly");
    }


}
