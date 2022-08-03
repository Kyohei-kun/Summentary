using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CS_TurretUp : MonoBehaviour
{
    [SerializeField] Slider progressBar;
    [SerializeField] GameObject prefabWater;
    [SerializeField] GameObject prefabFire;
    [SerializeField] GameObject prefabWind;
    [SerializeField] GameObject prefabEarth;

    float currentUnit;

    private void Start()
    {
        progressBar.transform.position = new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z);
        progressBar.transform.rotation = Quaternion.Euler(40f, 0f, 0f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Unit"))
        {
            CS_Ally temp = other.GetComponent<CS_Ally>();

            if (temp != null)
            {
                currentUnit++;
                progressBar.value = currentUnit / 3f;
                Destroy(other.gameObject);
            }

            if (currentUnit >= 3f)
            {
                Instantiate(prefabWater, transform.position, transform.rotation);
                Destroy(gameObject);
            }
        }
    }
}
