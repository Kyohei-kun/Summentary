using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CS_GameController : MonoBehaviour
{
    Camera cam;
    Vector3 startPosition;
    List<CS_Ally> selectedUnitList;

    [SerializeField] NavMeshAgent playerAgent;
    [SerializeField] Transform selectionAreaTransform;

    [SerializeField] Transform debugAreaSelection;
    //LayerMask groundLayer;

    private void Awake()
    {
        selectedUnitList = new List<CS_Ally>();
    }

    private void Start()
    {
        selectionAreaTransform.gameObject.SetActive(false);
        cam = Camera.main;
    }

    void Update()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit) && Input.GetMouseButtonDown(1))
        {
            playerAgent.SetDestination(hit.point);
        }

        //Left mouse button pressed
        if (Input.GetMouseButtonDown(0)) 
        {
            selectionAreaTransform.gameObject.SetActive(true);
            startPosition = hit.point;

            //Deselect all units
            foreach (CS_Ally unit in selectedUnitList)
            {
                unit.SetSelectedVisible(false);
            }
            selectedUnitList.Clear();
        }

        //Left mouse button hold
        if (Input.GetMouseButton(0))
        {
            foreach (CS_Ally unit in selectedUnitList)
            {
                unit.SetSelectedVisible(false);
            }
            selectedUnitList.Clear();

            debugAreaSelection.position = (startPosition + hit.point) / 2;

            Vector3 scaleOverlap = Abs(startPosition - hit.point) / 2;
            if (scaleOverlap.y <= 0.5)
            {
                scaleOverlap.y = 1;
            }

            Vector3 centerOverlap = (startPosition + hit.point) / 2;

            Collider[] hitColliders = Physics.OverlapBox(centerOverlap, scaleOverlap, Quaternion.identity);

            selectionAreaTransform.position = centerOverlap;
            selectionAreaTransform.localScale = scaleOverlap;

            //Select units within selection area
            foreach (Collider collider in hitColliders)
            {
                CS_Ally unit = collider.GetComponent<CS_Ally>();

                if (unit != null)
                {
                    Debug.Log("addList");
                    unit.SetSelectedVisible(true);
                    selectedUnitList.Add(unit);
                }
            }

            //Vector3 currentMousePosition = hit.point;
            //Vector3 lowerLeft = new Vector3(Mathf.Min(startPosition.x, currentMousePosition.x), startPosition.y+.1f, Mathf.Min(startPosition.z, currentMousePosition.z));
            //Vector3 upperRight = new Vector3(Mathf.Max(startPosition.x, currentMousePosition.x), startPosition.y+.1f, Mathf.Max(startPosition.z, currentMousePosition.z));

            //selectionAreaTransform.position = lowerLeft;
            //selectionAreaTransform.localScale = upperRight - lowerLeft;
        }

        //Left mouse button released
        if (Input.GetMouseButtonUp(0))
        {
            selectionAreaTransform.gameObject.SetActive(false);
        }
    }

    private Vector3 Abs(Vector3 v)
    {
        return new Vector3(Mathf.Abs(v.x), Mathf.Abs(v.y), Mathf.Abs(v.z));
    }
}
