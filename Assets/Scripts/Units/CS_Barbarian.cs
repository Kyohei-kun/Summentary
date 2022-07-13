using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.AI;

public class CS_Barbarian : CS_Unite
{
    List<CS_Ally> alliesInZone;
    bool stateCoroutineLastFrame = false;
    Coroutine mainCoroutine;
    Coroutine sortCoroutine;
    NavMeshAgent navMeshAgent;

    private void Start()
    {
        alliesInZone = new List<CS_Ally>();
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void OnTriggerEnter(Collider other)
    {
        CS_Ally temp = other.GetComponent<CS_Ally>();
        if (temp != null)
        {
            alliesInZone.Add(temp);
        }
        UpdateCoroutineCheck();
    }

    private void OnTriggerExit(Collider other)
    {
        CS_Ally temp = other.GetComponent<CS_Ally>();
        if (temp != null)
        {
            alliesInZone.Remove(temp);
        }
        UpdateCoroutineCheck();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.GetComponent<CS_Ally>())
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }

    private void UpdateCoroutineCheck()
    {
        if (alliesInZone.Count > 0 && stateCoroutineLastFrame == false)
        {
            //Start COROUTINE
            stateCoroutineLastFrame = true;
            mainCoroutine = StartCoroutine(UpdateIA());
        }
        else if(alliesInZone.Count == 0 && stateCoroutineLastFrame == true)
        {
            //STOP COROUTINE
            stateCoroutineLastFrame = false;
            StopCoroutine(mainCoroutine);
            StopCoroutine(sortCoroutine);
        }
    }

    public IEnumerator UpdateIA()
    {
        sortCoroutine = StartCoroutine(SortAllies());
        while (true)
        {
            //Debug.Log("EnRoute");
            navMeshAgent.SetDestination(alliesInZone[0].transform.position);
            yield return 0;
        }
    }

    public IEnumerator SortAllies()
    {
        while (true)
        {
            alliesInZone = alliesInZone.OrderBy(x => Vector3.Distance(this.transform.position, x.transform.position)).ToList();

            Debug.Log(alliesInZone[0].name);
            yield return new WaitForSeconds(2);
        }
    }
}
