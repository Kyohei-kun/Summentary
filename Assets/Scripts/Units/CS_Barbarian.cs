using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.AI;

public class CS_Barbarian :MonoBehaviour
{
    List<CS_Ally> alliesInZone;
    bool stateCoroutineLastFrame = false;
    Coroutine mainCoroutine;
    Coroutine sortCoroutine;
    NavMeshAgent navMeshAgent;
    CS_BarbarianCamp referenceCamp;
    Vector3 startPosition;


    private void Start()
    {
        alliesInZone = new List<CS_Ally>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        startPosition = transform.position;
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
        if (collision.gameObject.GetComponent<CS_Ally>())
        {
            referenceCamp.BarbarianIsDead();
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
        //else if (alliesInZone.Count == 0 && stateCoroutineLastFrame == true)
        //{
        //    //STOP COROUTINE
        //    stateCoroutineLastFrame = false;
        //    StopCoroutine(mainCoroutine);
        //    StopCoroutine(sortCoroutine);
        //}
    }

    private IEnumerator UpdateIA()
    {
        sortCoroutine = StartCoroutine(SortAllies());

        while (true)
        {
            //Debug.Log("EnRoute");
            CleanList();
            if (alliesInZone.Count > 0)
            {
                navMeshAgent.SetDestination(alliesInZone[0].transform.position);
            }
            else
            {
                if (Vector3.Distance(gameObject.transform.position, startPosition) > 3)
                {
                    yield return new WaitForSecondsRealtime(2f);
                    navMeshAgent.SetDestination(startPosition);
                }
                else
                {
                    //StopCoroutine
                }

            }
            yield return 0;
        }
    }

    private IEnumerator SortAllies()
    {
        while (true)
        {
            CleanList();
            alliesInZone = alliesInZone.OrderBy(x => Vector3.Distance(this.transform.position, x.transform.position)).ToList();
            //Debug.Log(alliesInZone[0].name);
            yield return new WaitForSeconds(2);
        }
    }

    private void CleanList()
    {
        alliesInZone = alliesInZone.Where(item => item != null).ToList();
    }

    public CS_BarbarianCamp ReferenceCamp { get => referenceCamp; set => referenceCamp = value; }
}
