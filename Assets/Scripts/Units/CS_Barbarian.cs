using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.AI;

public class CS_Barbarian : CS_Unit
{
    [SerializeField] float slowDownPuddle = 3f;
    [SerializeField] float slowDownBetterWaterTurret = 3f;

    List<CS_Ally> alliesInZone;
    bool stateCoroutineLastFrame = false;
    Coroutine mainCoroutine;
    Coroutine sortCoroutine;
    NavMeshAgent navMeshAgent;
    CS_BarbarianCamp referenceCamp;
    Vector3 startPosition;
    bool canStop = false;
    int puddleCount = 0;
    int waterTurretCount = 0;


    protected override void Start()
    {
        base.Start();
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
        if (collision.gameObject.GetComponent<CS_Ally>() || collision.gameObject.GetComponent<CS_Building>())
        {
            hp--;
            if (hp <= 0)
            {
                referenceCamp.BarbarianIsDead();
                Destroy(gameObject);
            }
        }
    }

    private void UpdateCoroutineCheck()
    {
        if (alliesInZone.Count > 0 && stateCoroutineLastFrame == false)
        {
            //Start COROUTINE
            stateCoroutineLastFrame = true;
            if (mainCoroutine != null) StopCoroutine(mainCoroutine);
            canStop = false;
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

        while (!canStop)
        {
            //GetComponent<NavMeshAgent>().speed = 3.5f;
            //Debug.Log("EnRoute");
            CleanList();
            if (alliesInZone.Count > 0)
            {
                MoveTo(alliesInZone[0].transform.position);
            }
            else
            {
                yield return new WaitForSecondsRealtime(2f);
                MoveTo(startPosition);

                if (navMeshAgent.isStopped) canStop = true;
            }
            yield return 0;
        }
    }

    private IEnumerator SortAllies()
    {
        while (!canStop)
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

    public void AddPuddleCount()
    {
        puddleCount++;
        if(puddleCount == 1)
        {
            navMeshAgent.speed -= slowDownPuddle;
        }
    }

    public void SubPuddleCount()
    {
        puddleCount--;
        if (puddleCount <= 0)
        {
            puddleCount = 0;
            navMeshAgent.speed += slowDownPuddle;
        }
    }

    public void AddWaterTurretCount()
    {
        waterTurretCount++;
        if (waterTurretCount == 1)
        {
            navMeshAgent.speed -= slowDownBetterWaterTurret;
        }
    }

    public void SubWaterTurretCount()
    {
        waterTurretCount--;
        if (waterTurretCount <= 0)
        {
            waterTurretCount = 0;
            navMeshAgent.speed += slowDownBetterWaterTurret;
        }
    }

    public CS_BarbarianCamp ReferenceCamp { get => referenceCamp; set => referenceCamp = value; }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        //try
        //{
        //    Camera.main.GetComponent<AudioSource>().Play(0);
        //}
        //catch (System.Exception)
        //{
        //    if (Camera.main != null)
        //    {
        //        throw;
        //    }
        //}
    }
}
