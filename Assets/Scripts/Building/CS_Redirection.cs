using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Redirection : CS_Building
{

    private void OnTriggerEnter(Collider other)
    {
        CS_Ally temp = other.GetComponent<CS_Ally>();
        if(temp != null)
        {
            temp.MoveTo(reallyPoint);
        }
    }
}
