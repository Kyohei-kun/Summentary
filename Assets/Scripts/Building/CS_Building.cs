using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Building : CS_Selectable
{
    protected Vector3 reallyPoint;
    protected int hp;
    protected GameObject go_reallyPoint;

    protected override void Start()
    {
        base.Start();
        go_reallyPoint = RecursiveFindChild(transform, "PR_ReallyPoint").gameObject;
        go_reallyPoint.SetActive(false);
        reallyPoint = gameObject.transform.position;
    }

    public void ChangeReallyPoint(Vector3 pos)
    {
        reallyPoint = pos;
        go_reallyPoint.SetActive(true);
        go_reallyPoint.transform.position = pos;
    }

    protected virtual void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.GetComponent<CS_Barbarian>())
        {
            hp--;
            if(hp <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
