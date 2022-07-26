using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CS_SetupLake_Editor : MonoBehaviour
{
    [SerializeField] Sizes size;
    int nbPlaces;

    GameObject size1;
    GameObject size2;
    GameObject size3;

    int nbUnitGenerate;
    float frequencyGeneration;
    Transform socketSpawn;

    internal Sizes Size { get => size; set => size = value; }
    public int NbPlaces { get => nbPlaces; set => nbPlaces = value; }
    public int NbUnitGenerate { get => nbUnitGenerate; set => nbUnitGenerate = value; }
    public float FrequencyGeneration { get => frequencyGeneration; set => frequencyGeneration = value; }
    public Transform SocketSpawn { get => socketSpawn; set => socketSpawn = value; }

    private void Update()
    {
        Initialisation();   
    }

    public void ClearGameObject()
    {
        //Move Trigger into parent
        BoxCollider col = gameObject.AddComponent<BoxCollider>();
        

        switch (size)
        {
            case Sizes.Little:
                Destroy(size2);
                Destroy(size3);
                col.size = size1.GetComponent<BoxCollider>().size;
                Vector3.Scale(size1.GetComponent<BoxCollider>().size, size1.transform.localScale);
                Destroy(size1.GetComponent<BoxCollider>());
                break;
            case Sizes.Medium:
                Destroy(size1);
                Destroy(size3);
                col.size = size2.GetComponent<BoxCollider>().size;
                Vector3.Scale(size2.GetComponent<BoxCollider>().size, size2.transform.localScale);
                Destroy(size2.GetComponent<BoxCollider>());
                break;
            case Sizes.Big:
                Destroy(size1);
                Destroy(size2);
                col.size = Vector3.Scale(size3.GetComponent<BoxCollider>().size, size3.transform.localScale);
                Destroy(size3.GetComponent<BoxCollider>());
                break;
            case Sizes.Testing:
                Destroy(size2);
                Destroy(size3);
                col.size = Vector3.Scale(size1.GetComponent<BoxCollider>().size, size1.transform.localScale);
                Destroy(size1.GetComponent<BoxCollider>());
                break;
            default:
                break;
        }

        
        
    }

    public void Initialisation()
    {
        size1 = transform.Find("Size_1").gameObject;
        size2 = transform.Find("Size_2").gameObject;
        size3 = transform.Find("Size_3").gameObject;


        switch (Size)
        {
            case Sizes.Little:
                size1.SetActive(true);
                size2.SetActive(false);
                size3.SetActive(false);
                NbPlaces = 3;
                nbUnitGenerate = 1;
                frequencyGeneration = 1;
                SocketSpawn = size1.transform.Find("SocketSpawn");
                break;
            case Sizes.Medium:
                size1.SetActive(false);
                size2.SetActive(true);
                size3.SetActive(false);
                NbPlaces = 7;
                nbUnitGenerate = 5;
                frequencyGeneration = 60;
                SocketSpawn = size2.transform.Find("SocketSpawn");
                break;
            case Sizes.Big:
                size1.SetActive(false);
                size2.SetActive(false);
                size3.SetActive(true);
                NbPlaces = 13;
                nbUnitGenerate = 15;
                frequencyGeneration = 120;
                SocketSpawn = size3.transform.Find("SocketSpawn");
                break;
            case Sizes.Testing:
                size1.SetActive(true);
                size2.SetActive(false);
                size3.SetActive(false);
                NbPlaces = 1;
                nbUnitGenerate = 1;
                frequencyGeneration = 2;
                SocketSpawn = size1.transform.Find("SocketSpawn");
                break;
            default:
                break;
        }
    }
}

enum Sizes
{
    Little,
    Medium,
    Big,
    Testing
}
