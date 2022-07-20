using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class CS_Ally : CS_Unit
{
    [SerializeField] GameObject prefabTurret;
    [SerializeField] Slider progressBar;

    CS_Selected_Dictionary selectedDictionay;
    Coroutine timeTransformTurret;
    float maxValue = 3f;
    float currentValue = 0f;

    List<GameObject> listTemp;

    protected override void Start()
    {
        base.Start();
        selectedDictionay = Camera.main.GetComponent<CS_Selected_Dictionary>();
        progressBar.gameObject.SetActive(false);
    }

    public override void MoveTo(Vector3 position)
    {
        base.MoveTo(position);

        if (timeTransformTurret != null)
        {
            currentValue = 0f;
            progressBar.gameObject.SetActive(false);

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
        progressBar.gameObject.SetActive(true);

        while (currentValue < maxValue)
        {
            progressBar.transform.position = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
            progressBar.transform.rotation = Quaternion.Euler(40f, 0f, 0f);

            currentValue += Time.deltaTime;
            progressBar.value = currentValue / maxValue;

            yield return 0;
        }        

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
