using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CS_BarbarianCamp : MonoBehaviour
{
    Transform _transform;
    float radiusCamp;
    int nbUnit;
    [SerializeField] CampSize campSize;
    [SerializeField] GameObject prefabSpawn;
    [SerializeField] GameObject prefabBarbarian;
    [SerializeField] LayerMask spawnLayerMask;
    [SerializeField] float respawnRate = 15f;

    Dictionary<Vector3, CS_Barbarian> dictionarySpawnBarbarians;

    void Start()
    {
        _transform = GetComponent<Transform>();
        dictionarySpawnBarbarians = new Dictionary<Vector3, CS_Barbarian>();

        switch (campSize)
        {
            case CampSize.Little:
                radiusCamp = 5;
                nbUnit = Random.Range(8, 10);
                break;
            case CampSize.Medium:
                radiusCamp = 10;
                nbUnit = Random.Range(20, 25);
                break;
            case CampSize.Big:
                radiusCamp = 25;
                nbUnit = Random.Range(70, 100);
                break;
            default:
                break;
        }
        StartCoroutine(PlaceSpawnsAndUnits());
    }

    private IEnumerator PlaceSpawnsAndUnits()
    {
        int counter = 0;

        while (counter < nbUnit)
        {
            //Instantiate spawner
            Vector3 position = FindRandomValidPosition();
            dictionarySpawnBarbarians.Add(position, null);
            GameObject temp = Instantiate(prefabSpawn); //Debug
            temp.transform.position = position; //Debug
            counter++;
            yield return 0;
        }

        //Instantiate Barbarians
        for (int i = 0; i < dictionarySpawnBarbarians.Count; i++)
        {
            GameObject temp = Instantiate(prefabBarbarian);
            temp.transform.position = dictionarySpawnBarbarians.ElementAt(i).Key;
            temp.GetComponent<CS_Barbarian>().ReferenceCamp = this;
            dictionarySpawnBarbarians[dictionarySpawnBarbarians.ElementAt(i).Key] = temp.GetComponent<CS_Barbarian>();
        }
    }

    public void BarbarianIsDead()
    {
        StartCoroutine(RespawnUpdate());
    }

    private IEnumerator RespawnUpdate()
    {
        yield return 0;

        Vector3 key = Vector3.zero;

        foreach (KeyValuePair<Vector3, CS_Barbarian> pair in dictionarySpawnBarbarians)
        {
            if (pair.Value == null)
            {
                key = pair.Key;
                break;
            }
        }
        yield return new WaitForSeconds(respawnRate);

        GameObject temp = Instantiate(prefabBarbarian);
        temp.transform.position = key;
        temp.GetComponent<CS_Barbarian>().ReferenceCamp = this;
        dictionarySpawnBarbarians[key] = temp.GetComponent<CS_Barbarian>();
    }

    private Vector3 FindRandomValidPosition()
    {
        Vector3 result = Vector3.zero;
        int currentTry = 0;
        int maxTry = 100;
        while (result == Vector3.zero && currentTry < maxTry)
        {
            Vector2 temp = Random.insideUnitCircle * (radiusCamp / 2);
            result = new Vector3(_transform.position.x + temp.x, 0, _transform.position.z + temp.y);
            if (Physics.OverlapCapsule(result, result + Vector3.up, 0.5f, spawnLayerMask).Length > 0)
            {
                result = Vector3.zero;
                currentTry++;
            }
        }
        return result;
    }
}

enum CampSize
{
    Little,
    Medium,
    Big
}


