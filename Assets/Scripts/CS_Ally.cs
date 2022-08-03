using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class CS_Ally : CS_Unit
{    
    [SerializeField] Slider progressBar;

    CS_Selected_Dictionary selectedDictionay;
    CS_WaterPopulation popManager;
    Coroutine coTransform;
    float currentValue = 0f;
    List<GameObject> listTemp;

    protected override void Start()
    {
        base.Start();
        selectedDictionay = Camera.main.GetComponent<CS_Selected_Dictionary>();
        popManager = Camera.main.GetComponent<CS_WaterPopulation>();
        progressBar.gameObject.SetActive(false);

        popManager.AddCurrentPop();
    }

    public override void MoveTo(Vector3 position)
    {
        base.MoveTo(position);

        if (coTransform != null)
        {
            currentValue = 0f;
            progressBar.gameObject.SetActive(false);

            StopCoroutine(coTransform);
            gameObject.GetComponent<NavMeshAgent>().isStopped = false;
            coTransform = null;
        }
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        popManager.SubCurrentPop();
    }

    public void Transformation(GameObject goTransfo, int timeTransfo)
    {
        gameObject.GetComponent<NavMeshAgent>().isStopped = true;
        coTransform = StartCoroutine(TimeTransform(timeTransfo, goTransfo));
    }

    IEnumerator TimeTransform(int maxValue, GameObject prefabTransfo)
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

        Instantiate(prefabTransfo, transform.position, transform.rotation);
        listTemp.Add(gameObject);

        foreach (GameObject goTemp in listTemp)
        {
            selectedDictionay.SelectedTable.Remove(goTemp.GetInstanceID());
        }

        Destroy(gameObject);
    }
}
