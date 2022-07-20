using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Turret : MonoBehaviour
{
    List<CS_Unit> listUnit;

    Coroutine mainCoroutine;

    private void Awake()
    {
        listUnit = new List<CS_Unit>();
    }

    private void Update()
    {
        //Debug.Log(listUnit.Count);
        //Debug.Log(mainCoroutine);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Unit"))
        {
            CS_Ally temp = other.GetComponent<CS_Ally>();

            if (temp == null)
            {
                listUnit.Add(other.gameObject.GetComponent<CS_Unit>());

                UpdateCoroutine();
            }            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Unit"))
        {
            CS_Ally temp = other.GetComponent<CS_Ally>();

            if (temp == null)
            {
                listUnit.Remove(other.gameObject.GetComponent<CS_Unit>());

                UpdateCoroutine();
            }            
        }
    }

    void UpdateCoroutine()
    {
        if (listUnit.Count > 0 && mainCoroutine == null)
        {
            mainCoroutine = StartCoroutine(TimerFire());
        }
        else if (listUnit.Count == 0 && mainCoroutine != null)
        {
            Debug.Log("stop coroutine");
            StopCoroutine(mainCoroutine);
            mainCoroutine = null;
        }     
    }
    
    IEnumerator TimerFire()
    {
        while (true)
        {
            yield return new WaitForSecondsRealtime(2f);
            KillUnit();
            Debug.Log("kill");
        }  
    }

    void KillUnit()
    {
        Destroy(listUnit[0].gameObject);
        listUnit.RemoveAt(0);

        UpdateCoroutine();
    }
}
