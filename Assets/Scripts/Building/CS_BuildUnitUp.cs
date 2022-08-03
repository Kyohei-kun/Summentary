using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class CS_BuildUnitUp : MonoBehaviour
{
    [SerializeField] Transform trTarget;
    [SerializeField] Slider progressBar;
    [SerializeField] GameObject prefabUnitUp;
    [SerializeField] Text txNbUnit;

    List<GameObject> listTemp = new List<GameObject>();

    Coroutine unitUp;
    float currentValue = 0f;
    int nbUnit = 0;

    private void Start()
    {
        progressBar.gameObject.SetActive(false);
        txNbUnit.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Unit"))
        {
            nbUnit++;
            txNbUnit.text = nbUnit.ToString();

            other.GetComponent<NavMeshAgent>().isStopped = true;
            other.transform.position = trTarget.position;
            listTemp.Add(other.gameObject);

            if (unitUp == null)
            {
                unitUp = StartCoroutine(TimeUnitUp(10));
            }
        }
    }

    IEnumerator TimeUnitUp(int maxValue)
    {
        progressBar.gameObject.SetActive(true);
        txNbUnit.gameObject.SetActive(true);

        while (listTemp.Count > 0)
        {
            while (currentValue < maxValue)
            {
                progressBar.transform.position = new Vector3(transform.position.x + 2.5f, transform.position.y + 4.5f, transform.position.z + 1);
                progressBar.transform.rotation = Quaternion.Euler(40f, 0f, 0f);

                currentValue += Time.deltaTime;
                progressBar.value = currentValue / maxValue;

                yield return 0;
            }

            Instantiate(prefabUnitUp, transform.position, transform.rotation);
            currentValue = 0;
            Destroy(listTemp[0]);
            listTemp.RemoveAt(0);

            nbUnit--;
            txNbUnit.text = nbUnit.ToString();

            yield return 0;
        }

        progressBar.gameObject.SetActive(false);
        txNbUnit.gameObject.SetActive(false);
        unitUp = null;
    }
}
