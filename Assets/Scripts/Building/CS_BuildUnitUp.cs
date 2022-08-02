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

    CS_Selected_Dictionary selectedDictionay;
    List<GameObject> listTemp;

    Coroutine unitUp;
    float currentValue = 0f;

    private void Start()
    {
        selectedDictionay = Camera.main.GetComponent<CS_Selected_Dictionary>();
        progressBar.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Unit"))
        {
            other.GetComponent<NavMeshAgent>().isStopped = true;
            other.transform.position = trTarget.position;
            selectedDictionay.SelectedTable.Remove(other.gameObject.GetInstanceID());

            unitUp = StartCoroutine(TimeUnitUp(10, other.gameObject));
        }
    }

    IEnumerator TimeUnitUp(int maxValue, GameObject go)
    {
        progressBar.gameObject.SetActive(true);

        while (currentValue < maxValue)
        {
            progressBar.transform.position = new Vector3(transform.position.x + 2.5f, transform.position.y + 4.5f, transform.position.z + 1);
            progressBar.transform.rotation = Quaternion.Euler(40f, 0f, 0f);

            currentValue += Time.deltaTime;
            progressBar.value = currentValue / maxValue;

            yield return 0;
        }

        Instantiate(prefabUnitUp, transform.position, transform.rotation);
        Destroy(go);        
    }
}
