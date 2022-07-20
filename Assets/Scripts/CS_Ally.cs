using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CS_Ally : CS_Unit
{
    [SerializeField] GameObject prefabTurret;

    CS_Selected_Dictionary selectedDictionay;
    Coroutine timeTransformTurret;

    List<GameObject> listTemp;

    protected override void Start()
    {
        base.Start();
        selectedDictionay = Camera.main.GetComponent<CS_Selected_Dictionary>();
    }

    public override void MoveTo(Vector3 position)
    {
        base.MoveTo(position);

        if (timeTransformTurret != null)
        {
            StopCoroutine(timeTransformTurret);
            gameObject.GetComponent<NavMeshAgent>().isStopped = false;
            timeTransformTurret = null;
        }
    }

    public void TransformTurret()
    {
        gameObject.GetComponent<NavMeshAgent>().isStopped = true;
        timeTransformTurret = StartCoroutine(TimeTransformTurret());
    }

    IEnumerator TimeTransformTurret()
    {
        yield return new WaitForSecondsRealtime(3f);

        listTemp = new List<GameObject>();

        Instantiate(prefabTurret, transform.position, transform.rotation);
        listTemp.Add(gameObject);

        foreach (GameObject goTemp in listTemp)
        {
            selectedDictionay.SelectedTable.Remove(goTemp.GetInstanceID());
        }

        Destroy(gameObject);
    }
}
