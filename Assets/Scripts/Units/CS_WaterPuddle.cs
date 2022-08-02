using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_WaterPuddle : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        CS_Barbarian barbarian = other.GetComponent<CS_Barbarian>();

        if(barbarian != null)
        {
            barbarian.AddPuddleCount();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        CS_Barbarian barbarian = other.GetComponent<CS_Barbarian>();

        if (barbarian != null)
        {
            barbarian.SubPuddleCount();
        }
    }
}
