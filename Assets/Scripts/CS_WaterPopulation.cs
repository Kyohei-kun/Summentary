using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CS_WaterPopulation : MonoBehaviour
{
    int maxPop = 0;
    int currentPop;

    [SerializeField] Text population;

    public int CurrentPop { get => currentPop; set => currentPop = value; }
    public int MaxPop { get => maxPop; set => maxPop = value; }

    void Start()
    {
        maxPop = (GameObject.FindGameObjectsWithTag("House").Length) * 4;
    }

    private void UpdatePop()
    {
        population.text = "Population : " + currentPop + " / " + maxPop;
    }

    public int AddMaxPop()
    {
        maxPop += 4;
        UpdatePop();

        return maxPop;
    }

    public int SubMaxPop()
    {
        maxPop -= 4;
        UpdatePop();

        return maxPop;
    }

    public int AddCurrentPop()
    {
        currentPop++;
        UpdatePop();

        return currentPop;
    }

    public int SubCurrentPop()
    {
        currentPop--;
        UpdatePop();

        return currentPop;
    }
}
