using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_TurretUp_Water : CS_Turret
{
    List<CS_Barbarian> listBarb = new List<CS_Barbarian>();

    protected override void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Unit"))
        {
            CS_Barbarian temp = other.GetComponent<CS_Barbarian>();

            if (temp != null)
            {
                listBarb.Add(other.gameObject.GetComponent<CS_Barbarian>());
                temp.AddWaterTurretCount();

                UpdateCoroutine();
            }
        }
    }

    protected override void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Unit"))
        {
            CS_Barbarian temp = other.GetComponent<CS_Barbarian>();

            if (temp != null)
            {
                listBarb.Remove(other.gameObject.GetComponent<CS_Barbarian>());
                temp.SubWaterTurretCount();

                UpdateCoroutine();
            }
        }
    }
}