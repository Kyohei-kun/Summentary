using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CS_Lake : MonoBehaviour
{
    Sizes size;
    int nbPlaces;
    List<CS_WaterGenerator> currentsWaterGenerator;

    int nbUnitGenerate;
    float frequencyGeneration;

    Transform socketSpawn;
    CS_SetupLake_Editor setup;

    Coroutine co_Generation;

    bool canGenerate = false;

    [SerializeField] GameObject prefabUnit;

    CS_WaterPopulation popManager;

    private void Start()
    {
        currentsWaterGenerator = new List<CS_WaterGenerator>();
        setup = GetComponent<CS_SetupLake_Editor>();
        setup.Initialisation();
        size = setup.Size;
        nbPlaces = setup.NbPlaces;
        nbUnitGenerate = setup.NbUnitGenerate;
        frequencyGeneration = setup.FrequencyGeneration;
        socketSpawn = setup.SocketSpawn;
        popManager = Camera.main.GetComponent<CS_WaterPopulation>();

        setup.ClearGameObject();
        Destroy(setup);
    }

    private void OnTriggerEnter(Collider other)
    {
        CS_WaterGenerator temp = other.GetComponent<CS_WaterGenerator>();
        if (temp != null && co_Generation == null)
        {
            currentsWaterGenerator.Add(temp);
            if (currentsWaterGenerator.Count >= nbPlaces)
            {
                canGenerate = true;
                co_Generation = StartCoroutine(GenerateUnit());
            }
        }
    }

    private IEnumerator GenerateUnit()
    {
        while (canGenerate && (popManager.CurrentPop < popManager.MaxPop))
        {
            yield return new WaitForSecondsRealtime(frequencyGeneration);

            List<int> temp = new List<int>();

            for (int i = 0; i < currentsWaterGenerator.Count; i++)
            {
                if (currentsWaterGenerator[i] == null) temp.Add(i);
            }
            for (int i = 0; i < temp.Count; i++)
            {
                currentsWaterGenerator.RemoveAt(temp[i]);
            }

            if (currentsWaterGenerator.Count < nbPlaces)
                canGenerate = false;

            if (canGenerate && (popManager.CurrentPop < popManager.MaxPop))
            {
                for (int i = 0; i < nbUnitGenerate; i++)
                {
                    GameObject go = Instantiate(prefabUnit);
                    go.transform.position = socketSpawn.transform.position;
                    Vector2 tempDecal = (Random.insideUnitCircle * 5);
                    go.GetComponent<NavMeshAgent>().SetDestination(socketSpawn.transform.position + (new Vector3(tempDecal.x, 0, tempDecal.y)));
                    yield return 0;
                }
            }
        }
        co_Generation = null;
    }
}
