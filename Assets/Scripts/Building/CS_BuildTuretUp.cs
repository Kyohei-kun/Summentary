using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CS_BuildTuretUp : MonoBehaviour
{
    [SerializeField] Slider progressBar;
    [SerializeField] Text txtNb;

    CS_Selected_Dictionary selectedDictionary;
    
    int unitCount = 0;
    int currentNbUnit = 0;
    int nbPalier = 0;

    private void Start()
    {
        selectedDictionary = Camera.main.GetComponent<CS_Selected_Dictionary>();
        unitCount = 10;
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
        selectedDictionary.SelectedTable.Remove(go.GetInstanceID());
        Destroy(go);

        txtNb.text = (currentNbUnit + " / " + unitCount);

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