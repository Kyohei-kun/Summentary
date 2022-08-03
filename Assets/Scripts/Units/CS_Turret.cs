using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Turret : MonoBehaviour
{
    List<CS_Unit> listUnit;

    Coroutine mainCoroutine;

    protected virtual void Awake()
    {
        listUnit = new List<CS_Unit>();
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Unit"))
        {
            CS_Barbarian temp = other.GetComponent<CS_Barbarian>();

            if (temp != null)
            {
                listUnit.Add(other.gameObject.GetComponent<CS_Unit>());

                UpdateCoroutine();
            }            
        }
    }

    protected virtual void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Unit"))
        {
            CS_Barbarian temp = other.GetComponent<CS_Barbarian>();

            if (temp != null)
            {
                listUnit.Remove(other.gameObject.GetComponent<CS_Unit>());

                UpdateCoroutine();
            }            
        }
    }

    protected virtual void UpdateCoroutine()
    {
        if (listUnit.Count > 0 && mainCoroutine == null)
        {
            mainCoroutine = StartCoroutine(TimerFire());
        }
        else if (listUnit.Count == 0 && mainCoroutine != null)
        {
            StopCoroutine(mainCoroutine);
            mainCoroutine = null;
        }     
    }

    protected virtual IEnumerator TimerFire()
    {
        while (true)
        {
            yield return new WaitForSecondsRealtime(2f);
            KillUnit();
        }  
    }

    protected virtual void KillUnit()
    {
        Destroy(listUnit[0].gameObject);
        listUnit.RemoveAt(0);

        UpdateCoroutine();
    }
}
