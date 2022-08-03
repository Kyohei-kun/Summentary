using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CS_BuildTuretUp : MonoBehaviour
{
    [SerializeField] Slider progressBar;
    [SerializeField] Text txtNb;
    
    float unitCount = 10;
    float currentNbUnit = 0;
    int nbPalier = 0;

    private void Start()
    {
        txtNb.text = (currentNbUnit + " / " + unitCount);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Unit"))
        {
            UpdateCount(other.gameObject);
        }
    }

    void UpdateCount(GameObject go)
    {
        currentNbUnit++;
        Destroy(go);

        txtNb.text = (currentNbUnit + " / " + unitCount);
        progressBar.value = currentNbUnit / unitCount;

        if (currentNbUnit >= unitCount)
        {
            UpdatePalier();
        }
    }

    void UpdatePalier()
    {
        nbPalier++;
        currentNbUnit = 0;
        unitCount = unitCount * 2;

        txtNb.text = (currentNbUnit + " / " + unitCount);
        progressBar.value = currentNbUnit / unitCount;

        if (nbPalier == 1)
        {
            //Unlock better turret
        }
        if (nbPalier > 1)
        {
            //Up turret's stat
        }
    }
}