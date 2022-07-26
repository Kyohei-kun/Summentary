using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Building : CS_Selectable
{
    protected Vector3 reallyPoint;
    protected int PV;
    protected GameObject GO_reallyPoint;

    protected override void Start()
    {
        base.Start();
        GO_reallyPoint = RecursiveFindChild(transform, "PR_ReallyPoint").gameObject;
        GO_reallyPoint.SetActive(false);
        reallyPoint = gameObject.transform.position;
    }

    public void ChangeReallyPoint(Vector3 pos)
    {
        reallyPoint = pos;
        GO_reallyPoint.SetActive(true);
        GO_reallyPoint.transform.position = pos;
    }
}
