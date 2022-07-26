using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Building : CS_Selectable
{
    protected Vector3 reallyPoint;
    protected int PV;

    public void ChangeReallyPoint(Vector3 pos)
    {
        reallyPoint = pos;
        Debug.Log("NewPoint raelly");
    }
}
